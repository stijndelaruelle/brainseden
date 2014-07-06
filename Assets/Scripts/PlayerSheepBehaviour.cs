﻿using System;
using UnityEngine;
using System.Collections;

public class PlayerSheepBehaviour : MonoBehaviour 
{
    public float m_BaseSpeed = 1500.0f;
    public float m_SprintSpeed = 2500.0f;

    private bool m_CanJump;

    private ScoreManager m_ScoreManager;
    private bool m_IsRogue = false;

	// Use this for initialization
	void Start () 
	{
        m_ScoreManager = GameObject.Find("Scorebar").GetComponent<ScoreManager>();
	}
	
	// Update is called once per frame
	void Update () 
	{
        //Move
        float horizontal = Input.GetAxis("Player2_Horizontal");
        float vertical = Input.GetAxis("Player2_Vertical");

        float speed = m_BaseSpeed;
        if (Input.GetButton("Player2_Sprint")) speed = m_SprintSpeed;

        rigidbody.AddForce(Vector3.right * horizontal * speed, ForceMode.Acceleration);
        rigidbody.AddForce(Vector3.forward * vertical * speed, ForceMode.Acceleration);

        transform.LookAt(transform.position + new Vector3(horizontal, 0.0f, vertical));

        //Jump (this will be an animation)
        if (Input.GetButtonDown("Player2_Jump") && m_CanJump)
        {
            rigidbody.AddForce(Vector3.up * 10000.0f, ForceMode.Acceleration);
            m_CanJump = false;
        }
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Shepherd") Debug.Log("Fuck i'm dead.");
        if (collision.gameObject.tag == "Terrain")  m_CanJump = true;
    }

    void OnTriggerEnter(Collider collision)
    {
        //Ok I'll behave, sorry eh
        if (collision.gameObject.tag == "Range" && m_IsRogue)
        {
            m_ScoreManager.RemoveRogueSheep();
            m_IsRogue = false;
        }
    }

    void OnTriggerExit(Collider collision)
    {
        //we're going rogue!
        if (collision.gameObject.tag == "Range" && !m_IsRogue)
        {
            m_ScoreManager.AddRogueSheep();
            m_IsRogue = true;
        }
    }
}