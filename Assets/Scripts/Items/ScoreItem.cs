using UnityEngine;
using System.Collections;

public class ScoreItem : IItem
{
    public IPlayer Target { get; set; }

    public bool OnPickup()
    {
        Debug.Log("SCORE");
        Target.AddScore(20);
        return false;
    }

    public void Activate()
    {
        //This isn't an activatable item
    }
}