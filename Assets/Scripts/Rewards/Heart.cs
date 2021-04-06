using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// functionality inherits from Reward.cs
public class Heart : Reward
{
    public int healAmount = 25; 

    PlayerStats playerStats;

    private void Awake()
    {
        playerStats = FindObjectOfType<PlayerStats>();
    }
    public override void RecieveReward()
    {
        playerStats.Heal(healAmount);

        Destroy(gameObject);
    }
}
