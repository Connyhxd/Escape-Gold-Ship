using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    
    public void PlayGame()
    {
        SceneManager.LoadScene("Main");
    }

    
    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu_de_inicio");
    }

    
    public void QuitGame()
    {
        Application.Quit();
    }
}
