using UnityEngine;
using System.Collections;

public class PickupBehaviour : MonoBehaviour 
{
    public enum PickupType { Score, Cloud };
    public PickupType Type { get; set; }

	// Use this for initialization
	void Start () 
	{
	    //Randomise which pickup we'll be
        Type = PickupType.Cloud;
	}
	
	// Update is called once per frame
	void Update () 
	{
	    //Draai rond
	}

    void OnCollisionEnter(Collision collision)
    {
        IPlayer player = null;
        if (collision.gameObject.tag == "Shepherd")     player = collision.gameObject.GetComponent<ShepherdBehaviour>() as IPlayer;
        if (collision.gameObject.tag == "PlayerSheep")  player = collision.gameObject.GetComponent<PlayerSheepBehaviour>() as IPlayer;

        if (player != null)
        {
            IItem item = null;

            switch (Type)
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