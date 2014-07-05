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

        float horizontal = Input.GetAxis("Player1_Horizontal");
        float vertical = Input.GetAxis("Player1_Vertical");

        rigidbody.AddForce(Vector3.right * horizontal * 3000.0f, ForceMode.Acceleration);
        rigidbody.AddForce(Vector3.forward * vertical * 3000.0f, ForceMode.Acceleration);

        transform.LookAt(transform.position + new Vector3(horizontal, 0.0f, vertical));
	}
}