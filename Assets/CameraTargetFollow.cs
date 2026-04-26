using UnityEngine;

public class CameraTargetFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset = new Vector3(0f, 1.2f, 0f);

    private void LateUpdate()
    {
        if (player == null) return;

        transform.position = player.position + offset;
        transform.rotation = player.rotation;
    }
}