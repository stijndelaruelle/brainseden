using UnityEngine;
using System.Collections;

public class PinataEffects : MonoBehaviour 
{
	private ParticleSystem _particles = null;
	private Animator _animator = null;

	private float _timer =0;

	// Use this for initialization
	void Start () 
	{
		_particles = transform.FindChild("Pinata_Root").
			FindChild("upperRope").
			FindChild("middleRope").
			FindChild("lowerRope").
			FindChild("pinata").
			FindChild("PinataParitcle").GetComponent<ParticleSystem>();

		_particles.Stop();

		_animator = transform.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(_timer>0)
			_timer+=Time.deltaTime;	

		if(_timer>0.3)
		{
			_animator.SetBool("Shake",false);
			_timer = 0;
		}
	}

	public void Hit()
	{
		_particles.Play();
		_animator.SetBool("Shake",true);
		_timer+=Time.deltaTime;
	}
}
