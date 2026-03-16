using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public string message = "You interacted with the object!";

    public void Interact()
    {
        Debug.Log(message);
    }
}
