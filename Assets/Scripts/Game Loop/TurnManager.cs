using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TurnManager : MonoBehaviour {
    [Header("Components")]
    public PlayerController playerController;
    public GameModeManager gameModeManager;
    public ResourceManager resourceManager;

    [Header("Turn UI")]
    public GameObject loseMenu;
    public TMP_Text turnText;
    public string turnTextPrefix;

    [Header("Logic")]
    public int maxTurns;
    private int turn = 1;
    private bool ended = false;

    [Header("Timer")]
    public float maxTime;
    public TMP_Text timeText;
    private float time = 0f;

    private void Update() {
        time += Time.deltaTime;
        if (time >= maxTime) {
            AdvanceTurn();
            time = 0f;
        }
        
        timeText.text = (Mathf.Floor(maxTime) - Mathf.Floor(time)).ToString();
    }

    public void AdvanceTurn() {
        if (!ended) {
            if (++turn > maxTurns || resourceManager.TooLongAtDeficit()) {
                loseMenu.SetActive(true);
                playerController.enabled = false;
                gameModeManager.Disable();

                ended = true;
            }

            turnText.text = turnTextPrefix + turn;
            time = 0f;
            resourceManager.UpdateResources();
        }
    }

    public void Quit() { SceneManager.LoadScene(0); }
    public void Restart() { SceneManager.LoadScene(1); }
}
