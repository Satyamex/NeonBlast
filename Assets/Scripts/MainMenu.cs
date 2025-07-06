using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void SwitchToGameScene() 
    {
        SceneManager.LoadScene("GameScene");
    }

    public void QuitTheGame() 
    {
        Application.Quit();
    }
}
