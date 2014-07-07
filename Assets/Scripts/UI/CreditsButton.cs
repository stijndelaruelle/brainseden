using UnityEngine;
using System.Collections;

public class CreditsButton : MonoBehaviour, IButton
{
    public GameObject m_CreditsWindow;

    public void OnMouseDown()
    {
        m_CreditsWindow.GetComponent<CreditImage>().Enable();
    }

    public void OnMouseEnter()
    {
        transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
    }

    public void OnMouseExit()
    {
        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }
}