
using UnityEngine;

public class AnimationChanger : MonoBehaviour
{
    private PlayerControllerScript playerControllerScript;

    private void Start()
    {
        playerControllerScript = GetComponent<PlayerControllerScript>();
    }

    public void AnimationChange()
    {
        GetComponent<Animator>().SetTrigger("SwitchOn");
    }
}