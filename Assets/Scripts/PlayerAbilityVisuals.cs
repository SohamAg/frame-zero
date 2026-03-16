using UnityEngine;

public class PlayerAbilityVisuals : MonoBehaviour
{
    public GameObject swordVisual;
    public GameObject shieldVisual;
    public GameObject spellVisual;

    private PlayerInventory inventory;

    void Start()
    {
        inventory = GetComponent<PlayerInventory>();
        UpdateVisuals();
    }

    void Update()
    {
        UpdateVisuals();
    }

    void UpdateVisuals()
    {
        if (inventory == null) return;

        if (swordVisual != null)
            swordVisual.SetActive(inventory.hasSword);

        if (shieldVisual != null)
            shieldVisual.SetActive(inventory.hasShield);

        if (spellVisual != null)
            spellVisual.SetActive(inventory.hasSpell);
    }
}