using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IceCube : MonoBehaviour
{
    public GameObject interactUI;
    public TextMeshProUGUI dialogueText;
    public GameObject dialogueObj;

    public GameObject enemy;
    public GameObject keyItem;

    public string[] dialogues;

    public float timeBetweenDialogues = 5f;

    public float meltDuration = 300f;
    private bool survivalActive = false;
    [SerializeField] private float survivalTimer = 0f;

    private bool playerNearby = false;
    private bool eventStarted = false;

    private void Update()
    {

        if (playerNearby && !eventStarted && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(StartEvent());
        }

        if (survivalActive)
        {
            survivalTimer += Time.deltaTime;
            if (survivalTimer >= meltDuration)
            {
                survivalActive = false;
                MeltCube();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hands") && !eventStarted)
        {
            interactUI.SetActive(true);
            playerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hands"))
        {
            interactUI.SetActive(false);
            playerNearby = false;
        }
    }

    private IEnumerator StartEvent()
    {
        eventStarted = true;
        interactUI.SetActive(false);
        dialogueObj.SetActive(true);

        for (int i = 0; i < dialogues.Length; i++)
        {
            dialogueText.text = dialogues[i];
            yield return new WaitForSeconds(timeBetweenDialogues);
        }

        // Al terminar los diálogos, aparece el enemigo
        dialogueObj.SetActive(false);
        survivalActive = true;
        survivalTimer = 0f;
    }

    private void MeltCube()
    {
        keyItem.SetActive(true);
        Destroy(gameObject);
    }
}
