using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour
{
    //Variable Initialization/Declaration
    public Image hpBarFull; //Full HP bar that will be adjusted when the player heals/takes damage
    public float HPtemp = 200; //AHL - Temporary HP value until we have the actual player stats implemented
    public Text Hp; //Text that will hold and adjust the player health for the player to see during gameplay
    public Button Hurt; //AHL - Temporary button that will damage the health of the player to show the change.

    //AHL - Temporary function to show how the damage works
    public void Damage()
    {
        HPtemp -= 24f;
    }

    /// <summary>
    /// Alexander Lopez (2/15/21): Function that takes in the player health and adjusts the health bar based on it. 
    /// </summary>
    void HealthUpdate(float health)
    {
        if (health > 200)
        {
            Hp.text = "100";
            hpBarFull.fillAmount = 200;
            HPtemp = 200;
        }
        else if(health <= 200 && health > 0)
        {
            int stringHP = (int)health / 2;
            Hp.text = "" + stringHP;
            float HPadjustment = (health / 100.0f) * 180.0f / 360;
            hpBarFull.fillAmount = HPadjustment;
        }
        else
        {
            Hp.text = "0";
            hpBarFull.fillAmount = 0;
            HPtemp = 0;
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        //AHL - Will need to remove this and put this with enemy collisons or when the player takes damage.
        HealthUpdate(HPtemp); //Checks to make sure that the health of the player is always up to date.
    }
}
