using UnityEngine;
using System.Collections;

public class TumbleweedBehavior : MonoBehaviour {

	public  float LimitOne =30;
	public float LimitTwo =30;

	private float _timer = 20;

	// Use this for initialization
	void Start ()
	{
		_timer = transform.GetComponent<ParticleSystem>().duration;
	}
	
	// Update is called once per frame
	void Update ()
	{
		_timer-=Time.deltaTime;

		if(_timer<=0)
		{
			_timer = transform.GetComponent<ParticleSystem>().duration;
			float angle = Random.Range(LimitOne,LimitTwo);

			transform.rotation= Quaternion.AngleAxis(angle,Vector3.up);
			Debug.Log(transform.rotation);
		}
	}

	void OnDrawGizmos()
	{
		var startPos = transform.position;
		var pointOne = Quaternion.AngleAxis(LimitOne, Vector3.up) * (Vector3.right *30.0f);
		var pointTwo = Quaternion.AngleAxis(LimitTwo, Vector3.up) * (Vector3.right *30.0f);
		Gizmos.DrawLine(startPos, pointOne+transform.position);
		Gizmos.DrawLine(startPos, pointTwo+transform.position);
	}
}
