using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TossObject : MonoBehaviour
{
    public GameObject objectPrefab;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Throw());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Throw()
    {
        yield return new WaitForSeconds(1);
        Instantiate(objectPrefab, transform.position, Quaternion.identity);
        StartCoroutine(Throw());
    }
}
