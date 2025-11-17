
using UnityEngine;
using UnityEngine.UI;

public class CoffeeGame : MonoBehaviour
{
    [Header("UI Elements")]
    public Slider grindSlider, tempSlider, pressureSlider, timeSlider, doseSlider;
    public Text resultText, levelText;

    private float targetGrind, targetTemp, targetPressure, targetTime, targetDose;
    private int level = 1;
    private float tolerance = 5f;

    void Start()
    {
        GenerateNewTarget();
    }

    public void OnBrewPressed()
    {
        float score = 0f;
        score += ScoreValue(grindSlider.value, targetGrind);
        score += ScoreValue(tempSlider.value, targetTemp);
        score += ScoreValue(pressureSlider.value, targetPressure);
        score += ScoreValue(timeSlider.value, targetTime);
        score += ScoreValue(doseSlider.value, targetDose);
        score /= 5f;

        if (score >= 80f)
        {
            resultText.text = $"☕️ Perfect Extraction! Score: {score:F1}%";
            level++;
            tolerance = Mathf.Max(1f, tolerance - 1f);
            GenerateNewTarget();
        }
        else
        {
            resultText.text = $"😕 Almost! Score: {score:F1}% — Try again!";
        }
    }

    private float ScoreValue(float playerVal, float targetVal)
    {
        float diff = Mathf.Abs(playerVal - targetVal);
        if (diff > tolerance) return Mathf.Max(0, 100 - (diff - tolerance) * 10);
        return 100 - (diff / tolerance) * 10;
    }

    private void GenerateNewTarget()
    {
        targetGrind = Random.Range(0f, 100f);
        targetTemp = Random.Range(80f, 96f);
        targetPressure = Random.Range(5f, 10f);
        targetTime = Random.Range(20f, 35f);
        targetDose = Random.Range(14f, 20f);

        levelText.text = $"Level {level}";
    }
}
