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
    /// by default, it just deletes the object
    /// </summary>
    public virtual void RecieveReward()
    {
        Destroy(gameObject);
    }
}
