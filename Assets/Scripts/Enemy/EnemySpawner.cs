using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public bool HasEnemiesToSpawn { get { return enemiesToSpawn.Count > 0; } }

    [SerializeField] List<GameObject> enemiesToSpawn = new List<GameObject>();
    [SerializeField] float spawnFrequency = 1; // in seconds
    int enemiesSpawned;

    void Start()
    {
        enemiesSpawned = 0;
        StartCoroutine("SpawnNextEnemy");
    }
    
    IEnumerator SpawnNextEnemy()
    {
        yield return new WaitForSeconds(spawnFrequency);

        if (HasEnemiesToSpawn)
        {
            // spawn more enemies relative to spawn pos
            var spawnPos = transform.position;
            var e = Instantiate(enemiesToSpawn[0], spawnPos, Quaternion.identity);

            // increment counter and update the name of the enemy
            enemiesSpawned++;
            e.name = gameObject.name + "_" + enemiesToSpawn[0].name + "_" + enemiesSpawned.ToString();

            // give them a target
            var playerTransform = FindObjectOfType<PlayerStats>().gameObject.transform;
            var destSetter = e.GetComponent<Pathfinding.AIDestinationSetter>();
            if (destSetter) destSetter.target = playerTransform;

            // remove enemy from the list
            enemiesToSpawn.RemoveAt(0);

            // start cycle again until there are no more enemies
            StartCoroutine("SpawnNextEnemy");
        }
    }
}
