using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class PinataManager : MonoBehaviour
{
    public GameObject m_PinataGameObject;
    public List<GameObject> m_PinataSpawns;
    public float m_MaxSpawnTime = 12.0f;
    public float m_MinSpawnTime = 8.0f;

    private float m_SpawnTimer;
    private GameObject m_Pinata;

	// Use this for initialization
	void Start ()
	{
        m_PinataSpawns = new List<GameObject>(GameObject.FindGameObjectsWithTag("PinataSpawn"));
	    m_Pinata = null;
	    ResetTimer();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (m_Pinata != null)
	    {
	        if (m_Pinata.GetComponent<Pinata>().CanIBeDestroyed())
	        {
	            Destroy(m_Pinata);
	            m_Pinata = null;
	        }
	    }
	    else
	    {
            m_SpawnTimer -= Time.deltaTime;
            if (m_SpawnTimer <= 0)
            {
                ResetTimer();
                SpawnRandomPinata();
            } 
	    }
	}

    private void SpawnRandomPinata()
    {
        int count = m_PinataSpawns.Count;
        if (count <= 0)
            return;
        int i = Random.Range(0, count);
        m_Pinata = GameObject.Instantiate(m_PinataGameObject, m_PinataSpawns[i].transform.position,
            m_PinataGameObject.transform.rotation) as GameObject;
        m_Pinata.GetComponent<Pinata>().Reset();
    }

    void ResetTimer()
    {
        m_SpawnTimer = Random.Range(m_MinSpawnTime, m_MaxSpawnTime);
    }
}