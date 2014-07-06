using UnityEngine;
using System.Collections;

public class LamaEffects : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	public void StartLine()
	{
		transform.GetComponentInChildren<DashLineBehaviour>().StartEffect();
	}
	
	public void EndLine()
	{
		transform.GetComponentInChildren<DashLineBehaviour>().StartEffect();
	}
}
