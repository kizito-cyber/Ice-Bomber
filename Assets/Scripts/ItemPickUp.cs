
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public enum ItemType
    {
        ExtraBomb,
        blastRadius,
        SpeedIncrease,
    }

    public ItemType type;

    void OnItemPickUp(GameObject player)
    {
        switch(type)
        {
            case ItemType.ExtraBomb:
                player.GetComponent<BombController>().AddBomb();
                break;

            case ItemType.blastRadius:
                player.GetComponent<BombController>().explosionRadius++;
                break;

            case ItemType.SpeedIncrease:
                player.GetComponent<MovementController>().speed++;
                break;


        }

        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            OnItemPickUp(collision.gameObject);
        }
    }
}
