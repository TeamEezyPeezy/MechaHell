using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ak47 : Weapon
{
    public Ak47()
    {
        bulletForce = 20f;
        damage = 20;
        timeBetweenShooting = 0.15f;
        spread = 0.1f;
        reloadTime = 1.5f;
        timeBetweenShots = 0.15f;
        magazineSize = 30;
        bulletsPerTap = 1;
        allowButtonHold = true; 
        weaponName = "Ak47";
    }
   
}
