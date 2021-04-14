using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Pause : MonoBehaviour
{
    public GameObject pause; //Variable to hold the pause menu (not the canvas) to be adjusted through the card collection buttons
    public GameObject DeckListMenu; //Variable to hold the Card Collection Menu that will be turned on and off based on the button input
    public GameObject DiscardCardPopUp; //Little popup for the player to choose if they want to discard a card or not

    //public bool isDiscardReward; //

    /// <summary>
    /// Function that lets the player continue playing - AHL (3/10/21)
    /// </summary>
    public void resume()
    {
        GetComponent<GameplayUI>().gamePaused = false;
        Time.timeScale = 1f;
        GetComponent<GameplayUI>().pauseMenu.SetActive(false);
        GetComponent<PlayerInput>().SwitchCurrentActionMap("Gameplay");
    }

    /// <summary>
    /// Function that puts the player back to the main menu - AHL (3/10/21)
    /// </summary>
    public void mainMenu()
    {
        GetComponent<GameplayUI>().gamePaused = false;
        Time.timeScale = 1f;
        GetComponent<GameplayUI>().pauseMenu.SetActive(false);     
        SceneManager.LoadScene("Main Menu");
        Destroy(GetComponentInParent<DontDestroyOnLoad>().gameObject);
    }

    /// <summary>
    /// Function that puts the player at the Card Collection Screen - AHL (3/31/21)
    /// </summary>
    public void DeckList()
    {
        pause.SetActive(false);
        DeckListMenu.SetActive(true);
    }

    /// <summary>
    /// Function that puts the player back to the pause menu from the Card Collection Menu - AHL (3/31/21)
    /// </summary>
    public void DeckListBack()
    {
        DeckListMenu.SetActive(false);
        pause.SetActive(true);
    }

    /// <summary>
    /// Function that quits the game entirely- AHL (3/10/21)
    /// </summary>
    public void quit()
    {
        Application.Quit();
    }

    /// <summary>
    /// Function to un-pause the game based on the esc key being pressed - AHL (3/10/21)
    /// **Key bindings can be changed by using the input manager**
    /// </summary>
    public void gameResume(InputAction.CallbackContext context)
    {
        if (context.performed && GetComponent<GameplayUI>().gamePaused == true)
        {
            GetComponent<GameplayUI>().gamePaused = false;
            Time.timeScale = 1f;
            GetComponent<GameplayUI>().pauseMenu.SetActive(false);
            GetComponent<PlayerInput>().SwitchCurrentActionMap("Gameplay");
        }
    }





    public void DiscardCardReward()
    {
        print("Discarding a card?");




        //Use the destroy card function located in the deck script
    }
}
