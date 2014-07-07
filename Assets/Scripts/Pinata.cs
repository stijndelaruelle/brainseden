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


        //Random direction
        int randX = Random.Range(25, 100);
        int randY = Random.Range(100, 500);
        int randZ = Random.Range(25, 100);

        int negateX = Random.Range(0, 2);
        int negateZ = Random.Range(0, 2);
        if (negateX == 0) randX *= -1;
        if (negateZ == 0) randZ *= -1;

        //Fire some point pickups
        GameObject pickup = Instantiate(m_PickupPrefab, transform.position, Quaternion.identity) as GameObject;
		if (collider.gameObject.tag == "Shepherd") 
		{ 
			pickup.GetComponent<PickupBehaviour>().Type = PickupBehaviour.PickupType.Bark; 
		}
		else                                           
		{ 
			pickup.GetComponent<PickupBehaviour>().Type = PickupBehaviour.PickupType.Cloud; 
		}
        pickup.rigidbody.AddForce(new Vector3(randX, randY, randZ));


        gameObject.GetComponent<PinataEffects>().Hit();
        m_PrevCollider = collider;
        m_Countdown = 1.0f;
    }

	public void Reset()
	{
	    m_Respawn = true;
	}

    void Update()
    {
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