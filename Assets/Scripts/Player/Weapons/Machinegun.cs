using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machinegun : Weapon
{
    public Machinegun()
    {
        bulletForce = 80f;
        damage = 20;
        timeBetweenShooting = 0.15f;
        spread = 0.05f;
        reloadTime = 1.5f;
        timeBetweenShots = 0.15f;
        magazineSize = 30;
        bulletsLeftWhenSwitching = 30;
        bulletsPerTap = 1;
        allowButtonHold = true; 
        weaponName = "machinegun";
    }
    
    override public void FireEffects()
    {
        //  ScreenShakeController.instance.StartShake(.05f, 0.1f);
    }
}
