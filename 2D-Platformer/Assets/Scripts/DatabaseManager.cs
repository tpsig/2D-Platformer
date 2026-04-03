using UnityEngine;
using SQLite;
using System.IO;
using System.Collections.Generic;
using SQLitePCL;

public class HighScore {
    public int id { get; set; }
    public string playerName { get; set; }
    public int score { get; set; }
    public float completionTime { get; set; }
}

public class DatabaseManager : MonoBehaviour {
    public static DatabaseManager Instance { get; private set; }

    private string dbPath;
    private SQLiteConnection dbConnection;

    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Batteries_V2.Init();
        SetDatabasePath();
        InitializeDatabase();
    }

    private void SetDatabasePath() {
        dbPath = Path.Combine(Application.persistentDataPath, "gamedata.db");
        Debug.Log("[DatabaseManager] Database Path: " + dbPath);
    }

    private void InitializeDatabase() {
        if (dbConnection != null) return;

        try {
            dbConnection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
            CreateHighScoresTable();
            Debug.Log("[DatabaseManager] Database initialized successfully.");
        }
        catch (System.Exception ex) {
            Debug.LogError("[DatabaseManager] Failed to initialize database: " + ex);
        }
    }

    private void CreateHighScoresTable() {
        if (dbConnection == null) return;

        string query = @"
            CREATE TABLE IF NOT EXISTS HighScores (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                playerName TEXT,
                score INTEGER,
                completionTime REAL
            );
        ";

        dbConnection.Execute(query);
        Debug.Log("[DatabaseManager] HighScores table ready.");
    }

    public void SaveHighScore(string playerName, int score, float completionTime) {
        if (dbConnection == null) {
            Debug.LogError("[DatabaseManager] Cannot save: database not initialized!");
            return;
        }

        dbConnection.Execute(
            "INSERT INTO HighScores (playerName, score, completionTime) VALUES (?, ?, ?);",
            playerName, score, completionTime
        );

        Debug.Log($"[DatabaseManager] Saved Score: {playerName} | {score} | {completionTime}");
    }

    public List<HighScore> GetTopHighScores(int limit = 5) {
        if (dbConnection == null) {
            Debug.LogError("[DatabaseManager] Cannot retrieve scores: database not initialized!");
            return new List<HighScore>();
        }

        return dbConnection.Query<HighScore>(
            $"SELECT * FROM HighScores ORDER BY score DESC LIMIT {limit};"
        );
    }

    private void OnApplicationQuit() {
        if (dbConnection != null) {
            dbConnection.Close();
            dbConnection = null;
            Debug.Log("[DatabaseManager] Database connection closed.");
        }
    }
}