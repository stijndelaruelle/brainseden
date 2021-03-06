﻿using System;
using System.Security.Principal;
using UnityEngine;
using System.Collections;


public class SheepBehaviour : MonoBehaviour
{
    private Vector3 m_Direction;
    private Vector3 m_TargetDirection;
    public float m_Speed;
    public float m_JumpVelocity;

    public float m_MaxFleeDist;
    public float m_MinFleeDist;

    public float m_MaxFleeSpeed;
    public float m_MinFleeSpeed;

    public float m_MaxRunSpeed;
    public float m_MinRunSpeed;

    private bool m_Moving;
    private bool m_CanJump;
    private float m_UpdateTimer;
    private float m_JumpTimer;

    private GameObject m_Herder;

    private ScoreManager m_ScoreManager;
    private bool m_IsRogue;
    private bool m_Influenced;
    private float m_FleeTimer;
    private bool m_UltimatePanic;

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
	    m_Influenced = false;
	    m_FleeTimer = 0.0f;
	    m_CanJump = true;
	    m_IsRogue = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    Jump();
	    HandleMovement();
	}

    void Jump()
    {
        //Jump (this will be an animation)
        if (m_CanJump)
        {
            m_JumpTimer -= Time.deltaTime;
            if (m_JumpTimer < 0)
            {
                rigidbody.AddForce(Vector3.up * m_JumpVelocity, ForceMode.Acceleration);
                m_CanJump = false;
                m_JumpTimer = UnityEngine.Random.Range(0.5f, 10.0f);
            }
        }
    }

    private void HandleMovement()
    {
        if (!HandleFlee())
        {
            m_UpdateTimer -= Time.deltaTime;
            if (m_UpdateTimer <= 0)
            {
                if (UnityEngine.Random.Range(0, 2) == 0)
                {
                    m_Moving = !m_Moving;
                }

                if (m_Moving)
                {
                    float angle = UnityEngine.Random.Range(0, 360);
                    m_TargetDirection = Quaternion.AngleAxis(angle, Vector3.up) * Vector3.forward;
                    m_Speed = UnityEngine.Random.Range(m_MinRunSpeed, m_MaxRunSpeed);
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
        transform.position = transform.position + m_Direction*m_Speed*Time.deltaTime;

        if (m_Speed == 0.0f)
        {
            GetComponentInChildren<Animator>().SetBool("IsWalking", false);
        }
        else
        {
            GetComponentInChildren<Animator>().SetBool("IsWalking", true);
        }
    }

    public bool HandleFlee()
    {
        bool wasRunning = m_FleeTimer > 0;
        if (m_FleeTimer > 0) m_FleeTimer -= Time.deltaTime;

        bool canRun = m_FleeTimer > 0;
        if (!canRun)
        {
            //Should we start running?
            if (!m_Influenced && wasRunning)
            {
                m_FleeTimer = 0;
                m_Speed = 0;
                return false;
            }
            else if (!m_Influenced)
            {
                m_FleeTimer = 0;
                return false;
            }
            
            // We are influenced and rogue, start fleeing
            m_FleeTimer = UnityEngine.Random.Range(0.2f, 0.5f);
            canRun = true;
        }

        if (canRun)
        {
            Vector3 moveDir = transform.position - m_Herder.transform.position;
            float dist = moveDir.magnitude;

            if (dist < m_MinFleeDist)
                return false;

            m_Speed = Mathf.Lerp(m_MinFleeSpeed, m_MaxFleeSpeed, 1 - ((dist - m_MinFleeDist) / (m_MaxFleeDist - m_MinFleeDist)));
            moveDir = GameObject.Find("Ring").transform.position - transform.position;
            moveDir.Normalize();

            m_Direction = m_TargetDirection = moveDir;
            return true;
        }

        return false;
    }

    public void StartFlee(GameObject herder)
    {
        m_Herder = herder;
        m_Influenced = true;
        m_JumpTimer = 0;
        transform.FindChild("Alert").GetComponent<ParticleSystem>().Play();
        SoundManager.PlaySound("Footsteps", null, 1.0f);
    }

    public void StopFlee()
    {
        m_Influenced = false;
    }

    public void UltimatePanic(GameObject herder)
    {
        m_Influenced = true;
        m_UltimatePanic = true;
        m_FleeTimer = 5.0f;
        m_Herder = herder;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Terrain") m_CanJump = true;
        if (collision.gameObject.tag == "Sheep" && !m_IsRogue && m_UltimatePanic)
        {
            m_Influenced = false;
            m_UltimatePanic = false;
            m_FleeTimer = 0.0f;
            m_Speed = 0.0f;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Influence")
        {
            StartFlee(collider.gameObject);
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
        if (collider.gameObject.name == "Influence")
        {
            StopFlee();
        }

        //we're going rogue!
        if (collider.gameObject.tag == "Range" && !m_IsRogue)
        {
            m_ScoreManager.AddRogueSheep();
            m_IsRogue = true;
        }
    }
}