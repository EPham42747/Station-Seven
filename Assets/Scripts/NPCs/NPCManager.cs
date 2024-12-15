using UnityEngine;

public class NPCManager : MonoBehaviour {
    public GameObject astronautPrefab;
    public Transform npcParent;

    public void InstantiateNPC(Vector3 position) {
        Instantiate(astronautPrefab, position, Quaternion.identity, npcParent);
    }
}
