using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CigarEnemy : MonoBehaviour
{
    public GameObject cigarBurn;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CigarBurn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator CigarBurn()
    {
        Instantiate(cigarBurn, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(.5f);
        StartCoroutine(CigarBurn());
    }
}
