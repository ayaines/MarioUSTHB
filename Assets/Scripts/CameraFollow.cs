
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;       // Glisse Mario ici
    public float sSpeed = 10.0f;   // Vitesse de suivi
    public Vector3 dist = new Vector3(3, 5, -10); // Distance (Offset)
    
    // On retire LookAt pour un jeu de plateforme 2.5D, 
    // sinon la caméra va pivoter bizarrement quand Mario saute.

    void FixedUpdate()
    {
        if (player == null) return;

        // Position voulue
        Vector3 dPos = player.position + dist;
        
        // Mouvement fluide
        Vector3 sPos = Vector3.Lerp(transform.position, dPos, sSpeed * Time.deltaTime);
        
        transform.position = sPos;
    }
}