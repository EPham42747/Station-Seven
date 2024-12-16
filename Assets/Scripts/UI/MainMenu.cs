using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public GameObject main;
    public GameObject help;
    public PlaySound playSound;

    public void Help() {
        main.SetActive(false);
        help.SetActive(true);
        playSound.PlayClick();
    }

    public void Back() {
        main.SetActive(true);
        help.SetActive(false);
        playSound.PlayClick();
    }
    
    public void StartGame() {
        playSound.PlayClick();
        SceneManager.LoadScene(1);
    }
}
