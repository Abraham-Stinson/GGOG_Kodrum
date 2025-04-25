using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerSwitch : MonoBehaviour
{
    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    public void SetGamepad1()
    {
        var gamepad = Gamepad.all.Count > 0 ? Gamepad.all[0] : null;
        if (gamepad != null)
        {
            playerInput.SwitchCurrentControlScheme("Gamepad", gamepad);
        }
    }
    public void SetGamepad2()
    {
        var gamepad = Gamepad.all.Count > 1 ? Gamepad.all[1] : null;
        if (gamepad != null)
        {
            playerInput.SwitchCurrentControlScheme("Gamepad", gamepad);
        }
    }
}
