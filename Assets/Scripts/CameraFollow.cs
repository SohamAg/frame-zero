using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Drag your Player Capsule here
    public Vector3 offset;

    void Start()
    {
        offset = new Vector3(0, 16, -24);
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // Keeps the camera at a fixed distance from the player
            transform.position = target.position + offset;
        }
    }
}