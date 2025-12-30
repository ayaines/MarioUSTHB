using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{
    public GameObject victoryPanel;
    public Text victoryScoreText;
    public Text victoryTimeText;
    public Text victoryCoinsText;

    // ✅ FONCTION DE TEST (VISIBLE DANS BUTTON)
    public void TestShowVictory()
    {
        ShowVictory(5000, 180f, 25);
    }

    // 🎮 FONCTION RÉELLE (APPELÉE PAR LE GAMEPLAY PLUS TARD)
    public void ShowVictory(int finalScore, float timeTaken, int coinsCollected)
    {
        victoryPanel.SetActive(true);

        victoryScoreText.text = "Score: " + finalScore;

        int minutes = Mathf.FloorToInt(timeTaken / 60);
        int seconds = Mathf.FloorToInt(timeTaken % 60);
        victoryTimeText.text = "Time: " + minutes.ToString("00") + ":" + seconds.ToString("00");

        victoryCoinsText.text = "Coins: " + coinsCollected;

        Time.timeScale = 0f;
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1f;
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index + 1);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
