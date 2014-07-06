using UnityEngine;
using System.Collections;

public class DashLineBehaviour : MonoBehaviour {

	public float LifeTime = 0;

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void StartEffect()
	{
		transform.GetComponentInChildren<TrailRenderer>().enabled = true;
		transform.GetComponentInChildren<TrailRenderer>().time = LifeTime;
	}

	public void EndEffect()
	{
		transform.GetComponentInChildren<TrailRenderer>().enabled = false;
		transform.GetComponentInChildren<TrailRenderer>().time = 0;
	}
}
