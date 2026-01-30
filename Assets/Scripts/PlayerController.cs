using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 6f;
    public float jumpForce = 10f;
    private Rigidbody rb;
    private bool isGrounded = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Input axes
        float moveX = Input.GetAxis("Horizontal"); // Left/Right arrows or A/D
        float moveZ = Input.GetAxis("Vertical");   // Up/Down arrows or W/S

        // Movement vector in world space
        Vector3 movement = new Vector3(moveX, 0, moveZ);

        // Move Mario
        transform.Translate(movement * speed * Time.deltaTime, Space.World);

        // Rotate Mario to face the direction pressed
        if (movement != Vector3.zero)
        {
            transform.forward = movement; 
        }

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
