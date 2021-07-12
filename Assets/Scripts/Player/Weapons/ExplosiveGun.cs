using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExplosiveGun : MonoBehaviour
{
    public GameObject explosiveBulletPreFab;
    public Transform firePoint;
    public float bulletForce = 20f;
    public float cooldown;
    public TextMeshProUGUI cooldownInfo;
    public AudioSource BazookaAudioSource;

    float shootTime;


    public Animator animator;       //karin

    bool readyToShoot = true;

    void Update()
    {
        if(Input.GetMouseButtonDown(1)){
            Fire();
        }
        HandleCooldowns();
    }
    public void Fire()
    {
        if(readyToShoot)
        {
            readyToShoot = false;
            // Invoke("ResetGun", cooldown);
  
            Vector3 sp = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 direction = (Input.mousePosition - sp).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion bulletRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            GameObject bullet = Instantiate(explosiveBulletPreFab, firePoint.position, bulletRotation);

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(direction * bulletForce, ForceMode2D.Impulse);

            ScreenShakeController.instance.StartShake(.1f, 0.2f);

            animator.SetTrigger("shootingBazooka");

            BazookaAudioSource.Play();

            shootTime = Time.time;

            Destroy(bullet, 3f);
        }
  
    }

    void HandleCooldowns()
    {
        if(!readyToShoot)
        {
            cooldownInfo.SetText((int)cooldown - ((int)(Time.time - shootTime)) + "s");
            if(Time.time - shootTime > cooldown)
            {
                cooldownInfo.SetText("");
                ResetGun();
            }
        }
    }
    public void ResetGun()
    {
        readyToShoot = true;
    }
}
