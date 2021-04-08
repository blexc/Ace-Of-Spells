using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script keeps track of the number of enemies in the room
public class EnemyRoomManager : MonoBehaviour
{
    // returns true if there are no enemy instances in the scene nor
    // are there enemies to be spawned
    public bool DefeatedAllEnemies
    {
        get
        {
            // if AT LEAST ONE of the spawners are spawning, its true
            bool roomHasEnemiesToSpawn = false;
            foreach (EnemySpawner es in enemySpawners)
            {
                if (es.HasEnemiesToSpawn && es.gameObject.activeSelf)
                {
                    roomHasEnemiesToSpawn = true;
                    break;
                }
            }
            return enemiesLeft == 0 && !roomHasEnemiesToSpawn;
        }
    }

    public GameObject rewardObject;

    [SerializeField] int enemiesLeft;
    [SerializeField] int checkFrequency = 3;

    // used to make sure there are no more enemies to spawn
    // before spawning reward
    [SerializeField] EnemySpawner[] enemySpawners;
    
    private void Start()
    {
        rewardObject.SetActive(false); 
        enemiesLeft = FindObjectsOfType<EnemyBase>().Length;
        enemySpawners = FindObjectsOfType<EnemySpawner>();
        StartCoroutine("CheckEnemyCount");
    }

    /// <summary>
    /// checks the number of enemies every checkFrequency seconds
    /// </summary>
    /// <returns></returns>
    IEnumerator CheckEnemyCount()
    {
        yield return new WaitForSeconds(checkFrequency);

        enemiesLeft = FindObjectsOfType<EnemyBase>().Length;
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
