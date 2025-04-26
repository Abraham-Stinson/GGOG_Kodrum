using UnityEngine;

public class itemHubScript : MonoBehaviour
{
    public bool isHubCollide;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Item_Detector")) isHubCollide = true;
        
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.CompareTag("Item_Detector")) isHubCollide = true;
    }

    void OnTriggerExit2D(Collider2D collider)
    {   
        if(collider.CompareTag("Item_Detector")) isHubCollide = false;
        
    }
}
