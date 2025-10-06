using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemManager : MonoBehaviour
{
    public PlayerMovement player;
    public Inventory playerInventory;

    public GameObject bullet;
    public Transform bulletSpawn;
    public float bulletSpeed;

    public bool iceNearby = false;
    public GameObject noFire;

    private float messageTimer = 2f;

    void Update()
    {
        if (messageTimer > 0f)
        {
            messageTimer -= Time.deltaTime;
            if (messageTimer <= 0f)
            {
                noFire.SetActive(false);
            }
        }
    
        for (int i = 0; i < playerInventory.items.Length; i++)
        {
            if (playerInventory.items[i] != null && playerInventory.items[i].itemName == "Energy Spray" && Input.GetKeyDown(KeyCode.F))
            {
                if (playerInventory.selectedItem == i)
                {
                    playerInventory.items[i] = null;
                    playerInventory.UIUpdate();

                    player.stamina += 2;
                    return;
                }
            }

            if (playerInventory.items[i] != null && playerInventory.items[i].itemName == "Throwable Gem" && Input.GetKeyDown(KeyCode.F))
            {
                if (playerInventory.selectedItem == i)
                {
                    playerInventory.items[i] = null;
                    playerInventory.UIUpdate();

                    Rigidbody bulletRb = Instantiate(bullet, bulletSpawn.position, Quaternion.identity).GetComponent<Rigidbody>();
                    bulletRb.linearVelocity = bulletSpawn.forward * bulletSpeed;
                    Destroy(bulletRb.gameObject, 1f);
                    return;
                }
            }

            if (playerInventory.items[i] != null && playerInventory.items[i].itemName == "Lighter" && Input.GetKeyDown(KeyCode.F))
            {
                if (playerInventory.selectedItem == i)
                {
                    if (iceNearby)
                    {
                        SceneManager.LoadScene("BadEnd");
                    }
                    else
                    {
                        noFire.SetActive(true);
                        messageTimer = 2f;
                    }
                    return;
                }
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("IceCube"))
        {
            iceNearby = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("IceCube"))
        {
            iceNearby = false;
        }
    }
}
