using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class SheepSpawner : MonoBehaviour
{
    public GameObject m_SheepPrefab;
    public int m_SheepCount = 10;
    public float m_MinDistance = 1.0f;
    public float m_MaxDistance = 2.0f;
    private static List<GameObject> m_Sheeps;

    // Use this for initialization
    private void Start()
    {
        m_Sheeps = new List<GameObject>();
        for (int i = 0; i < m_SheepCount; i++)
        {
            Vector3 newPos = GetNextPos(i);
            GameObject newSheep =
                Instantiate(m_SheepPrefab, newPos, Quaternion.AngleAxis(Random.Range(0, (float) Math.PI), Vector3.up))
                    as GameObject;
            m_Sheeps.Add(newSheep);
        }
    }

    // Update is called once per frame
    private void Update() {}

    private Vector3 GetNextPos(int index)
    {
        int rows = (int) Math.Sqrt(m_SheepCount);
        int x = index%rows;
        int y = index/rows;
        Vector3 origin = new Vector3((-rows/2)*m_MaxDistance, 1, (-rows/2)*m_MaxDistance);
        Vector3 newPos = origin;
        newPos.x += (x*m_MaxDistance) + Random.Range(m_MinDistance, m_MaxDistance);
        newPos.z += (y*m_MaxDistance) + Random.Range(m_MinDistance, m_MaxDistance);
        return newPos;
    }

    public static List<GameObject> GetSheeps()
    {
        return m_Sheeps;
    }
}