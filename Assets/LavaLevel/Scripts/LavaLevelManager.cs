using UnityEngine;

public class LavaLevelManager : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private LevelCrystal crystal;
    [SerializeField] private GameObject findSwordText;

    private bool hasSword = false;

    private void Start()
    {
        if (findSwordText != null)
            findSwordText.SetActive(false);
    }

    public void PickupSword()
    {
        if (hasSword) return;

        hasSword = true;
        Debug.Log("Sword picked up");

        if (player != null)
            player.SetEquipmentOnBack();

        if (findSwordText != null)
            findSwordText.SetActive(false);

        if (crystal != null)
            crystal.ActivateCrystal();
    }

    public void TryFinishLevel()
    {
        if (!hasSword)
        {
            Debug.Log("Need sword first");

            if (findSwordText != null)
                findSwordText.SetActive(true);

            return;
        }

        if (crystal != null)
            crystal.ActivateCrystal();
    }

    public bool HasSword()
    {
        return hasSword;
    }
}