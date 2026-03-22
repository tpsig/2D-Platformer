using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float moveSpeed = 30f;
    public float jumpForce = 85f;

    private Rigidbody2D rb;
    private bool isGrounded = false;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded) {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Enemy")) {
            Debug.Log("Player hit by enemy!");
            GameManager.Instance.TakeDamage(10);
        }
    }

    void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            isGrounded = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            Debug.Log("Pool Instance = " + CoinPoolManager.Instance);
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddScore(10);
            }

            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.coinSound);
            }

            if (CoinPoolManager.Instance != null)
            {
                CoinPoolManager.Instance.CollectCoin(other.gameObject);
                Debug.Log("Coin collected!");

            }

        }
    }
}