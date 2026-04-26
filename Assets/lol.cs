using UnityEngine;
using Unity.Cinemachine;

public class KeyboardCinemachineOrbit : MonoBehaviour
{
    [SerializeField] private CinemachinePanTilt panTilt;

    [Header("Camera Rotation")]
    [SerializeField] private float yawSpeed = 120f;
    [SerializeField] private float pitchSpeed = 80f;
    [SerializeField] private float minPitch = -25f;
    [SerializeField] private float maxPitch = 55f;

    private void Reset()
    {
        panTilt = GetComponent<CinemachinePanTilt>();
    }

    private void Update()
    {
        if (panTilt == null) return;

        float yawInput = 0f;
        float pitchInput = 0f;

        // Rotate camera left/right
        if (Input.GetKey(KeyCode.Q)) yawInput -= 1f;
        if (Input.GetKey(KeyCode.E)) yawInput += 1f;

        // Optional: tilt camera up/down
        if (Input.GetKey(KeyCode.R)) pitchInput += 1f;
        if (Input.GetKey(KeyCode.F)) pitchInput -= 1f;

        panTilt.PanAxis.Value += yawInput * yawSpeed * Time.deltaTime;
        panTilt.TiltAxis.Value += pitchInput * pitchSpeed * Time.deltaTime;

        panTilt.TiltAxis.Value = Mathf.Clamp(
            panTilt.TiltAxis.Value,
            minPitch,
            maxPitch
        );
    }
}