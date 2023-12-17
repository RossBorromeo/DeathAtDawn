using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.AI;

public class enemyController : MonoBehaviour
{
    public float lookRadius = 10f;
    Transform target;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerController.instance.player.transform; // get the player's transform
        agent = GetComponent<NavMeshAgent>(); // get the navmesh agent component

    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position); // get the distance between the player and the enemy

        if (distance <= lookRadius) // if the player is within the lookRadius
        {
            agent.SetDestination(target.position); // set the destination of the navmesh agent to the player's position
            if (distance <= agent.stoppingDistance) // if the enemy is within the stopping distance
            {
                // attack the target
                // face the target
                FaceTarget();
            }
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized; // get the direction to the target
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z)); // get the rotation to look at the target
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f); // rotate the enemy to look at the target
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
