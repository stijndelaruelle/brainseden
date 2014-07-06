using System;
using UnityEngine;
using System.Collections;


public class SheepBehaviour : MonoBehaviour
{
    private Vector3 m_Direction;
    private Vector3 m_TargetDirection;
    public float m_Speed;

    private bool m_Moving;
    private bool m_CanJump;
    private float m_UpdateTimer;
    private float m_JumpTimer;

    private GameObject m_Herder;

    private ScoreManager m_ScoreManager;
    private bool m_IsRogue = false;
    private float m_FleeTimer;
    private bool m_Influenced;

	// Use this for initialization
	void Start ()
	{
        m_ScoreManager = GameObject.Find("Scorebar").GetComponent<ScoreManager>();

        m_JumpTimer = UnityEngine.Random.Range(0.5f,10.0f);
	    m_UpdateTimer = UnityEngine.Random.Range(0.5f, 2.2f);

	    m_Speed = 0.0f;
        float angle = UnityEngine.Random.Range(0, 360);

	    transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
        m_Direction = transform.rotation * Vector3.forward;
	    m_TargetDirection = m_Direction;
	    m_FleeTimer = 0;
	    m_Influenced = false;
	    m_CanJump = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    Jump();
	    HandleMovement();
	}

    private void HandleMovement()
    {
        if (m_FleeTimer > 0)
        {
            if (m_Influenced)
                Flee(m_Herder);
            m_FleeTimer -= Time.deltaTime;
            if (m_FleeTimer <= 0)
            {
                if (!m_Influenced)
                    StopFlee();
                else
                {
                    Flee(m_Herder);
                }
            }
        }
        else
        {
            m_UpdateTimer -= Time.deltaTime;
            if (m_UpdateTimer < 0)
            {
                if (UnityEngine.Random.Range(0, 2) == 0)
                    m_Moving = !m_Moving;
                if (m_Moving)
                {
                    float angle = UnityEngine.Random.Range(0, 360);
                    m_TargetDirection = Quaternion.AngleAxis(angle, Vector3.up) * Vector3.forward;
                    m_Speed = UnityEngine.Random.Range(0.2f, 0.8f);
                }
                else
                {
                    m_Speed = 0;
                }
                m_UpdateTimer = UnityEngine.Random.Range(5.0f, 10.0f);
            }
        }

        if (Vector3.Angle(m_Direction, m_TargetDirection) > 1)
        {
            m_Direction = Vector3.Lerp(m_Direction, m_TargetDirection, Time.deltaTime);
        }
        else
        {
            m_Direction = m_TargetDirection;
        }

        transform.LookAt(transform.position + m_Direction);
        transform.position = transform.position + m_Direction*m_Speed;

        if (m_Speed < 0) Debug.Log(m_Speed);
    }

    void Jump()
    {
        //Jump (this will be an animation)
        if (m_CanJump)
        {
            m_JumpTimer -= Time.deltaTime;
            if (m_JumpTimer < 0)
            {
                rigidbody.AddForce(Vector3.up*10000.0f, ForceMode.Acceleration);
                m_CanJump = false;
                m_JumpTimer = UnityEngine.Random.Range(0.5f, 10.0f);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Terrain")  m_CanJump = true;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Influence")
        {
            m_JumpTimer = 0;
            m_Influenced = true;
            Flee(collider.gameObject);
        }

        //Ok I'll behave, sorry eh
        if (collider.gameObject.tag == "Range" && m_IsRogue)
        {
            m_ScoreManager.RemoveRogueSheep();
            m_IsRogue = false;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        //we're going rogue!
        if (collider.gameObject.tag == "Range" && !m_IsRogue)
        {
            m_ScoreManager.AddRogueSheep();
            m_IsRogue = true;
        }
        if (collider.gameObject.name == "Influence")
        {
            m_Influenced = false;
        }

    }

    public void Flee(GameObject herder)
    {
        m_Herder = herder;
        if (m_FleeTimer <= 0)
        {
            m_FleeTimer = UnityEngine.Random.Range(1.5f, 3.0f);
            m_Speed = UnityEngine.Random.Range(1.4f, 2.0f);
        }
        Vector3 moveDir = transform.position - m_Herder.transform.position;
        moveDir.Normalize();
        // moveDir = moveDir;
        m_Direction = m_TargetDirection = moveDir;
    }

    public void StopFlee()
    {
        m_FleeTimer = 0;
        m_Speed = 0;
        m_UpdateTimer = UnityEngine.Random.Range(0.5f, 2.2f);
    }
}