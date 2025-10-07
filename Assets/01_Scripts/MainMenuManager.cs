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
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene("Intro");
    }

    
    public void BackToMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("Menu_de_inicio");
    }

    
    public void QuitGame()
    {
        Application.Quit();
    }
}
