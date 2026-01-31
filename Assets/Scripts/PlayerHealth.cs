using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [Header("Lives")]
    public int maxLives = 3;
    public int lives;

    [Header("Invincibility")]
    public float invincibleTime = 1.0f;
    private bool invincible;

    [Header("Respawn")]
    public Transform respawnPoint;
    public bool resetLivesOnDeath = false;

    private Vector3 startPosition;
    private Rigidbody rb;
    private PlayerController controller;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<PlayerController>();
    }

    void Start()
    {
        lives = maxLives;
        startPosition = transform.position;
        Debug.Log("Lives: " + lives);
    }

    public void TakeDamage(int amount)
    {
        if (invincible) return;

        lives -= amount;
        Debug.Log("Lives: " + lives);

        if (lives <= 0)
        {
            Die();
            return;
        }

        StartCoroutine(InvincibleRoutine());
    }

    void Die()
    {
        Debug.Log("PLAYER DEAD - Lives: " + lives);

        if (controller != null)
            controller.SetDeathState();

        // Si plus de vies → Game Over
        if (lives <= 0)
        {
            GameOverScreen gameOver = FindObjectOfType<GameOverScreen>();
            if (gameOver != null)
            {
                UIManager uiManager = FindObjectOfType<UIManager>();
                int finalScore = (uiManager != null) ? uiManager.score : 0;

                gameOver.ShowGameOver(finalScore);
            }

            return; // Ne pas respawn si Game Over
        }

        // Sinon respawn
        Respawn();

        if (resetLivesOnDeath)
        {
            lives = maxLives;
            Debug.Log("Lives reset: " + lives);
        }

        Invoke("EnableControl", 0.2f);
    }

    void EnableControl()
    {
        // Le controller repassera en Idle/Move automatiquement
    }

    void Respawn()
    {
        Vector3 pos = (respawnPoint != null) ? respawnPoint.position : startPosition;

        if (rb != null)
            rb.velocity = Vector3.zero;

        transform.position = pos;
    }

    IEnumerator InvincibleRoutine()
    {
        invincible = true;
        yield return new WaitForSeconds(invincibleTime);
        invincible = false;
    }
}