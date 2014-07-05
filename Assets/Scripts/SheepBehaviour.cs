using System;
using UnityEngine;
using System.Collections;


public class SheepBehaviour : MonoBehaviour
{
    private float m_Height;
    public float m_JumpHeight = 2.0f;
	// Use this for initialization
	void Start ()
	{
	    m_Height = transform.position.y;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    Jump();
	}

    void Jump()
    {
        Vector3 pos = transform.position;
        pos.y = m_Height + (float)(Math.Abs(Math.Sin(Time.time * 4.0f)*m_JumpHeight));
        transform.position = pos;
    }
}
