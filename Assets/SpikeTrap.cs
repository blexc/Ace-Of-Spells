using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{

    public int damage;
    public float triggerTime;
    public float spikesCooldown;

    private bool canSpikes;
    private GameObject player;
    private bool isOnSpikes;

    // Start is called before the first frame update
    void Start()
    {
        canSpikes = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            isOnSpikes = true;
            player = other.gameObject;
            if(canSpikes)
            {
                StartCoroutine(SpikeWait());
                canSpikes = false;

            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isOnSpikes = false;
            
        }
    }

    public IEnumerator SpikeWait()
    {
        yield return new WaitForSeconds(triggerTime);
        if(isOnSpikes)
        {
            player.GetComponent<PlayerStats>().TakeDamage(damage);
        }
        yield return new WaitForSeconds(.5f);

        GetComponent<SpriteRenderer>().color = Color.white;

        yield return new WaitForSeconds(spikesCooldown);

        canSpikes = true;
    }
}
