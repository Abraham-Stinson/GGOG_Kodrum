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
    [SerializeField] private GameObject happyItemPrefab;
    [SerializeField] private GameObject sadItemPrefab;
    [SerializeField] private GameObject itemWithTag;
    [SerializeField] private GameObject itemDetector;
    [SerializeField] private GameObject itemHub;
    [SerializeField] private Transform heldPosition;
    [SerializeField] private bool hasPlayer1TakenItemOnHub=false;
    [SerializeField] private bool hasPlayer2TakenItemOnHub=false;


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
            Interact();
        }
    }
    void Update()
    {
        if(itemWithTag==null){
            itemWithTag=GameObject.FindWithTag("Item");
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


    private void Interact(){
       
        if(!isCarrying){
            if(player1StartingDevice == playerInput.devices[0]&&!hasPlayer1TakenItemOnHub&&itemHub.GetComponent<itemHubScript>().isHubCollide){
                CreateItem(happyItemPrefab);//create item and set parentt
                hasPlayer1TakenItemOnHub=true;
                Debug.Log("Itemi oluşturdu ve iyi item aldı");
            }
            else if(player1StartingDevice != playerInput.devices[0]&&!hasPlayer2TakenItemOnHub&&itemHub.GetComponent<itemHubScript>().isHubCollide){
                CreateItem(sadItemPrefab);//create item and set parentt
                hasPlayer2TakenItemOnHub=true;
                Debug.Log("Itemi oluşturdu ve kötü item aldı");
            }
            else if(itemWithTag.GetComponent<itemScript>()!=null&&itemWithTag.GetComponent<itemScript>().isCollide){
                
                itemWithTag.transform.SetParent(heldPosition,false);
                itemWithTag.transform.localPosition = Vector3.zero;
                isCarrying=true;
                itemWithTag.GetComponent<Rigidbody2D>().simulated=false;
                Debug.Log("Itemi aldı");
            }
        }
        
        else if(isCarrying){
            
            itemWithTag.transform.SetParent(null);
            itemWithTag.GetComponent<Rigidbody2D>().simulated=true;
            isCarrying=false;
            Debug.Log("Itemi bıraktı");
            itemWithTag.GetComponent<itemScript>().ChangeSprite();//SPRITE DEGİSECEK AMA DENEME
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
        }
        
                    
    }
    

    void CreateItem(GameObject prefab){
        GameObject newItem = Instantiate(prefab, transform.position, transform.rotation); // yeni objeyi yakala
        newItem.transform.SetParent(heldPosition, false);
        newItem.transform.localPosition = Vector3.zero;
        newItem.GetComponent<Rigidbody2D>().simulated=false;
        isCarrying=true;
        //itemWithTag=GameObject.FindWithTag("Helding");
    }
}