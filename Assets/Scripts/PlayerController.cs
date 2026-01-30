using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class PlayerController : MonoBehaviour
{
    public enum PlayerState { Idle, Move, Attack, Death }
    public PlayerState State { get; private set; }

    [Header("Move")]
    public float moveSpeed = 6f;
    public float jumpForce = 7f;
    public float crouchSpeedMultiplier = 0.5f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundMask;

    [Header("Attack - Book Throw")]
    public Transform firePoint;
    public GameObject bookPrefab;
    public float bookSpeed = 12f;
    public float bookLifeTime = 2f;
    public float fireCooldown = 0.35f;

    private Rigidbody rb;
    private CapsuleCollider col;

    private bool isGrounded;
    private bool isCrouching;
    private float lastFireTime = -999f;
    private int facingDir = 1;

    private float defaultHeight;
    private Vector3 defaultCenter;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();

        defaultHeight = col.height;
        defaultCenter = col.center;

        State = PlayerState.Idle;
    }

    void Update()
    {
        if (groundCheck != null)
            isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundMask);

        bool crouchInput = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.C);
        SetCrouch(crouchInput);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Espace appuyé ! isGrounded = " + isGrounded);
        }

        // Utilise W pour sauter (flèche haut est maintenant pour avancer)
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && isGrounded && State != PlayerState.Death)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, 0f);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        if (State != PlayerState.Death)
        {
            if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.J))
                TryThrowBook();
        }

        if (State != PlayerState.Death)
        {
            float x = Input.GetAxisRaw("Horizontal");
            if (State != PlayerState.Attack)
                State = Mathf.Abs(x) > 0.01f ? PlayerState.Move : PlayerState.Idle;
        }
    }

    void FixedUpdate()
    {
        if (State == PlayerState.Death) return;

        float x = Input.GetAxisRaw("Horizontal"); // Gauche/Droite (flèches ou A/D)
        float z = Input.GetAxisRaw("Vertical");   // Avant/Arrière (flèches haut/bas)

        // Facing direction (pour lancer le livre)
        if (x > 0.01f) facingDir = 1;
        else if (x < -0.01f) facingDir = -1;

        float speed = isCrouching ? moveSpeed * crouchSpeedMultiplier : moveSpeed;

        // Mouvement en 3D (X et Z)
        Vector3 movement = new Vector3(x, 0f, z).normalized * speed;
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
    }

    void SetCrouch(bool crouch)
    {
        if (crouch == isCrouching) return;
        isCrouching = crouch;

        if (isCrouching)
        {
            col.height = defaultHeight * 0.6f;
            col.center = defaultCenter + new Vector3(0f, -0.2f, 0f);
        }
        else
        {
            col.height = defaultHeight;
            col.center = defaultCenter;
        }
    }

    void TryThrowBook()
    {
        if (bookPrefab == null || firePoint == null) return;
        if (Time.time - lastFireTime < fireCooldown) return;

        lastFireTime = Time.time;
        State = PlayerState.Attack;

        GameObject proj = Instantiate(bookPrefab, firePoint.position, Quaternion.identity);

        Rigidbody prb = proj.GetComponent<Rigidbody>();
        if (prb != null)
        {
            prb.useGravity = false;
            prb.velocity = new Vector3(facingDir * bookSpeed, 0f, 0f);
        }

        Destroy(proj, bookLifeTime);

        Invoke("EndAttack", 0.15f);
    }

    void EndAttack()
    {
        if (State == PlayerState.Attack)
            State = Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.01f ? PlayerState.Move : PlayerState.Idle;
    }

    public void SetDeathState()
    {
        State = PlayerState.Death;
        rb.velocity = Vector3.zero;
    }

    void OnDrawGizmosSelected()
    {
        if (!groundCheck) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}