using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PuzzlePlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public PlayerInput playerInput;
    public InputDevice player2StartingDevice;
    private Vector2 moveInput;
    public PlayerControllerSwitch playerControllerSwitch;
    public PuzzleManager puzzleManager;
    private Rigidbody2D rb;


    private void Start()
    {
        player2StartingDevice = playerInput.devices[0];
        rb = GetComponent<Rigidbody2D>();

    }
    void Update()
    {
        Move();
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnReset(InputValue value)
    {
        //placeholder code to test sw�tch
        if (value.isPressed)
        {
            puzzleManager.ResetPuzzle();
        }
        
    }

    private void Move()
    {

        Vector2 movement = moveInput.normalized * moveSpeed;
        rb.linearVelocity = movement;
    }

    
}
