using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public GameObject main;
    public GameObject help;

    public void Help() {
        main.SetActive(false);
        help.SetActive(true);
    }

    public void Back() {
        main.SetActive(true);
        help.SetActive(false);
    }
    
    public void StartGame() { SceneManager.LoadScene(1); }
}
