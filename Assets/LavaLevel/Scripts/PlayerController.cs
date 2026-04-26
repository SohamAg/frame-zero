using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;

public class PlayerController : MonoBehaviour, InputSystem_Actions.IPlayerActions
{
    [Header("Equipment")]
    [SerializeField] private GameObject swordHand;
    [SerializeField] private GameObject swordBack;
    [SerializeField] private GameObject shieldHand;
    [SerializeField] private GameObject shieldBack;


    [Header("Movement Multipliers")]
    [SerializeField] private float backwardSpeedMultiplier = 0.6f;
    [SerializeField] private float airMoveMultiplier = 1.25f;

    private Vector2 smoothedMoveInput;
    [SerializeField] private float animationSmoothTime = 12f;

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
    private static readonly int PickupHash = Animator.StringToHash("Pickup");

    private Coroutine equipmentRoutine;

    private IEnumerator ReturnEquipmentAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SetEquipmentNormal();
    }
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
        SetEquipmentNormal();
    }

    private void FixedUpdate()
    {
        HandleMovement();
        HandleJump();
        ApplyBetterGravity();
    }

    private void HandleMovement()
    {
        float turnInput = moveInput.x;
        float forwardInput = moveInput.y;

        if (Mathf.Abs(turnInput) > 0.01f)
        {
            transform.Rotate(
                Vector3.up,
                turnInput * rotationSpeed * 10f * Time.fixedDeltaTime
            );
        }

        float speedMultiplier = 1f;

        if (forwardInput < 0f)
        {
            speedMultiplier = backwardSpeedMultiplier;
        }

        float currentMoveSpeed = isGrounded ? moveSpeed : moveSpeed * airMoveMultiplier;

        Vector3 moveDirection = transform.forward * forwardInput;
        Vector3 targetVelocity = moveDirection * currentMoveSpeed * speedMultiplier;

        rb.linearVelocity = new Vector3(
            targetVelocity.x,
            rb.linearVelocity.y,
            targetVelocity.z
        );

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

        animator.SetFloat(MoveXHash, 0f, 0.12f, Time.deltaTime);
        animator.SetFloat(MoveYHash, smoothedMoveInput.y, 0.12f, Time.deltaTime);
        animator.SetBool(IsGroundedHash, isGrounded);
    }

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
            {
                audioSource.PlayOneShot(jumpClip, jumpVolume);
            }
        }

        jumpPressed = false;
    }

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
        if (context.performed)
        {
            animator?.SetTrigger(AttackHash);
        }
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        bool isBlocking = context.ReadValueAsButton();

        animator?.SetBool(IsBlockingHash, isBlocking);

        if (isBlocking)
        {
            SetEquipmentBlock();
        }
        else
        {
            SetEquipmentNormal();
        }
    }

    // Q = Pickup
    public void OnPickup(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        SetEquipmentOnBack();
        animator?.SetTrigger(PickupHash);

        // cancel previous if spammed
        if (equipmentRoutine != null) StopCoroutine(equipmentRoutine);
        equipmentRoutine = StartCoroutine(ReturnEquipmentAfterDelay(2.4f));

        // Add actual pickup detection/logic here later.
    }

    // F = Spell
    // This works after you add CastSpell action in the Input Actions asset
    // and regenerate the C# class.
    public void OnCast(InputAction.CallbackContext context)
    {
        if(!context.performed) return;

        SetEquipmentOnBack();
        animator?.SetTrigger(CastSpellHash);

        if (equipmentRoutine != null) StopCoroutine(equipmentRoutine);
        equipmentRoutine = StartCoroutine(ReturnEquipmentAfterDelay(2.0f));

        // Add projectile/spell spawning logic here later.
    }

    public void OnInteract(InputAction.CallbackContext context)
    {

        if (context.performed)
            animator?.SetTrigger(CastSpellHash);
    }

    public void SetEquipmentNormal()
    {
        if (swordHand != null) swordHand.SetActive(true);
        if (swordBack != null) swordBack.SetActive(false);

        if (shieldHand != null) shieldHand.SetActive(false);
        if (shieldBack != null) shieldBack.SetActive(true);
    }

    public void SetEquipmentBlock()
    {
        if (swordHand != null) swordHand.SetActive(false);
        if (swordBack != null) swordBack.SetActive(true);

        if (shieldHand != null) shieldHand.SetActive(true);
        if (shieldBack != null) shieldBack.SetActive(false);
    }

    public void SetEquipmentOnBack()
    {
        if (swordHand != null) swordHand.SetActive(false);
        if (swordBack != null) swordBack.SetActive(true);

        if (shieldHand != null) shieldHand.SetActive(false);
        if (shieldBack != null) shieldBack.SetActive(true);
    }

    public void OnLook(InputAction.CallbackContext context) { }
    public void OnPrevious(InputAction.CallbackContext context) { }
    public void OnNext(InputAction.CallbackContext context) { }
    public void OnSprint(InputAction.CallbackContext context) { }

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