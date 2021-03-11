using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //Variable Declaration/Initialization
    public string sceneName; //The name of the scene that the player will load in first

    /// <summary>
    /// Function that starts the game and sends the player to the scene specified - AHL (3/10/21)
    /// </summary>
    public void play()
    {
        SceneManager.LoadScene(sceneName);
    }
}
