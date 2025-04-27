using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private int p1Score = 0;
    private int p2Score = 0;
    [SerializeField] private Image[] p1Bars;
    [SerializeField] private Image[] p2Bars;
    public Sprite p1EmptySprite;
    public Sprite p1FullSprite;
    public Sprite p2EmptySprite;
    public Sprite p2FullSprite;
    
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
        for(int i = 0; i<p1Bars.Length;i++){
            if(i<p1Score){
                p1Bars[i].sprite=p1FullSprite;
            }
            else{
                p1Bars[i].sprite=p1EmptySprite;
            }
        }
        for(int i = 0; i<p2Bars.Length;i++){
            if(i<p2Score){
                p2Bars[i].sprite=p2FullSprite;
            }
            else{
                p2Bars[i].sprite=p2EmptySprite;
            }
        }
        
    }

    void WinCondition()
    {
        
        if (p1Score >= 3)
        {
            resultText.text = "Red Player Wins!";
            StartCoroutine(WaitAndLoad());
            playerControllerScript.enabled = false;
            winScreen.SetActive(true);
        }
        else if (p2Score >= 3)
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
