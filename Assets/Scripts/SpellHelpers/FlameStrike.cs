using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameStrike : MonoBehaviour
{
    public GameObject flameStrikePrefab;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitToStrike());
    }

    public IEnumerator WaitToStrike()
    {
        yield return new WaitForSeconds(1);
        SpawnStrike();
     
    }

    public void SpawnStrike()
    {
        Instantiate(flameStrikePrefab, this.gameObject.transform.position, Quaternion.identity);
        gameObject.transform.parent = null;
    }
}
