using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    public Animator animator;
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround,whatIsPlayer;
    public float health;
    private GameController gameController;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public bool alreadyAttacked;
    public float timeBetweenAttacks;
    public GameObject projectile;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if(!playerInSightRange && !playerInAttackRange) Patroling();
        if(playerInSightRange && !playerInAttackRange) ChasePlayer();
        if(playerInSightRange && playerInAttackRange) AttackPlayer();
    }

    private void Patroling(){
        Walk();
        if(!walkPointSet) SearchWalkPoint();

        if(walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if(distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;

    }

    private void SearchWalkPoint(){
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if(Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        Walk();
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        Idle();
        //Make sure enemy does not move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if(!alreadyAttacked){

            //Attack Code 
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 16f, ForceMode.Impulse);
            rb.AddForce(transform.up * 2f, ForceMode.Impulse);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), 2f);
        }
    }
    
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage){
        health -= damage;

        if (health <= 0)
        {
            gameController.enemyKilled(gameObject);
            Invoke(nameof(DestroyEnemy), 0.1f);
        }
    }

    private void DestroyEnemy(){
        Destroy(gameObject);
    }


    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
    }

    public void Idle ()
    {
        animator = GetComponent<Animator>();
        animator.SetBool ("Walk", false);
        animator.SetBool ("SprintJump", false);
        animator.SetBool ("SprintSlide", false);
    }

    public void Walk ()
    {
        animator = GetComponent<Animator>();
        animator.SetBool ("Walk", true);
        animator.SetBool ("SprintJump", false);
        animator.SetBool ("SprintSlide", false);
    }

    public void SprintJump()
    {
        animator = GetComponent<Animator>();
        animator.SetBool ("Walk", false);
        animator.SetBool ("SprintJump", true);
        animator.SetBool ("SprintSlide", false);
    }

    public void SprintSlide()
    {
        animator = GetComponent<Animator>();
        animator.SetBool ("Walk", false);
        animator.SetBool ("SprintJump", false);
        animator.SetBool ("SprintSlide", true);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerProj"))
        {
            TakeDamage(1);
            Debug.Log("Hit");
        }
    }
}
