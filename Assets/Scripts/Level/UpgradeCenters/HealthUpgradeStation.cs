using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthUpgradeStation : MonoBehaviour
{

    public TextMeshProUGUI upgradeInfo;
    public PlayerMovement playerMovementReference;
    public int healCost = 1000;
    public int overHealCost = 1500;
    int upgradeCost;
    
    GameManager gameManager;
    GameObject textVisibilityHandler;
    bool playerClose = false;
    [SerializeField]
    private ParticleSystem upgradeParticle;
    [SerializeField]
    private AudioSource upgradeSound;
 

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
           UpgradeHealth();
        }
 
    }

  
    public void UpgradeHealth()
    {
        bool shouldHeal =  gameManager.player.healthPoints < 100 ? true : false;
        upgradeCost = gameManager.player.healthPoints < 100 ? healCost : overHealCost;
       
        if(hasEnoughMoneyFor(upgradeCost) && gameManager.player.healthPoints < 150)
        {   
            gameManager.Money -= upgradeCost;
            upgradeParticle.Play();
            upgradeSound.Play();
            if(shouldHeal)
            {
                healPlayer();
            } else 
            {
                overHealPlayer();
            }
            infoTextUpdateHandler();
        }
    }

    void healPlayer()
    {
        gameManager.player.RefillHealth();
        healCost += 300;
    }

    void overHealPlayer()
    {
        gameManager.player.healthPoints = 150;
        overHealCost += 300;
    }
    bool hasEnoughMoneyFor(int amount)
    {
        return gameManager.Money > amount;
    }

    void infoTextUpdateHandler()
    {
        if(gameManager.player.healthPoints == 150)
        {
            upgradeInfo.SetText("Player full health!");

        } else if (gameManager.player.healthPoints < 100) {
            upgradeInfo.SetText("Press E to heal player: " + healCost);
        } else {
             upgradeInfo.SetText("Press E to overheal player: " + overHealCost);
        }
    }
}
