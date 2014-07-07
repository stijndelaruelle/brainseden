using System;
using UnityEngine;
using System.Collections;

public class ShepherdBehaviour : MonoBehaviour, IPlayer
{
    public GameObject GameObject
    {
        get
        {
            return gameObject;
        }

        set
        {
            //gameObject = value;
        }
    }

    public Vector3 Position
    {
        get
        {
            return transform.position;
        }

        set
        {
            transform.position = value;
        }
    }

    public float m_BaseSpeed = 20000.0f;
    public float m_DashSpeed = 80000.0f;
    public float m_DashCooldown = 5.0f;
    public float m_DashLenght = 0.5f;

    private bool m_IsDashing;
    private float m_LastHorizontal;
    private float m_LastVertical;
    private float m_DashTimer = 0.0f;

    private IItem m_Item;
    private ScoreManager m_ScoreManager;

	private Vector3 _previousPos = Vector3.zero;

	// Use this for initialization
	void Start ()
    {
        m_ScoreManager = GameObject.Find("Scorebar").GetComponent<ScoreManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        float speed = m_BaseSpeed;

        float horizontal = Input.GetAxis("Player1_Horizontal");
        float vertical = Input.GetAxis("Player1_Vertical");

        //Start dashing
        if (Input.GetButton("Player1_Sprint") && m_DashTimer <= 0.0f)
        {
            m_IsDashing = true;
            m_DashTimer = m_DashLenght;
            m_LastHorizontal = transform.forward.x;
            m_LastVertical = transform.forward.z;
        }

        //Be dashing
        if (m_IsDashing && m_DashTimer > 0.0f)
        {
            speed = m_DashSpeed;
            horizontal = m_LastHorizontal;
            vertical = m_LastVertical;
        }

        //Stop dashing
        if (m_IsDashing && m_DashTimer <= 0.0f)
        {
            m_IsDashing = false;
            m_DashTimer = m_DashCooldown;
        }

        if (m_DashTimer > 0.0f) m_DashTimer -= Time.deltaTime;

        rigidbody.AddForce(Vector3.right * horizontal * speed * Time.deltaTime, ForceMode.Acceleration);
        rigidbody.AddForce(Vector3.forward * vertical * speed * Time.deltaTime, ForceMode.Acceleration);

        transform.LookAt(transform.position + new Vector3(horizontal, 0.0f, vertical));

        //Use item
        if (Input.GetButtonDown("Player1_Fire") && m_Item != null)
        {
            m_Item.Activate();
            m_Item = null;
        }

		if (VectorsApproxEqual(transform.position,_previousPos,0.5))
        {
            GetComponentInChildren<Animator>().SetBool("IsRunning", false);
        }
        else
        {
            GetComponentInChildren<Animator>().SetBool("IsRunning", true);
        }

		_previousPos = transform.position;
	}

	static public bool VectorsApproxEqual(Vector3 a, Vector3 b, double eps)
	{
		bool result = false;
		
		if(a.x + eps > b.x && a.x - eps < b.x) 
			if(a.y + eps > b.y && a.y - eps < b.y)
				if(a.z + eps > b.z && a.z - eps < b.z)
					result = true;
		
		return result;
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