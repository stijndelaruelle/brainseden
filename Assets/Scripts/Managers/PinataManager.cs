using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class PinataManager : MonoBehaviour
{
    public List<GameObject> m_Pinatas;
    public float m_MaxSpawnTime = 12.0f;
    public float m_MinSpawnTime = 8.0f;

    private float m_SpawnTimer;
	// Use this for initialization
	void Start ()
	{
	    m_Pinatas = new List<GameObject>(GameObject.FindGameObjectsWithTag("Pinata"));
	    ResetTimer();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    m_SpawnTimer -= Time.deltaTime;
	    if (m_SpawnTimer <= 0)
	    {
	        ResetTimer();
	        SpawnRandomPinata();
	    }
	}

    private void SpawnRandomPinata()
    {
        int count = m_Pinatas.Count;
        if (count <= 0)
            return;

        int i = Random.Range(0, count);
        m_Pinatas[i].GetComponent<Pinata>().Reset();
    }

    void ResetTimer()
    {
        m_SpawnTimer = Random.Range(m_MinSpawnTime, m_MaxSpawnTime);
    }
}