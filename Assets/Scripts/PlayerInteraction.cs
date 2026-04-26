using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject interactionText;

    private InteractableObject currentInteractable;

    void Start()
    {
        if (interactionText != null)
        {
            interactionText.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentInteractable != null)
            {
                currentInteractable.Interact();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        InteractableObject interactable = other.GetComponent<InteractableObject>();

        if (interactable != null)
        {
            currentInteractable = interactable;

            if (interactionText != null)
            {
                interactionText.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        InteractableObject interactable = other.GetComponent<InteractableObject>();

        if (interactable != null && currentInteractable == interactable)
        {
            currentInteractable = null;

            if (interactionText != null)
            {
                interactionText.SetActive(false);
            }
        }
    }
}