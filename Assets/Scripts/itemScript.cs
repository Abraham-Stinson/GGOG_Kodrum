using UnityEngine;
public class itemScript : MonoBehaviour
{
    [SerializeField] public bool isCollide=false;
    [SerializeField] public bool isRightSideItem;
    [Header("Inspector'dan Atanacaklar")]
    [SerializeField] private Sprite[] spriteArray;
    [SerializeField] private SpriteRenderer spriteRenderer; 
    [SerializeField] private int currentIndex = 0;  

    
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Item_Detector")) {
            isCollide = true;
            tag="Item";
        }
        
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.CompareTag("Item_Detector")){
            isCollide = true;
            tag="Item";
        } 
    }

    void OnTriggerExit2D(Collider2D collider)
    {   
        if(collider.CompareTag("Item_Detector")){
            isCollide = false;
            tag="Item";
        } 
        
    }
    public void ChangeSprite()
    {

        Debug.Log("Sprite değiştirmeye girdi ve sprite değiştir");//OYUNCU EŞYAYI TESLİM ETTİKTEN SONRA HUBDAN INTERACT YAPTIĞI ZAMAN SPRITE DEGİSECEK
        if (spriteArray.Length == 0 || spriteRenderer == null)
        {
            Debug.LogWarning("Sprite array veya SpriteRenderer eksik!");
            return;
        }
        currentIndex++;

        if (currentIndex >= spriteArray.Length)
            currentIndex = 0;

        spriteRenderer.sprite = spriteArray[currentIndex];
    }
}
