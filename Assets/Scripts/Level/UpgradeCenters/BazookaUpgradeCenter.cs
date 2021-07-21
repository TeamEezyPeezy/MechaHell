using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BazookaUpgradeCenter : MonoBehaviour
{

    public TextMeshProUGUI upgradeInfo;
    public ExplosiveGun bazookaReference;
    public int upgradeCost = 200;
    public int bazookaLvl = 1;
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
           UpgradeBazooka();
        }
 
    }

     public void UpgradeBazooka()
    {
        if(bazookaReference.cooldown > 1f && hasEnoughMoneyFor(upgradeCost)){
            gameManager.Money -= upgradeCost;
            upgradeCost += 200;
            bazookaLvl += 1;
            upgradeParticle.Play();
            upgradeSound.Play();

            bazookaReference.cooldown -= 1f;

            infoTextUpdateHandler();
            
        }
    }

    bool hasEnoughMoneyFor(int amount)
    {
        return gameManager.Money > amount;
    }

    void infoTextUpdateHandler()
    {
        if(bazookaReference.cooldown == 1f)
        {
            upgradeInfo.SetText("Bazooka fully upgraded!");

        } else {
            upgradeInfo.SetText("Press E to upgrade bazooka. Cost: " + upgradeCost + "\ncurrent cooldown: " + bazookaReference.cooldown + "s");

        }
    }
}
