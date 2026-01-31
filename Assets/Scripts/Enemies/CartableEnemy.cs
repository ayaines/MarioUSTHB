using UnityEngine;

public class CartableEnemy : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 2f;
    public float moveDistance = 4f;

    [Header("Damage")]
    public int damage = 1;

    private Vector3 startPosition;
    private int direction = 1;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Bouge horizontalement
        transform.position += new Vector3(direction * speed * Time.deltaTime, 0, 0);

        // Change de direction si trop loin
        float distanceTraveled = Mathf.Abs(transform.position.x - startPosition.x);
        if (distanceTraveled >= moveDistance)
        {
            direction *= -1;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Si collision avec le joueur
        PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
        if (player != null)
        {
            player.TakeDamage(damage);
            Debug.Log("Cartable a touché Mario !");
        }

       
    }

    // Fonction pour tuer l'ennemi (quand Mario saute dessus)
    public void Die()
    {
        Debug.Log("Cartable mort !");

        // Son si AudioManager existe
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.enemyDeathSound);
        }

        Destroy(gameObject);
    }
}