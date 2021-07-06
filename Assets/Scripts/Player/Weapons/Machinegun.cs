using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machinegun : Weapon
{
    public Machinegun()
    {
        bulletForce = 80f;
        damage = 20;
        timeBetweenShooting = 0.3f;
        spread = 0.1f;
        reloadTime = 1.5f;
        timeBetweenShots = 0.3f;
        magazineSize = 30;
        bulletsLeftWhenSwitching = 30;
        bulletsPerTap = 1;
        allowButtonHold = true; 
        weaponName = "machinegun";
        upgradeLevel = 1;
    }
    
    override public void FireEffects()
    {
        //  ScreenShakeController.instance.StartShake(.05f, 0.1f);
    }
}
