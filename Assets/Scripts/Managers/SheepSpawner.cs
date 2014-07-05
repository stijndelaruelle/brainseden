using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class SheepSpawner : MonoBehaviour
{
    public GameObject m_SheepPrefab;
    public int m_SheepCount = 10;
    public float m_MinDistance = 1.0f;
    public float m_MaxDistance = 2.0f;
	// Use this for initialization
	void Start ()
	{
        for (int i = 0; i < m_SheepCount; i++)
        {
            Vector3 newPos = GetNextPos(i);
            GameObject newSheep = Instantiate(m_SheepPrefab, newPos, Quaternion.AngleAxis(Random.Range(0,(float)Math.PI),Vector3.up)) as GameObject;
        }
	}

	// Update is called once per frame
	void Update () 
	{
	
	}

    Vector3 GetNextPos(int index)
    {
        int rows = (int) Math.Sqrt(m_SheepCount);
        int x = index % rows;
        int y = index / rows;
        Vector3 origin = new Vector3((-rows/2)*m_MaxDistance, 1 ,(-rows/2)*m_MaxDistance);
        Vector3 newPos = origin;
        newPos.x += x * Random.Range(m_MinDistance, m_MaxDistance);
        newPos.z += y * Random.Range(m_MinDistance, m_MaxDistance);
        return newPos;
    }
}