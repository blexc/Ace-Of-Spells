using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanCard : MonoBehaviour
{
    public int damage;
    public GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * 10, ForceMode2D.Impulse);
        StartCoroutine(DestroyThis());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerStats>().TakeDamage(damage);
            Destroy(this.gameObject);
        }

        if(other.gameObject.tag != "BossCard" && other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);

        }

        if(other.gameObject.tag == "Wall")
        {
            Instantiate(enemy, this.gameObject.transform.parent);
            GetComponent<BoxCollider2D>().enabled = false;
        }

    }

     

    public IEnumerator DestroyThis()
    {
        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);
    }
}
