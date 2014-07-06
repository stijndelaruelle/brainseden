using UnityEngine;
using System.Collections;

public class CloudItem : IItem
{
    public IPlayer Target { get; set; }

    public bool OnPickup()
    {
        Target.AddScore(5); //Taking the pickup gives you a bit of score
        return true;
    }

    public void Activate()
    {
        //On activate we spawn a cloud!
        //(test just give score again)
        Target.AddScore(20);
    }
}