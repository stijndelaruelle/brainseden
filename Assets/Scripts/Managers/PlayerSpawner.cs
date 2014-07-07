using UnityEngine;
using System;
using System.Collections;
using Random = UnityEngine.Random;

public class PlayerSpawner : MonoBehaviour 
{
    //public GameObject m_ShepherdPrefab;
    public GameObject m_PlayerSheepPrefab;

	// Use this for initialization
	void Start () 
	{
        float radius = GameObject.Find("ShepherdCollision").collider.bounds.size.x / 2;

        //---------------------------
	    // Spawn the shepherd player
        //---------------------------
        //int min = (int)(radius + (radius / 10.0f));
        //int max = (int)(radius + (radius / 2.0f));
        //int randX = Random.Range(min, max);
        //int randZ = Random.Range(min, max);

        //int negateX = Random.Range(0, 2);
        //int negateZ = Random.Range(0, 2);
        //if (negateX == 0) randX *= -1;
        //if (negateZ == 0) randZ *= -1;

        //Instantiate(m_ShepherdPrefab, new Vector3(randX + transform.parent.position.x, 0.0f, randZ + transform.parent.position.z), Quaternion.identity);

        //---------------------------
	    // Spawn the sheep player
        //---------------------------
        //http://stackoverflow.com/questions/5837572/generate-a-random-point-within-a-circle-uniformly

        double t = 2 * Math.PI * Random.Range(0.0f, 1.0f);
        double u = Random.Range(0.0f, 1.0f) + Random.Range(0.0f, 1.0f);

        double r = u;
        if (u > 1) r = 2 - u;

        Vector3 newPos = new Vector3((float)((r * Math.Cos(t)) * radius) + +transform.parent.position.x, 0.0f, (float)((r * Math.Sin(t)) * radius) + +transform.parent.position.z);
        Instantiate(m_PlayerSheepPrefab, newPos, Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}