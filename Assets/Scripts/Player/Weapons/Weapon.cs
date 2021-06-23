using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// superclass for weapons to hold weapon spesific data
public abstract class Weapon
{
    public float bulletForce;
    public int damage;
    public float timeBetweenShooting;
    public float spread;
    public float reloadTime;
    public float timeBetweenShots;
    public int magazineSize;
    public int bulletsPerTap;
    public bool allowButtonHold;
    public string weaponName;

    public abstract void FireEffects();
}
