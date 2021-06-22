using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBullet : MonoBehaviour
{
   
   public GameObject explosionPreFab;
   public float explosionForce, radius;

   void OnCollisionEnter2D(Collision2D other)
   {
       Explode();
       Destroy(gameObject);
   }


   void Explode()
   {
        GameObject _explosionPreFab = Instantiate(explosionPreFab, transform.position, transform.rotation);
        Destroy(_explosionPreFab, 3);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (Collider2D nearby in colliders)
        {
            Rigidbody2D rigidbody2D = nearby.GetComponent<Rigidbody2D>();
            if(rigidbody2D)
            {
                Debug.Log("Enemy hit do something");
            }
        }
   }
}
