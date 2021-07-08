using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradePoint : MonoBehaviour
{
  
    public GameObject infoText;
    public GameObject upgradePanel;
    public GunSystem gunSystem;
    public PlayerMovement playerMovementReference;
    public ExplosiveGun bazookaReference;
    Sniper sniperReference;
    Machinegun machinegunReference;

    public TextMeshProUGUI machineGunStats;
    public TextMeshProUGUI sniperStats;
    public TextMeshProUGUI playerMovementStats;
    public TextMeshProUGUI dashStats;
    public TextMeshProUGUI bazookaStats;

    public TextMeshProUGUI machineGunCostInfo;
    public TextMeshProUGUI sniperCostInfo;
    public TextMeshProUGUI bazookaCostInfo;
    public TextMeshProUGUI dashCostInfo;
    public TextMeshProUGUI playerCostInfo;

    private GameManager gameManager;
    bool isPanelOpen = false;
    bool playerClose = false;

    int machinegunUpgradeCost = 300;
    int sniperUpgraceCost = 300;
    int bazookaUpgradeCost = 300;
    int dashUpgradeCost = 300;
    int playerUpgradeCost = 300;

    void Awake()
    {
        sniperReference = gunSystem.sniper;
        machinegunReference = gunSystem.machinegun;
        gameManager = GameManager.Instance;
        UpdateTexts();

        machineGunCostInfo.SetText(machinegunUpgradeCost + "$");
        sniperCostInfo.SetText(sniperUpgraceCost + "$");
        bazookaCostInfo.SetText(bazookaUpgradeCost+ "$");
        dashCostInfo.SetText(dashUpgradeCost + "$");
        playerCostInfo.SetText(playerUpgradeCost + "$");
    }
    void Update()
    {
        UpdateInputs();
    }

    void UpdateTexts()
    {
        machineGunStats.SetText("Machinegun level " + machinegunReference.upgradeLevel);
        sniperStats.SetText("Sniper level " + sniperReference.upgradeLevel);
        playerMovementStats.SetText("Player movementspeed " + playerMovementReference.moveSpeed);
        bazookaStats.SetText("Bazooka cooldown " + bazookaReference.cooldown + "s");
        dashStats.SetText("Dash cooldown " + playerMovementReference.dashCooldown + "s");

    }
      void UpdateInputs()
    {
        if(Input.GetKeyDown(KeyCode.E) && playerClose)
        {
            if(!isPanelOpen)
            {
                isPanelOpen = true;
                OpenPanel();  
            } else {
                isPanelOpen = false;
                ClosePanel();
            }
        }
 
    }

    void OpenPanel()
    {
        infoText.SetActive(false);
        upgradePanel.SetActive(true);
    }

    void ClosePanel()
    {
        infoText.SetActive(true);
        upgradePanel.SetActive(false);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player")){
            infoText.SetActive(true);
            playerClose = true;
        }
      
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player")){
            infoText.SetActive(false);
            playerClose = false;
            isPanelOpen = false;
            upgradePanel.SetActive(false);
        }
    }

    public void UpgradeMachineGun()
    {
        if(machinegunReference.upgradeLevel < 3  && hasEnoughMoneyFor(machinegunUpgradeCost)){

            gameManager.Money -= machinegunUpgradeCost;
            machinegunUpgradeCost += 150;

            machinegunReference.magazineSize += 5;
            machinegunReference.timeBetweenShooting *= 0.7f;
            machinegunReference.timeBetweenShots *= 0.7f;
            machinegunReference.spread -= 0.02f;
            machinegunReference.upgradeLevel++;
            gunSystem.UpdateWeaponInfo();
            UpdateTexts();

            CostInfoUpdateHandler(machineGunCostInfo, machinegunReference.upgradeLevel == 3, machinegunUpgradeCost);

        }
    }
    public void UpgradeSniper()
    {
        if(sniperReference.upgradeLevel < 3 && hasEnoughMoneyFor(sniperUpgraceCost)){
            gameManager.Money -= sniperUpgraceCost;
            sniperUpgraceCost += 150;

            sniperReference.magazineSize += 1;
            sniperReference.timeBetweenShooting *= 0.8f;
            sniperReference.reloadTime *= 0.8f;
            sniperReference.upgradeLevel++;
            gunSystem.UpdateWeaponInfo();
            UpdateTexts();

            CostInfoUpdateHandler(sniperCostInfo, sniperReference.upgradeLevel == 3, sniperUpgraceCost);
        }
    }
    public void UpgradeBazooka()
    {
        if(bazookaReference.cooldown > 1f && hasEnoughMoneyFor(bazookaUpgradeCost)){
            gameManager.Money -= bazookaUpgradeCost;
            bazookaUpgradeCost += 150;

            bazookaReference.cooldown -= 1f;
            UpdateTexts();
            
            CostInfoUpdateHandler(bazookaCostInfo, bazookaReference.cooldown == 1f, bazookaUpgradeCost);
        }
    }
    public void UpgradeDash()
    {
        if(playerMovementReference.dashCooldown > 1 && hasEnoughMoneyFor(dashUpgradeCost)){
            gameManager.Money -= dashUpgradeCost;
            dashUpgradeCost += 150;

            playerMovementReference.dashCooldown  -= 1f;
            UpdateTexts();

            CostInfoUpdateHandler(dashCostInfo, playerMovementReference.dashCooldown == 1f, dashUpgradeCost);
        }
       
    }

    public void UpgradePlayerStats()
    {
        if(playerMovementReference.moveSpeed < 10f && hasEnoughMoneyFor(playerUpgradeCost))
        {   
            gameManager.Money -= playerUpgradeCost;
            playerUpgradeCost += 150;
            playerMovementReference.moveSpeed += 1f;
            UpdateTexts();

            CostInfoUpdateHandler(playerCostInfo, playerMovementReference.moveSpeed == 10f, playerUpgradeCost);
        }
    }

    bool hasEnoughMoneyFor(int amount)
    {
        return gameManager.Money > amount;
    }

    void CostInfoUpdateHandler(TextMeshProUGUI text, bool isMaxed, int cost)
    {
        if(isMaxed){
            text.SetText("MAX");
        } else {
            text.SetText(cost + "$");
        }
    }
  
}
