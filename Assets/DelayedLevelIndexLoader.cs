using UnityEngine;
using System.Collections;

public class DelayedLevelIndexLoader : MonoBehaviour 
{

	public int NextLevelIndex = 1;
	public float NextLevelLoadDelay = 3.0f;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		NextLevelLoadDelay -= Time.deltaTime;
		if(NextLevelLoadDelay<=0)
		{
			Application.LoadLevel(NextLevelIndex);
		}
	}
}
