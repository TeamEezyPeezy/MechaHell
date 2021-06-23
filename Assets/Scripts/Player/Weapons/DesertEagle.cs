using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesertEagle : Weapon
{
    public DesertEagle()
    {
        bulletForce = 50f;
        timeBetweenShooting = 0.7f;
        spread = 0.02f;
        reloadTime = 1.5f;
        timeBetweenShots = 0.05f;
        magazineSize = 7;
        bulletsPerTap = 1;
        allowButtonHold = false;
        weaponName = "DesertEagle";
    }

    override public void FireEffects()
    {
        ScreenShakeController.instance.StartShake(.1f, 0.1f);
    }
   
}
