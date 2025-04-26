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
        playerInput1.SwitchCurrentControlScheme(tempScheme2);
        playerInput2.SwitchCurrentControlScheme(tempScheme1);
        player1Controller.enabled=false;
        player2Controller.enabled=false;

        Animator animator=player1Controller.GetComponent<Animator>();
        if(player1StartingDevice==playerInput1.devices[0]){
            animator.Play("Hoodie_Off");
            
        } 
        else{
            animator.Play("Hoodie_On");
        } 
        StartCoroutine(SwitchBreak(animator));
        
    }

    IEnumerator SwitchBreak(Animator animator){
        yield return new WaitForSeconds(waitingSeconds); 
        animator.ResetTrigger("SwitchOn");
        player1Controller.enabled=true;
        player2Controller.enabled=true;
    }
}
