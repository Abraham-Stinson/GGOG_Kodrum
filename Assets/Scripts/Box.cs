using UnityEngine;

public class Box : MonoBehaviour
{
    private PuzzleManager manager;
    [SerializeField]private PlayerControllerSwitch playerControllerSwitch;
    private void Start()
    {
        playerControllerSwitch = GameObject.FindGameObjectWithTag("PlayerSwitch").GetComponent<PlayerControllerSwitch>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PuzzleWin"))
        {
            manager = GetComponentInParent<PuzzleManager>();
            if (manager == null) {
                Debug.Log("null");
            }
            manager.SpawnRandomPuzzle();
            playerControllerSwitch.SwitchControl();
            
        }
    }
}
