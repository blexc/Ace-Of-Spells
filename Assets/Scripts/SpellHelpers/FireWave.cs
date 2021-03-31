using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWave : MonoBehaviour
{
    private int numberOfWaves = 1;
    public GameObject FireWavePrefab;
    public GameObject deck;

    private void Start()
    {
        deck  = GameObject.Find("Deck");
        StartCoroutine(Wait());
        numberOfWaves = 3;
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        print("hit something");
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        print("hit something");

    }

    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }

}
