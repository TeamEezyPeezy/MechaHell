using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesertEagle : Weapon
{
    public DesertEagle()
    {
        bulletForce = 30f;
        damage = 40;
        timeBetweenShooting = 0.7f;
        spread = 0.1f;
        reloadTime = 1.5f;
        timeBetweenShots = 0.7f;
        magazineSize = 7;
        bulletsPerTap = 1;
        allowButtonHold = false; 
    }
   
}
