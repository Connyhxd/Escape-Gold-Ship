using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingEvent : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Cursor.lockState = CursorLockMode.Locked;
            SceneManager.LoadScene("GoodEnd");
        }
    }
}
