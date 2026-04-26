using UnityEngine;
using System.Collections;

public class LavaLevelManager : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private LevelCrystal crystal;
    [SerializeField] private GameObject findSwordText;

    private bool hasSword = false;
    private Coroutine hideRoutine;

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
            player.SetEquipment(EquipmentPlacement.Back, EquipmentPlacement.None);

        if (findSwordText != null)
            findSwordText.SetActive(false);

        if (crystal != null)
            crystal.ActivateCrystal();
    }

    public void OnCrystalTouched()
    {
        if (!hasSword)
        {
            Debug.Log("Find the sword first");

            if (findSwordText != null)
            {
                findSwordText.SetActive(true);

                if (hideRoutine != null)
                    StopCoroutine(hideRoutine);

                hideRoutine = StartCoroutine(HideText());
            }

            return;
        }

        if (crystal != null)
            crystal.CollectCrystal();
    }

    private IEnumerator HideText()
    {
        yield return new WaitForSeconds(2f);

        if (findSwordText != null)
            findSwordText.SetActive(false);
    }
}