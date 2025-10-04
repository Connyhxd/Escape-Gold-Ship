using System.Collections.Generic;
using TMPro;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class Door : MonoBehaviour
{
    public string keyName = "Key";
    public Inventory playerInventory;
    private bool doorOpened = false;

    public Animator doorAnim;

    public TMP_Text doorMessage;
    private float messageTimer = 0f;

    [SerializeField] private bool playerNearby = false;

    public NavMeshSurface navSurface;

    public GoldShipAI enemyAI;
    public Transform[] newPatrolPoints;

    private void Update()
    {
        if(messageTimer > 0f)
        {
            messageTimer -= Time.deltaTime;
            if(messageTimer <= 0f)
                doorMessage.gameObject.SetActive(false);
        }

        if (playerNearby && !doorOpened && Input.GetKeyDown(KeyCode.E))
        {
            OpenDoor();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hands"))
        {
            playerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hands"))
        {
            playerNearby = false;
        }
    }

    public void OpenDoor()
    {
        for (int i = 0; i < playerInventory.items.Length; i++)
        {
            if (playerInventory.items[i] != null && playerInventory.items[i].itemName == keyName)
            {
                if (playerInventory.selectedItem == i)
                {
                    doorOpened = true;
                    doorAnim.SetTrigger("Opening");

                    playerInventory.items[i] = null;
                    playerInventory.UIUpdate();

                    navSurface.BuildNavMesh();

                    AddNewPatrolPoints();


                    return;
                }
                else
                {
                    ShowDoorMessage();
                    return;
                }
            }
        }
    }

    public void AddNewPatrolPoints()
    {
        if (enemyAI.patrolPoints == null)
            enemyAI.patrolPoints = new List<Transform>();

        enemyAI.patrolPoints.AddRange(newPatrolPoints);
    }

    public void ShowDoorMessage()
    {
        doorMessage.gameObject.SetActive(true);
        messageTimer = 2f;
    }
}
