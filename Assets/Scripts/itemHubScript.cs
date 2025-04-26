using UnityEngine;

public class itemHubScript : MonoBehaviour
{
    public bool isHubCollide;

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
