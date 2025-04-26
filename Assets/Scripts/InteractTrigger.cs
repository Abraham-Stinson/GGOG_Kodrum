using UnityEngine;
using UnityEngine.InputSystem;

public class InteractTrigger : MonoBehaviour
{
    public float detectionRadius = 1f;
    public Vector2 detectionOffset = new Vector2(1f, 0f); // In front of player
    public LayerMask detectionLayer;
    public SpriteRenderer itemSpriteRenderer;
    public PlayerControllerScript playerControllerScript;
    public GameObject itemPlayer1;
    public GameObject itemPlayer2;
    public PlayerInput playerInput;


    private Sprite currentObjectSprite;

    

    public void DetectItem()
    {
        Vector2 detectionPoint = (Vector2)transform.position + detectionOffset;
        Collider2D hit = Physics2D.OverlapCircle(detectionPoint, detectionRadius, detectionLayer);

        if (hit != null)
        {
            HandleItem(hit.gameObject);
        }
        PlayerControllerScript.itemTrigger = false;
    }

    void HandleItem(GameObject obj)
    {
        Sprite spriteData = obj.GetComponent<Sprite>();
        if (!playerControllerScript.isCarryingItem)
        {
            if (obj.CompareTag("item"))
            {
                currentObjectSprite = spriteData;
                Destroy(obj);
            }
            else if (obj.CompareTag("item_hub"))
            {
                if (playerControllerScript.player1StartingDevice == playerInput.devices[0] && playerControllerScript.player1HasTakenItemFromHub)
                {
                    playerControllerScript.player1HasTakenItemFromHub = true;
                    currentObjectSprite = itemPlayer1.GetComponent<Sprite>();
                    itemSpriteRenderer.sprite = currentObjectSprite;
                }
                else if (playerControllerScript.player1StartingDevice == playerInput.devices[0] && playerControllerScript.player1HasTakenItemFromHub)
                {
                    playerControllerScript.player1HasTakenItemFromHub = true;
                    currentObjectSprite = itemPlayer2.GetComponent<Sprite>();
                    itemSpriteRenderer.sprite = currentObjectSprite;
                }
            }
        }
        
    }


}
