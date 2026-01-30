using UnityEngine;

public class DamageOnTouch : MonoBehaviour
{
    public int damage = 1;

    private void OnTriggerEnter(Collider other)
    {
        var ph = other.GetComponent<PlayerHealth>();
        if (ph != null)
        {
            ph.TakeDamage(damage);
        }
    }
}
