using UnityEngine;
using UnityEngine.InputSystem;

public class ReadNote : MonoBehaviour
{
    private bool playerNearby = false;
    public GameObject noteUI;
    public GameObject pickUp;
    public GameObject button;

    private void Update()
    {

        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            noteUI.SetActive(true);
            button.SetActive(true);
            pickUp.SetActive(false);
            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hands"))
        {
            playerNearby = true;
            pickUp.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hands"))
        {
            playerNearby = false;
            pickUp.SetActive(false);
        }
    }

    public void ExitNote()
    {
        noteUI.SetActive(false);
        button.SetActive(false);
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
