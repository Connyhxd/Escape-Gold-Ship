using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Locker : MonoBehaviour
{
    public bool doorOpened = false;

    public Animator lockerAnim;

    private bool playerNearby = false;
    public GameObject open;

    public GameObject idkBro;

    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Update()
    {

        if (playerNearby && !doorOpened && Input.GetKeyDown(KeyCode.E))
        {
            audioManager.PlaySFX(audioManager.locker);
            doorOpened = true;
            lockerAnim.SetTrigger("Opening");
            open.SetActive(false);
            idkBro.SetActive(true);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hands"))
        {
            playerNearby = true;
            if (!doorOpened)
            {
                open.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hands"))
        {
            playerNearby = false;
            open.SetActive(false);
        }
    }

}
