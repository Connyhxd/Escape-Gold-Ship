using TMPro;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public ItemTemplate[] items;
    public TMP_Text[] buttonsText = new TMP_Text[3];
    public Image[] itemSprite = new Image[3];

    public GameObject pickUp;
    private Item nearbyItem = null;

    public int selectedItem = -1;

    public TMP_Text fullInv;
    private float messageTimer = 0f;

    private void Awake()
    {
        items = new ItemTemplate[3];
        UIUpdate();
    }
    private void Update()
    {

        if (messageTimer > 0f)
        {
            messageTimer -= Time.deltaTime;
            if (messageTimer <= 0f)
            {
                fullInv.gameObject.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && nearbyItem != null)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] == null)
                {
                    items[i] = nearbyItem.itemsTemplate;
                    Destroy(nearbyItem.gameObject);
                    nearbyItem = null;
                    pickUp.SetActive(false);
                    UIUpdate();
                    return;
                }
                
               
            }
            ShowFullInvMessage();

        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) SelectItem(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SelectItem(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SelectItem(2);

    }

    public void AddItem(Item itemToAdd)
    {
        for(int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                items[i] = itemToAdd.itemsTemplate;
                UIUpdate();
                break;
            }
        }
    }

       private void SelectItem(int i)
       {
          selectedItem = i;
          HighlightSelectedSlot();
      }

      private void HighlightSelectedSlot()
      {
        for (int i = 0; i < itemSprite.Length; i++)
        {

            if(i == selectedItem)
            {
                itemSprite[i].color = Color.white;
                buttonsText[i].color = Color.white;
            }
            else
            {
                buttonsText[i].color = Color.black;
            }

        }
     }

    public void ShowFullInvMessage()
    {
        fullInv.gameObject.SetActive(true);
        messageTimer = 2f;
    }

    private void OnTriggerEnter(Collider other)
    {
        Item item = other.GetComponent<Item>();
        if (item != null)
        {
            nearbyItem = item;
            pickUp.SetActive(true);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        Item item = other.GetComponent<Item>();
        if(item != null && item == nearbyItem)
        {
            nearbyItem = null;
            pickUp.SetActive(false);
        }
    }



    public void UIUpdate()
    {
        for(int i = 0; i < buttonsText.Length; i++)
        {
            if(items[i] != null)
            {
                buttonsText[i].text = items[i].itemName;
                itemSprite[i].sprite = items[i].itemSprite;
                itemSprite[i].enabled = true;
            }
            else
            {
                buttonsText[i].text = "";
                itemSprite[i].sprite = null;
                itemSprite[i].enabled = false;
            }
        }
    }
}
