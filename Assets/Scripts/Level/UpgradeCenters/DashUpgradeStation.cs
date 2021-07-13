using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DashUpgradeStation : MonoBehaviour
{

    public TextMeshProUGUI upgradeInfo;
    public PlayerMovement playerMovementReference;
    public int upgradeCost = 200;
    GameManager gameManager;
    GameObject textVisibilityHandler;
    bool playerClose = false;
  

    void Start()
    {
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
           UpgradeDash();
        }
 
    }

  
  public void UpgradeDash()
    {
        if(playerMovementReference.dashCooldown > 1 && hasEnoughMoneyFor(upgradeCost)){
            gameManager.Money -= upgradeCost;
            upgradeCost += 150;

            playerMovementReference.dashCooldown  -= 1f;

            infoTextUpdateHandler();
        }
       
    }
    bool hasEnoughMoneyFor(int amount)
    {
        return gameManager.Money > amount;
    }

    void infoTextUpdateHandler()
    {
        if(playerMovementReference.dashCooldown == 1f)
        {
            upgradeInfo.SetText("Dash fully upgraded!");

        } else {
            upgradeInfo.SetText("Press E to upgrade dash. Cost: " + upgradeCost + "\ncurrent cooldown: " + playerMovementReference.dashCooldown + "s");

        }
    }
}
