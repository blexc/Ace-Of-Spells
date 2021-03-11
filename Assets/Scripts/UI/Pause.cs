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
        Time.timeScale = 1f;
        GetComponent<GameplayUI>().pauseMenu.SetActive(false);
        GetComponent<PlayerInput>().SwitchCurrentActionMap("Gameplay");
    }

    /// <summary>
    /// Function that puts the player back to the main menu - AHL (3/10/21)
    /// </summary>
    public void mainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    /// <summary>
    /// Function that quits the game entirely- AHL (3/10/21)
    /// </summary>
    public void quit()
    {
        Application.Quit();
    }
}
