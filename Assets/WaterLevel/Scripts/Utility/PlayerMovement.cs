using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 6f;
    public float rotationSmoothTime = 0.1f;

    [Header("Gravity Settings")]
    public float gravity = -9.81f;

    [Header("Camera Reference")]
    public Transform cameraTransform;

    private CharacterController controller;
    private Vector3 velocity;
    private float currentAngle;
    private float angleVelocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        HandleMovement();
        ApplyGravity();
    }

    void HandleMovement()
    {
        // Get player input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 inputDir = new Vector3(horizontal, 0, vertical).normalized;

        if (inputDir.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(inputDir.x, inputDir.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            currentAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref angleVelocity, rotationSmoothTime);
            transform.rotation = Quaternion.Euler(0f, currentAngle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }

    void ApplyGravity()
    {
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}