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

    bool isPanelOpen = false;
    bool playerClose = false;

    void Awake()
    {
        sniperReference = gunSystem.sniper;
        machinegunReference = gunSystem.machinegun;
        UpdateTexts();
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
        bazookaStats.SetText("Bazooka cooldown " + bazookaReference.cooldown);
        dashStats.SetText("Dash cooldown " + playerMovementReference.dashCooldown);
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
        if(machinegunReference.upgradeLevel < 3){
            machinegunReference.magazineSize += 5;
            machinegunReference.timeBetweenShooting *= 0.7f;
            machinegunReference.timeBetweenShots *= 0.7f;
            machinegunReference.spread -= 0.02f;
            machinegunReference.upgradeLevel++;
            gunSystem.UpdateWeaponInfo();

            UpdateTexts();
        }
    }
    public void UpgradeSniper()
    {
        if(sniperReference.upgradeLevel < 3){
            sniperReference.magazineSize += 1;
            sniperReference.timeBetweenShooting *= 0.8f;
            sniperReference.reloadTime *= 0.8f;
            sniperReference.upgradeLevel++;
            gunSystem.UpdateWeaponInfo();
            UpdateTexts();
        }
    }
    public void UpgradeBazooka()
    {
        if(bazookaReference.cooldown > 1f){
            bazookaReference.cooldown -= 1f;
            UpdateTexts();
        }
    }
    public void UpgradeDash()
    {
        if(playerMovementReference.dashCooldown > 1){
            playerMovementReference.dashCooldown  -= 1f;
            UpdateTexts();
        }
       
    }

    public void UpgradePlayerStats()
    {
        if(playerMovementReference.moveSpeed < 10f)
        {
            playerMovementReference.moveSpeed += 1f;
            UpdateTexts();
        }
    }

  
}
