using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveGun : MonoBehaviour
{
    public GameObject explosiveBulletPreFab;
    public Transform firePoint;
    public float bulletForce = 20f;

    public Animator animator;       //karin

    bool readyToShoot = true;


    void Awake()
    {
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        if(Input.GetMouseButtonDown(1)){
            Fire();
            animator.SetTrigger("isShootingBazooka");    //karin
        }
    }
    public void Fire()
    {
        if(readyToShoot)
        {
            readyToShoot = false;
            Invoke("ResetGun", 2f);
  
            Vector3 sp = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 direction = (Input.mousePosition - sp).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion bulletRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            GameObject bullet = Instantiate(explosiveBulletPreFab, firePoint.position, bulletRotation);

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(direction * bulletForce, ForceMode2D.Impulse);

            ScreenShakeController.instance.StartShake(.1f, 0.2f);

            Destroy(bullet, 3f);
            animator.ResetTrigger("isShootingBazooka");    //karin
        }
  
    }
    public void ResetGun()
    {
        readyToShoot = true;
    }
}
