using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public string sceneName;
    public Sprite rewardSpriteForNextRoom;

    Color originalColor;
    [SerializeField] bool locked = true;

    private void Start()
    {
        if (!locked) Unlock();
        originalColor = GetComponent<SpriteRenderer>().color;
    }

    /// <summary>
    /// change the scene to be door's destinationScene 
    /// send player to new room
    /// </summary>
    public void Open()
    {
        if (!locked) SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// change door's color to red
    /// </summary>
    public void IndicateActive()
    {
        if (!locked)
            GetComponent<SpriteRenderer>().color = Color.red;
    }

    /// <summary>
    /// change door's color to original color
    /// </summary>
    public void IndicateStop()
    {
        GetComponent<SpriteRenderer>().color = originalColor;
    }

    public void Unlock()
    {
        locked = false;
        
        // hide the lock sprite
        GameObject iconObject = GetComponentInChildren<DoorIcon>().gameObject;
        if (iconObject.activeSelf)
        {
            iconObject.GetComponent<SpriteRenderer>().sprite = rewardSpriteForNextRoom;
        }
    }
}




















