﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class SheepSpawner : MonoBehaviour
{
    public GameObject m_SheepPrefab;
    public int m_SheepCount = 10;
    private List<GameObject> m_Sheep;

    // Use this for initialization
    private void Start()
    {
        float radius = GameObject.Find("ShepherdCollision").collider.bounds.size.x / 2;
        m_Sheep = new List<GameObject>();
        for (int i = 0; i < m_SheepCount; i++)
        {
            //http://stackoverflow.com/questions/5837572/generate-a-random-point-within-a-circle-uniformly

            double t = 2 * Math.PI * Random.Range(0.0f, 1.0f);
            double u = Random.Range(0.0f, 1.0f) + Random.Range(0.0f, 1.0f);

            double r = u;
            if (u > 1)
            { 
                r = 2 - u;
            }

            Vector3 newPos = new Vector3((float)(r * Math.Cos(t)) * radius, 0.0f, (float)(r * Math.Sin(t)) * radius);
            newPos += transform.position;
            GameObject newSheep = Instantiate(m_SheepPrefab, newPos, Quaternion.identity) as GameObject;

            m_Sheep.Add(newSheep);
        }
    }

    public List<GameObject> GetSheep()
    {
        return m_Sheep;
    }
}