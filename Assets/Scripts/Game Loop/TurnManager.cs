using TMPro;
using UnityEngine;

public class TurnManager : MonoBehaviour {
    [Header("Components")]
    public PlayerController playerController;
    public GameModeManager gameModeManager;

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
            if (++turn > maxTurns) {
                loseMenu.SetActive(true);
                playerController.enabled = false;
                gameModeManager.Disable();

                ended = true;
            }

            turnText.text = turnTextPrefix + turn;
            time = 0f;
        }
    }
}
