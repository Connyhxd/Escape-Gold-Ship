using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Keypad : MonoBehaviour
{
    public GameObject keypad;
    
    public GameObject locker;

    public TMP_Text text;
    public string answer = "564";

    public Animator doorAgain;

    private float wrongEndTime = -1f;

    public bool rightAnswer = false;

    public openKeypad kp;

    public GameObject falseKey;
    public GameObject realKey;

    public bool doorOpened = false;

    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }


    private void Update()
    {

        if(wrongEndTime > 0 && Time.unscaledTime >= wrongEndTime)
        {
            text.text = "";
            text.color = Color.white;
            wrongEndTime = -1f;
        }

        if (rightAnswer)
        {
            audioManager.PlaySFX(audioManager.right);
            audioManager.PlaySFX(audioManager.openDoor);

            doorAgain.SetTrigger("Opening");
            locker.SetActive(false);
            kp.openText.SetActive(false);
            doorOpened = true;

            Time.timeScale = 1f;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            falseKey.SetActive(false);
            realKey.SetActive(true);
        }

        if(keypad.activeInHierarchy)
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

    public void number(int number)
    {
        if (text.text.Length >= 3)
        {
            return;
        }
        audioManager.PlaySFX(audioManager.click);
        text.text += number.ToString();
    }

    public void Execute()
    {
        if(text.text == answer)
        {
            rightAnswer = true;
        }
        else
        {
            audioManager.PlaySFX(audioManager.wrong);
            text.color = Color.red;
            wrongEndTime = Time.unscaledTime + 2f;
        }
    }

    public void Exit()
    {
        keypad.SetActive(false);
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}

//Yo despues de autocopiarme la mitad del código en puras puertas el futuro es oi oiste biejo
