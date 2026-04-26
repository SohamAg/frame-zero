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
    private Vector2 smoothedMoveInput;
    [SerializeField] private float animationSmoothTime = 12f;
    [SerializeField] private float acceleration = 60f;
    [SerializeField] private float deceleration = 100f;

    [Header("Movement")]
    public float moveSpeed = 6f;
    public float jumpForce = 7f;
    public float rotationSpeed = 10f;
    public int maxJumps = 2;

    [Header("Jump Feel")]
    public float fallMultiplier = 3f;
    public float lowJumpMultiplier = 2f;

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

    private void OnEnable() => inputActions.Player.Enable();
    private void OnDisable() => inputActions.Player.Disable();

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main.transform;
        audioSource = GetComponent<AudioSource>();

        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }

        jumpsRemaining = maxJumps;
    }

    private void FixedUpdate()
    {
        HandleMovement();
        HandleJump();
        ApplyBetterGravity();
    }

    // =========================
    // MOVEMENT (FIXED)
    // =========================
    private void HandleMovement()
    {
        Vector3 forward = cam.forward;
        Vector3 right = cam.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = forward * moveInput.y + right * moveInput.x;

        if (moveDirection.magnitude > 1f)
            moveDirection.Normalize();

        Vector3 targetVelocity = moveDirection * moveSpeed;

        rb.linearVelocity = new Vector3(
            targetVelocity.x,
            rb.linearVelocity.y,
            targetVelocity.z
        );

        // Rotate only when moving
        if (moveDirection.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.fixedDeltaTime
            );
        }

        UpdateMovementAnimation();
    }

    private void UpdateMovementAnimation()
    {
        if (animator == null) return;

        smoothedMoveInput = Vector2.Lerp(
        smoothedMoveInput,
        moveInput.normalized,
        animationSmoothTime * Time.deltaTime
        );

        animator.SetFloat(MoveXHash, smoothedMoveInput.x, 0.12f, Time.deltaTime);
        animator.SetFloat(MoveYHash, smoothedMoveInput.y, 0.12f, Time.deltaTime);
        animator.SetBool(IsGroundedHash, isGrounded);
    }

    // =========================
    // JUMP (FIXED)
    // =========================
    private void HandleJump()
    {
        if (jumpPressed && jumpsRemaining > 0)
        {
            jumpsRemaining--;
            isGrounded = false;

            animator?.SetTrigger(JumpHash);

            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            if (audioSource && jumpClip)
                audioSource.PlayOneShot(jumpClip, jumpVolume);
        }

        jumpPressed = false;
    }

    // =========================
    // BETTER GRAVITY (FIX FLOATY JUMP)
    // =========================
    private void ApplyBetterGravity()
    {
        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
        else if (rb.linearVelocity.y > 0 && !jumpPressed)
        {
            rb.linearVelocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }
    }

    // =========================
    // GROUND CHECK (FIX TRIPLE JUMP)
    // =========================
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (rb.linearVelocity.y <= 0.1f)
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

    // =========================
    // INPUTS
    // =========================
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
            jumpPressed = true;
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
            animator?.SetTrigger(AttackHash);
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        bool isBlocking = context.ReadValueAsButton();

        animator?.SetBool(IsBlockingHash, isBlocking);

        // equipment swap
        swordHand.SetActive(!isBlocking);
        swordBack.SetActive(isBlocking);

        shieldHand.SetActive(isBlocking);
        shieldBack.SetActive(!isBlocking);
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
            animator?.SetTrigger(CastSpellHash);
    }

    public void OnLook(InputAction.CallbackContext context) { }
    public void OnPrevious(InputAction.CallbackContext context) { }
    public void OnNext(InputAction.CallbackContext context) { }
    public void OnSprint(InputAction.CallbackContext context) { }

    // =========================
    // GAME LOGIC
    // =========================
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Lava"))
        {
            transform.position = new Vector3(95.1f, 2.5f, 164.9f);
            rb.linearVelocity = Vector3.zero;
        }

        if (other.CompareTag("Winning"))
        {
            rb.linearVelocity = Vector3.zero;
            rb.isKinematic = true;
            enabled = false;
            winText?.SetActive(true);
        }
    }
}