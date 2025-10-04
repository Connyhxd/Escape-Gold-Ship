using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    
    public void PlayGame()
    {
        SceneManager.LoadScene("Main");
    }

    
    public void QuitGame()
    {
        Application.Quit();
    }
}
