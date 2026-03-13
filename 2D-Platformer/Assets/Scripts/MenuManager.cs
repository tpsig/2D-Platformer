using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {
    public void LoadGame() {
        if (GameManager.Instance != null) {
            GameManager.Instance.ResetGame();
        }

        Debug.Log("MenuManager: Loading GameScene");
        SceneManager.LoadScene("GameScene");
    }
}