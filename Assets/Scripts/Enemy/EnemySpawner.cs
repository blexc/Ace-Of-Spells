using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public bool HasEnemiesToSpawn { get { return enemiesToSpawn.Count > 0; } }

    [SerializeField] List<GameObject> enemiesToSpawn;
    [SerializeField] float spawnFrequency; // in seconds

    void Start() { StartCoroutine("SpawnNextEnemy"); }
    
    IEnumerator SpawnNextEnemy()
    {
        yield return new WaitForSeconds(spawnFrequency);

        if (HasEnemiesToSpawn)
        {
            // spawn more enemies relative to spawn pos
            var spawnPos = transform.position;
            var e = Instantiate(enemiesToSpawn[0], spawnPos, Quaternion.identity);

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
