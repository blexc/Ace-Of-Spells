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

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator WaitToStrike()
    {
        yield return new WaitForSeconds(1);
        Instantiate(flameStrikePrefab, this.gameObject.transform.position, Quaternion.identity);
        gameObject.transform.parent = null;
        print("spawned");
    }
}
