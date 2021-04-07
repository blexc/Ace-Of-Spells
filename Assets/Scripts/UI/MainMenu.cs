using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //Variable Declaration/Initialization
    public string sceneName; //The name of the scene that the player will load in first
    public GameObject CardCollectionMenu; //Variable to hold the Card Collection Menu that will be activated or deactivated based on the button input

    /// <summary>
    /// Function that starts the game and sends the player to the scene specified - AHL (3/10/21)
    /// </summary>
    public void play()
    {
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// Function that opens up a browser tab to the feedback form - AHL (3/10/21)
    /// </summary>
    public void feedbackForm()
    {
        Application.OpenURL("https://forms.gle/WWURFyrLW8bmNAE39");
    }

    /// <summary>
    /// Function that quits the game entirely- AHL (3/10/21)
    /// </summary>
    public void quit()
    {
        Application.Quit();
    }

    /// <summary>
    /// Function that puts the player back to the main menu - AHL (3/10/21)
    /// </summary>
    public void mainMenu()
    {
        SceneManager.LoadScene("Main Menu");
        Destroy(FindObjectOfType<PlayerStats>().GetComponentInParent<DontDestroyOnLoad>().gameObject);
    }

    /// <summary>
    /// Function that puts the player at the Card Collection Screen - AHL (4/7/21)
    /// </summary>
    public void CardCollection()
    {
        CardCollectionMenu.SetActive(true);
    }

    /// <summary>
    /// Function that puts the player back to the main menu from the Card Collection Menu - AHL (4/7/21)
    /// </summary>
    public void CardCollectionBack()
    {
        CardCollectionMenu.SetActive(false);
    }
}
