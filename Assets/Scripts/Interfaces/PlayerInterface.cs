using UnityEngine;
using System.Collections;

public interface IPlayer
{
    GameObject GameObject { get; set; }
    Vector3 Position { get; set; }

    void SetItem(IItem item);
    void AddScore(int score);
}