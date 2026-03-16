using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem; // This is the key addition

public class MiniGameController : MonoBehaviour
{
    public float speed;
    public Text statusText;

    private int collectedCount = 0;
    private int totalWoodInScene;
    private bool isGameOver = false;
    private Rigidbody rb;

    void Start()
    {
        speed = 50f;
        rb = GetComponent<Rigidbody>(); // Links the script to the Rigidbody
        totalWoodInScene = GameObject.FindGameObjectsWithTag("Wood").Length;
        UpdateUI();
    }
    void Update()
    {
        if (isGameOver || Keyboard.current == null) return;

        // 1. Calculate direction based on input
        Vector3 moveDirection = Vector3.zero;

        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) moveDirection += Vector3.forward;
        if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) moveDirection += Vector3.back;
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) moveDirection += Vector3.left;
        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) moveDirection += Vector3.right;

        // 2. If we are moving, rotate the player to face that way
        if (moveDirection != Vector3.zero)
        {
            // This makes the capsule look where it's going
            transform.forward = moveDirection;

            // Move the player
            rb.MovePosition(transform.position + moveDirection.normalized * speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (isGameOver) return;

        if (other.CompareTag("Wood"))
        {
            collectedCount++;
            Destroy(other.gameObject);
            UpdateUI();

            if (collectedCount >= totalWoodInScene)
            {
                isGameOver = true;
                statusText.text = "ALL WOOD COLLECTED! YOU WIN!";
                statusText.color = Color.green;
            }
        }

        if (other.CompareTag("Hazard"))
        {
            isGameOver = true;
            statusText.text = "GAME OVER! YOU HIT A HAZARD!";
            statusText.color = Color.red;
        }
    }

    void UpdateUI()
    {
        if (statusText != null)
            statusText.text = "Wood: " + collectedCount + " / " + totalWoodInScene;
    }
}