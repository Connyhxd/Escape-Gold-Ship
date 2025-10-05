using UnityEngine;

public class openKeypad : MonoBehaviour
{
    public GameObject keypad;
    public GameObject openText;

    private bool playerNearby = false;

    private void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            keypad.SetActive(true);
            openText.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hands"))
        {
            playerNearby = true;
            openText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hands"))
        {
            playerNearby = false;
            openText.SetActive(false);
        }
    }
}
