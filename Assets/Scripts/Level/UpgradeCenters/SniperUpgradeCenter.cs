using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SniperUpgradeCenter : MonoBehaviour
{

    public TextMeshProUGUI upgradeInfo;
    public GunSystem gunSystem;
    public int upgradeCost = 200;
    GameManager gameManager;
    Sniper sniperReference;
    GameObject textVisibilityHandler;
    bool playerClose = false;
  

    void Start()
    {
        sniperReference = gunSystem.sniper;
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
           UpgradeSniper();
        }
 
    }


    public void UpgradeSniper()
    {
        if(sniperReference.upgradeLevel < 3 && hasEnoughMoneyFor(upgradeCost)){
            gameManager.Money -= upgradeCost;
            upgradeCost += 150;

            sniperReference.magazineSize += 1;
            sniperReference.timeBetweenShooting *= 0.8f;
            sniperReference.reloadTime *= 0.8f;
            sniperReference.upgradeLevel++;
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
        if(sniperReference.upgradeLevel == 3)
        {
            upgradeInfo.SetText("Sniper fully upgraded!");

        } else {
            upgradeInfo.SetText("Press E to upgrade sniper. Cost: " + upgradeCost + "\ncurrent upgrade level: " + gunSystem.machinegun.upgradeLevel);

        }
    }
}