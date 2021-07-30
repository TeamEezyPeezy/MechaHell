using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStatsUpgradeCenter : MonoBehaviour
{
 
    public TextMeshProUGUI upgradeInfo;
    public PlayerMovement playerMovementReference;
    public int upgradeCost = 400;
    public int movespeedLvl = 1;
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
           UpgradePlayerStats();
        }
 
    }

  
    public void UpgradePlayerStats()
    {
        if(movespeedLvl <= maxLevel && playerMovementReference.moveSpeed < 10f && hasEnoughMoneyFor(upgradeCost))
        {   
            gameManager.Money -= upgradeCost;
            upgradeCost *= 4;
            playerMovementReference.moveSpeed += 1f;
            movespeedLvl += 1;
            upgradeParticle.Play();
            upgradeSound.Play();

            infoTextUpdateHandler();
        }
    }
    bool hasEnoughMoneyFor(int amount)
    {
        return gameManager.Money >= amount;
    }

    void infoTextUpdateHandler()
    {
        if(movespeedLvl == 4)
        {
            upgradeInfo.SetText("Player fully upgraded!");

        } else {
            upgradeInfo.SetText("Press E to upgrade player movespeed. Cost: " + upgradeCost);

        }
    }
}
