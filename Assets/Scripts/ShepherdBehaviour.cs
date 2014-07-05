using UnityEngine;
using System.Collections;

public class ShepherdBehaviour : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	    //bool down = Input.GetButtonDown("Jump");
        //bool held = Input.GetButton("Jump");
        //bool up = Input.GetButtonUp("Jump");

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        rigidbody.AddForce(Vector3.right * horizontal * 2000.0f, ForceMode.Acceleration);
        rigidbody.AddForce(Vector3.forward * vertical * 2000.0f, ForceMode.Acceleration);
	}
}