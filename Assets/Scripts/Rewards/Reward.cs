using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : MonoBehaviour
{
    protected virtual void Start()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// give the player reward
    /// right now, it just deletes the Chest object
    /// </summary>
    public virtual void RecieveReward()
    {
        Destroy(gameObject);
    }
}
