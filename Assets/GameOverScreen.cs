using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public GameObject gameOverPanel;
    public Text finalScoreText;

    // Fonction appelée quand le joueur meurt
    public void ShowGameOver(int finalScore)
    {
        gameOverPanel.SetActive(true);
        finalScoreText.text = "Final Score: " + finalScore;
        Time.timeScale = 0f; // Pause le jeu
    }

    // Bouton RETRY
    public void RetryGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Bouton MAIN MENU
    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
