using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCrystal : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LavaLevelManager levelManager;
    [SerializeField] private Renderer crystalRenderer;

    [Header("Materials")]
    [SerializeField] private Material dullMaterial;
    [SerializeField] private Material glowMaterial;

    [Header("Animation")]
    [SerializeField] private float riseHeight = 2f;
    [SerializeField] private float riseDuration = 1.2f;
    [SerializeField] private float spinSpeed = 900f;

    [Header("Scene")]
    [SerializeField] private string nextSceneName;

    private bool isActivated = false;
    private bool isCollected = false;

    private Vector3 startPos;
    private Vector3 targetPos;

    private void Start()
    {
        startPos = transform.position;
        targetPos = startPos + Vector3.up * riseHeight;

        if (crystalRenderer == null)
            crystalRenderer = GetComponentInChildren<Renderer>();

        if (crystalRenderer != null && dullMaterial != null)
            crystalRenderer.material = dullMaterial;
    }

    private void Update()
    {
        if (!isActivated || isCollected) return;

        transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime, Space.World);
    }

    public void ActivateCrystal()
    {
        if (isActivated) return;

        isActivated = true;

        if (crystalRenderer != null && glowMaterial != null)
            crystalRenderer.material = glowMaterial;

        StartCoroutine(RiseUp());
    }

    private IEnumerator RiseUp()
    {
        float t = 0f;
        Vector3 from = transform.position;

        while (t < riseDuration)
        {
            t += Time.deltaTime;
            float lerp = t / riseDuration;
            transform.position = Vector3.Lerp(from, targetPos, lerp);
            yield return null;
        }

        transform.position = targetPos;
    }

    public void CollectCrystal()
    {
        if (!isActivated || isCollected) return;

        isCollected = true;

        Debug.Log("Crystal collected → loading scene");

        if (!string.IsNullOrEmpty(nextSceneName))
            SceneManager.LoadScene(nextSceneName);
        else
            Debug.LogError("Next scene name not set on crystal");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Debug.Log("Player touched crystal");

        if (levelManager != null)
            levelManager.OnCrystalTouched();
    }
}