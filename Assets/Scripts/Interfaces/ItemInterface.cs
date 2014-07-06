using UnityEngine;
using System.Collections;

public interface IItem
{
    IPlayer Target { get; set; }

    bool OnPickup();
    void Activate();
}