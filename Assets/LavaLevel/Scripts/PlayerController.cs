using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour, InputSystem_Actions.IPlayerActions
{
    [Header("Equipment")]
    [SerializeField] private GameObject swordHand;
    [SerializeField] private GameObject swordBack;
    [SerializeField] private GameObject shieldHand;
    [SerializeField] private GameObject shieldBack;

    [Header("Movement")]
    public float moveSpeed = 6f;
    public float jumpForce = 7f;
    public float rotationSpeed = 10f;
    public int maxJumps = 2;

    [Header("Audio")]
    public AudioClip jumpClip;
    [Range(0f, 1f)] public float jumpVolume = 1f;

    [Header("UI")]
    public GameObject winText;

    private Rigidbody rb;
    private Transform cam;
    [SerializeField] private Animator animator;
    private AudioSource audioSource;
    private InputSystem_Actions inputActions;

    private Vector2 moveInput;
    private bool jumpPressed;
    private bool isGrounded;
    private bool isBlocking;
    private int jumpsRemaining;

    private static readonly int MoveXHash = Animator.StringToHash("MoveX");
    private static readonly int MoveYHash = Animator.StringToHash("MoveY");
    private static readonly int IsGroundedHash = Animator.StringToHash("IsGrounded");
    private static readonly int IsBlockingHash = Animator.StringToHash("IsBlocking");
    private static readonly int JumpHash = Animator.StringToHash("Jump");
    private static readonly int AttackHash = Animator.StringToHash("Attack");
    private static readonly int CastSpellHash = Animator.StringToHash("CastSpell");

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Player.SetCallbacks(this);
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
    }

    private void Start()
    {

        rb = GetComponent<Rigidbody>();
        cam = Camera.main.transform;
        animator = GetComponentInChildren<Animator>(); if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
        animator.Play("WalkForward", 0, 0f);
        Debug.Log("Force playing WalkForward");
        Debug.Log("Animator being used: " + animator.gameObject.name);
        audioSource = GetComponent<AudioSource>();

        jumpsRemaining = maxJumps;
    }

    private void FixedUpdate()
    {
        HandleMovement();
        HandleJump();
    }

    private void HandleMovement()
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

        UpdateMovementAnimation();
    }

    private void UpdateMovementAnimation()
    {
        if (animator == null) return;

        Vector2 normalizedInput = moveInput.normalized;

        animator.SetFloat(MoveXHash, normalizedInput.x, 0.1f, Time.deltaTime);
        animator.SetFloat(MoveYHash, normalizedInput.y, 0.1f, Time.deltaTime);
        animator.SetBool(IsGroundedHash, isGrounded);
    }

    private void HandleJump()
    {
        if (jumpPressed && jumpsRemaining > 0)
        {
            jumpsRemaining--;
            isGrounded = false;

            if (animator != null)
            {
                animator.ResetTrigger(JumpHash);
                animator.SetTrigger(JumpHash);
                animator.SetBool(IsGroundedHash, false);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Lava"))
        {
            transform.position = new Vector3(95.1f, 2.5f, 164.9f);
            rb.linearVelocity = Vector3.zero;
        }

        if (other.CompareTag("Winning"))
        {
            WinGame();
        }
    }

    private void WinGame()
    {
        rb.linearVelocity = Vector3.zero;
        rb.isKinematic = true;
        enabled = false;

        if (winText != null)
        {
            winText.SetActive(true);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (!isGrounded)
            {
                isGrounded = true;
                jumpsRemaining = maxJumps;

                if (animator != null)
                {
                    animator.SetBool(IsGroundedHash, true);
                }
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;

            if (animator != null)
            {
                animator.SetBool(IsGroundedHash, false);
            }
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            jumpPressed = true;
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed && animator != null)
        {
            animator.SetTrigger(AttackHash);
        }
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        bool isBlocking = context.ReadValueAsButton();

        if (animator != null)
        {
            animator.SetBool(IsBlockingHash, isBlocking);
        }

        // Sword swap
        swordHand.SetActive(!isBlocking);
        swordBack.SetActive(isBlocking);

        // Shield swap
        shieldHand.SetActive(isBlocking);
        shieldBack.SetActive(!isBlocking);
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        // Using Interact input as Cast Spell for now.
        if (context.performed && animator != null)
        {
            animator.SetTrigger(CastSpellHash);
        }
    }

    public void OnLook(InputAction.CallbackContext context) { }

    public void OnPrevious(InputAction.CallbackContext context) { }

    public void OnNext(InputAction.CallbackContext context) { }

    public void OnSprint(InputAction.CallbackContext context) { }
}