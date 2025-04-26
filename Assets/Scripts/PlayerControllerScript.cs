using NUnit.Framework;
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
    [SerializeField] private GameObject itemPrefab;
    //[SerializeField] private GameObject itemPrefab;
    [SerializeField] private GameObject itemWithTag;
    [SerializeField] private GameObject itemDetector;
    [SerializeField] private GameObject itemHub;
    [SerializeField] private bool hasTakenItemOnHub=false;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }
    void Start()
    {
        
        
        itemDetector=GameObject.FindWithTag("Item_Detector");
        itemHub=GameObject.FindWithTag("Item_Hub");
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
            if(hasTakenItemOnHub){
                if(itemWithTag.GetComponent<itemScript>()!=null){
                    if(!isCarrying&&itemWithTag.GetComponent<itemScript>().isCollide){
                        Debug.Log("Itemi aldı");
                        itemWithTag.GetComponent<SpriteRenderer>().enabled=false;
                        itemWithTag.GetComponent<BoxCollider2D>().enabled=false;                    
                        isCarrying=true;
                    }
                    else if(isCarrying/*&&GameObject.FindWithTag("Item").GetComponent<itemScript>().isCollide*/){
                        Debug.Log("Itemi bıraktı");
                        itemWithTag.GetComponent<SpriteRenderer>().enabled=true;
                        itemWithTag.GetComponent<BoxCollider2D>().enabled=true;
                        //GameObject.FindWithTag("Item").gameObject.transform.position=new Vector3(0,2,0);

                        if(moveInput<0){
                            itemWithTag.gameObject.transform.position=new Vector3(this.gameObject.transform.position.x-0.5f,this.gameObject.transform.position.y);
                            itemWithTag.GetComponent<Rigidbody2D>().AddForce(Vector2.left * thrownForce, ForceMode2D.Impulse);
                        }
                        else if(moveInput>0){
                            itemWithTag.gameObject.transform.position=new Vector3(this.gameObject.transform.position.x+0.5f,this.gameObject.transform.position.y);
                            itemWithTag.GetComponent<Rigidbody2D>().AddForce(Vector2.right * thrownForce, ForceMode2D.Impulse);
                        }
                        else{
                            itemWithTag.gameObject.transform.position=new Vector3(this.gameObject.transform.position.x,this.gameObject.transform.position.y+0.5f);
                            itemWithTag.GetComponent<Rigidbody2D>().AddForce(Vector2.up * thrownForce, ForceMode2D.Impulse);
                        }
                        //else item.gameObject.transform.position=new Vector3(this.gameObject.transform.position.x,this.gameObject.transform.position.y+0.5f);

                        
                        itemWithTag.GetComponent<itemScript>().ChangeSprite();//SPRITE DEGİSECEK AMA DENEME
                        isCarrying=false;
                    }
                }
            }
            else if(itemHub.GetComponent<itemHubScript>()!=null&&itemHub.GetComponent<itemHubScript>().isHubCollide){
                    
                    Debug.Log("Hubdan item aldı");
                    Instantiate(itemPrefab,this.gameObject.transform.position,this.gameObject.transform.rotation);
                    itemWithTag=GameObject.FindWithTag("Item");
                    itemWithTag.GetComponent<SpriteRenderer>().enabled=false;
                    itemWithTag.GetComponent<BoxCollider2D>().enabled=false;
                    Debug.Log(itemWithTag.GetComponent<BoxCollider2D>().enabled);
                    isCarrying=true;
                    hasTakenItemOnHub=true;
            }
        }
    }
}