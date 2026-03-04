using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 12f;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI scoreText;

    private Rigidbody2D rb;
    private bool isGrounded = false;

    private int health = 100;
    private int score = 0;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        UpdateUI();
    }

    void Update() {
        // Horizontal movement
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Jumping
        if (Input.GetButtonDown("Jump") && isGrounded) {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Enemy")) {
            health -= 10;
            UpdateUI();

            if (health <= 0) {
                GameOver();
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            isGrounded = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        // Coin collection
        if (other.CompareTag("Coin")) {
            score += 10;
            Destroy(other.gameObject);
            UpdateUI();
        }
    }

    void UpdateUI() {
        healthText.text = "Health: " + health;
        scoreText.text = "Score: " + score;
    }
    
    void GameOver() {
        // Save score for next scene
        PlayerPrefs.SetInt("FinalScore", score);

        // Load GameOver scene
        SceneManager.LoadScene("GameOver");
    }
}