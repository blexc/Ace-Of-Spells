using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    //Variable Initialization/Declaration
    public Image HPBarFull; //Full HP bar that will be adjusted when the enemy heals/takes damage
    //[HideInInspector]
    public int HPMax; //Int for the maximum amount of hp that the enemy has


    /// <summary>
    /// Alexander Lopez (3/3/21): Function that takes in the enemy health and adjusts the health bar based on it. 
    /// </summary>
    public void enemyHPUpdate(int health)
    {
        int currHeatlh = Mathf.Clamp(health, 0, HPMax); //Sets health to be within a range of values
        float HPBaradjustment = (float)health / (float)HPMax; //Gets a percantage of health remaining to adjust the HP UI
        HPBarFull.fillAmount = HPBaradjustment; //Adjusts the HP bar Image
    }
}
