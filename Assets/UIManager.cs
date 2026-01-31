using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Text UI")]
    public Text livesText;
    public Text scoreText;
    public Text timerText;

    [Header("References")]
    public PlayerHealth playerHealth;

    [Header("Game Values")]
    public int score = 0;
    public float timeLeft = 300f;

    void Start()
    {
        if (playerHealth != null)
            UpdateLives(playerHealth.lives);

        UpdateScore(score);
        UpdateTimerText();

        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayMusic(AudioManager.instance.gameplayMusic);
        }
    }

    void Update()
    {
        // Update lives from player
        if (playerHealth != null)
            UpdateLives(playerHealth.lives);

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

    public void UpdateLives(int newLives)
    {
        livesText.text = "Lives: " + newLives;
    }

    public void UpdateScore(int newScore)
    {
        score = newScore;
        scoreText.text = "Score: " + score;
    }

    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timeLeft / 60);
        int seconds = Mathf.FloorToInt(timeLeft % 60);
        timerText.text = "Time: " + minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScore(score);
    }
}