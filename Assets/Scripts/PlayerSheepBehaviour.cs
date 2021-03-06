﻿using System;
using UnityEngine;
using System.Collections;

public class PlayerSheepBehaviour : MonoBehaviour, IPlayer
{
    public GameObject GameObject
    {
        get
        {
            return gameObject;
        }

        set
        {
            //fuck this
        }
    }

    public Vector3 Position
    {
        get
        {
            return transform.position;
        }

        set
        {
            transform.position = value;
        }
    }

    public float m_BaseSpeed = 15000.0f;
    public float m_SprintSpeed = 25000.0f;
    public float m_JumpVelocity;

    private bool m_CanJump;

    private ScoreManager m_ScoreManager;
    private bool m_IsRogue = false;

    private IItem m_Item;

    private float m_SoundTimer;

	// Use this for initialization
	void Start () 
	{
        SoundManager.PlaySound("Start", null, 1.0f);
        m_ScoreManager = GameObject.Find("Scorebar").GetComponent<ScoreManager>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    HandleSound();

        if (m_ScoreManager.Victory) return;

        //Move
        float horizontal = Input.GetAxis("Player2_Horizontal");
        float vertical = Input.GetAxis("Player2_Vertical");

        float speed = m_BaseSpeed;
        if (Input.GetButton("Player2_Sprint"))
		{
			speed = m_SprintSpeed;
			transform.GetComponentInChildren<DashLineBehaviour>().StartEffect();
		}
		else
		{
			transform.GetComponentInChildren<DashLineBehaviour>().EndEffect();
		}

        rigidbody.AddForce(Vector3.right * horizontal * speed * Time.deltaTime, ForceMode.Acceleration);
        rigidbody.AddForce(Vector3.forward * vertical * speed * Time.deltaTime, ForceMode.Acceleration);

        transform.LookAt(transform.position + new Vector3(horizontal, 0.0f, vertical));

        //Jump (this will be an animation)
        if (Input.GetButtonDown("Player2_Jump") && m_CanJump)
        {
            rigidbody.AddForce(Vector3.up * m_JumpVelocity, ForceMode.Acceleration);
            m_CanJump = false;
        }

        //Use item
        if (Input.GetButtonDown("Player2_Fire") && m_Item != null)
        {
            m_ScoreManager.HideSheepPickup();
            m_Item.Activate();
            m_Item = null;
        }

        if (Math.Abs(horizontal + vertical) < 0.05f)
		{
            GetComponentInChildren<Animator>().SetBool("IsWalking", false);
        }
        else
        {
            GetComponentInChildren<Animator>().SetBool("IsWalking", true);
        }
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Shepherd")
        {
            m_ScoreManager.AddScoreShepherd(100); //We lose
        }

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

    //--------------------------
    // IPlayer Interface
    //--------------------------
    public void SetItem(IItem item)
    {
        m_Item = item;
        m_ScoreManager.ShowSheepPickup();
    }

    public void AddScore(int amount)
    {
        m_ScoreManager.AddScoreSheep(amount);

    }

    void HandleSound()
    {
        m_SoundTimer -= Time.deltaTime;
        if (m_SoundTimer <= 0)
        {
            m_SoundTimer = UnityEngine.Random.Range(4.0f, 8.0f);
            SoundManager.PlaySound("LamaSound", gameObject, UnityEngine.Random.Range(3.0f, 4.5f));
        }
    }
}