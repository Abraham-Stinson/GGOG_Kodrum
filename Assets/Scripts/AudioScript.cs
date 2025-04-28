using UnityEngine;

public class AudioScript : MonoBehaviour
{
    [SerializeField] AudioClip[] sfxClips;
    [SerializeField] private AudioSource audioSource;
    void Start()
    {
        
    }

    
    void Update()
    {
               
    }

    public void PlaySFX(int eventType){
            audioSource.PlayOneShot(sfxClips[eventType]);
    }
}
