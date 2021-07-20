using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Player playerReference;

    public Image playerHealthImage;

    // Update is called once per frame
    void Update()
    {
        HealthBarCheck();
    }
    
    void HealthBarCheck()
    {
        playerHealthImage.fillAmount = ((float)playerReference.healthPoints / 100);
    }
}
