using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public TextMeshProUGUI finalScoreText;

    void Start() {
        if (GameManager.Instance != null)
        {
            // Display the final score from GameManager
            finalScoreText.text = "Final Score: " + GameManager.Instance.CurrentScore;
            Debug.Log("GameOverManager: Final Score displayed: " + GameManager.Instance.CurrentScore);
        }
    }

    public void TryAgain() {
        if (GameManager.Instance != null) {
            GameManager.Instance.ResetGame();
        }

        Debug.Log("GameOverManager: Restarting game...");
        SceneManager.LoadScene("GameScene");
    }
}