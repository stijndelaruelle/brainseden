using UnityEngine;
using System.Collections;

public class CayotteEffectsBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void StartLine()
	{
		transform.GetComponentInChildren<DashLineBehaviour>().StartEffect();
	}
	
	public void EndLine()
	{
		transform.GetComponentInChildren<DashLineBehaviour>().StartEffect();
	}

	public void Shout()
	{
		transform.FindChild("Shout").GetComponent<ParticleSystem>().Play();
	}
}
