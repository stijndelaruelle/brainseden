using UnityEngine;
using System.Collections;

public class Pinata : MonoBehaviour 
{
    public GameObject m_PickupPrefab;

    private float m_Countdown;
    private Collider m_PrevCollider;

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
        if (m_Countdown > 0.0f) m_Countdown -= Time.deltaTime;
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
		gameObject.GetComponent<PinataEffects>().Reset();
	}
}