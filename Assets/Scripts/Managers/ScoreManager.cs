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

	void Start () 
	{
        // Create 1x1 white texture
        var texture = new Texture2D(1, 1, TextureFormat.ARGB32, false);
        texture.SetPixel(0, 0, Color.white);
        texture.Apply();

        guiTexture.texture = texture;

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
        ShepherdScore += amount;
        CalculateBar();
    }

    public void AddScoreSheep(int amount)
    {
        SheepScore += amount;
        CalculateBar();
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
        //Difference
        //float diff = ShepherdScore - SheepScore;
        //diff /= 200; //100 points of difference is a full bar
        //if (diff > 0.5f) diff = 0.5f;
        //if (diff < -0.5f) diff = -0.5f;

        //transform.localScale = new Vector3(diff, 0.1f, 1.0f);
        //transform.position = new Vector3(0.5f - diff / 2.0f, 0.94f, 0.0f);

        //if (diff > 0.0f) guiTexture.color = new Color(0.44f, 0.039f, 0.039f);
        //else             guiTexture.color = new Color(0.04f, 0.325f, 1.0f);
    }
}