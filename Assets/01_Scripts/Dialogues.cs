using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Dialogues : MonoBehaviour
{
    public TextMeshProUGUI text;
    public string[] lines;
    public float textSpeed;

    private int index;

    public string currentScene;
    private void Start()
    {
        text.text = string.Empty;
        StartDialogue();
    }

    void Update()
    {

        Cursor.lockState = CursorLockMode.Locked;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (text.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                text.text = lines[index];
            }
        }
    }

    public void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach(char c in lines[index].ToCharArray())
        {
            text.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
    void NextLine()
    {
        if(index < lines.Length - 1)
        {
            index++;
            text.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else if(currentScene == "Intro")
        {
            SceneManager.LoadScene("Main");
        }
        else if (currentScene == "BadEnd")
        {
            SceneManager.LoadScene("Menu_de_inicio");
        }
        else if (currentScene == "GoodEnd")
        {
            SceneManager.LoadScene("Menu_de_inicio");
        }
    }
}
