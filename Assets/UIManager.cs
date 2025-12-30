using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Text UI")]
    public Text livesText;
    public Text scoreText;
    public Text timerText;

    [Header("Game Values (Test)")]
    public int lives = 3;
    public int score = 0;
    public float timeLeft = 300f;

    void Start()
    {
        UpdateLives(lives);
        UpdateScore(score);
        UpdateTimerText();
    }

    void Update()
    {
        // Timer
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            UpdateTimerText();
        }
        else
        {
            timeLeft = 0;
        }
    }

    // ❤️ Mettre à jour les vies
    public void UpdateLives(int newLives)
    {
        lives = newLives;
        livesText.text = "Lives: " + lives;
    }

    // 🪙 Mettre à jour le score
    public void UpdateScore(int newScore)
    {
        score = newScore;
        scoreText.text = "Score: " + score;
    }

    // ⏱️ Mettre à jour l'affichage du timer
    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timeLeft / 60);
        int seconds = Mathf.FloorToInt(timeLeft % 60);
        timerText.text = "Time: " + minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}

