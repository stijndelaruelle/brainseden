using System;
using UnityEngine;
using System.Collections;


public class SheepBehaviour : MonoBehaviour
{
    private float m_Height;
    public float m_JumpHeight = 2.0f;
    public float m_UpdateTime = 5.0f;

    private Vector3 m_Direction;
    public float m_Speed;

    private bool m_Fleeing;
    private float m_UpdateTimer;
    private float m_RandomOffset;
	// Use this for initialization
	void Start ()
	{
	    m_RandomOffset = UnityEngine.Random.Range(0, 100.0f);

	    m_Height = transform.position.y;
	    m_UpdateTimer = m_UpdateTime;

	    m_Speed = 0;
        float angle = UnityEngine.Random.Range(0, 360);

	    transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
        m_Direction = transform.rotation * Vector3.forward;
	    m_Fleeing = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    Jump();
	    //HandleMovement();
	}

    private void HandleMovement()
    {
        m_UpdateTimer -= Time.deltaTime;
        if (m_UpdateTimer < 0)
        {
            if (!m_Fleeing)
            {

            }
            m_UpdateTimer = m_UpdateTime;
        }

        transform.position = transform.position + m_Direction*m_Speed;
    }

    void Jump()
    {
        Vector3 pos = transform.position;
        pos.y = m_Height + (float)(Math.Abs(Math.Sin((Time.time + m_RandomOffset) * 4.0f)*m_JumpHeight));
        transform.position = pos;
    }

    public void Flee(Vector3 herderPos)
    {
        m_Fleeing = true;
    }

    public void StopFlee()
    {
        m_Fleeing = false;
    }
}
