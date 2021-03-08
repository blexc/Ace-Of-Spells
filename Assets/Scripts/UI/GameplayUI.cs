using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour
{
    /// <summary>
    /// **AHL - MAKE SURE TO CHECK OUT JOSH'S SCRIPT FOR PLAYER STATS**
    /// </summary>

    //Variable Initialization/Declaration
    public Image hpBarFull; //Full HP bar that will be adjusted when the player heals/takes damage
    public int HPtemp = 100; //AHL - Temporary HP value until we have the actual player stats implemented
    public Text Hptxt; //Text that will hold and adjust the player health for the player to see during gameplay
    public Image timeFull; //Stop Time image that will be adjusted to show that it is filling up
    public Text timetxt; //Time value to be adjusted to show that it is tracking time %
    public int timeNum; //Time variable to show the number change
    public float timeAdjuster; //Adjustable float variable to change the time manipulation speed of the scene
    public Text coinstxt; //Coin # value to be adjusted to show that it is tracking coins collected
    public int coinNum = 10; //AHL -Temporary coin variable to show the number change


    //AHL - Temporary function to show how the damage works
    public void Damage()
    {
        HPtemp -= 12;
    }

    //AHL - Temporary function to show how the time stop works
    public void TimeGain()
    {
        timeNum += 10;
    }

    //AHL - Temporary function to show coin addition
    public void GotCoins()
    {
        coinNum += 2;
    }

    /// <summary>
    /// Alexander Lopez (2/15/21): Function that takes in the player health and adjusts the health bar based on it. 
    /// </summary>
    void HealthUpdate(int health)
    {
        HPtemp = Mathf.Clamp(health, 0, 100); //Sets health to be within a range of values
        Hptxt.text = "" + health; //Changes the text
        float HPBaradjustment = (float)health / 100f; //Gets a percantage of health remaining to adjust the HP UI
        hpBarFull.fillAmount = HPBaradjustment; //Adjusts the HP Image
    }

    /// <summary>
    /// Alexander Lopez (2/16/21): Function that takes in the time addition and adjusts the time bar based on it. 
    /// **AHL- Might need to be removed with an enum or power-up section. Need to talk to lead about it.**
    /// </summary>
    void TimeUpdate(int Time)
    {
        timeNum = Mathf.Clamp(Time, 0, 100); //Sets time to be within a range of values
        timetxt.text = "" + timeNum + '%'; //Changes the text
        float TimeBaradjustment = (float)Time / 100f; //Gets a percantage of health remaining to adjust the HP UI
        timeFull.fillAmount = TimeBaradjustment; //Adjusts the HP Image
    }

    /// <summary>
    /// Alexander Lopez (2/16/21): Function that takes in the player coins and adjusts the number based on it. 
    /// </summary>
    void coinUpdate(int Coins)
    {
        coinNum = Mathf.Clamp(Coins, 0, 9999); //Sets coins to be within a range of values
        coinstxt.text = "" + coinNum; //Changes the text
    }

    // Update is called once per frame
    void Update()
    {
        //AHL - Will need to remove this and put this with enemy collisons or when the player takes damage.
        HealthUpdate(HPtemp); //Checks to make sure that the health of the player is always up to date.
        TimeUpdate(timeNum); //Checks to make sure that the time is always up to date.
        coinUpdate(coinNum); //Checks to make sure that the coins are always up to date.
    }
}
