using System;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int damage = 10;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 5f);        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();
            player.TakeDamage(damage);
        }

        Destroy(this.gameObject);
    }
}
