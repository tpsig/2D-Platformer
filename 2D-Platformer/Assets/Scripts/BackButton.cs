using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour {
    public void OnBackClicked() {
        SceneManager.LoadScene("MainMenu");
    }
}