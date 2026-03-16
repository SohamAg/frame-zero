using UnityEngine;

public class SwordPickup : MonoBehaviour
{
    public void Interact(GameObject player)
    {
        PlayerInventory inventory = player.GetComponent<PlayerInventory>();

        if (inventory != null)
        {
            inventory.GetSword();
            Destroy(gameObject);
        }
    }
}