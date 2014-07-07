using UnityEngine;
using System.Collections;

public class ControlButton : MonoBehaviour, IButton
{
    public GameObject m_ControlWindow;

    public void OnMouseDown()
    {
        m_ControlWindow.GetComponent<ControlImage>().Enable();
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