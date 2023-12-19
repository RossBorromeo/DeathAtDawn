using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCombat : MonoBehaviour
{

    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage = 50;
    public float attackRate = 2f;
    float nextAttackTime = 0f;
    public int maxHealth = 100;
   public  int currentHealth;
    public int Respawn = 0;
    private Rigidbody rb;
    private Rigidbody playerRb;
    public AudioSource src;
    public AudioClip swingWeapon;
    public AudioClip takeDamage;




// Update is called once per frame

void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        playerRb = GetComponent<Rigidbody>();   

         playerRb.isKinematic = false;
        
    }
    void Update()
    {
        if(Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
       
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
        src.clip = swingWeapon;
        src.Play();
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);
        
        

        foreach(Collider enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }


   public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Damage Taken" + damage);
        // Play hurt animation
        animator.SetTrigger("Hurt");

        src.clip = takeDamage;
        src.Play();
        if (currentHealth <= 0)
        {
            Die();
            
            
        }
    }

    void Die()
    {
        animator.SetBool("IsDead", true);
        playerRb.isKinematic = true;
        rb.velocity = Vector3.zero;
        Invoke("ResetScene", 3);

    }

    void ResetScene()
    {
        SceneManager.LoadScene(Respawn);
    }

    void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
