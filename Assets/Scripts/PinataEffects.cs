using UnityEngine;
using System.Collections;

public class PinataEffects : MonoBehaviour 
{
	private ParticleSystem _particles = null;
	private Animator _animator = null;

	private float _timer =0;

	public Mesh Full = null;
	public Mesh Damaged = null;
	public Mesh NearDestroyed = null;

	private int _damageState = 0;

	// Use this for initialization
	void Start () 
	{
        _particles = transform.FindChild("Pinata_Root").
			FindChild("upperRope").
			FindChild("middleRope").
			FindChild("lowerRope").
			FindChild("pinata").
			FindChild("PinataParticle").GetComponent<ParticleSystem>();

		_particles.Stop();

		_animator = transform.GetComponentInChildren<Animator>();

		transform.FindChild("Pinata_Root").
			FindChild("upperRope").
			FindChild("middleRope").
			FindChild("lowerRope").
			FindChild("pinata").
				FindChild("Doll").GetComponent<MeshFilter>().mesh=null;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(_timer>0)
			_timer+=Time.deltaTime;	

		if(_timer>0.3)
		{
			_timer = 0;
		}
	}

	public void Hit()
	{
		if(_timer>0)
			return;

		if(_damageState<0)
			return;

		_particles.Play();
		_animator.SetTrigger("Shake");
		_timer+=Time.deltaTime;

		--_damageState;

		switch (_damageState)
		{
		case 0:	
			transform.FindChild("Pinata_Root").
				FindChild("upperRope").
					FindChild("middleRope").
					FindChild("lowerRope").
					FindChild("pinata").
					FindChild("Doll").GetComponent<MeshFilter>().mesh = NearDestroyed;
			break;
		case 1:
			transform.FindChild("Pinata_Root").
				FindChild("upperRope").
					FindChild("middleRope").
					FindChild("lowerRope").
					FindChild("pinata").
					FindChild("Doll").GetComponent<MeshFilter>().mesh = Damaged;

			break;
		case -1:
			transform.FindChild("Pinata_Root").
				FindChild("upperRope").
					FindChild("middleRope").
					FindChild("lowerRope").
					FindChild("pinata").
					FindChild("Doll").GetComponent<MeshFilter>().mesh = null;
			break;
		}
	}

	public void Reset()
	{
		_timer = 0;
		_damageState= 2;
		transform.FindChild("Pinata_Root").
			FindChild("upperRope").
				FindChild("middleRope").
				FindChild("lowerRope").
				FindChild("pinata").
				FindChild("Doll").GetComponent<MeshFilter>().mesh=Full;
	}
}
