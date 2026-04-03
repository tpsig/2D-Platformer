using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour {
    public TextMeshProUGUI finalScoreText;
    public TMP_InputField playerNameInput;

    public Button retryButton;
    public TextMeshProUGUI retryButtonText;
    public Button backToMenuButton;

    void Start() {
        if (GameManager.Instance == null) {
            Debug.LogError("GameManager missing!");
            return;
        }

        int finalScore = GameManager.Instance.CurrentScore;
        finalScoreText.text = "Final Score: " + finalScore;

        Debug.Log("Final Score displayed: " + finalScore);

        retryButton.gameObject.SetActive(true);
        backToMenuButton.gameObject.SetActive(true);

        if (finalScore >= 100) {
            retryButtonText.text = "Play Again";
        }
        else {
            retryButtonText.text = "Try Again";
        }
    }

    public void OnSubmitScore() {
        if (GameManager.Instance == null || DatabaseManager.Instance == null) {
            Debug.LogError("Missing GameManager or DatabaseManager!");
            return;
        }

        string playerName = string.IsNullOrEmpty(playerNameInput.text) ? "Anonymous" : playerNameInput.text;
        int finalScore = GameManager.Instance.CurrentScore;
        float completionTime = Time.timeSinceLevelLoad;

        DatabaseManager.Instance.SaveHighScore(playerName, finalScore, completionTime);
        Debug.Log("Score submitted!");
    }

    public void TryAgain() {
        OnSubmitScore();
        GameManager.Instance.ResetGame();
        SceneManager.LoadScene("GameScene");
        Debug.Log("Game restarting...");
    }

    public void BackToMenu() {
        OnSubmitScore();
        SceneManager.LoadScene("MainMenu");
        Debug.Log("Returning to main menu...");
    }
}