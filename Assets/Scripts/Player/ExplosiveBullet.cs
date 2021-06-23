using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBullet : MonoBehaviour
{
   
   public GameObject explosionPreFab;
   public float explosionForce, radius;

   public int damage = 100;

   void OnCollisionEnter2D(Collision2D other)
   {
       Explode();
       ScreenShakeController.instance.StartShake(.2f, 1f);
       Destroy(gameObject);
   }


   void Explode()
   {
        GameObject _explosionPreFab = Instantiate(explosionPreFab, transform.position, transform.rotation);
        Destroy(_explosionPreFab, 3);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (Collider2D nearby in colliders)
        {
           
            Enemy enemy = nearby.gameObject.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        
        }
   }
}
