using System;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControllerScript : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.2f;

    [Header("InputManager")]
    public InputDevice player1StartingDevice;
    public PlayerInput playerInput;

    public SpriteRenderer itemSpriteRenderer;
    private Sprite currentItemSprite;
    private bool isPlayer1 = true;
    public GameObject itemPlayer1;
    public GameObject itemPlayer2;


    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    private float moveInput;
    private bool isJumping;
    private InteractTrigger interactTrigger;
    [Header("Interact")]
    [SerializeField] public bool isCarryingItem = false;
    [SerializeField] private float thrownForce=10f;
    [SerializeField] private GameObject itemHub;
    [SerializeField] public bool player1HasTakenItemFromHub = false;
    [SerializeField] public bool player2HasTakenItemFromHub = false;

    public BoxCollider2D triggerCollider;
    public bool itemTriggerActive = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        interactTrigger = GetComponent<InteractTrigger>();


    }
    void Start()
    {
        player1StartingDevice = playerInput.devices[0];
    }

    private void Update()
    {
        if (itemTriggerActive) {
            
            ActivateTrigger();
        }
    }

    private void ActivateTrigger()
    {
        interactTrigger.DetectItem();
        
    }
    public void OnThrow(InputValue value)
    {
        if (value.isPressed && IsGrounded())
        {
            Throw();
        }
    }

    private void Throw()
    {
        if (currentItemSprite == null) { return; }
        if (currentItemSprite == itemPlayer1.GetComponent<SpriteRenderer>().sprite)
        {
            GameObject throwItem = Instantiate(itemPlayer1, itemSpriteRenderer.transform.position, Quaternion.identity);
            throwItem.GetComponent<Rigidbody2D>().AddForce(Vector2.right * thrownForce, ForceMode2D.Impulse);
            isCarryingItem = false;
        }
        if (currentItemSprite == itemPlayer2.GetComponent<SpriteRenderer>().sprite)
        {
            GameObject throwItem = Instantiate(itemPlayer2, itemSpriteRenderer.transform.position, Quaternion.identity);
            throwItem.GetComponent<Rigidbody2D>().AddForce(Vector2.right * thrownForce, ForceMode2D.Impulse);
            isCarryingItem = false;

        }

        itemSpriteRenderer.enabled = false;
        currentItemSprite = null;
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<float>();
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed && IsGrounded())
        {
            isJumping = true;
        }
    }

    public void OnInteract(InputValue value)
    {
        if (value.isPressed)
        {
            itemTriggerActive = true;
            Debug.Log("Trigger Activated");
            triggerCollider.enabled = true;
        }
    }
    private void FixedUpdate()
    {
        // Move the player
        if (moveInput < 0){
            gameObject.transform.eulerAngles = new Vector3(0, -180, 0);
        }
        else if (moveInput > 0){
            gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Jump
        if (isJumping)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isJumping = false;
        }
    }

        

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    public void IsCarrying()
    {
        isCarryingItem = !isCarryingItem;
    }
    

    




}