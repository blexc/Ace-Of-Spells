using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscardCardReward : Reward
{
    public Pause pauseScript; //Pause script to handle the main functions and components for this script

    private void Awake()
    {
        pauseScript = FindObjectOfType<Pause>();
    }

    public override void RecieveReward()
    {
        pauseScript.DiscardCardReward();
        Destroy(gameObject);
    }
}
