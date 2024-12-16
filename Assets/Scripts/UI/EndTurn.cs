using UnityEngine;

public class EndTurn : MonoBehaviour {
    public TurnManager turnManager;
    public PlaySound playSound;

    public void AdvanceTurn() {
        turnManager.AdvanceTurn();
        playSound.PlayClick();
    }
}
