using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public string sceneName;
    Color originalColor;

    GameObject playerObject;
    AsyncOperation sceneAsync;

    private void Start()
    {
        originalColor = GetComponent<SpriteRenderer>().color;
    }

    /// <summary>
    /// change the scene to be door's destinationScene 
    /// send player to new room
    /// </summary>
    public void Open()
    {
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// change door's color to red
    /// </summary>
    public void IndicateActive()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
    }

    /// <summary>
    /// change door's color to original color
    /// </summary>
    public void IndicateStop()
    {
        GetComponent<SpriteRenderer>().color = originalColor;
    }

}




















