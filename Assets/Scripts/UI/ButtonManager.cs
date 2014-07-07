using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ButtonManager : MonoBehaviour 
{
    private List<IButton> m_Buttons;
    private int m_Selected = 0;
    private bool m_HasMoved = false;

    void Start()
    {
        m_Buttons = new List<IButton>();
        m_Buttons.Add(GameObject.Find("Button_Start").GetComponent<StartButton>() as IButton);
        m_Buttons.Add(GameObject.Find("Button_Controls").GetComponent<ControlButton>() as IButton);
        m_Buttons.Add(GameObject.Find("Button_Quit").GetComponent<QuitButton>() as IButton);

        Debug.Log(m_Buttons[1]);
    }

	// Update is called once per frame
	void Update () 
	{
        //Submit
        if (Input.GetButtonDown("Player1_Sprint"))
        {
            m_Buttons[m_Selected].OnMouseDown();
        }

        //Move up/down
        float vertical = Input.GetAxis("Player1_Vertical");

        if (Mathf.Abs(vertical) < 0.5f) m_HasMoved = false;
        
        if (Mathf.Abs(vertical) > 0.5f && m_HasMoved == false)
        {
            m_Buttons[m_Selected].OnMouseExit();

            //Move down
            if (vertical < 0.0f)
            {
                ++m_Selected;
                if (m_Selected >= m_Buttons.Count) m_Selected = 0;
            }

            //Move up
            else
            {
                --m_Selected;
                if (m_Selected < 0) m_Selected = m_Buttons.Count - 1;
            }

            m_Buttons[m_Selected].OnMouseEnter();
            m_HasMoved = true;
        }
	}
}