using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePoint : MonoBehaviour
{
  
    public GameObject infoText;
    public GameObject upgradeMachineGunButton;
    public GameObject upgradeSniperButton;
    public GameObject upgradeDashButton;
    public GameObject upgradeBazookaButton;
    public GameObject upgradePlayerStatsButton;
    public GameObject upgradePanel;
    public GunSystem gunSystem;
    Sniper sniperReference;
    Machinegun machinegunReference;

    bool isActive = false;
    bool playerClose = false;

    void Awake()
    {
        sniperReference = gunSystem.sniper;
        machinegunReference = gunSystem.machinegun;
        // ShowUI(false);
        upgradePanel.SetActive(false);
    }
    void Update()
    {
        UpdateInputs();
    }

    void ShowUI(bool visibile)
    {
        upgradeMachineGunButton.SetActive(visibile);
        upgradeSniperButton.SetActive(visibile);
        upgradeDashButton.SetActive(visibile);
        upgradeBazookaButton.SetActive(visibile);
        upgradePlayerStatsButton.SetActive(visibile);

    }

  
    void UpdateInputs()
    {
        if(Input.GetKeyDown(KeyCode.E) && playerClose)
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
        //ShowUI(true);
        upgradePanel.SetActive(true);
        
    }

    void ClosePanel()
    {
        infoText.SetActive(true);
        upgradePanel.SetActive(false);

        // ShowUI(false);
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
            isActive = false;
            // ShowUI(false);
            upgradePanel.SetActive(false);
        }
    }

    public void UpgradeMachineGun()
    {
         // test for upgrades
        if(machinegunReference.upgradeLevel < 3){
            machinegunReference.magazineSize += 5;
            machinegunReference.upgradeLevel++;
            gunSystem.UpdateWeaponInfo();
        }
    }
    public void UpgradeSniper()
    {
        Debug.Log("Snoper ugrade");
      
    }
    public void UpgradeBazooka()
    {
        Debug.Log("Bazookq ugrade");
    }
    public void UpgradeDash()
    {
        Debug.Log("Dash ugrade"); 
    }

    public void UpgradePlayerStats()
    {
        Debug.Log("playere ugrade");
    }

  
}
