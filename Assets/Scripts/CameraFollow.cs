using UnityEngine;

public class CameraFollow : MonoBehaviour
{
<<<<<<< HEAD
    public Transform target;
    public Vector3 offset = new Vector3(0, 5, -8);
    public float followSpeed = 5f;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

        transform.LookAt(target);
=======
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
>>>>>>> c6d9fa11fef5b14289e00f641f396c0cc54aa91b
    }
}