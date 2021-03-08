using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TimeManipulatior : MonoBehaviour
{
    /// <summary>
    /// Script to adjust the Time manipulation sprites and adjust gameplay time.deltatime to slow things down for the player
    /// AHL (3/2/21)
    /// </summary>

    //Variables used to keep track of the time manipulator sprite and to be used throughout the script
    public int timeNum; //Time variable to keep time adjustments based on time duration in game
    private float Timer = 0f; //Float variable to keep track of the time to be used for resource gain
    private bool notSlow = true; //Sets a bool to make sure that the time doesn't grow while time is slowed down
    public float slowedTime; //Variable to keep track of how slow we want the time manipulation to be

    [Header("Delay in seconds")]
    public float increaseDelayNum; //Delay variable that keeps track in seconds the gain in the time resource
    public float decreaseDelayNum; //Delay variable that keeps track in seconds the deduction in the time resource

    /// <summary>
    /// Slow Time funciton that when the certain key binding is pressed the time will slow down for a few seconds
    /// **Currently set to 'E' key but can be adjusted in the input manager**
    /// AHL (3/2/21)
    /// </summary>
    public void slowtime(InputAction.CallbackContext context)
    { 
        if(context.performed)
        {
            if(timeNum == 100) //This function can only be performed it the timer is at 100%
            {
                notSlow = false;
                Time.timeScale = slowedTime;
            }
        }
    }



    /// <summary>
    /// Make sure that the UI displays correctly upon the first frame - AHL (3/2/21)
    /// </summary>
    private void Awake()
    {
        GetComponent<GameplayUI>().TimeUpdate(timeNum);
    }

    /// <summary>
    /// During update the Timer variable goes up based on Time.deltaTime to be used to increase the resource
    /// AHL (3/2/21)
    /// </summary>
    // Update is called once per frame
    private void Update()
    {
        Timer += Time.deltaTime; //Timer goes up based on the time change in game 

        //If statment to make sure that the time increase only happens when everything isn't slow
        if(notSlow)
        {
            //If statement to make sure this doesn't run if the timeNum variable is already at 100%+
            if (timeNum < 100)
            {
                //If statement to make sure that the code only runs if the Timer is greater or equal to the increase delay amount (seconds past) 
                if (Timer >= increaseDelayNum)
                {
                    Timer = 0f; //Resets the timer back to 0
                    timeNum++;
                    timeNum = Mathf.Clamp(timeNum, 0, 100); //Makes sure timeNum is within the range of values
                    GetComponent<GameplayUI>().TimeUpdate(timeNum);
                }
            }
        }
        
        else //If everything is slow
        {
            //If statement to make sure this does run if the timeNum variable is greater than 0
            if (timeNum > 0)
            {
                //If statement to make sure that the code only runs if the Timer is greater or equal to the decrease delay amount (seconds past) 
                if (Timer >= decreaseDelayNum)
                {
                    Timer = 0f; //Resets the timer back to 0
                    timeNum--;
                    timeNum = Mathf.Clamp(timeNum, 0, 100); //Makes sure timeNum is within the range of values
                    GetComponent<GameplayUI>().TimeUpdate(timeNum);
                }
            }

            else //If the TimeNum == 0
            {
                Time.timeScale = 1f;
                notSlow = true;
            }
        }
    }
}
