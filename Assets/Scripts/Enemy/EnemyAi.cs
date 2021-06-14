using UnityEngine.AI;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    [SerializeField] Transform target;

    private NavMeshAgent agent;

    // Attacking
    public float timeBetweenAttacks;
    private bool alreadyAttacked;
    public GameObject projectile;

    public LayerMask whatIsPlayer;

    // States
    public float attackRange;
    public bool playerInSightRange, playerInAttackRange;

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
        print(playerInAttackRange);

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

        // ROTATE
        RotateTowards(target.position);

        print(transform.name + "Attacks player");
        /*
        if (!alreadyAttacked)
        {
            // Attack code here.
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 15f, ForceMode.Impulse);
            Physics.IgnoreCollision(rb.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
            Destroy(rb.gameObject, 5f);
            //
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        } */
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