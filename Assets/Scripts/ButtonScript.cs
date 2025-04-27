using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    [SerializeField] private GameObject creditsScreen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    public void LoadGame(){
        SceneManager.LoadScene("Game");//Oyun
    }
    public void Credits(){
        //Oyun
        creditsScreen.SetActive(true);
    }
    public void CloseCreditsScreen(){
        creditsScreen.SetActive(false);
    }

    public void ExitGame(){
        Application.Quit();
    }
}
