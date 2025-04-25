using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerSwitch : MonoBehaviour
{
    public PlayerControllerScript player1Controller;
    public PuzzlePlayerController player2Controller;
    private PlayerInput playerInput1;
    private PlayerInput playerInput2;
    

    private void Start()
    {
        playerInput1 = player1Controller.GetComponent<PlayerInput>();
        playerInput2 = player2Controller.GetComponent<PlayerInput>();

        

        Debug.Log("devices: " + playerInput1.devices);
        


    }


    public void SwitchControl()
    {
        InputDevice tempScheme1;
        InputDevice tempScheme2;
        tempScheme1 = playerInput1.devices[0];
        tempScheme2 = playerInput2.devices[0];
        playerInput1.SwitchCurrentControlScheme(tempScheme2);
        playerInput2.SwitchCurrentControlScheme(tempScheme1);

    }

}
