using UnityEngine;
using TMPro;

public class SmallTut : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject textObj;
    public GameObject event1;

    public float timer;

    public string fmessage;
    public string smessage;

    public float firstDuration = 2f;
    public float secondDuration = 2f;

    private int stage = 0;

    private void Update()
    {
        if (stage > 0)
        {
            timer += Time.deltaTime;

            if (stage == 1 && timer >= firstDuration)
            {
               
                text.text = smessage;
                timer = 0f;
                stage = 2;
            }
            else if (stage == 2 && timer >= secondDuration)
            {
                
                textObj.SetActive(false);
                stage = 0;
                event1.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && stage == 0)
        {
            textObj.SetActive(true);
            text.text = fmessage;
            timer = 0f;
            stage = 1;
        }
    }
}
