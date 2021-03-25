using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script will destroy the gameobject attached in a specified
// amount of time
public class Destroy : MonoBehaviour
{
    public float timeToDestroySec = 1.5f;

    void Start()
    {
        Destroy(gameObject, timeToDestroySec);
    }
}
