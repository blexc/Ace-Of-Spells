using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyUI : MonoBehaviour
{
    //Variable Initialization/Declaration
    public Image HPBarFull; //Full HP bar that will be adjusted when the enemy heals/takes damage
    public GameObject damageText; //Variable to hold the damage text

    //Variables for Status Effects
    public GameObject fireEffect;
    public GameObject frostEffect;
    public GameObject shockEffect;


    [HideInInspector]
    public int HPMax; //Int for the maximum amount of hp that the enemy has

    /// <summary>
    /// Function that takes in the enemy health and adjusts the health bar based on it. - AHL (3/4/21)
    /// </summary>
    public void enemyHPUpdate(int health)
    {
        int currHealth = Mathf.Clamp(health, 0, HPMax); //Sets health to be within a range of values
        float HPBaradjustment = (float)currHealth / (float)HPMax; //Gets a percantage of health remaining to adjust the HP UI
        HPBarFull.fillAmount = HPBaradjustment; //Adjusts the HP bar Image
    }

    /// <summary>
    /// Function to spawn in the damage text prefab and adjust the number based on the damage the enemy took
    /// </summary>
    public void damagePopUP(int DamageAmount)
    {
        //Creates a temp GameObject of the current damage text to be adjusted
        GameObject DamageTextInstance = Instantiate(damageText, this.gameObject.transform);

        //Assigns the Damage Amount to the Text game object (which is the child of the DamageText GameObject)
        DamageTextInstance.GetComponent<Transform>().GetComponentInChildren<TextMeshPro>().SetText(DamageAmount.ToString());   
    }

    /// <summary>
    /// Function to display the proper status effects that the enemy currently has - AHL (4/1/21)
    /// </summary>
    public void statusEffectUpdate()
    {
        //List of if statements to check what status effect was taken or removed

        print("Time to show a status!");

        //Shock Effect
        if (GetComponentInParent<EnemyBase>().HasStatusEffect(StatusEffect.Shock) == true)
        {
            print("Shock Status!");
            shockEffect.SetActive(true);
        }

        else //Non-active effect
            shockEffect.SetActive(false);

        //Fire Effect
        if (GetComponentInParent<EnemyBase>().HasStatusEffect(StatusEffect.Ignite) == true)
        {
            print("Ignite Status!");
            fireEffect.SetActive(true);
        }

        else //Non-active effect
            fireEffect.SetActive(false);

        //Freeze Effect
        if (GetComponentInParent<EnemyBase>().HasStatusEffect(StatusEffect.Freeze) == true)
        {
            print("Freeze Status!");
            frostEffect.SetActive(true);            
        }

        else //Non-active effect
            frostEffect.SetActive(false);
    }
}
