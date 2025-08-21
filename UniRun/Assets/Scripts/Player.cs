using UnityEngine;

public class Player : MonoBehaviour
{
    public float JumpForce = 100f;
    public int JumpCountMax = 2;

    public AudioClip dieAudioClip;

    private int jumpCount = 0;
    private Animator animator;
    private Rigidbody2D rb;
    private AudioSource audioSource;

    private bool isGrounded = true;
    private bool isDead = false;

    private GameManager gameManager;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (isDead)
            return;

        if (Input.GetMouseButtonDown(0) && jumpCount < JumpCountMax)
        {
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
            ++jumpCount;

            audioSource.Play();
        }

        if (Input.GetMouseButtonUp(0) && rb.linearVelocity.y > 0)
        {
            rb.linearVelocity *= 0.5f;
        }

        animator.SetBool("Grounded", isGrounded);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isDead && collision.CompareTag("DeadZone"))
        {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Platform") &&
            collision.contacts[0].normal.y > 0.7f)
        {
            jumpCount = 0;
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Platform"))
        {
            isGrounded = false;
        }
    }

    private void Die()
    {
        isDead = true;

        animator.SetTrigger("Die");
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.linearVelocity = Vector2.zero;

        gameManager.OnPlayerDead();
    }
}