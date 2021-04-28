using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWave : MonoBehaviour
{
    public GameObject FireWavePrefab;
    public GameObject deck;

    private void Start()
    {
        deck  = GameObject.Find("Deck");

        // I made Deck singleton, so you can refer to Deck like this:
        // Deck.instance.DiscardCardUntil(CardType.Fire, false);
        // -- Alex (delete after read)

        StartCoroutine(Wait());
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
