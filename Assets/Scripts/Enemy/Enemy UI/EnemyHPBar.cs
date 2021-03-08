using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPBar : MonoBehaviour
{
    //Variable Initialization and Declaration
    private Quaternion rotation; //Variable to keep track of the rotation
    private Vector3 pos; //Vector3 to keep track of the position and stay above the enemy at all times


    //AHL - (3/4/21)
    //On awake the UI sets the current rotation to keep it as the standard throughout gameplay
    private void Awake()
    {
        rotation = transform.rotation;
        pos = transform.parent.position - transform.position;
    }

    //Every frame the UI adjusts to make sure that it never rotates
    private void Update()
    {
        transform.rotation = rotation;
        transform.position = transform.parent.position - pos;
    }
}
