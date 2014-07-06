using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BarkItem : IItem
{
    public IPlayer Target { get; set; }

    public bool OnPickup()
    {
        Target.AddScore(5); //Taking the pickup gives you a bit of score
        return true;
    }

    public void Activate()
    {
        //On activate we scare everyone
        List<GameObject> sheep = GameObject.Find("GLOBALS").GetComponent<SheepSpawner>().GetSheep();

        Debug.Log("BARK");

        foreach (GameObject sh in sheep)
        {
            sh.GetComponent<SheepBehaviour>().UltimatePanic(Target.GameObject);
        }
    }
}