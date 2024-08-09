using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeManagement : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float timeLimit = 120f;
    private float timeRemaining;

    void Start()
    {
        timeRemaining = timeLimit;
    }

    void Update()
    {
        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            GameOver();
        }

        timerText.text = $"Time: {Mathf.Round(timeRemaining)}";
    }

    void GameOver()
    {
        Debug.Log("Game Over!");
        Time.timeScale = 0;
    }
}
