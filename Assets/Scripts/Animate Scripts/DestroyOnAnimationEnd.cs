using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnAnimationEnd : MonoBehaviour
{
    /// <summary>
    /// Function to destroy the animation and the parent object holding it - AHL (3/9/21)
    /// </summary>
    public void DestroyParent()
    {
        //Assigns the parent of the current game object that this script is attached to
        GameObject parent = gameObject.transform.parent.gameObject;
        Destroy(parent);
    }


}
