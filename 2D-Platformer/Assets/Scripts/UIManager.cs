using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI healthText;

    void OnEnable() {
        if (GameManager.Instance != null) {
            GameManager.Instance.onScoreChanged += UpdateScore;
            GameManager.Instance.onHealthChanged += UpdateHealth;
            GameManager.Instance.onGameOver += HandleGameOver;
        }
    }

    void OnDisable() {
        if (GameManager.Instance != null) {
            GameManager.Instance.onScoreChanged -= UpdateScore;
            GameManager.Instance.onHealthChanged -= UpdateHealth;
            GameManager.Instance.onGameOver -= HandleGameOver;
        }
    }

    void Start() {
        if (GameManager.Instance != null) {
            UpdateScore(GameManager.Instance.CurrentScore);
            UpdateHealth(GameManager.Instance.CurrentHealth);
        }
    }

    void UpdateScore(int newScore) {
        scoreText.text = "Score: " + newScore;
        Debug.Log("UIManager: Score updated to " + newScore);
    }

    void UpdateHealth(int newHealth) {
        healthText.text = "Health: " + newHealth;
        Debug.Log("UIManager: Health updated to " + newHealth);
    }

    void HandleGameOver() {
        Debug.Log("UIManager: GameOver event received. Loading GameOver scene.");
        SceneManager.LoadScene("GameOver");
    }
}