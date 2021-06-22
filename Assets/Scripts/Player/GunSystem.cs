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

    public Weapon mainWeapon;

    int bulletsLeft, bulletsToShoot;

    bool shooting, readyToShoot, reloading;

    void Awake()
    {
        mainWeapon = SelectWeapon("Ak47");
        UpdateWeaponInfo();
        readyToShoot = true;
        reloadInfo.SetText("");
        ammoInfo.SetText(bulletsLeft + " / " + mainWeapon.magazineSize);

    }

    void UpdateWeaponInfo()
    {
        bulletsLeft = mainWeapon.magazineSize;
        ammoInfo.SetText(bulletsLeft + " / " + mainWeapon.magazineSize);

    }

    // Update is called once per frame
    void Update()
    {
        UpdateInputs();
    }

    void UpdateInputs()
    {
        // shooting inputs
        if(mainWeapon.allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        // reload inputs
        if(Input.GetKeyDown(KeyCode.R) && bulletsLeft < mainWeapon.magazineSize && !reloading) Reload();

        // test weapon swap
        if(Input.GetKeyDown(KeyCode.Q))
        {
            // yes very bad until i figure out what we gna do with these:D
            if(mainWeapon.weaponName == "Ak47"){
                mainWeapon = SelectWeapon("DesertEagle");
            } else {
                mainWeapon = SelectWeapon("Ak47");
            }
            UpdateWeaponInfo();
            
        } 

        // check if shooting was allowed
        if(readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {   
            bulletsToShoot = mainWeapon.bulletsPerTap;
            Shoot();
            ammoInfo.SetText(bulletsLeft + " / " + mainWeapon.magazineSize);
        }

    }
    void Shoot()
    {
        readyToShoot = false;
        float xSpread = Random.Range(-mainWeapon.spread, mainWeapon.spread);
        float ySpread = Random.Range(-mainWeapon.spread, mainWeapon.spread);

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
        rb.AddForce(direction * mainWeapon.bulletForce, ForceMode2D.Impulse);

        Destroy(bullet, 3f);

        bulletsLeft--;
        bulletsToShoot--;


        // Takes care of how many bullets is being shot per tap
        if(bulletsToShoot > 0 && bulletsLeft > 0)
        {
            Invoke("Shoot", mainWeapon.timeBetweenShots);
        } else  {
            Invoke("ResetShot", mainWeapon.timeBetweenShooting);
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
        Invoke("ReloadingFinished", mainWeapon.reloadTime);
    }
    void ReloadingFinished()
    {
        bulletsLeft = mainWeapon.magazineSize;
        reloading = false;
        reloadInfo.SetText("");
        UpdateWeaponInfo();
    }

    Weapon SelectWeapon(string weaponName)
    {
        switch(weaponName)
        {
            case "DesertEagle" : return new DesertEagle();
            case "Ak47" : return new Ak47();

            // TODO maybe implement something else since this can cause bugs with typos
            default : return new Ak47();
        }
    }
}
