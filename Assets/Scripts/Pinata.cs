using UnityEngine;
using System.Collections;

public class Pinata : MonoBehaviour 
{
    public GameObject m_PickupPrefab;

    private float m_Countdown;
    private Collider m_PrevCollider;

    private bool m_Respawn;

    private bool m_Removing = false;
    private float m_RemoveTimer;
    private float m_RemoveDelay;
    private bool m_CanBeDestroyed;

	public float DustSpawnTime = 1.0f;
	private float _timer =0;

	// Use this for initialization
	void Start ()
	{
	    m_RemoveDelay = 0;
	    m_RemoveTimer = 0;
	    m_Removing = false;
	    m_CanBeDestroyed = false;
	}

	void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag != "Shepherd" && collider.gameObject.tag != "PlayerSheep") return;
        if (m_PrevCollider == collider && m_Countdown > 0.0f) return;

        gameObject.GetComponent<PinataEffects>().Hit();
        m_PrevCollider = collider;
        m_Countdown = 1.0f;

        if (GetComponent<PinataEffects>().IsBroken())
        {
            //Random direction
            GameObject circle = GameObject.Find("Ring");
            Vector3 dir = circle.transform.position - transform.position;
            dir.Normalize();

            int randX = Random.Range(25, 150);
            int randY = Random.Range(300, 1000);
            int randZ = Random.Range(25, 150);

            //Fire some point pickups
            GameObject pickup = Instantiate(m_PickupPrefab, new Vector3(transform.position.x, transform.position.y + 5.0f, transform.position.z), Quaternion.identity) as GameObject;

            pickup.rigidbody.AddForce(new Vector3(dir.x * randX, randY, dir.z * randZ));
        }
    }

	public void Reset()
	{
	    m_Respawn = true;
	}

    void Update()
    {
		if(_timer>=0)
		_timer+=Time.deltaTime;
		if(_timer>DustSpawnTime)
		{
			transform.FindChild("DropDust").GetComponent<ParticleSystem>().Play();
			_timer=-1;
		}

        if (m_Countdown > 0.0f) 
            m_Countdown -= Time.deltaTime;

        if (m_Respawn)
        {
            gameObject.GetComponent<PinataEffects>().Reset();
            m_Respawn = false;
        }

        if (!m_Removing && gameObject.GetComponent<PinataEffects>().IsBroken() && m_RemoveDelay <= 0)
            m_RemoveDelay = 3.0f;

        if (!m_Removing && m_RemoveDelay > 0)
        {
            m_RemoveDelay -= Time.deltaTime;
            if (m_RemoveDelay <= 0)
            {
                Fly();
                m_RemoveDelay = 0;
            }
        }

        if (m_Removing)
        {
            m_RemoveTimer -= Time.deltaTime;
            if (m_RemoveTimer <= 0)
            {
                gameObject.GetComponent<PinataEffects>().StopFly();
                m_Respawn = false;
                gameObject.SetActive(false);
                m_RemoveTimer = 0;
                m_Removing = false;
                gameObject.active = false;
                m_CanBeDestroyed = true;
            }
        }
    }

    public void Fly()
    {
        gameObject.GetComponent<PinataEffects>().Fly();
        m_Removing = true;
        m_RemoveTimer = 1.5f;
    }

    public bool CanIBeDestroyed()
    {
        return m_CanBeDestroyed;
    }
}