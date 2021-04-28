using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectThrown : MonoBehaviour
{
    private GameObject player;

    private Transform startPos;
    private Transform endPos;

    // Time to move from sunrise to sunset position, in seconds.
    public float journeyTime = 5.0f;

    // The time at which the animation started.
    private float startTime;

    private int damage;

    public float slowTime;
    public float slowAmount;
    public float force;
    private bool struck = false;
    private Vector2 lastPos;
    Rigidbody2D rb;
    void Start()
    {
        // Note the time at the start of the animation.
        //startPos = transform;
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        
        //endPos = player.transform;
        //startTime = Time.time;
        StartCoroutine(LastPos());
        StartCoroutine(DestroyThis());
        Thrown();
    }

    void Update()
    {
        //transform.position = Vector2.MoveTowards(this.gameObject.transform.position, lastPos, .1f);

   

        /*
        endPos = lastPos;

        // The center of the arc
        Vector3 center = (startPos.position + endPos.position) * 0.5F;

        // move the center a bit downwards to make the arc vertical
        center -= new Vector3(0, 10, 0);

        // Interpolate over the arc relative to center
        Vector3 riseRelCenter = startPos.position - center;
        Vector3 setRelCenter = endPos.position - center;

        // The fraction of the animation that has happened so far is
        // equal to the elapsed time divided by the desired time for
        // the total journey.
        float fracComplete = (Time.time - startTime) / journeyTime;

        transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, fracComplete);
        transform.position += center;
        */

    }


    public void Thrown()
    {
        Vector2 dir = (player.transform.position - transform.position).normalized;
        rb.velocity = dir * force;
    }

    public IEnumerator LastPos()
    {
        lastPos = player.transform.position;
        yield return new WaitForSeconds(3);
        StartCoroutine(LastPos());
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
  
        if(other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(player.GetComponent<PlayerStats>().SlowPlayer(slowTime, slowAmount));

            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            player.gameObject.GetComponent<PlayerStats>().TakeDamage(3);
            struck = true;
           
        }

        if (other.gameObject.tag == "Wall" && !struck)
        {
            Destroy(gameObject);
        }

    }

    public IEnumerator DestroyThis()
    {

        yield return new WaitForSeconds(2f);
       
        if (!struck)
        {
            Destroy(this.gameObject);
        }
    }

    /*
    void Start()
    {
        player = GameObject.Find("Player");

        target = player.transform.position;
        startPosition = transform.position;

    }
    void Update()
    {
                  
        t += Time.deltaTime / timeToReachTarget;
        transform.position = Vector3.Slerp(startPosition, target, t);

    }
    */
}
