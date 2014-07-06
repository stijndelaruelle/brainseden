using UnityEngine;
using System.Collections;

public class ShepherdBehaviour : MonoBehaviour, IPlayer
{
    public float m_BaseSpeed = 20000.0f;
    public float m_SprintSpeed = 30000.0f;

    private IItem m_Item;
    private ScoreManager m_ScoreManager;

	// Use this for initialization
	void Start ()
    {
        m_ScoreManager = GameObject.Find("Scorebar").GetComponent<ScoreManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    //bool down = Input.GetButtonDown("Jump");
        //bool held = Input.GetButton("Jump");
        //bool up = Input.GetButtonUp("Jump");

        float horizontal = Input.GetAxis("Player1_Horizontal");
        float vertical = Input.GetAxis("Player1_Vertical");

        rigidbody.AddForce(Vector3.right * horizontal * m_BaseSpeed * Time.deltaTime, ForceMode.Acceleration);
        rigidbody.AddForce(Vector3.forward * vertical * m_BaseSpeed * Time.deltaTime, ForceMode.Acceleration);

        transform.LookAt(transform.position + new Vector3(horizontal, 0.0f, vertical));

        //Use item
        if (Input.GetButtonDown("Player1_Fire") && m_Item != null)
        {
            m_Item.Activate();
            m_Item = null;
        }
	}

    //--------------------------
    // IPlayer Interface
    //--------------------------
    public void SetItem(IItem item)
    {
        m_Item = item;
    }

    public void AddScore(int amount)
    {
        m_ScoreManager.AddScoreShepherd(amount);
    }
}