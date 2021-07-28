using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Weapon
{
    public Sniper()
    {
        bulletForce = 50f;
        timeBetweenShooting = 0.7f;
        spread = 0.02f;
        reloadTime = 1.5f;
        timeBetweenShots = 0.05f;
        magazineSize = 4;
        bulletsLeftWhenSwitching = 7;
        bulletsPerTap = 1;
        allowButtonHold = false;
        weaponName = "sniper";
        upgradeLevel = 1;
    }

    override public void FireEffects()
    {
        ScreenShakeController.instance.StartShake(.1f, 0.1f);
    }
   
}
