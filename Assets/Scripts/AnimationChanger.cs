using UnityEngine;

public class AnimationChanger : MonoBehaviour
{
    public void AnimationChange(){
        GetComponent<Animator>().SetTrigger("SwitchOn");
    }
}
