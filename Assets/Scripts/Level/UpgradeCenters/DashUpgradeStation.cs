using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DashUpgradeStation : MonoBehaviour
{

    public TextMeshProUGUI upgradeInfo;
    public PlayerMovement playerMovementReference;
    public int upgradeCost = 200;
    public int dashLvl = 1;
    GameManager gameManager;
    GameObject textVisibilityHandler;
    bool playerClose = false;
    [SerializeField]
    private ParticleSystem upgradeParticle;
    [SerializeField]
    private AudioSource upgradeSound;
    int maxLevel = 3;


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
        if(dashLvl <= maxLevel && hasEnoughMoneyFor(upgradeCost) && playerMovementReference.dashCooldown - 1 >= 1){
            gameManager.Money -= upgradeCost;
            upgradeCost += 150;
            dashLvl += 1;
            upgradeParticle.Play();
            upgradeSound.Play();

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
