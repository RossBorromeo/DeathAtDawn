using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    public int maxHealth = 100;
    int currentHealth;
    public Animator animator;
    [SerializeField] float lookRadius, attackRange;
    bool playerInRange , playerInAttackRange;
    Transform target;
    NavMeshAgent agent;
    [SerializeField]LayerMask groundLayer , playerLayer;
    //patrol
    Vector3 destPoint;
    bool walkPointSet;
    [SerializeField]float walkPointRange = 10f;
    BoxCollider boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        target = PlayerController.instance.player.transform; // get the player's transform
        agent = GetComponent<NavMeshAgent>(); // get the navmesh agent component
        animator = GetComponent<Animator>();
        boxCollider = GetComponentInChildren<BoxCollider>();
    }
    public void Update()
    {

        playerInRange = Physics.CheckSphere(transform.position, lookRadius, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);


        if (!playerInRange && !playerInAttackRange)
        {
                Patrol();
        }
        if (playerInRange && !playerInAttackRange)
        {
            Chase();
        }
        
        if (playerInAttackRange && playerInRange)
        {
            Attack();
        }

       

    }

    void Patrol()
    {
        if(!walkPointSet)
        {
            SearchWalkPoint();
        }
        if(walkPointSet)
        {
            agent.SetDestination(destPoint);
        }
        if(Vector3.Distance(transform.position, destPoint) < 1f)
        {
            walkPointSet = false;
        }
    }


    void Attack()
    {
        
        if(!animator.GetCurrentAnimatorStateInfo(0).IsName("PunchRight 0"))
        {
            animator.SetTrigger("Attack");
        agent.SetDestination(transform.position);
        }

        if (currentHealth > 0)
        {
            float distance = Vector3.Distance(target.position, transform.position); // get the distance between the player and the enemy
            agent.SetDestination(target.position); // set the destination of the navmesh agent to the player's position
            if (distance <= agent.stoppingDistance) // if the enemy is within the stopping distance
            {
                // attack the target
                // face the target
                FaceTarget();
            }
        }
    }

    void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        destPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if(Physics.Raycast(destPoint, Vector3.down, groundLayer))
        {
            walkPointSet = true;
        }
    }


    void Chase()
    {
        if (currentHealth > 0)
        {
            
            agent.SetDestination(target.position); // set the destination of the navmesh agent to the player's position
               
         }


     }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Play hurt animation
        animator.SetTrigger("Hurt");
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized; // get the direction to the target
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z)); // get the rotation to look at the target
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f); // rotate the enemy to look at the target
    }

    void Die()
    {
        // Die animation

        Debug.Log("Enemy died!");
        animator.SetBool("IsDead", true);

        GetComponent<BoxCollider>().enabled = false;
        this.enabled = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    void EnableAttack()
    {
        boxCollider.enabled = true;
    }

    void DisableAttack()
    {
        boxCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerController>();

        if (player != null)
        {
            print("Player hit");
        }
    }

}
