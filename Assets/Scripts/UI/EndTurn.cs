using UnityEngine;

public class EndTurn : MonoBehaviour {
    public TurnManager turnManager;
    public void AdvanceTurn() { turnManager.AdvanceTurn(); }
}
