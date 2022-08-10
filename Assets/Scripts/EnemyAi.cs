using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
   public NavMeshAgent agent =null;

   public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    private Animator animator = null;

    private ZombieStats stats = null;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    public float patrolingSpeed = 2f;
    public float chaseSpeed = 5f;

    //Attacking
    private float timeOfLastAttack = 0;
    public float timeBetweenAttacks = 1.5f;
    bool alreadyAttacked;
   

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        stats = GetComponent<ZombieStats>();
    }

    private void Update()
    {
        //Check for sight and attack range.
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange)
        {
            Patroling();
            animator.SetBool("AwareToPlayer", false);
            agent.speed = patrolingSpeed;
        }

        if (playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
            animator.SetBool("AwareToPlayer", true);
            agent.speed = chaseSpeed;

        }
        if (playerInAttackRange && playerInSightRange)
        {
            animator.SetBool("Attack", true);

            timeBetweenAttacks = Time.time;
            CharacterStats playerStats = player.GetComponent<CharacterStats>();
            AttackPlayer(playerStats);
            agent.acceleration = 1;
            

            

        }
       

        if (!playerInAttackRange && playerInSightRange)
        {
            ChasePlayer();
            animator.SetBool("Attack", false);
            agent.acceleration = 4;
        }

    }

    private void Patroling()
    {
        if(!walkPointSet) SearchWalkPoint();

        if(walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;  
        
        //Walk point reached
        if(distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y,transform.position.z + randomZ);

        if(Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        transform.LookAt(player);
        
    }

    private void AttackPlayer(CharacterStats statsToDmage)
    {
        transform.LookAt(player);
        stats.DealDamage(statsToDmage);
        //Make sure enemy dosent move
        agent.SetDestination(transform.position);

        

        if(!alreadyAttacked)
        {
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
            Debug.Log("attacked player");
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked=false;
    }
}
