using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCrystal : MonoBehaviour
{
    [Header("Materials")]
    [SerializeField] private Renderer crystalRenderer;
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

        // Start dull
        if (crystalRenderer != null && dullMaterial != null)
            crystalRenderer.material = dullMaterial;
    }

    private void Update()
    {
        if (!isActivated || isCollected) return;

        // Spin
        transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime, Space.World);
    }

    public void ActivateCrystal()
    {
        if (isActivated) return;

        isActivated = true;

        // 🔥 SWITCH MATERIAL HERE
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

    private void OnTriggerEnter(Collider other)
    {
        if (!isActivated || isCollected) return;
        if (!other.CompareTag("Player")) return;

        isCollected = true;

        SceneManager.LoadScene(nextSceneName);
    }
}