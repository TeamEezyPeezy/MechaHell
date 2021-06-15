using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        print(gameObject.name + " Damage taken, amount: " + damage + "\n Health left: " + currentHealth);

        currentHealth -= damage;
        if (currentHealth <= 0) Invoke(nameof(DestroyEnemy), 0f);
    }

    private void DestroyEnemy()
    {
        print(gameObject.name + " Destroyed.");
        Destroy(gameObject);
    }
}