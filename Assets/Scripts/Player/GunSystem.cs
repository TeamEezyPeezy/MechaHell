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

    [Header("Gun options")]
    public float bulletForce = 20f;
    public int damage;
    public float timeBetweenShooting, spread, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsToShoot;


    bool shooting, readyToShoot, reloading;

    void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
        reloadInfo.SetText("");
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInputs();
        ammoInfo.SetText(bulletsLeft + " / " + magazineSize);
    }

    void UpdateInputs()
    {
        if(allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if(Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();

        if(readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {   
            bulletsToShoot = bulletsPerTap;
            Shoot();
        }

    }
    void Shoot()
    {
        readyToShoot = false;

        float xSpread = Random.Range(-spread, spread);

        GameObject bullet = Instantiate(bulletPreFab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector3 direction = firePoint.right + new Vector3(xSpread, 0, 0);
        rb.AddForce(direction * bulletForce, ForceMode2D.Impulse);

        bulletsLeft--;
        bulletsToShoot--;


        // Takes care of how many bullets is being shot per tap
        if(bulletsToShoot > 0 && bulletsLeft > 0)
        {
            Invoke("Shoot", timeBetweenShots);
        }

        if (bulletsToShoot == 0) {
            Invoke("ResetShot", timeBetweenShooting);
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
        Invoke("ReloadingFinished", reloadTime);
    }
    void ReloadingFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
        reloadInfo.SetText("");
    }
}
