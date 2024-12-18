using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    public PlayerController playerController;
    public GameModeManager gameModeManager;
    public GameObject panel;
    public PlaySound playSound;

    public void Pause() {
        gameModeManager.Disable();
        playerController.enabled = false;
        panel.SetActive(true);
        Cursor.visible = true;
        Time.timeScale = 0f;
        playSound.PlayClick();
    }

    public void Continue() {
        gameModeManager.enabled = true;
        playerController.enabled = true;
        panel.SetActive(false);
        Time.timeScale = 1f;
        playSound.PlayClick();
    }

    public void Quit() { 
        Continue();
        playSound.PlayClick();
        SceneManager.LoadScene(0);
    }
}
