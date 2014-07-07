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
        transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
    }

    public void OnMouseExit()
    {
        transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
    }
}