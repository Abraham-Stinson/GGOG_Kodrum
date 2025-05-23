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
        Vector2 detectionPoint = new Vector2(0,0);
        Debug.Log("Detecting item");
        if (playerControllerScript.facingRight)
        {
            detectionPoint = (Vector2)transform.position + detectionOffset;

        }
        else
        {
            detectionPoint = (Vector2)transform.position - detectionOffset;
        }

        Collider2D hit = Physics2D.OverlapCircle(detectionPoint, detectionRadius, detectionLayer);

        if (hit != null)
        {
            Debug.Log(hit.gameObject.name);
            HandleItem(hit.gameObject);
        }

    }

    void HandleItem(GameObject obj)
    {
        Sprite spriteData = obj.GetComponent<SpriteRenderer>().sprite;
        if (!playerControllerScript.isCarryingItem)
        {
            if (obj.CompareTag("Item"))
            {
                Debug.Log("Handle Item");
                currentObjectSprite = spriteData;
                playerControllerScript.isCarryingItem = true;
                playerControllerScript.currentItemSprite = currentObjectSprite;
                itemSpriteRenderer.sprite = currentObjectSprite;
                Destroy(obj);
            }
            else if (obj.CompareTag("Item_Hub"))
            {
                Debug.Log("Item Hub detected");
                if (playerControllerScript.player1StartingDevice == playerInput.devices[0] && !playerControllerScript.player1HasTakenItemFromHub)
                {
                    playerControllerScript.player1HasTakenItemFromHub = true;
                    playerControllerScript.isCarryingItem = true;
                    currentObjectSprite = itemPlayer1.GetComponent<SpriteRenderer>().sprite;
                    playerControllerScript.currentItemSprite = currentObjectSprite;
                    itemSpriteRenderer.sprite = currentObjectSprite;
                }
                else if (playerControllerScript.player1StartingDevice != playerInput.devices[0] && !playerControllerScript.player2HasTakenItemFromHub)
                {
                    playerControllerScript.isCarryingItem = true;
                    playerControllerScript.player2HasTakenItemFromHub = true;
                    currentObjectSprite = itemPlayer2.GetComponent<SpriteRenderer>().sprite;
                    playerControllerScript.currentItemSprite = currentObjectSprite;
                    itemSpriteRenderer.sprite = currentObjectSprite;
                }
            }
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Vector2 detectionPoint = (Vector2)transform.position + detectionOffset;
        Gizmos.DrawWireSphere(detectionPoint, detectionRadius);
    }


}
