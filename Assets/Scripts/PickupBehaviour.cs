using UnityEngine;
using System.Collections;

public class PickupBehaviour : MonoBehaviour 
{
    enum PickupType { Score, Cloud };

    PickupType m_PickupType;

	// Use this for initialization
	void Start () 
	{
	    //Randomise which pickup we'll be
        m_PickupType = PickupType.Cloud;
	}
	
	// Update is called once per frame
	void Update () 
	{
	    //Draai rond
	}

    void OnTriggerEnter(Collider collision)
    {
        IPlayer player = null;
        if (collision.gameObject.tag == "Shepherd")     player = collision.gameObject.GetComponent<ShepherdBehaviour>() as IPlayer;
        if (collision.gameObject.tag == "PlayerSheep")  player = collision.gameObject.GetComponent<PlayerSheepBehaviour>() as IPlayer;

        if (player != null)
        {
            IItem item = null;
            
            switch (m_PickupType)
            {
                case PickupType.Score:
                    item = new ScoreItem();
                    break;

                case PickupType.Cloud:
                    item = new CloudItem();
                    break;

                default:
                    break;
            }
            item.Target = player;

            if (item.OnPickup()) player.SetItem(item); //If it's usable set the item

            Destroy(gameObject);
        }
    }
}