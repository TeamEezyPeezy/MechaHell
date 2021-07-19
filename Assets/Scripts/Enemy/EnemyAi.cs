using System;
using UnityEngine.AI;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyAi : MonoBehaviour
{
    public Transform target;
    public Animator animator;
    private NavMeshAgent agent;
    public GameObject projectile;
    public LayerMask whatIsPlayer;

    private Timer timer;
    private Timer sprintTimer;

    // Sprint
    private float agentNormalSpeed;
    private float agentNormalAccel;
    public float minTimeSprint, maxTimeSprint;
    private float randomTimeToSprint;
    private bool isSprinting = false;
    public float sprintDuration = 1f;
    public float boostPercentage;

    public float timeBetweenAttacks;
    public float rotateSpeed;
    public float attackRange;
    public bool playerInSightRange, playerInAttackRange;

    public float mySpeed = 10f;
    public float myAccel = 6f;

    private float myVelocity;
    private bool alreadyAttacked = false;

    private void Awake()
    {
        timer = gameObject.AddComponent<Timer>();
        sprintTimer = gameObject.AddComponent<Timer>();
    }

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agentNormalSpeed = agent.speed;
        agentNormalAccel = agent.acceleration;
        StartSprintRandomTime();
    }

    void StartSprintRandomTime()
    {
        randomTimeToSprint = Random.Range(minTimeSprint, maxTimeSprint);
        timer.Run(randomTimeToSprint);
    }

    void Update()
    {
        agent.speed = mySpeed;
        agent.acceleration = myAccel;

        myVelocity = agent.velocity.magnitude;

        animator.SetFloat("Velocity", myVelocity);

        playerInAttackRange = CheckAttackRange(transform, target);

        if (!playerInAttackRange) ChasePlayer();
        if (playerInAttackRange) AttackPlayer();
    }

    bool CheckAttackRange(Transform me, Transform target)
    {
        float dist = Vector2.Distance(me.position, target.position);
        if (dist < attackRange) return true;

        return false;
    }

    private void Sprinting()
    {
        if (timer.IsComplete && !isSprinting)
        {
            isSprinting = true;
            sprintTimer.Run(sprintDuration);
        }

        if (isSprinting && !sprintTimer.IsComplete)
        {
            mySpeed = agentNormalSpeed * (1 + (boostPercentage / 100f));
            myAccel = agentNormalAccel * (1 + (boostPercentage / 100f));
        }
        else
        {
            sprintTimer.Stop();
            agent.speed = agentNormalSpeed;
            StartSprintRandomTime();
            isSprinting = false;
        }
    }

    private void ChasePlayer()
    {
        Sprinting();

        if (target != null)
        {
            RotateTowards(target.position);
            agent.SetDestination(target.position);
        }
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        RotateTowards(target.position);

        if (!alreadyAttacked)
        {
            animator.SetTrigger("Attack");

            alreadyAttacked = true;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRange);
            foreach (Collider2D nearby in colliders)
            {
                Player player = nearby.gameObject.GetComponent<Player>();

                if (player != null)
                {                                                                   
                    player.TakeDamage(5);
                    Debug.Log("Player took damage");
                }
            }
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void RotateTowards(Vector2 target)
    {
        var offset = 90f;
        Vector2 direction = target - (Vector2)transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Quaternion currentRotation = transform.rotation;
        Quaternion nextRotation = Quaternion.Euler(Vector3.forward * (angle + offset));
        transform.rotation = Quaternion.Lerp(currentRotation, nextRotation, Time.time * (rotateSpeed / 100));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}