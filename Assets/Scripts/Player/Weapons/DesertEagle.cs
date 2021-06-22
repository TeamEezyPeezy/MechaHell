using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesertEagle : Weapon
{
    public DesertEagle()
    {
        bulletForce = 50f;
        damage = 40;
        timeBetweenShooting = 0.7f;
        spread = 0.02f;
        reloadTime = 1.5f;
        timeBetweenShots = 0.05f;
        magazineSize = 7;
        bulletsPerTap = 2;
        allowButtonHold = false;
        weaponName = "DesertEagle";
    }
   
}
