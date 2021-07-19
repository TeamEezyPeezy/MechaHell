using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameManager gameManager;
    public int maxHealth;
    private int currentHealth;

    public GameObject keyCardDrop;

    private void Awake()
    {
        gameManager = GameManager.Instance;
    }

    void Start()
    {
        currentHealth = maxHealth;
        gameManager.EnemySpawned();
    }

    public void TakeDamage(int damage)
    {
        //print(gameObject.name + " Damage taken, amount: " + damage + "\n Health left: " + currentHealth);

        currentHealth -= damage;
        if (currentHealth <= 0) Invoke(nameof(DestroyEnemy), 0f);
    }

    private void DestroyEnemy()
    {
        // print(gameObject.name + " Destroyed.");
        gameManager.EnemyDied();

        if (gameManager.CanDropKeycard)
        {
            print("Dropping keycard...");
            gameManager.lastKeyDropWave = gameManager.WaveNumber;
            GameObject keycard = Instantiate(keyCardDrop, transform.position, Quaternion.identity);
        }

        Destroy(this.gameObject);
    }
}