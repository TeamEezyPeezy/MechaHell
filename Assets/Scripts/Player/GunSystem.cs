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
            mainWeapon =  SelectWeapon("DesertEagle");
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

        GameObject bullet = Instantiate(bulletPreFab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector3 direction = firePoint.right + new Vector3(xSpread, 0, 0);
        rb.AddForce(direction * mainWeapon.bulletForce, ForceMode2D.Impulse);

        bulletsLeft--;
        bulletsToShoot--;


        // Takes care of how many bullets is being shot per tap
        if(bulletsToShoot > 0 && bulletsLeft > 0)
        {
            Invoke("Shoot", mainWeapon.timeBetweenShots);
        }

        if (bulletsToShoot == 0) {
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
