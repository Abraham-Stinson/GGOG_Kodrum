using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerSwitch : MonoBehaviour
{

    public PlayerControllerScript player1Controller;
    public PuzzlePlayerController player2Controller;
    public InputDevice player1StartingDevice;
    private PlayerInput playerInput1;
    private PlayerInput playerInput2;
    [SerializeField]private float waitingSeconds=0.4f;
    public RuntimeAnimatorController  hoodieOnAnimations;
    public RuntimeAnimatorController  hoodieOffAnimations;
    [SerializeField] private AudioScript audioScript;
    

    private void Start()
    {
        playerInput1 = player1Controller.GetComponent<PlayerInput>();
        playerInput2 = player2Controller.GetComponent<PlayerInput>();
        player1StartingDevice= playerInput1.devices[0];
    }


    public void SwitchControl()
    {
        InputDevice tempScheme1;
        InputDevice tempScheme2;
        tempScheme1 = playerInput1.devices[0];
        tempScheme2 = playerInput2.devices[0];
        player1Controller.hoodieOn=!player1Controller.hoodieOn;
        playerInput1.SwitchCurrentControlScheme(tempScheme2);
        playerInput2.SwitchCurrentControlScheme(tempScheme1);
        player1Controller.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        player2Controller.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        player1Controller.enabled=false;
        player2Controller.enabled=false;

        Animator animator=player1Controller.GetComponent<Animator>();
        if(player1StartingDevice==playerInput1.devices[0]){
            //animator.SetBool("isFallingHoodieOff", false);
            //animator.SetBool("isJumpFallingingHoodieOn", false);
            animator.SetTrigger("Hoodie_Off");
            if (player1Controller.animator.runtimeAnimatorController == hoodieOffAnimations)
            {
                player1Controller.animator.runtimeAnimatorController = hoodieOnAnimations;
            }
            else if (player1Controller.animator.runtimeAnimatorController == hoodieOnAnimations)
            {
                player1Controller.animator.runtimeAnimatorController = hoodieOffAnimations;
            }
            
        } 
        else{
            //animator.SetBool("isFallingHoodieOff", false);
            //animator.SetBool("isFallingHoodieOff", false);
            animator.SetTrigger("Hoodie_On");
            if (player1Controller.animator.runtimeAnimatorController == hoodieOffAnimations)
            {
                player1Controller.animator.runtimeAnimatorController = hoodieOnAnimations;
            }
            else if (player1Controller.animator.runtimeAnimatorController == hoodieOnAnimations)
            {
                player1Controller.animator.runtimeAnimatorController = hoodieOffAnimations;
            }
        } 
        audioScript.PlaySFX(2);
        StartCoroutine(SwitchBreak(animator));
        
    }

    IEnumerator SwitchBreak(Animator animator){
        yield return new WaitForSeconds(waitingSeconds);
        
        animator.ResetTrigger("SwitchOn");
        /*animator.ResetTrigger("Hoodie_On");
        animator.ResetTrigger("Hoodie_Off");*/

        player1Controller.enabled=true;
        player2Controller.enabled=true;
    }
}
