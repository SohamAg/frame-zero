using UnityEngine;

public class AbilityPickup : MonoBehaviour
{
    public AbilityType abilityType;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        PlayerInventory inventory = other.GetComponent<PlayerInventory>();

        if (inventory != null)
        {
            if (abilityType == AbilityType.Sword)
            {
                inventory.GetSword();
            }
            else if (abilityType == AbilityType.Shield)
            {
                inventory.GetShield();
            }
            else if (abilityType == AbilityType.Spell)
            {
                inventory.GetSpell();
            }

            Destroy(gameObject);
        }
    }
}