using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour, InputSystem_Actions.IPlayerActions
{
    [Header("Movement")]
    public float moveSpeed = 6f;
    public float jumpForce = 7f;
    public float rotationSpeed = 10f;
    public int maxJumps = 2;

    [Header("Audio")]
    public AudioClip jumpClip;
    [Range(0f, 1f)] public float jumpVolume = 1f;

    private Rigidbody rb;
    private Transform cam;
    private Animator animator;
    private AudioSource audioSource;
    private InputSystem_Actions inputActions;

    private Vector2 moveInput;
    private bool jumpPressed;
    private bool isGrounded;
    private int jumpsRemaining;

    public GameObject winText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Lava"))
        {
            Debug.Log("Player touched lava!");

            transform.position = new Vector3(95.1f, 2.5f, 164.9f);
            rb.linearVelocity = Vector3.zero;
        }

        if (other.CompareTag("Winning"))
        {
            WinGame();
        }
    }

    void WinGame()
    {
        rb.linearVelocity = Vector3.zero;
        rb.isKinematic = true;
        enabled = false;
        Debug.Log("LMAO");
        winText.SetActive(true);
    }

    void Awake()
    {
        Debug.Log("Awake ran");
        inputActions = new InputSystem_Actions();
        inputActions.Player.SetCallbacks(this);
    }

    void OnEnable()
    {
        Debug.Log("OnEnable ran");
        inputActions.Player.Enable();
    }

    void OnDisable()
    {
        inputActions.Player.Disable();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main.transform;
        animator = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();

        jumpsRemaining = maxJumps;
    }

    void FixedUpdate()
    {
        HandleMovement();
        HandleJump();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        Debug.Log("OnMove fired: " + moveInput + " | phase: " + context.phase);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log("OnJump fired: " + context.phase);

        if (context.performed)
            jumpPressed = true;
    }

    void HandleMovement()
    {
        Vector3 forward = cam.forward;
        Vector3 right = cam.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = (forward * moveInput.y + right * moveInput.x).normalized;

        if (moveDirection.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.fixedDeltaTime
            );
        }

        Vector3 velocity = moveDirection * moveSpeed;
        velocity.y = rb.linearVelocity.y;
        rb.linearVelocity = velocity;

        if (animator != null)
        {
            animator.SetFloat("Speed", moveDirection.magnitude);
            animator.SetBool("IsGrounded", isGrounded);
        }
    }

    void HandleJump()
    {
        if (jumpPressed && jumpsRemaining > 0)
        {
            jumpsRemaining--;
            isGrounded = false;

            if (animator != null)
            {
                animator.ResetTrigger("Jump");
                animator.SetTrigger("Jump");
                animator.SetBool("IsGrounded", false);

                // Restart the Jump animation immediately
                animator.Play("Jump", 0, 0f);
            }

            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            if (audioSource != null && jumpClip != null)
            {
                audioSource.PlayOneShot(jumpClip, jumpVolume);
            }
        }

        jumpPressed = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (!isGrounded)
            {
                isGrounded = true;
                jumpsRemaining = maxJumps;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    public void OnLook(InputAction.CallbackContext context) { }
    public void OnAttack(InputAction.CallbackContext context) { }
    public void OnInteract(InputAction.CallbackContext context) { }
    public void OnCrouch(InputAction.CallbackContext context) { }
    public void OnPrevious(InputAction.CallbackContext context) { }
    public void OnNext(InputAction.CallbackContext context) { }
    public void OnSprint(InputAction.CallbackContext context) { }
}