﻿using UnityEngine;
using System.Collections;

public class ControlImage : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButtonUp("Player1_Sprint"))
        {
            OnMouseDown();
        }
    }

    void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
    }

    public void Enable()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
    }
}