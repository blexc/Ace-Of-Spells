using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script keeps track of the number of enemies in the room
public class EnemyRoomManager : MonoBehaviour
{
    public bool DefeatedAllEnemies { get { return enemiesLeft == 0; } }

    public GameObject rewardObject;
    [SerializeField] int enemiesLeft;
    [SerializeField] int checkFrequency = 3;
    
    private void Start()
    {
        enemiesLeft = FindObjectsOfType<EnemyBase>().Length;
        StartCoroutine("CheckEnemyCount");
    }

    /// <summary>
    /// checks the number of enemies every checkFrequency seconds
    /// </summary>
    /// <returns></returns>
    IEnumerator CheckEnemyCount()
    {
        enemiesLeft = FindObjectsOfType<EnemyBase>().Length;
        yield return new WaitForSeconds(checkFrequency);

        if (!DefeatedAllEnemies)
        {
            // check for enemies again
            StartCoroutine("CheckEnemyCount");
        }
        else
        {
            // unlock all of the doors, do not check for enemies again
            foreach (Door d in FindObjectsOfType<Door>()) d.Unlock();

            // activate the reward
            if (rewardObject) rewardObject.SetActive(true); 
        }
    }
}
