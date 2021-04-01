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


    private Transform lastPos;

    void Start()
    {
        // Note the time at the start of the animation.
        startPos = transform;
        player = GameObject.Find("Player");
        endPos = player.transform;
        startTime = Time.time;
        StartCoroutine(LastPos());
        StartCoroutine(DestroyThis());
    }

    void Update()
    {
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


    }

    public IEnumerator LastPos()
    {
        lastPos = player.transform;
        yield return new WaitForSeconds(1);
        StartCoroutine(LastPos());
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
            player.gameObject.GetComponent<PlayerStats>().TakeDamage(3);
        }
    }

    public IEnumerator DestroyThis()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
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
