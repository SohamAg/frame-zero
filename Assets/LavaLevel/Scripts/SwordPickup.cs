using UnityEngine;
using UnityEngine.InputSystem;

public class SwordPickup : MonoBehaviour
{
    [SerializeField] private LavaLevelManager levelManager;
    [SerializeField] private GameObject pickupPrompt;

    private bool playerNearby;

    private void Start()
    {
        if (pickupPrompt != null)
            pickupPrompt.SetActive(false);
    }

    private void Update()
    {
        if (!playerNearby) return;

        if (Keyboard.current != null && Keyboard.current.qKey.wasPressedThisFrame)
        {
            Debug.Log("Pickup key pressed");

            levelManager?.PickupSword();

            if (pickupPrompt != null)
                pickupPrompt.SetActive(false);

            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerNearby = true;

        if (pickupPrompt != null)
            pickupPrompt.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerNearby = false;

        if (pickupPrompt != null)
            pickupPrompt.SetActive(false);
    }
}