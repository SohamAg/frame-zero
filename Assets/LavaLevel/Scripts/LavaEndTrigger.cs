using UnityEngine;

public class LavaEndTrigger : MonoBehaviour
{
    [SerializeField] private LavaLevelManager levelManager;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        levelManager?.OnCrystalTouched();
    }
}