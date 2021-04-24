using UnityEngine;
using UnityEngine.AI;

public class ChasePlayer : MonoBehaviour {
    public GameObject player;

    private NavMeshAgent agent;

    void Start() {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update() {
        agent.SetDestination(player.transform.position);
    }
}
