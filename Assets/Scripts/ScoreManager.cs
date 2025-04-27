using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private int p1Score = 0;
    private int p2Score = 0;
    [SerializeField] private TextMeshProUGUI p1ScoreText;
    [SerializeField] private TextMeshProUGUI p2ScoreText;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private float endScreenLoadTime;
    [SerializeField] private PlayerControllerScript playerControllerScript;
    void Start()
    {
        RefreshScoreUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddScore(int val)
    {
        if (val == 1) p1Score++;
        else if (val == 2) p2Score++;

        RefreshScoreUI();
        WinCondition();
    }
    void RefreshScoreUI()
    {
        p1ScoreText.text = "Red Score: " + p1Score.ToString();
        p2ScoreText.text = "Blue Score: " + p2Score.ToString();
    }

    void WinCondition()
    {
        
        if (p1Score >= 5)
        {
            resultText.text = "Red Player Wins!";
            StartCoroutine(WaitAndLoad());
            playerControllerScript.enabled = false;
            winScreen.SetActive(true);
        }
        else if (p2Score >= 5)
        {
            resultText.text = "Blue Player Wins!";
            StartCoroutine(WaitAndLoad());
            playerControllerScript.enabled = false;
            winScreen.SetActive(true);
        }

    }
    IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(endScreenLoadTime);
        SceneManager.LoadScene("MainMenu");
    }

}
