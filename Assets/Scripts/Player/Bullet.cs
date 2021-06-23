using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 10;

    // public GameObject hitAnimation;
   void OnCollisionEnter2D(Collision2D collision)
   {
     
     if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Bullet")) return;
        Debug.Log(collision);


        CheckEnemyCollision(collision);
        Destroy(gameObject);
        // TODO When animation for bullet explosion / disappearing is done, enable these
        //GameObject animation = Instantiate(hitAnimation, transform.position, Quaternion.identity);
        //Destroy(animation, 5f);
       

   }



   private void CheckEnemyCollision(Collision2D collision)
   {
       if (collision.gameObject.CompareTag("Enemy"))
       {
           Enemy enemy = collision.gameObject.GetComponent<Enemy>();

           if (enemy != null)
           {
               enemy.TakeDamage(damage);
           }
       }
    }
}
