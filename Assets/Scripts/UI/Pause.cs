using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Pause : MonoBehaviour
{
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
}
