using UnityEngine;

public class FlagTrigger2 : MonoBehaviour
{
    public GameObject player2Prefab;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerControllerScript pcs = collision.gameObject.GetComponent<PlayerControllerScript>();
            if (pcs.isCarryingItem && (pcs.currentItemSprite == player2Prefab.GetComponent<SpriteRenderer>().sprite))
            {
                pcs.isCarryingItem = false;
                pcs.currentItemSprite = null;
                pcs.itemSpriteRenderer.sprite = null;
                pcs.player2HasTakenItemFromHub = false;
            }
        }
    }
}
