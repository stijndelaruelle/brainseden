using UnityEngine;
using System.Collections;

public class DirectionalLightRotator : MonoBehaviour 
{
	public float RotationSpeed  = 5.0f;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		//transform.Rotate(0,Time.deltaTime*10,0);
		transform.RotateAround(Vector3.zero,new Vector3(0,1,0),Time.deltaTime*RotationSpeed);
	}
}
