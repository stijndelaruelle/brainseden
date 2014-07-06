using UnityEngine;
using System.Collections;

public class Pinata : MonoBehaviour 
{
    public GameObject m_PickupPrefab;
    public int m_Health = 2;

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

    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.gameObject.tag != "Shepherd" && coll.collider.gameObject.tag != "PlayerSheep") return;
        if (m_PrevCollider == coll.collider && m_Countdown > 0.0f) return;

        m_Health -= 1;

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
        pickup.GetComponent<PickupBehaviour>().Type = PickupBehaviour.PickupType.Score;
        pickup.rigidbody.AddForce(new Vector3(randX, randY, randZ));

        if (m_Health <= 0)
        {
           //Fire pickup
           Destroy(gameObject);
        }

        m_PrevCollider = coll.collider;
        m_Countdown = 1.0f;
    }
}