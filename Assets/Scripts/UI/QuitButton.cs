using UnityEngine;
using System.Collections;

public class QuitButton : MonoBehaviour, IButton
{
    public void OnMouseDown()
    {
        Application.Quit();
    }

    public void OnMouseEnter()
    {
        transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        transform.Find("Button_Highlight").GetComponent<SpriteRenderer>().enabled = true;
    }

    public void OnMouseExit()
    {
        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        transform.Find("Button_Highlight").GetComponent<SpriteRenderer>().enabled = false;
    }
}