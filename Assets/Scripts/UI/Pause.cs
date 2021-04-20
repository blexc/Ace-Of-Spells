using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

public class Pause : MonoBehaviour
{
    //Variable Declaration/Initialization List
    public GameObject pause; //Variable to hold the pause menu (not the canvas) to be adjusted through the card collection buttons
    public GameObject DeckListMenu; //Variable to hold the Card Collection Menu that will be turned on and off based on the button input
    public GameObject DiscardCardPopUp; //Little popup for the player to choose if they want to discard a card or not
    public GameObject DiscardCardFinalPopup; //Popup to confirm that this is the card that the player wants to discard from their deck
    public TMP_Text DiscardCardFinalText; //The text for the final popup will be altered to show the name of the card that the player wants to discard
    [HideInInspector] public Card cardToBeDeleted; //Card gameobject that will be set by the card buttons


    //Bools
    [HideInInspector] public bool canDestroy = false; //Bool to keep control on if the player can discard a card or not from the deck


    //Favoriting Card Menus and needed variables
    public GameObject MakeFavoritePopup; //Pop-up menu that makes the card scriptable object favorited
    public GameObject RemoveFavoritePopup; //Pop-up menu that removes the card scriptable object favorited component
    public TMP_Text FavoriteCardText; //Favorite Card Pop-Up Text
    public TMP_Text UnfavoriteCardText; //Unfavorite Card Pop-Up Text


    /// <summary>
    /// Function that lets the player continue playing - AHL (3/10/21)
    /// </summary>
    public void resume()
    {
        //If canDestroy is yes then set it to no
        if (canDestroy)
            canDestroy = false;

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
        //If canDestroy is yes then set it to no
        if (canDestroy)
            canDestroy = false;

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
            //If the decklist is active then we set it back to the initial pause menu before resuming the game
            if(DeckListMenu)
                DeckListBack();

            //If canDestroy is yes then set it to no
            if (canDestroy)
                canDestroy = false;

            GetComponent<GameplayUI>().gamePaused = false;
            Time.timeScale = 1f;
            GetComponent<GameplayUI>().pauseMenu.SetActive(false);
            GetComponent<PlayerInput>().SwitchCurrentActionMap("Gameplay");
        }
    }




    /// <summary>
    /// Function that is accessed by the Discard Card Reward script that will pause the game and bring up the discard card popup so the player can choose if they want to discard a card - AHL (4/14/21)
    /// </summary>
    public void DiscardCardReward()
    {
        //Essentially the pause function from the Gameplay UI - AHL (4/14/21)
        GetComponent<GameplayUI>().gamePaused = true;
        Time.timeScale = 0f;
        GetComponent<GameplayUI>().pauseMenu.SetActive(true);
        GetComponent<PlayerInput>().SwitchCurrentActionMap("Menu");

        //Go to the decklist menu
        DeckList();

        //Open up the Discard Card Popup Menu
        DiscardCardPopUp.SetActive(true);
    }


    /// <summary>
    /// No button from the Discard Card Popup to resume the game - AHL (4/14/21)
    /// </summary>
    public void DiscardCardNo()
    {
        //Close the Discard Card Popup Menu
        DiscardCardPopUp.SetActive(false);

        //Goes back to the main pause menu
        DeckListBack();

        //Calls the resume function to resume the game
        resume();
    }

    /// <summary>
    /// Yes button from the Discard Card Popup to adjust the deck by discarding a card - AHL (4/14/21)
    /// </summary>
    public void DiscardCardYes()
    {
        //Closes the discard pop up and adjusts the canDestroy bool to yes
        DiscardCardPopUp.SetActive(false);
        canDestroy = true;
    }

    /// <summary>
    /// Function the Card buttons will access for the player to decide if this is the card they want to discard or not - AHL (4/14/21)
    /// </summary>
    public void DiscardACardFinalPopupYes()
    {
        //Removes the card from the deck
        gameObject.transform.root.GetComponentInChildren<Deck>().DestroyCard(cardToBeDeleted.name);
        canDestroy = false;

        //Goes back to the main pause menu
        DiscardCardFinalPopup.SetActive(false);
        DeckListBack();

        //Update the UI
        GetComponent<CardManager>().cardUIUpdate();
    }

    /// <summary>
    /// Function that backs the player out of the menu if they don't want to discard this card - AHL (4/14/21)
    /// </summary>
    public void DiscardACardFinalPopupNo()
    {
        DiscardCardFinalPopup.SetActive(false);
    }










    /// <summary>
    /// Back button that will be used for the Favorite and Unfavorite Pop-Ups - AHL (4/20/21)
    /// </summary>
    public void FavoriteBack()
    {
        //If the favorite pop-up is active then deactivate it
        if(MakeFavoritePopup.activeSelf)
            MakeFavoritePopup.SetActive(false);

        //If the remove pop-up is active then deactivate it
        if (RemoveFavoritePopup.activeSelf)
            RemoveFavoritePopup.SetActive(false);
    }






}
