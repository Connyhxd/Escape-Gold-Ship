using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    public void Update()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Intro");
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
