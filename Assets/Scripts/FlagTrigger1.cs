using UnityEngine;

public class FlagTrigger1 : MonoBehaviour
{
    public GameObject player1Prefab;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerControllerScript pcs = collision.gameObject.GetComponent<PlayerControllerScript>();
            if (pcs.isCarryingItem && (pcs.currentItemSprite == player1Prefab.GetComponent<SpriteRenderer>().sprite))
            {
                pcs.isCarryingItem = false;
                pcs.currentItemSprite = null;
                pcs.itemSpriteRenderer.sprite = null;
                pcs.player1HasTakenItemFromHub = false;
            }
        }
    }
}
