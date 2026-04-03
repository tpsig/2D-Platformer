using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }

    public event Action<int> onScoreChanged;
    public event Action<int> onHealthChanged;
    public event Action onGameOver;

    private int score = 0;
    private int health = 100;
    private int coinsCollected = 0;

    public int CurrentScore => score;
    public int CurrentHealth => health;
    public int CoinsCollected => coinsCollected;

    private int winScore = 100;       
    private int winCoins = 10;

    void Awake() {
        if (Instance != null && Instance != this) {
            Debug.Log("GameManager duplicate found. Destroying new instance.");
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Debug.Log("GameManager initialized.");
    }

    public void AddScore(int points) {
        score += points;
        Debug.Log("GameManager: Score increased to " + score);
        onScoreChanged?.Invoke(score);

        CheckWinCondition();
    }

    public void AddCoin() {
        coinsCollected++;
        Debug.Log("GameManager: Coins collected = " + coinsCollected);

        CheckWinCondition();
    }

    public void TakeDamage(int damage) {
        health -= damage;
        Debug.Log("GameManager: Health is now " + health);
        onHealthChanged?.Invoke(health);

        if (health <= 0) {
            Debug.Log("GameManager: Game Over triggered.");
            onGameOver?.Invoke();
            SceneManager.LoadScene("GameOver");
        }
    }

    public void ResetGame() {
        score = 0;
        health = 100;
        coinsCollected = 0;

        Debug.Log("GameManager: Game reset. Score=0, Health=100, Coins=0");

        onScoreChanged?.Invoke(score);
        onHealthChanged?.Invoke(health);
    }

    // Check if player has won
    private void CheckWinCondition() {
        if (score >= winScore || coinsCollected >= winCoins) {
            Debug.Log("GameManager: Player has won! Loading GameOver scene.");
            SceneManager.LoadScene("GameOver");
        }
    }
}