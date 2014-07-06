using UnityEngine;
using System.Collections;

public class DashLineBehaviour : MonoBehaviour {

	private float _time = 0;

	// Use this for initialization
	void Start ()
	{
		_time = transform.GetComponentInChildren<TrailRenderer>().time;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void StartEffect()
	{
		transform.GetComponentInChildren<TrailRenderer>().enabled = true;
		transform.GetComponentInChildren<TrailRenderer>().time = _time;
	}

	public void EndEffect()
	{
		transform.GetComponentInChildren<TrailRenderer>().enabled = false;
		transform.GetComponentInChildren<TrailRenderer>().time = 0;
	}
}
