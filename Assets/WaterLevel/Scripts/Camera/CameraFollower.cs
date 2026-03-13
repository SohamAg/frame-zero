using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 2, -5);
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        // Prevent MissingReferenceException
        if (target == null)
            return;

        Vector3 desiredPosition = target.position + target.TransformDirection(offset);

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothSpeed * Time.deltaTime
        );

        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}