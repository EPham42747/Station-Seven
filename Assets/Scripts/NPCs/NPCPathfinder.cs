using UnityEngine;
using UnityEngine.AI;

public class NPCPathfinder : MonoBehaviour {
    private NavMeshAgent agent;
    
    private void Start() {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)) {
            agent.SetDestination(hit.point);
        }
    }
}
