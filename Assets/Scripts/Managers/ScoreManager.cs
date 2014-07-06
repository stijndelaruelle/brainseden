using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour 
{
    public int ShepherdScore
    {
        get;
        private set;
    }

    public int SheepScore
    {
        get;
        private set;
    }

    private int m_RogueSheep = 0;
    private float m_Timer = 1.0f;

    private SpriteRenderer m_RedBar;
    private SpriteRenderer m_BlueBar;
    private SpriteRenderer m_ShepherdIcon;
    private SpriteRenderer m_SheepIcon;

	void Start () 
	{
        m_RedBar = transform.Find("ProgressBarRed").GetComponent<SpriteRenderer>();
        m_BlueBar = transform.Find("ProgressBarBlue").GetComponent<SpriteRenderer>();
        m_ShepherdIcon = transform.Find("Icon_Coyote").GetComponent<SpriteRenderer>();
        m_SheepIcon = transform.Find("Icon_Lama").GetComponent<SpriteRenderer>();

        ShepherdScore = 50;
        SheepScore = 50;

        CalculateBar();
	}

    void Update()
    {
        if (m_Timer <= 0.0f)
        {
            if (m_RogueSheep > 0) AddScoreSheep(1);
            else                  AddScoreShepherd(1);

            m_Timer = 1.0f;
        }

        m_Timer -= Time.deltaTime;
    }

    public void AddScoreShepherd(int amount)
    {
        if (SheepScore <= 0) return;

        ShepherdScore += amount;
        SheepScore -= amount;

        if (SheepScore <= 0)
        {
            Debug.Log("Coyote wins!");
        }
        else CalculateBar();
    }

    public void AddScoreSheep(int amount)
    {
        if (ShepherdScore <= 0) return;

        SheepScore += amount;
        ShepherdScore -= amount;

        if (ShepherdScore <= 0)
        {
            Debug.Log("Lamas win!");
        }
        else CalculateBar();
    }

    public void AddRogueSheep()
    {
        m_RogueSheep += 1;
    }

    public void RemoveRogueSheep()
    {
        m_RogueSheep -= 1;
        if (m_RogueSheep < 0) m_RogueSheep = 0;
    }

    void CalculateBar()
    {
        //Scale the bars
        m_BlueBar.transform.localScale = new Vector3(SheepScore / 100.0f, 1.0f, 1.0f);
        m_RedBar.transform.localScale = new Vector3(ShepherdScore / 100.0f, 1.0f, 1.0f);

        //Show or hide the icons
        if (ShepherdScore >= SheepScore)
        {
            m_ShepherdIcon.enabled = true;
            m_SheepIcon.enabled = false;
        }
        else
        {
            m_ShepherdIcon.enabled = false;
            m_SheepIcon.enabled = true;
        }

        //Move the icons
        m_ShepherdIcon.transform.localPosition = new Vector3(((ShepherdScore / 100.0f) * 8.6f) - 4.3f, 0.0f, -1.0f);
        m_SheepIcon.transform.localPosition = new Vector3(((ShepherdScore / 100.0f) * 8.6f) - 4.3f, 0.0f, -1.0f);
    }
}