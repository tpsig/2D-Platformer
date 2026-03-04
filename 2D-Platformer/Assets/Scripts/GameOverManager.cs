using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public TextMeshProUGUI finalScoreText;

    void Start() {
        // Retrieve saved score
        int finalScore = PlayerPrefs.GetInt("FinalScore", 0);

        // Display score
        finalScoreText.text = "Final Score: " + finalScore;
    }

    public void TryAgain() {
        SceneManager.LoadScene("GameScene");
    }
}