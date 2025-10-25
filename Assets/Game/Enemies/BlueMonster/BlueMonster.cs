using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BlueMonster : MonoBehaviour
{
    public Transform respawnPosition;

    public PlayerController playerController;

    public NavMeshAgent agent;

    public Transform player;

    private NavMeshPath path;

    public float startHealth;

    private bool isSleeping;
    private bool isInAction;

    public float walkSpeed;
    public float chaseSpeed;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health;

    private float oldX;
    private float oldY;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private Animator animator;

    [Header("Sounds")]
    public AudioSource sleepSound;
    public AudioSource killSound;
    public AudioSource chaseSound;

    private void Awake()
    {
        path = new NavMeshPath();
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        isSleeping = false;
        InvokeRepeating("SearchWalkPoint", 5, 15);
    }

    private void Update()
    {
        if (isSleeping)
        {
            if (chaseSound.isPlaying) chaseSound.Stop();
            animator.Play("Sleep");
            CancelInvoke("SearchWalkPoint");
            agent.SetDestination(transform.position);
        }
        else
        {
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            if (!playerInSightRange && !playerInAttackRange) Patroling();
            if (playerInSightRange && !playerInAttackRange)
            {
                transform.LookAt(player);
                Invoke("ChasePlayer", 3);
            }
            if (playerInAttackRange && playerInSightRange) KillPlayer();
        }

    }

    private void Patroling()
    {
        if (chaseSound.isPlaying)
        {
            chaseSound.Stop();
        }

        if (!walkPointSet)
        {
            SearchWalkPoint();
        }

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }
        animator.SetBool("IsWalking", true);

        agent.speed = walkSpeed;

        var distanceToWalkPoint = Vector3.Distance(walkPoint, transform.position);

        if (distanceToWalkPoint < 8f || oldX == transform.position.x && oldY == transform.position.y)
            walkPointSet = false;

        oldX = transform.position.x;
        oldY = transform.position.y;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        if (!chaseSound.isPlaying) chaseSound.Play();
        animator.SetBool("IsChasing", true);
        agent.SetDestination(player.position);
        agent.speed = chaseSpeed;
    }

    private void KillPlayer()
    {
        if (!killSound.isPlaying) killSound.Play();
        animator.Play("BlackMonster_PlayerKill");
        playerController.KillByBlackMonsterAnimationPlay();
        playerController.KillPlayer(2f);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            animator.Play("BlackMonster_KilledByPlayer");
            Invoke(nameof(BlackMonsterSleep), 2f);
            Invoke(nameof(BlackMonsterAwake), 12f);
        }
    }

    private void BlackMonsterSleep()
    {
        if (!sleepSound.isPlaying) sleepSound.Play();
        if (chaseSound.isPlaying) chaseSound.Stop();
        gameObject.isStatic = true;
        isSleeping = true;
    }

    private void BlackMonsterAwake()
    {
        if (sleepSound.isPlaying) sleepSound.Stop();
        gameObject.isStatic = false;
        isSleeping = false;
        health = startHealth * 2f;
        InvokeRepeating("SearchWalkPoint", 5, 15);
    }

    private void SetAction(bool isOnAction)
    {
        isInAction = isOnAction;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
