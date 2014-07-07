using UnityEngine;
using System.Collections;

public class ResetGame : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
        if (Input.GetButtonDown("Menu"))
            Application.LoadLevel(0);

	    if (Input.GetButtonDown("Reset"))
	        Application.LoadLevel(2);
	}
}