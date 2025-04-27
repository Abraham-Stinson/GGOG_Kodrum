using System;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControllerScript : MonoBehaviour
{
    public Animator animator;
    public bool hoodieOn;
    
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.2f;

    [Header("InputManager")]
    public InputDevice player1StartingDevice;
    public PlayerInput playerInput;

    private float thrownForce = 10f;

    public bool facingRight = true;
    private bool isJumping;
    private float moveInput;


    [Header("Interact")]
    public InteractTrigger interactTrigger;
    public bool isCarryingItem = false;
    public bool player1HasTakenItemFromHub = false;
    public bool player2HasTakenItemFromHub = false;
    
    private Rigidbody2D rb;

    [Header("Item")]
    public Sprite currentItemSprite;
    public GameObject itemPlayer1;
    public GameObject itemPlayer2;
    public SpriteRenderer itemSpriteRenderer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();



    }
    void Start()
    {
        player1StartingDevice = playerInput.devices[0];
    }

    private void Update()
    {
        //Animation
        Animation();
        
    }

    private void ActivateTrigger()
    {
        interactTrigger.DetectItem();
        
    }
    public void OnThrow(InputValue value)
    {
        if (value.isPressed)
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
            if (facingRight)
            {
                throwItem.GetComponent<Rigidbody2D>().AddForce(Vector2.right * thrownForce, ForceMode2D.Impulse);
            }
            else
            {
                throwItem.GetComponent<Rigidbody2D>().AddForce(Vector2.left * thrownForce, ForceMode2D.Impulse);
            }
            
            isCarryingItem = false;
        }
        if (currentItemSprite == itemPlayer2.GetComponent<SpriteRenderer>().sprite)
        {
            GameObject throwItem = Instantiate(itemPlayer2, itemSpriteRenderer.transform.position, Quaternion.identity);
            if (facingRight)
            {
                throwItem.GetComponent<Rigidbody2D>().AddForce(Vector2.right * thrownForce, ForceMode2D.Impulse);
            }
            else
            {
                throwItem.GetComponent<Rigidbody2D>().AddForce(Vector2.left * thrownForce, ForceMode2D.Impulse);
            }
            isCarryingItem = false;

        }

        itemSpriteRenderer.sprite = null;
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
            ActivateTrigger();
            Debug.Log("Trigger Activated");

        }
    }
    private void FixedUpdate()
    {
        // Move the player
        if (moveInput < 0){
            gameObject.transform.eulerAngles = new Vector3(0, -180, 0);
            facingRight = false;
        }
        else if (moveInput > 0){
            gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
            facingRight = true;
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
    

    void Animation(){
        animator.SetFloat("moveInput",Math.Abs(moveInput));
            if(rb.linearVelocity.y>0.1){
                animator.SetBool("isJumping",true);
            }
            else{
                animator.SetBool("isJumping",false);
            }

            if(rb.linearVelocity.y<-0.1){
                animator.SetBool("isFalling",true);
            }
            else{
                animator.SetBool("isFalling",false);
            }
        
    }

}