using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePoint : MonoBehaviour
{
  
    public GameObject infoText;
    public GunSystem gunSystem;
    Sniper sniperReference;
    Machinegun machinegunReference;

    bool isActive = false;
    bool playerClose = false;

    void Awake()
    {
        sniperReference = gunSystem.sniper;
        machinegunReference = gunSystem.machinegun;
    }
    void Update()
    {
        UpdateInputs();
    }

    void UpdateInputs()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(!isActive)
            {
                isActive = true;
                OpenPanel();  
            } else {
                isActive = false;
                ClosePanel();
            }
        }
 
    }

    void OpenPanel()
    {
        infoText.SetActive(false);
        Debug.Log("panel open");
        if(machinegunReference.upgradeLevel < 3)
        {
            // test for upgrades
            machinegunReference.magazineSize += 5;
            machinegunReference.upgradeLevel++;
            gunSystem.UpdateWeaponInfo();
        }
    }

    void ClosePanel()
    {
        infoText.SetActive(true);
        Debug.Log("panel close");
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        infoText.SetActive(true);
        playerClose = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        infoText.SetActive(false);
        playerClose = false;
    }
}
