using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Player playerReference;

    public Image playerHealthImage;

    void Update()
    {
        HealthBarCheck();
    }
    
    void HealthBarCheck()
    {
        if (playerHealthImage != null)
        {
            playerHealthImage.fillAmount = ((float)playerReference.healthPoints / 100);
        }
    }
}
