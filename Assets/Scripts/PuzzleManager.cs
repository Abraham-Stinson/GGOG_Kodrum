using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class PuzzleManager : MonoBehaviour
{
    public List<GameObject> puzzles = new List<GameObject>();
    
    public GameObject puzzlePlayer;
    private Transform playerCheckpoint;
    private Transform boxCheckpoint;
    private Transform boxPosition;

    private void Start()
    {
        //SpawnRandomPuzzle();
        FindCheckpoint();
    }

    public void FindCheckpoint()
    {
        playerCheckpoint = GameObject.FindWithTag("PlayerCheckpoint").transform;
        boxCheckpoint = GameObject.FindWithTag("BoxCheckpoint").transform;
        boxPosition = GameObject.FindWithTag("Box").transform;
        MovePlayerToCheckpoint();
    }
    private IEnumerator DelayedFindCheckpoint()
    {
        yield return new WaitForEndOfFrame();  // Wait until the next frame
        FindCheckpoint();
        
    }

    public void ResetPuzzle()
    {
        MovePlayerToCheckpoint();
    }


    public void SpawnRandomPuzzle()
    {
        GameObject currentPuzle = GameObject.FindGameObjectWithTag("Puzzle");
        if (currentPuzle != null)
        {
            GameObject.Destroy(currentPuzle);           
        }
        int listIndex = Random.Range(0, puzzles.Count);
        Instantiate(puzzles[listIndex], this.transform.localPosition, Quaternion.identity, this.transform);
        StartCoroutine(DelayedFindCheckpoint());
        
    }


void MovePlayerToCheckpoint()
    {
        puzzlePlayer.transform.position = playerCheckpoint.position;
        boxPosition.position = boxCheckpoint.position;
    }
}
