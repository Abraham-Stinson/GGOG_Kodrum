using NUnit.Framework;
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
    public InputDevice player1StartingDevice;
    public PlayerInput playerInput;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private float moveInput;
    private bool isJumping;
    [Header("Interact")]
    private bool isInteract;
    private bool isCarrying=false;
    [SerializeField] private float thrownForce=10f;
    [SerializeField] private GameObject itemDetector;
    [SerializeField] private GameObject item;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }
    void Start()
    {
        item=GameObject.FindWithTag("Item");
        itemDetector=GameObject.FindWithTag("Item_Detector");
        player1StartingDevice = playerInput.devices[0];


    }

    // Called by the Input System
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
            Interact(true);
        }
    }
    private void FixedUpdate()
    {
        // Move the player
        if (moveInput < 0){
            spriteRenderer.flipX = true;
            itemDetector.transform.rotation=Quaternion.Euler(0,180,0);
        }
        else if (moveInput > 0){
            spriteRenderer.flipX = false;
            itemDetector.transform.rotation=Quaternion.Euler(0,0,0);
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


    private void Interact(bool isInteract){
        if(isInteract){
            if(item.GetComponent<itemScript>()!=null){

                if(!isCarrying&&item.GetComponent<itemScript>().isCollide){
                    Debug.Log("Itemi aldı");
                    item.GetComponent<SpriteRenderer>().enabled=false;
                    item.GetComponent<BoxCollider2D>().enabled=false;                    
                    isCarrying=true;
                }
                else if(isCarrying/*&&GameObject.FindWithTag("Item").GetComponent<itemScript>().isCollide*/){
                    Debug.Log("Itemi bıraktı");
                    item.GetComponent<SpriteRenderer>().enabled=true;
                    item.GetComponent<BoxCollider2D>().enabled=true;
                    //GameObject.FindWithTag("Item").gameObject.transform.position=new Vector3(0,2,0);

                    if(moveInput<0)item.gameObject.transform.position=new Vector3(this.gameObject.transform.position.x-0.5f,this.gameObject.transform.position.y);
                    else if(moveInput>0)item.gameObject.transform.position=new Vector3(this.gameObject.transform.position.x+0.5f,this.gameObject.transform.position.y);
                    //else item.gameObject.transform.position=new Vector3(this.gameObject.transform.position.x,this.gameObject.transform.position.y+0.5f);
                    
                    item.GetComponent<Rigidbody2D>().AddForce(Vector2.right * thrownForce, ForceMode2D.Impulse);
                    item.GetComponent<itemScript>().ChangeSprite();//SPRITE DEGİSECEK AMA DENEME
                    isCarrying=false;
                }
            }
        }
    }
}