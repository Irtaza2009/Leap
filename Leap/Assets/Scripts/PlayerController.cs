using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 12f;
    public LayerMask groundLayer;

    [Header("Sacrifice")]
    public GameObject smallSquarePrefab; // Inspector
    public int maxSacrifices = 3;
    private int sacrificesUsed = 0;

    private Rigidbody2D rb;
    private bool isGrounded = false;
    private Transform groundCheck;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // groundCheck child object for collision detection
        groundCheck = new GameObject("GroundCheck").transform;
        groundCheck.SetParent(transform);
        groundCheck.localPosition = new Vector3(0, -0.6f, 0); // just below the square
    }

    void Update()
    {
        // movement
        float move = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(move * moveSpeed, rb.linearVelocity.y);

        // sacrifice
        if ((Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Space)) && sacrificesUsed < maxSacrifices)
        {
            Sacrifice();
        }

        // jump
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }


    }

    void Sacrifice()
    {
        Debug.Log("Sacrifice used!");
        sacrificesUsed++;

        // Spawn smaller square
        GameObject newPlayer = Instantiate(smallSquarePrefab, transform.position, Quaternion.identity);
        Rigidbody2D newPlayerRB = newPlayer.GetComponent<Rigidbody2D>();
        newPlayerRB.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); // jump

        // Camera follow fix (optional: reassign camera target to new player here)

        //Destroy(gameObject); // destroy current player
        this.enabled = false; // disable current player controls
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spike"))
        {
            Die();
        }
        if (collision.gameObject.CompareTag("Finish"))
        {
            Debug.Log("Level Complete!");
            UnityEngine.SceneManagement.SceneManager.LoadScene(
                UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1
            );
        }
    }

    void Die()
    {
        Debug.Log("Player died!");

        SceneTracker.GoToGameOver();
    }


}
