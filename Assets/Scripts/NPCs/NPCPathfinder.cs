using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCPathfinder : MonoBehaviour {
    private BuildingGridManager buildingGridManager;
    public Animator animator;
    private NavMeshAgent agent;

    public float stopThreshold;

    public float minCooldown;
    public float maxCooldown;
    private float cooldown;
    private float time;

    private int index;
    private int lastIndex;

    private void Start() {
        buildingGridManager = FindObjectOfType<BuildingGridManager>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update() {
        int buildings = buildingGridManager.GetBuildingList().Count;

        if (time > cooldown && buildings > 0) {
            do {
                index = (int) Random.Range(0f, buildings - 0.0001f);
            }
            while (buildings > 1 && lastIndex == index);
            agent.SetDestination(GetDestination(index));

            cooldown = Random.Range(minCooldown, maxCooldown);
            time = 0f;
            lastIndex = index;
        }

        if (agent.velocity.magnitude <= stopThreshold) {
            time += Time.deltaTime;
            animator.SetBool("Walking", false);
        }
        else animator.SetBool("Walking", true);
    }

    private Vector3 GetDestination(int index) {
        List<(PlaceableObject, Vector2Int)> buildings = buildingGridManager.GetBuildingList();
        return new Vector3(buildings[index].Item2.x, 8f, buildings[index].Item2.y);
    }
}
