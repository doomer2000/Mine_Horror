using UnityEngine;
using UnityEngine.AI;

public class BlackMonster : MonoBehaviour
{
    public bool isHumanMonster;

    public GameObject mainMonster;

    public GameObject human;

    public bool isDead;

    public bool isCrying;

    public float cryDuration;

    public Transform respawnPosition;

    public PlayerController playerController;

    public NavMeshAgent agent;

    public Transform player;

    public Transform playerHead;

    private NavMeshPath path;

    public Light monsterLight;

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

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    public Animator animator;

    [Header("Sounds")]
    public AudioSource sleepSound;
    public AudioSource killSound;
    public AudioSource chaseSound;

    private void Awake()
    {
        path = new NavMeshPath();
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        var selfAnimator = GetComponent<Animator>(); ;
        if(selfAnimator != null) animator = selfAnimator;
        isSleeping = false;
        isDead = false;
        killedCount = 0;
        InvokeRepeating("SearchWalkPoint", 5, 15);
        if(isHumanMonster) InvokeRepeating("Cry", 0, 30);
        isSearchWalkPointInvoked = true;
    }

    private bool isSearchWalkPointInvoked;
    private int killedCount;

    public void Cry()
    {
        isCrying = true;
        Invoke("StopCrying", cryDuration == 0 ? 5 : cryDuration);
    }

    public void StopCrying()
    {
        isCrying = false;
    }

    public void StopWalkPointSearch()
    {
        CancelInvoke("SearchWalkPoint");
        isSearchWalkPointInvoked = false;
    }

    public void StartWalkPointSearch()
    {
        if (!isSearchWalkPointInvoked)
        {
            InvokeRepeating("SearchWalkPoint", 5, 10);
            isSearchWalkPointInvoked = true;
        }
    }

    private void Update()
    {
        if(killedCount > 2 && isHumanMonster)
        {
            isDead = true;
        }
        if (isDead)
        {
            StopWalkPointSearch();
            this.GetComponent<BlackMonster>().enabled = false;
            if(mainMonster.active) mainMonster.SetActive(false);
            if(!human.active) human.SetActive(true);
        }
        else if(isSleeping)
        {
            if (chaseSound.isPlaying) chaseSound.Stop();
            animator.Play("Sleep");
            StopWalkPointSearch();
            agent.SetDestination(transform.position);
        }
        else if(isCrying)
        {
            if (sleepSound.isPlaying) sleepSound.Stop();
            animator.Play("Cry");
            StopWalkPointSearch();
            agent.SetDestination(transform.position);
        }
        else
        {
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
            var isPlayerHidden = false;
            RaycastHit raycastHit;
            Physics.Raycast(transform.position, (playerHead.position - transform.position), out raycastHit, Mathf.Infinity);
            if (raycastHit.transform != null)
            {
                if (raycastHit.transform.tag == "Player")
                {
                    isPlayerHidden = false;
                    monsterLight.intensity = 4f;
                }
                else
                {
                    isPlayerHidden = true;
                    monsterLight.intensity = 1f;
                }
            }
            if (!playerInSightRange && !playerInAttackRange) { StartWalkPointSearch(); Patroling(); }
            else if (playerInSightRange && isPlayerHidden && !playerInAttackRange) Patroling();
            if (playerInSightRange && !isPlayerHidden && !playerInAttackRange) { 
                Invoke("ChasePlayer", 3);
            }
            if (playerInAttackRange && playerInSightRange) KillPlayer();
        }
        
    }

    public void TurnHuman()
    {
        StopWalkPointSearch();
        this.GetComponent<BlackMonster>().enabled = false;
        if (mainMonster.active) mainMonster.SetActive(false);
        if (!human.active) human.SetActive(true);
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
        if(!chaseSound.isPlaying) chaseSound.Play();
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
        playerController.killedBy = this;
    }

    public void TakeDamage(int damage)
    {
        if (!isCrying && !isDead)
        {
            health -= damage;

            if (health <= 0)
            {
                killedCount += 1;
                Debug.Log(killedCount);
                SetSleep(2f);
            }
        }
    }

    public void SetSleep(float startAfterSec)
    {
        Invoke(nameof(BlackMonsterSleep), startAfterSec);
        Invoke(nameof(BlackMonsterAwake), 15f);
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