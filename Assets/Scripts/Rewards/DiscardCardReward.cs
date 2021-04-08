using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscardCardReward : Reward
{
    private void Awake()
    {
    }

    public override void RecieveReward()
    {
        // TODO display discard menu
        Destroy(gameObject);
    }
}
