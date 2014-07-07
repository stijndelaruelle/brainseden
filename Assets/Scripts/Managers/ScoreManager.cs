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

    public bool Victory
    {
        get;
        private set;
    }

    private int m_RogueSheep = 0;
    private float m_Timer = 1.0f;

    private SpriteRenderer m_Border;
    private SpriteRenderer m_RedBar;
    private SpriteRenderer m_BlueBar;
    private SpriteRenderer m_ShepherdIcon;
    private SpriteRenderer m_SheepIcon;

    private SpriteRenderer m_ShepherdVictory;
    private SpriteRenderer m_SheepVictory;

    private SpriteRenderer m_ShepherdPickup;
    private SpriteRenderer m_SheepPickup;

	void Start () 
	{
        m_Border = transform.Find("ProgressBar").GetComponent<SpriteRenderer>();
        m_RedBar = transform.Find("ProgressBarRed").GetComponent<SpriteRenderer>();
        m_BlueBar = transform.Find("ProgressBarBlue").GetComponent<SpriteRenderer>();
        m_ShepherdIcon = transform.Find("Icon_Coyote").GetComponent<SpriteRenderer>();
        m_SheepIcon = transform.Find("Icon_Lama").GetComponent<SpriteRenderer>();

        m_ShepherdVictory = transform.Find("Victory_Coyote").GetComponent<SpriteRenderer>();
        m_SheepVictory = transform.Find("Victory_Lamas").GetComponent<SpriteRenderer>();

        m_ShepherdPickup = transform.Find("Face_Coyote").GetComponent<SpriteRenderer>();
        m_SheepPickup = transform.Find("Face_Lama").GetComponent<SpriteRenderer>();

        ShepherdScore = 50;
        SheepScore = 50;

        CalculateBar();
	}

    void Update()
    {
        if (Victory)
        {
            if (Input.GetButtonDown("Player1_Sprint") || Input.GetButtonDown("Player2_Sprint"))
            {
                Application.LoadLevel(2);
            }
        }
        else
        {
            if (m_Timer <= 0.0f)
            {
                if (m_RogueSheep > 0) 
                    AddScoreSheep(Mathf.CeilToInt(m_RogueSheep/3.0f));
                else 
                    AddScoreShepherd(1);

                m_Timer = 1.0f;
            }

            m_Timer -= Time.deltaTime;
        }
    }

    public void AddScoreShepherd(int amount)
    {
        if (SheepScore <= 0) return;

        ShepherdScore += amount;
        SheepScore -= amount;

        if (SheepScore <= 0)
        {
            EndGame();
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
            EndGame();
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

    void EndGame()
    {
        Victory = true;

        //Hide our bars
        m_Border.enabled = false;
        m_RedBar.enabled = false;
        m_BlueBar.enabled = false;
        m_ShepherdIcon.enabled = false;
        m_SheepIcon.enabled = false;

        //Depending on who won show the right image
        if (ShepherdScore >= 100)
        {
            m_ShepherdVictory.enabled = true;
            SoundManager.PlaySound("CoyoteTaunt", null, 1.0f);
        }
        else
        {
            m_SheepVictory.enabled = true;
            SoundManager.PlaySound("SheepWin", null, 1.0f);
        }
    }

    public void ShowSheepPickup()
    {
        m_SheepPickup.enabled = true;
    }

    public void HideSheepPickup()
    {
        m_SheepPickup.enabled = false;
    }

    public void ShowShepherdPickup()
    {
        m_ShepherdPickup.enabled = true;
    }

    public void HideShepherdPickup()
    {
        m_ShepherdPickup.enabled = false;
    }
}