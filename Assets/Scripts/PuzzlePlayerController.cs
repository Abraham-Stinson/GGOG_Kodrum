using UnityEngine;
using UnityEngine.InputSystem;

public class PuzzlePlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public LayerMask obstacleLayer;
    public Vector2 gridSize = new Vector2(1f, 1f);
    private Vector2 moveInput;
    public PlayerControllerSwitch playerControllerSwitch;




    // Called by the Input System
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        if (moveInput != Vector2.zero)
        {
            TryMove();
        }
    }

    public void OnReset(InputValue value)
    {
        if (value.isPressed)
        {
            Debug.Log("Interact pressed");
            playerControllerSwitch.SwitchControl();
        }
    }

    private void TryMove()
    {
        Vector2 targetPosition = (Vector2)transform.position + new Vector2(moveInput.x * gridSize.x, moveInput.y * gridSize.y);
        Collider2D obstacle = Physics2D.OverlapBox(targetPosition, gridSize, 0f, obstacleLayer);

        if (obstacle != null)
        {
            Vector2 pushTargetPosition = (Vector2)obstacle.transform.position + new Vector2(moveInput.x * gridSize.x, moveInput.y * gridSize.y);
            Collider2D pushObstacle = Physics2D.OverlapBox(pushTargetPosition, gridSize, 0f, obstacleLayer);

            if (pushObstacle == null)
            {
                obstacle.transform.position = pushTargetPosition;
                transform.position = targetPosition;
            }
        }
        else
        {
            transform.position = targetPosition;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, gridSize);
    }
}
