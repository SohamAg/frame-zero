using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;

public class PlayerController : MonoBehaviour, InputSystem_Actions.IPlayerActions
{
    [Header("Level Config")]
    [SerializeField] private PlayerLevelConfig levelConfig;

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
    public float rotationSpeed = 6f;
    public int maxJumps = 2;

    [Header("Jump Feel")]
    public float fallMultiplier = 3f;
    public float lowJumpMultiplier = 2f;

    [Header("Audio")]
    public AudioClip jumpClip;
    [Range(0f, 1f)] public float jumpVolume = 1f;

    [SerializeField] private AudioClip attackClip;
    [SerializeField] private AudioClip attackClip1;
    [SerializeField] private AudioClip blockStartClip;
    [SerializeField] private AudioClip blockEndClip;
    [SerializeField] private AudioClip pickupClip;
    [SerializeField] private AudioClip castClip;
    [SerializeField] private AudioClip castClip1;

    [SerializeField, Range(0f, 1f)] private float actionVolume = 1f;
    [SerializeField] private float pickupPitch = 0.5f;

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
    private Coroutine pitchedAudioRoutine;

    private IEnumerator ReturnEquipmentAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SetEquipmentNormal();
    }

    private void PlayActionSound(AudioClip clip, float volume)
    {
        if (audioSource == null || clip == null) return;
        audioSource.PlayOneShot(clip, volume);
    }

    private IEnumerator PlayPitchedClip(AudioClip clip, float pitch, float volume)
    {
        if (audioSource == null || clip == null) yield break;

        float originalPitch = audioSource.pitch;
        float safePitch = Mathf.Max(0.01f, pitch);

        audioSource.pitch = safePitch;
        audioSource.PlayOneShot(clip, volume);

        yield return new WaitForSeconds(clip.length / safePitch);

        audioSource.pitch = originalPitch;
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

        if (Camera.main != null)
        {
            cam = Camera.main.transform;
        }

        audioSource = GetComponent<AudioSource>();

        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }

        jumpsRemaining = maxJumps;
        ApplyDefaultEquipmentFromConfig();
    }

    private void FixedUpdate()
    {
        HandleMovement();
        HandleJump();
        ApplyBetterGravity();
    }

    private void HandleMovement()
    {
        if (levelConfig != null && !levelConfig.canMove)
        {
            rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);
            UpdateMovementAnimation();
            return;
        }

        float turnInput = moveInput.x;
        float forwardInput = moveInput.y;

        if (Mathf.Abs(turnInput) > 0.01f)
        {
            float rotationAmount = turnInput * rotationSpeed * 100f * Time.fixedDeltaTime;
                Quaternion turnRotation = Quaternion.Euler(0f, rotationAmount, 0f);
                rb.MoveRotation(rb.rotation * turnRotation);
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
        if (levelConfig != null && !levelConfig.canJump)
        {
            jumpPressed = false;
            return;
        }

        if (jumpPressed && jumpsRemaining > 0)
        {
            jumpsRemaining--;
            isGrounded = false;

            animator?.SetTrigger(JumpHash);

            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            PlayActionSound(jumpClip, jumpVolume);
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
        if (levelConfig != null && !levelConfig.canJump) return;

        if (context.performed)
        {
            jumpPressed = true;
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (levelConfig != null && !levelConfig.canAttack) return;

        if (context.performed)
        {
            animator?.SetTrigger(AttackHash);

            PlayActionSound(attackClip, actionVolume);
            PlayActionSound(attackClip1, actionVolume);
        }
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (levelConfig != null && !levelConfig.canBlock) return;

        bool isBlocking = context.ReadValueAsButton();

        animator?.SetBool(IsBlockingHash, isBlocking);

        if (isBlocking)
        {
            SetEquipmentBlock();
            PlayActionSound(blockStartClip, actionVolume);
        }
        else
        {
            SetEquipmentNormal();
            PlayActionSound(blockEndClip, actionVolume);
        }
    }

    public void OnPickup(InputAction.CallbackContext context)
    {
        if (levelConfig != null && !levelConfig.canPickup) return;
        if (!context.performed) return;

        SetEquipmentOnBack();
        animator?.SetTrigger(PickupHash);

        if (pitchedAudioRoutine != null) StopCoroutine(pitchedAudioRoutine);
        pitchedAudioRoutine = StartCoroutine(PlayPitchedClip(pickupClip, pickupPitch, actionVolume));

        if (levelConfig != null)
        {
            levelConfig.defaultSword = levelConfig.swordAfterPickup;
            levelConfig.defaultShield = levelConfig.shieldAfterPickup;
        }

        if (equipmentRoutine != null) StopCoroutine(equipmentRoutine);
        equipmentRoutine = StartCoroutine(ReturnEquipmentAfterDelay(2.4f));
    }

    public void OnCast(InputAction.CallbackContext context)
    {
        if (levelConfig != null && !levelConfig.canCast) return;
        if (!context.performed) return;

        SetEquipmentOnBack();
        animator?.SetTrigger(CastSpellHash);

        PlayActionSound(castClip, actionVolume);
        PlayActionSound(castClip1, actionVolume);

        if (equipmentRoutine != null) StopCoroutine(equipmentRoutine);
        equipmentRoutine = StartCoroutine(ReturnEquipmentAfterDelay(2.0f));
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
    }

    private void ApplyDefaultEquipmentFromConfig()
    {
        if (levelConfig == null)
        {
            SetEquipment(EquipmentPlacement.Hand, EquipmentPlacement.Back);
            return;
        }

        SetEquipment(levelConfig.defaultSword, levelConfig.defaultShield);
    }

    public void SetEquipment(EquipmentPlacement swordPlacement, EquipmentPlacement shieldPlacement)
    {
        if (swordHand != null) swordHand.SetActive(swordPlacement == EquipmentPlacement.Hand);
        if (swordBack != null) swordBack.SetActive(swordPlacement == EquipmentPlacement.Back);

        if (shieldHand != null) shieldHand.SetActive(shieldPlacement == EquipmentPlacement.Hand);
        if (shieldBack != null) shieldBack.SetActive(shieldPlacement == EquipmentPlacement.Back);
    }

    public void SetEquipmentNormal()
    {
        ApplyDefaultEquipmentFromConfig();
    }

    public void SetEquipmentBlock()
    {
        SetEquipment(EquipmentPlacement.Back, EquipmentPlacement.Hand);
    }

    public void SetEquipmentOnBack()
    {
        SetEquipment(EquipmentPlacement.Back, EquipmentPlacement.Back);
    }

    public void OnLook(InputAction.CallbackContext context) { }
    public void OnPrevious(InputAction.CallbackContext context) { }
    public void OnNext(InputAction.CallbackContext context) { }
    public void OnSprint(InputAction.CallbackContext context) { }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Lava"))
        {
            transform.position = levelConfig != null
                ? levelConfig.respawnPosition
                : new Vector3(95.1f, 2.5f, 164.9f);

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