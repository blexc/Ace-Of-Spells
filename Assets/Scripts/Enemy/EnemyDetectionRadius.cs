using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// updated so that this child object is responsible
// for detecting whether or not the parent enemy attached
// (assuming with EnemyFollow.cs) should follow the player
public class EnemyDetectionRadius : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            GetComponentInParent<EnemyFollow>().canFollow = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            GetComponentInParent<EnemyFollow>().canFollow = false;
        }
    }

    /// <summary>
    /// set the radius of the circle collider attached to this object
    /// </summary>
    public void SetRadius(int amount)
    {
        GetComponent<CircleCollider2D>().radius = amount;
    }
}
