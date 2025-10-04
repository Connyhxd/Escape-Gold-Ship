using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Este método carga la escena del juego
    // Cambia "GameScene" por el nombre exacto de tu escena de juego
    public void PlayGame()
    {
        SceneManager.LoadScene("Main");
    }

    // Este método cierra el juego
    public void QuitGame()
    {
        Application.Quit();
    }
}
