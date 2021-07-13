using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MachineGunUpgradeCenter : MonoBehaviour
{

    public TextMeshProUGUI upgradeInfo;
    public GunSystem gunSystem;
    public int upgradeCost = 200;
    GameManager gameManager;
    Machinegun machinegunReference;
    GameObject textVisibilityHandler;
    bool playerClose = false;
  

    void Start()
    {
        machinegunReference = gunSystem.machinegun;
        gameManager = GameManager.Instance;
        textVisibilityHandler = upgradeInfo.gameObject;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player")){
            infoTextUpdateHandler();
            textVisibilityHandler.SetActive(true);
            playerClose = true;
        }
      
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player")){
            textVisibilityHandler.SetActive(false);
            playerClose = false;
        }
    }
    void Update()
    {
         UpdateInputs();
    }

      void UpdateInputs()
    {
        if(Input.GetKeyDown(KeyCode.E) && playerClose)
        {
           UpgradeMachineGun();
        }
 
    }

    public void UpgradeMachineGun()
    {
        if(machinegunReference.upgradeLevel < 3  && hasEnoughMoneyFor(upgradeCost)){

            gameManager.Money -= upgradeCost;
            upgradeCost += 150;

            machinegunReference.magazineSize += 5;
            machinegunReference.timeBetweenShooting *= 0.7f;
            machinegunReference.timeBetweenShots *= 0.7f;
            machinegunReference.spread -= 0.02f;
            machinegunReference.upgradeLevel++;
            gunSystem.UpdateWeaponInfo();

            infoTextUpdateHandler();


        }
    }

    bool hasEnoughMoneyFor(int amount)
    {
        return gameManager.Money > amount;
    }

    void infoTextUpdateHandler()
    {
        if(machinegunReference.upgradeLevel == 3)
        {
            upgradeInfo.SetText("Machinegun fully upgraded!");

        } else {
            upgradeInfo.SetText("Press E to upgrade machinegun. Cost: " + upgradeCost + "\n current upgrade level: " + gunSystem.machinegun.upgradeLevel);

        }
    }
}