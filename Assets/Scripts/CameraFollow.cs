using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public Transform desiredPose;
    public Transform target;

    private void Start()
    {
        Vector3 relativePosition = new Vector3(0, 5, -7);
    }

    void LateUpdate()
    {

        if (desiredPose != null)
        {
            transform.position = desiredPose.position;
            transform.rotation = desiredPose.rotation;

        }
    }
}