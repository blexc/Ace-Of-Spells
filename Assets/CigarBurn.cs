using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CigarBurn : MonoBehaviour
{
    public int damage;

    private GameObject player;

    private bool canDamage = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay2D(Collision2D other)
    {
      //  if(other.gameObject.tag == "Player")
       // {
        //    other.gameObject.GetComponent<PlayerStats>().TakeDamage(damage);
      //  }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            player = other.gameObject;
            if(canDamage)
            {
                StartCoroutine(DamagePlayer());

            }
        }
    }

    public IEnumerator DamagePlayer()
    {
        if(canDamage)
        {
            canDamage = false;

            player.gameObject.GetComponent<PlayerStats>().TakeDamage(damage);

        }
        yield return new WaitForSeconds(.5f);

        canDamage = true;
    }

    public IEnumerator DestroyThis()
    {
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
    }
}
