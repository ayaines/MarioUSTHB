using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{
    public GameObject victoryPanel;
    public Text victoryScoreText;
    public Text victoryTimeText;
    public Text victoryCoinsText;

    public void TestShowVictory()
    {
        ShowVictory(5000, 180f, 25);
    }

    public void ShowVictory(int finalScore, float timeTaken, int coinsCollected)
    {
        victoryPanel.SetActive(true);

        victoryScoreText.text = "Score: " + finalScore;

        int minutes = Mathf.FloorToInt(timeTaken / 60);
        int seconds = Mathf.FloorToInt(timeTaken % 60);
        victoryTimeText.text = "Time: " + minutes.ToString("00") + ":" + seconds.ToString("00");

        victoryCoinsText.text = "Coins: " + coinsCollected;

        Time.timeScale = 0f;

        // Son de victoire
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.levelCompleteSound);
            AudioManager.instance.PlayMusic(AudioManager.instance.victoryMusic);
        }
    }

    public void LoadNextLevel()
    {
        // Son de clic
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.buttonClickSound);
        }

        Time.timeScale = 1f;
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index + 1);
    }

    public void LoadMainMenu()
    {
        // Son de clic
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.buttonClickSound);
        }

        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}