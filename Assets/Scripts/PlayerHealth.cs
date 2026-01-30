using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int lives = 3;
    private int currentLives;
    private Animator anim;

    void Start()
    {
        currentLives = lives;
        anim = GetComponent<Animator>();
        Debug.Log("Lives: " + currentLives);
    }

    public void TakeDamage(int amount)
    {
        currentLives -= amount;
        Debug.Log("Lives: " + currentLives);

        if (currentLives <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Game Over!");
        if (anim != null)
            anim.SetTrigger("Die");

        // Respawn Mario
        transform.position = new Vector3(0, 2, 0);
        currentLives = lives; // reset lives
    }
}
