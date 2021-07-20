using System.Collections;
using UnityEngine.AI;
using UnityEngine;

public class EnemyAiFlying : MonoBehaviour
{
    [SerializeField] Transform target;
    private NavMeshAgent agent;

    public Transform firePoint, firePoint2, firePoint3;

    public GameObject projectile;
    public LayerMask whatIsPlayer;

    public float bulletSpeed = 9f;
    public int burstBulletAmount = 3;
    public float burstSpeed = 0.2f;
    public float timeBetweenAttacks;
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

        int random = Random.Range(0, 3);

        if (!alreadyAttacked)
        {
            print(random);

            if (random == 0)
            {
                Shoot();
                alreadyAttacked = true;
            }
            else if(random == 1)
            {
                StartCoroutine(Burst());
                alreadyAttacked = true;
            } else if (random == 2)
            {
                ShootTriple();
                alreadyAttacked = true;
            }
        }
    }

    private void ShootTriple()
    {
        GameObject bullet = Instantiate(projectile, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(-firePoint.up * bulletSpeed, ForceMode2D.Impulse);

        GameObject bullet2 = Instantiate(projectile, firePoint2.position, firePoint2.rotation);
        Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
        rb2.AddForce(-firePoint2.up * bulletSpeed, ForceMode2D.Impulse);

        GameObject bullet3 = Instantiate(projectile, firePoint3.position, firePoint3.rotation);
        Rigidbody2D rb3 = bullet3.GetComponent<Rigidbody2D>();
        rb3.AddForce(-firePoint3.up * bulletSpeed, ForceMode2D.Impulse);
        Invoke(nameof(ResetAttack), timeBetweenAttacks);
    }

    public IEnumerator Burst()
    {
        for (int i = 0; i < burstBulletAmount; i++)
        {
            GameObject bullet = Instantiate(projectile, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(-firePoint.up * bulletSpeed, ForceMode2D.Impulse);

            yield return new WaitForSeconds(burstSpeed);
        }

        Invoke(nameof(ResetAttack), timeBetweenAttacks);
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(projectile, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(-firePoint.up * bulletSpeed, ForceMode2D.Impulse);

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