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

    public float slowTimeScale;

    private float slowTimer;
    private bool isSlow = true;

    /// <summary>
    /// Slow Time funciton that when the certain key binding is pressed the time will slow down for a few seconds
    /// **Currently set to 'Q' key but can be adjusted in the input manager**
    /// AHL (3/2/21)
    /// </summary>
    public void slowtime(InputAction.CallbackContext context)
    { 
        if(context.performed)
        {
            isSlow = !isSlow;
        }

        Time.timeScale = (isSlow) ? slowTimeScale : 1f;
    }

    /// <summary>
    /// Make sure that the UI displays correctly upon the first frame - AHL (3/2/21)
    /// </summary>
    private void Awake()
    {
        GetComponent<GameplayUI>().TimeUpdate(slowTimer);
    }

    /// <summary>
    /// During update the Timer variable goes up based on Time.deltaTime to be used to increase the resource
    /// AHL (3/2/21)
    /// </summary>
    // Update is called once per frame
    private void Update()
    {
        if(isSlow)
        {
            slowTimer -= Time.deltaTime * 10; // lose slow fast
            if (slowTimer < 0f)
            {
                isSlow = false;
                Time.timeScale = 1f;
            }
        }
        else
        {
            slowTimer += Time.deltaTime; 
        }

        slowTimer = Mathf.Clamp(slowTimer, 0, 100);
        GetComponent<GameplayUI>().TimeUpdate(slowTimer);
    }
}
