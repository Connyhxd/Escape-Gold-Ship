using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public PlayerMovement player;
    public Inventory playerInventory;

    public GameObject bullet;
    public Transform bulletSpawn;
    public float bulletSpeed;

    void Update()
    {
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
        }
    }
}
