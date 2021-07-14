using UnityEngine.AI;
using UnityEngine;

public class EnemyAiFlying : MonoBehaviour
{
    [SerializeField] Transform target;
    public Transform firePoint;

    public float bulletSpeed = 10f;

    private NavMeshAgent agent;

    // Attacking
    public float timeBetweenAttacks;
    //private bool alreadyAttacked;
    public GameObject projectile;

    public LayerMask whatIsPlayer;

    // States
    public float attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private bool alreadyAttacked = false;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
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

    private void ChasePlayer()
    {
        agent.SetDestination(target.position);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        RotateTowards(target.position);

        if (!alreadyAttacked)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        float angle = Vector2.Angle(target.position, transform.position);

        GameObject bullet = Instantiate(projectile, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(-firePoint.up * bulletSpeed, ForceMode2D.Impulse);

        alreadyAttacked = true;
        Invoke(nameof(ResetAttack), timeBetweenAttacks);
    }

    private void RotateTowards(Vector2 target)
    {
        var offset = 90f;
        Vector2 direction = target - (Vector2)transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}