using UnityEngine;
using TMPro;

public class TimeController : MonoBehaviour
{
    [SerializeField] private GameObject gameOverCanvas;
    private bool gameEnded = false;

    [Header("Time Settings")]
    [SerializeField] private float timeMultiplier = 60f; // speed of time
    [SerializeField] private float startHour = 8f;
    private float currentTime; // 0–24 hours

    [SerializeField] private TextMeshProUGUI timeText;

    [Header("Light Settings")]
    [SerializeField] private Light sunLight;
    [SerializeField] private float sunriseHour = 6f;
    [SerializeField] private float sunsetHour = 18f;
    [SerializeField] private float maxSunLightIntensity = 1f;

    [SerializeField] private Light moonLight;
    [SerializeField] private float maxMoonLightIntensity = 0.5f;

    [SerializeField] private Color dayAmbientLight;
    [SerializeField] private Color nightAmbientLight;
    [SerializeField] private AnimationCurve lightChangeCurve;

    void Start()
    {
        currentTime = startHour;
    }

    void Update()
    {
        if (gameEnded) return;

        UpdateTime();
        RotateSun();
        UpdateLighting();
        CheckForGameOver();
    }

    private void UpdateTime()
    {
        currentTime += Time.deltaTime * timeMultiplier / 3600f;

        if (currentTime >= 24f)
            currentTime -= 24f;

        if (timeText != null)
        {
            int hours = Mathf.FloorToInt(currentTime);
            int minutes = Mathf.FloorToInt((currentTime - hours) * 60f);
            timeText.text = $"{hours:00}:{minutes:00}";
        }
    }

    private void RotateSun()
    {
        float normalizedTime;

        if (currentTime >= sunriseHour && currentTime < sunsetHour)
        {
            normalizedTime = (currentTime - sunriseHour) / (sunsetHour - sunriseHour);
            sunLight.transform.rotation = Quaternion.Euler(Mathf.Lerp(0, 180, normalizedTime), 0, 0);
        }
        else
        {
            float nightLength = (24f - sunsetHour) + sunriseHour;

            float timeSinceSunset = (currentTime >= sunsetHour)
                ? currentTime - sunsetHour
                : currentTime + (24f - sunsetHour);

            normalizedTime = timeSinceSunset / nightLength;
            sunLight.transform.rotation = Quaternion.Euler(Mathf.Lerp(180, 360, normalizedTime), 0, 0);
        }
    }

    private void UpdateLighting()
    {
        float dot = Vector3.Dot(sunLight.transform.forward, Vector3.down);
        float curveValue = lightChangeCurve.Evaluate(dot);

        sunLight.intensity = Mathf.Lerp(0, maxSunLightIntensity, curveValue);
        moonLight.intensity = Mathf.Lerp(maxMoonLightIntensity, 0, curveValue);

        RenderSettings.ambientLight = Color.Lerp(nightAmbientLight, dayAmbientLight, curveValue);
    }

    private void CheckForGameOver()
    {
        float gameOverTime = sunsetHour + 1f;

        if (gameOverTime >= 24f)
            gameOverTime -= 24f;

        if (currentTime >= gameOverTime && !gameEnded)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        gameEnded = true;
        Time.timeScale = 0f;

        if (gameOverCanvas != null)
            gameOverCanvas.SetActive(true);

        Debug.Log("Game Over: It is night!");
    }
}