using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NPCPathfinder))]
[RequireComponent(typeof(NavMeshAgent))]
public class AstronautAnimationController : MonoBehaviour {
    public Animator animator;
    private NavMeshAgent agent;

    private float stopThreshold;

    private void Start() {
        agent = GetComponent<NavMeshAgent>();
        stopThreshold = GetComponent<NPCPathfinder>().stopThreshold;
    }

    private void Update() {
        if (agent.velocity.magnitude <= stopThreshold) animator.SetBool("Walking", false);
        else animator.SetBool("Walking", true);
    }
}
