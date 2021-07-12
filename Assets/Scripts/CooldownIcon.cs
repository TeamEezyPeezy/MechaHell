using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownIcon : MonoBehaviour
{
    public PlayerMovement playerMovementReference;
    public ExplosiveGun bazookaReference;

    [Header("Dash")]
    public Image dashImage;
    public KeyCode dashButton;
    bool isCooldown = false;

    [Header("Bazooka")]
    public Image bazookaImage;
    public KeyCode bazookaButton;
    bool isCooldown2 = false;


    // Start is called before the first frame update
    void Start()
    {
        dashImage.fillAmount = 0;
        bazookaImage.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Dash();
        Bazooka();
    }

    void Dash()
    {
        if(Input.GetKey(dashButton) && isCooldown == false)
        {
            isCooldown = true;
            dashImage.fillAmount = 1;
        }

        if(isCooldown)
        {
            dashImage.fillAmount -= 1 / playerMovementReference.dashCooldown * Time.deltaTime;

            if(dashImage.fillAmount <= 0)
            {
                dashImage.fillAmount = 0;
                isCooldown = false;
            }
        }
    }

    void Bazooka()
    {
        if (Input.GetKey(bazookaButton) && isCooldown2 == false)
        {
            isCooldown2 = true;
            bazookaImage.fillAmount = 1;
        }

        if (isCooldown2)
        {
            bazookaImage.fillAmount -= 1 / bazookaReference.cooldown * Time.deltaTime;

            if (bazookaImage.fillAmount <= 0)
            {
                bazookaImage.fillAmount = 0;
                isCooldown2 = false;
            }
        }
    }
}
