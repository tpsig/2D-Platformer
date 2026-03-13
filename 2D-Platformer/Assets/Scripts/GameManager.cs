using UnityEngine;
using System;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }

    public event Action<int> onScoreChanged;
    public event Action<int> onHealthChanged;
    public event Action onGameOver;

    private int score = 0;
    private int health = 100;

    public int CurrentScore => score;
    public int CurrentHealth => health;

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
    }

    public void TakeDamage(int damage) {
        health -= damage;
        Debug.Log("GameManager: Health is now " + health);
        onHealthChanged?.Invoke(health);

        if (health <= 0) {
            Debug.Log("GameManager: Game Over triggered.");
            onGameOver?.Invoke();
        }
    }

    public void ResetGame() {
        score = 0;
        health = 100;
        Debug.Log("GameManager: Game reset. Score=0, Health=100");

        onScoreChanged?.Invoke(score);
        onHealthChanged?.Invoke(health);
    }
}