using UnityEngine;

public class FlagTrigger1 : MonoBehaviour
{
    public GameObject player1Prefab;
    [SerializeField] private ScoreManager scoreManager;
    private void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player collided with flag trigger 1");
            PlayerControllerScript pcs = collision.gameObject.GetComponent<PlayerControllerScript>();
            if (pcs.isCarryingItem && (pcs.currentItemSprite == player1Prefab.GetComponent<SpriteRenderer>().sprite))
            {
                Debug.Log("Match");
                pcs.isCarryingItem = false;
                pcs.currentItemSprite = null;
                pcs.itemSpriteRenderer.sprite = null;
                pcs.player1HasTakenItemFromHub = false;
                scoreManager.AddScore(1);
            }
        }
    }
}
