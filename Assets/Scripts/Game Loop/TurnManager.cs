using TMPro;
using UnityEngine;

public class TurnManager : MonoBehaviour {
    [Header("Components")]
    public PlayerController playerController;
    public GameModeManager gameModeManager;

    [Header("UI")]
    public GameObject loseMenu;
    public TMP_Text turnText;
    public string turnTextPrefix;

    [Header("Logic")]
    public int maxTurns;
    private int turn = 1;
    private bool ended = false;

    public void AdvanceTurn() {
        if (!ended) {
            if (++turn > maxTurns) {
                loseMenu.SetActive(true);
                playerController.enabled = false;
                gameModeManager.Disable();

                ended = true;
            }

            turnText.text = turnTextPrefix + turn;
        }
    }
}
