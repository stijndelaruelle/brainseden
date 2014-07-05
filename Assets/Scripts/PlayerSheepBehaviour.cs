using System;
using UnityEngine;
using System.Collections;

public class PlayerSheepBehaviour : MonoBehaviour 
{
    private bool m_CanJump;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
        //Move
        float horizontal = Input.GetAxis("Player2_Horizontal");
        float vertical = Input.GetAxis("Player2_Vertical");

        rigidbody.AddForce(Vector3.right * horizontal * 2000.0f, ForceMode.Acceleration);
        rigidbody.AddForce(Vector3.forward * vertical * 2000.0f, ForceMode.Acceleration);

        transform.LookAt(transform.position + new Vector3(horizontal, 0.0f, vertical));

        //Jump (this will be an animation)
        if (Input.GetButtonDown("Player2_Jump") && m_CanJump)
        {
            rigidbody.AddForce(Vector3.up * 10000.0f, ForceMode.Acceleration);
        }
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Shepherd")
        {
            Debug.Log("Fuck i'm dead.");
        }

        if (collision.gameObject.tag == "Terrain")
        {
            Debug.Log("rofl");
            m_CanJump = true;
        }
    }

    void OnCollisionLeave(Collision collision)
    {
        if (collision.gameObject.tag == "Terrain")
        {
            Debug.Log("roflqdqdqd");
            m_CanJump = false;
        }
    }
}