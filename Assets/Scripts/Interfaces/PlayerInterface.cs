using UnityEngine;
using System.Collections;

public interface IPlayer
{
    void SetItem(IItem item);
    void AddScore(int score);
}