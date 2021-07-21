using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject bulletHitPrefab;
    public GameObject enemyHitPrefab;
    public int damage = 10;
    public bool pierceEnemy = false;

    // public GameObject hitAnimation;
   void OnCollisionEnter2D(Collision2D collision)
   {
     
     if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Bullet")) return;

        // CheckEnemyCollision(collision);
        GameObject _bulletHitPrefab = Instantiate(bulletHitPrefab, transform.position, transform.rotation);
        Destroy(_bulletHitPrefab, 1);
        Destroy(gameObject);

        // TODO When animation for bullet explosion / disappearing is done, enable these
        //GameObject animation = Instantiate(hitAnimation, transform.position, Quaternion.identity);
        //Destroy(animation, 5f);
       

   }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("triger hit");
        CheckEnemyCollision(other);
    }


   private void CheckEnemyCollision(Collider2D collision)
   {
       if (collision.gameObject.CompareTag("Enemy"))
       {
           Enemy enemy = collision.gameObject.GetComponent<Enemy>();

           if (enemy != null)
           {
                enemy.TakeDamage(damage);
                GameObject _enemyHitPrefab = Instantiate(enemyHitPrefab, transform.position, transform.rotation);
                Destroy(_enemyHitPrefab, 1);
               if(!pierceEnemy)
               {
                    Destroy(gameObject); 
                }
           }
       }
    }
}
