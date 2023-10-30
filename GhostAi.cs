using UnityEngine;
using UnityEngine.AI;

public class GhostAI : MonoBehaviour
{
    public Transform player;
    public GameObject safeZone;
    public GameObject safeZone2;
    public GameObject unsafeZone;
    public float ghostSpeed = 1f;
    public float stoppingDistance = 2f;
    private NavMeshAgent agent;
    private bool isChasing = true;
    private float randomMoveInterval = 15.0f;
    private float nextRandomMoveTime = 0f;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = stoppingDistance; // Adjust the stopping distance as needed
        agent.speed = ghostSpeed; // Adjust the speed as needed

        // Set the initial time for the next random move
        nextRandomMoveTime = Time.time + randomMoveInterval;
    }

    private void Update()
    {
        if (player != null && safeZone != null && safeZone2 != null && unsafeZone != null)
        {
            if (!IsInsideSafeZone() && !IsInsideSafeZone2() && isChasing)
            {
                // Chasing the player outside the safe zone
                agent.SetDestination(player.position);
            }
            else if (IsInsideSafeZone() || IsInsideSafeZone2())
            {
                // Moving randomly inside the unsafe zone when the player is in the safe zone
                if (Time.time >= nextRandomMoveTime)
                {
                    Vector3 randomDestination = GetRandomDestinationInsideUnsafeZone();
                    agent.SetDestination(randomDestination);
                    nextRandomMoveTime = Time.time + randomMoveInterval;
                }
            }
        }
    }

    private Vector3 GetRandomDestinationInsideUnsafeZone()
    {

        Vector3 unsafeZonePosition = unsafeZone.transform.position;
        float unsafeZoneRadius = unsafeZone.transform.localScale.x / 2;
        Vector3 randomDirection = Random.insideUnitSphere * unsafeZoneRadius; 
        randomDirection += unsafeZonePosition;

        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, unsafeZoneRadius, 1);
        return hit.position;
    }

    private bool IsInsideSafeZone()
    {
        return Vector3.Distance(player.position, safeZone.transform.position) <= safeZone.transform.localScale.x / 2;
    }
    private bool IsInsideSafeZone2()
    {
        return Vector3.Distance(player.position, safeZone2.transform.position) <= safeZone2.transform.localScale.x / 2;
    }

    public void SetChasing(bool shouldChase)
    {
        isChasing = shouldChase;
    }
}