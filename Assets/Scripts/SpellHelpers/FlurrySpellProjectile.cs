using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlurrySpellProjectile : Spell
{
    [SerializeField] float timeDuration = 0.32f;

    Vector2 origin, middlePos;
    Transform target;
    float u, timeStart; 
    bool moving;

    protected override void Start()
    {
        origin = transform.position;
        target = GetClosestEnemy(20f);

        // if no enemies in the room
        if (!target)
        {
            Destroy(gameObject);
            return;
        }

        float len = Vector2.Distance(origin, target.position) / 2;
        float angle = Vector2.Angle(origin, target.position);

        // either rotate clockwise or counterclockwise by random degrees
        angle += Random.Range(-45f, 45f);
        angle *= Mathf.Deg2Rad;

        Vector2 offset = new Vector2(len * Mathf.Cos(angle), len * Mathf.Sin(angle));

        middlePos = offset + origin;

        // don't move by velocity, rather by MoveTowards
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        timeStart = Time.time;
        moving = true;
    }

    protected override void Update()
    {
        // if no enemies in the room
        if (!target || !moving)
        {
            Destroy(gameObject);
            return;
        }

        base.Update();

        // seek enemy, if one exists
        if (moving)
        {
            u = (Time.time - timeStart) / timeDuration;
            if (u >= 1f)
            {
                u = 1f;
                moving = false;
            }

            Vector2 c0 = origin;
            Vector2 c1 = middlePos;
            Vector2 c2 = target.position;
            Vector2 p01, p12, p012;
            p01 = (1 - u) * c0 + u * c1;
            p12 = (1 - u) * c1 + u * c2;
            p012 = (1 - u) * p01 + u * p12;

            transform.position = p012;
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        //if hits an enemy
        if (other.gameObject.CompareTag("Enemy"))
        {
            var eb = other.gameObject.GetComponent<EnemyBase>();
            DealDamageTo(eb);
            Destroy(gameObject);
        }
    }
}
