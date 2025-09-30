using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    public GameObject optionsMenu;
    private bool isMenuOpen = false;

    public Slider mouseSlider;
    public PlayerMovement playerMove;


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }

    }

    private void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen;
        optionsMenu.SetActive(isMenuOpen);

        if(isMenuOpen)
        {
            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Time.timeScale = 1f;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void UpdateSensitivity(float newSens)
    {
        playerMove.mouseSensivity = newSens;
    }

}
