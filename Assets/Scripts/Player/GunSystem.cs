using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunSystem : MonoBehaviour
{
 
 
    public Transform firePoint;
    public GameObject bulletPreFab;
    public TextMeshProUGUI ammoInfo;
    public TextMeshProUGUI reloadInfo;

    public Weapon currentWeapon;
    Sniper sniper;
    Machinegun machinegun;

    public GameObject machinegunBullet;
    public GameObject sniperBullet;
    public AudioSource machineGunAudioSource;
    public AudioSource sniperAudioSource;
    public AudioSource reloadAudioSource;
    int bulletsLeft, bulletsToShoot;

    bool shooting, readyToShoot, reloading;

    void Awake()
    {
        sniper = new Sniper();
        machinegun = new Machinegun();
        currentWeapon = SelectWeapon("default"); // use default on load, to not update bulletsleft before game starts
        UpdateWeaponInfo();
        readyToShoot = true;
        reloadInfo.SetText("");

    }

    void UpdateWeaponInfo()
    {
        bulletsLeft = currentWeapon.magazineSize;
        ammoInfo.SetText(bulletsLeft + " / " + currentWeapon.magazineSize);
    }
     void UpdateInfoAfterWeaponSwitch()
    {
        bulletsLeft = currentWeapon.bulletsLeftWhenSwitching;
        ammoInfo.SetText(bulletsLeft + " / " + currentWeapon.magazineSize);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInputs();
    }

    void UpdateInputs()
    {
        // shooting inputs
        if(currentWeapon.allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        // reload inputs
        if(Input.GetKeyDown(KeyCode.R) && bulletsLeft < currentWeapon.magazineSize && !reloading) Reload();

        // test weapon swap
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if(currentWeapon.weaponName == "machinegun"){
                currentWeapon = SelectWeapon("sniper");
            } else {
                currentWeapon = SelectWeapon("machinegun");
            }
            UpdateInfoAfterWeaponSwitch();
            
        } 

        // check if shooting was allowed
        if(readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {   
            bulletsToShoot = currentWeapon.bulletsPerTap;
            Shoot();

            ammoInfo.SetText(bulletsLeft + " / " + currentWeapon.magazineSize);
        }

    }
    void Shoot()
    {
        readyToShoot = false;
        float xSpread = Random.Range(-currentWeapon.spread, currentWeapon.spread);
        float ySpread = Random.Range(-currentWeapon.spread, currentWeapon.spread);

        Vector3 sp = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 direction = (Input.mousePosition - sp).normalized;
        direction.x += xSpread;
        direction.y += ySpread;
        direction.Normalize();
        
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion bulletRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        
        // Instantiate the bullet using our new rotation
        GameObject bullet = Instantiate(bulletPreFab, firePoint.position, bulletRotation);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(direction * currentWeapon.bulletForce, ForceMode2D.Impulse);
        currentWeapon.FireEffects();
        PlayGunSound();

        Destroy(bullet, 3f);

        bulletsLeft--;
        bulletsToShoot--;


        // Takes care of how many bullets is being shot per tap
        if(bulletsToShoot > 0 && bulletsLeft > 0)
        {
            Invoke("Shoot", currentWeapon.timeBetweenShots);
        } else  {
            Invoke("ResetShot", currentWeapon.timeBetweenShooting);
        }

        if(bulletsLeft == 0)
        {
            SuggestReload();
        }
    
    }

    void SuggestReload()
    {
        reloadInfo.SetText("Press R to reload!");
    }
    void ResetShot()
    {
        readyToShoot = true;
    }

    void Reload()
    {
        reloadInfo.SetText("Reloading..");
        reloading = true;
        Invoke("ReloadingFinished", currentWeapon.reloadTime);
        reloadAudioSource.Play();
    }
    void ReloadingFinished()
    {
        bulletsLeft = currentWeapon.magazineSize;
        reloading = false;
        reloadInfo.SetText("");
        UpdateWeaponInfo();
    }

    void PlayGunSound()
    {
        switch(currentWeapon.weaponName)
        {
            case "machinegun" : 
                machineGunAudioSource.Play();
                break;
            case "sniper" :
                sniperAudioSource.Play();
                break;
            default : break;
        }
    }

    Weapon SelectWeapon(string weaponName)
    {
        switch(weaponName)
        {
            case "sniper" : 
                bulletPreFab = sniperBullet;
                machinegun.bulletsLeftWhenSwitching = bulletsLeft;
                return sniper;
            case "machinegun" :
                bulletPreFab = machinegunBullet;
                sniper.bulletsLeftWhenSwitching = bulletsLeft;
                return machinegun;

            // TODO maybe implement something else since this can cause bugs with typos
            default :
                bulletPreFab = machinegunBullet;
                return machinegun;
        }
    }
}
