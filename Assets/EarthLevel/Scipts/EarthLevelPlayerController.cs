using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement; // Added for restarting on death

public class EarthLevelPlayerController : MonoBehaviour
{
    public float speed; // Using a lower value with linearVelocity is usually more stable
    public Text statusText;
    
    [Header("Alpha Objects")]
    public GameObject shieldOnPlayer; // Drag hidden shield (child of player) here
    public GameObject earthCrystal;   // Drag Crystal prefab here
    public AudioClip craftSound;      // Drag crafting SFX here
    
    private int collectedCount = 0;
    private int totalWoodInScene;
    private bool isGameOver = false;
    private bool canCraft = false; 
    private Rigidbody rb;
    private AudioSource playerAudio;

    void Start()
    {
        speed = 15f;
        rb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
        
        // Setup initial state
        totalWoodInScene = GameObject.FindGameObjectsWithTag("Wood").Length;
        if(shieldOnPlayer) shieldOnPlayer.SetActive(false);
        if(earthCrystal) earthCrystal.SetActive(false);
        
        UpdateUI();
    }

    void Update()
    {
        if (isGameOver || Keyboard.current == null) return;

        // --- Interaction Logic ---
        if (canCraft && Keyboard.current.eKey.wasPressedThisFrame)
        {
            CraftShield();
        }

        // --- Movement Logic ---
        Vector3 moveDirection = Vector3.zero;
        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) moveDirection += Vector3.forward;
        if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) moveDirection += Vector3.back;
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) moveDirection += Vector3.left;
        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) moveDirection += Vector3.right;

        if (moveDirection != Vector3.zero)
        {
            transform.forward = moveDirection;
            // Use velocity for better terrain/gravity handling
            Vector3 vel = moveDirection.normalized * speed;
            rb.linearVelocity = new Vector3(vel.x, rb.linearVelocity.y, vel.z);
        }
        else
        {
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (isGameOver) return;

        if (other.CompareTag("Wood"))
        {
            playerAudio.PlayOneShot(playerAudio.clip);
            collectedCount++;
            Destroy(other.gameObject);
            UpdateUI();

            if (collectedCount >= totalWoodInScene)
            {
                Debug.Log("All wood collected! Attempting to remove enemies...");

                // 1. Find by Tag
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                Debug.Log("Found " + enemies.Length + " enemies to disable.");

                foreach (GameObject enemy in enemies)
                {
                    enemy.SetActive(false);
                }

                // 2. Backup: Find by Script (if you have a script named RootEnemy on them)
                RootEnemy[] enemyScripts = GameObject.FindObjectsByType<RootEnemy>(FindObjectsSortMode.None);
                foreach (RootEnemy script in enemyScripts)
                {
                    script.gameObject.SetActive(false);
                }

                statusText.text = "Forest is Safe! Go to Altar (base of hill)";
                statusText.color = Color.yellow;
            }
        }

        if (other.CompareTag("Altar") && collectedCount >= totalWoodInScene)
        {
            canCraft = true;
            statusText.text = "Press [E] to Craft Shield";
        }

        if (other.CompareTag("Hazard") || other.CompareTag("Enemy"))
        {
            Die();
        }
        
        // The Win Condition
        if (other.CompareTag("Crystal"))
        {
            isGameOver = true;
            statusText.text = "CRYSTAL OBTAINED! LEVEL COMPLETE!";
            statusText.color = Color.green;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Altar")) canCraft = false;
    }

    void CraftShield()
    {
        if(shieldOnPlayer) shieldOnPlayer.SetActive(true);
        if(earthCrystal) earthCrystal.SetActive(true);
        if(craftSound) playerAudio.PlayOneShot(craftSound);
        
        canCraft = false;
        statusText.text = "Shield Crafted! Collect the Crystal!";
    }

    void Die()
    {
        isGameOver = true;
        statusText.text = "GAME OVER!";
        statusText.color = Color.red;
        // Restarts scene after 2 seconds
        Invoke("RestartLevel", 2f);
    }

    void RestartLevel() { SceneManager.LoadScene(SceneManager.GetActiveScene().name); }

    void UpdateUI()
    {
        if (statusText != null)
            statusText.text = "Wood: " + collectedCount + " / " + totalWoodInScene;
    }
}