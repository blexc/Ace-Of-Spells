using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameplayUI : MonoBehaviour
{
    /// <summary>
    /// **AHL - MAKE SURE TO CHECK OUT JOSH'S SCRIPT FOR PLAYER STATS**
    /// </summary>

    //Variable Initialization/Declaration
    public Image hpBarFull; //Full HP bar that will be adjusted when the player heals/takes damage
    [HideInInspector] public float HPMax; //Max possible HP variable to keep track of the players max HP
    public Text Hptxt; //Text that will hold and adjust the player health for the player to see during gameplay
    public Image timeFull; //Stop Time image that will be adjusted to show that it is filling up
    public Text timetxt; //Time value to be adjusted to show that it is tracking time %
    public Text coinstxt; //Coin # value to be adjusted to show that it is tracking coins collected
    public int coinNum = 10; //AHL -Temporary coin variable to show the number change

    //References to other game objects (Mainly the player stats)
    public PlayerStats playerStats;

    //Pause Menu Variables
    public GameObject pauseMenu; //Pause menu that will be set to active or de-active during certain conditions
    [HideInInspector] public bool gamePaused = false; //Variable to hold the game paused bool

    private void Awake()
    {
        HPMax = playerStats.maxHealth; //Sets max health on the UI - AHL (3/4/21)
        Hptxt.text = "" + (int)HPMax; //Changes the text
    }

    /// <summary>
    /// Alexander Lopez (2/15/21): Function that takes in the player health and adjusts the health bar based on it. 
    /// </summary>
    public void HealthUpdate(float health)
    {
        if(health <= 0)
        {
            playerDeath();
        }

        else
        {
            float HPtemp = Mathf.Clamp(health, 0, HPMax); //Sets health to be within a range of values
            Hptxt.text = "" + (int)HPtemp; //Changes the text
            float HPBaradjustment = HPtemp / HPMax; //Gets a percantage of health remaining to adjust the HP UI
            hpBarFull.fillAmount = HPBaradjustment; //Adjusts the HP Image
        }
    }

    /// <summary>
    /// Alexander Lopez (2/16/21): Function that takes in the time addition and adjusts the time bar based on it. 
    /// **AHL- Might need to be removed with an enum or power-up section. Need to talk to lead about it.**
    /// </summary>
    public void TimeUpdate(int Time)
    {
        timetxt.text = "" + Time + '%'; //Changes the text
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
        //HealthUpdate(HPtemp); //Checks to make sure that the health of the player is always up to date.
        coinUpdate(coinNum); //Checks to make sure that the coins are always up to date.
    }

    /// <summary>
    /// Function to kill the player and end the game - AHL (3/10/21)
    /// </summary>
    private void playerDeath()
    {
        GetComponent<PlayerInput>().SwitchCurrentActionMap("Menu");
        SceneManager.LoadScene("Game Over");
    }

    /// <summary>
    /// Function to pause the game based on the esc key being pressed - AHL (3/10/21)
    /// **Key bindings can be changed by using the input manager**
    /// </summary>
    public void gamePause(InputAction.CallbackContext context)
    {
        if(context.performed && gamePaused == false)
        {
            gamePaused = true;
            Time.timeScale = 0f;
            GetComponent<GameplayUI>().pauseMenu.SetActive(true);
            GetComponent<PlayerInput>().SwitchCurrentActionMap("Menu");
        }
    }
}
