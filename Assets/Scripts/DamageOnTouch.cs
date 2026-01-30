using UnityEngine;

public class DamageOnTouch : MonoBehaviour
{
    public int damage = 1;

    // Marche si BoxCollider (EnemyTest) est "Is Trigger" ON
    private void OnTriggerEnter(Collider other)
    {
        TryDamage(other.gameObject);
    }

    // Si tu préfères ne PAS utiliser trigger, tu peux aussi laisser OnCollisionEnter
    private void OnCollisionEnter(Collision collision)
    {
        TryDamage(collision.gameObject);
    }

    void TryDamage(GameObject obj)
    {
        // Cherche PlayerHealth sur l’objet touché OU son parent
        PlayerHealth ph = obj.GetComponent<PlayerHealth>();
        if (ph == null) ph = obj.GetComponentInParent<PlayerHealth>();

        if (ph != null)
        {
            ph.TakeDamage(damage);
        }
    }
}
