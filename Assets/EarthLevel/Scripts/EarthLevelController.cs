using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro; // Added for restarting on death

public class EarthLevelController : MonoBehaviour
{
    //public float speed; // Using a lower value with linearVelocity is usually more stable
    public Text statusText;
    
    [Header("Alpha Objects")]
    public GameObject shieldOnPlayer; // Drag hidden shield (child of player) here
    public GameObject earthCrystal;   // Drag Crystal prefab here
    public AudioClip craftSound;      // Drag crafting SFX here

    private Vector3 crystalRotate;
    private int collectedCount = 0;
    private int totalWoodInScene;
    private bool shieldCrafted = false;
    private bool isGameOver = false;
    private bool canCraft = false; 
    private Rigidbody rb;
    private AudioSource playerAudio;

    void Start()
    {
        crystalRotate = new Vector3(0, 100f, 0);
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

        if (earthCrystal.activeSelf)
        {
            earthCrystal.transform.Rotate(crystalRotate * Time.deltaTime);
        }

        // --- Interaction Logic ---
        if (canCraft && Keyboard.current.eKey.wasPressedThisFrame)
        {
            CraftShield();
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

                earthCrystal.SetActive(true);

                statusText.text = "Forest is Safe! Go to Altar (top of hill)";
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
            if (!shieldCrafted) return;

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
        if (shieldOnPlayer) shieldOnPlayer.SetActive(true);

        if(craftSound) playerAudio.PlayOneShot(craftSound);
        
        canCraft = false;
        statusText.text = "Shield Crafted! Collect the Crystal!";
        shieldCrafted = true;
    }

    void Die()
    {
        isGameOver = true;
        statusText.text = "GAME OVER!";
        statusText.color = Color.red;
        // Restarts scene after 2 seconds
        Invoke(nameof(RestartLevel), 2f);
    }

    void RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void UpdateUI()
    {
        if (statusText != null)
            statusText.text = "Wood: " + collectedCount + " / " + totalWoodInScene;
    }
}