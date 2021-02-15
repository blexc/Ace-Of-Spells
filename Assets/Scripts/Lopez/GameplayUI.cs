using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour
{
    //Variable Initialization/Declaration
    public Image hpBarFull; //Full HP bar that will be adjusted when the player heals/takes damage
    public int HPtemp = 100; //AHL - Temporary HP value until we have the actual player stats implemented
    public Text Hp; //Text that will hold and adjust the player health for the player to see during gameplay
    public Button Hurt; //AHL - Temporary button that will damage the health of the player to show the change.

    //AHL - Temporary function to show how the damage works
    public void Damage()
    {
        HPtemp -= 12;
    }

    /// <summary>
    /// Alexander Lopez (2/15/21): Function that takes in the player health and adjusts the health bar based on it. 
    /// </summary>
    void HealthUpdate(int health)
    {
        HPtemp = Mathf.Clamp(health, 0, 100); //Sets health to be within a range of values
        Hp.text = "" + health; //Changes the text
        float HPBaradjustment = (float)health / 100f; //Gets a percantage of health remaining to adjust the HP UI
        hpBarFull.fillAmount = HPBaradjustment; //Adjusts the HP Image
    }

    // Update is called once per frame
    void Update()
    {
        //AHL - Will need to remove this and put this with enemy collisons or when the player takes damage.
        HealthUpdate(HPtemp); //Checks to make sure that the health of the player is always up to date.
    }
}
