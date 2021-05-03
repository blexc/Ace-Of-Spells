using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamethrowerSpellProjectile : Spell
{
    [SerializeField] float timeDuration = 0.6f;

    float angleMax = 10f;
    Vector2 origin, middlePos, target;
    float u, timeStart; 
    bool moving;

    int numEnemiesHit = 0;

    protected override void Start()
    {
        origin = transform.position;
        target = PlaceAtMousePos();

        float len = Vector2.Distance(origin, target) / 2;
        float angle = Vector2.Angle(origin, target);

        // either rotate clockwise or counterclockwise by random degrees
        angle += Random.Range(-angleMax, angleMax);
        angle *= Mathf.Deg2Rad;

        Vector2 offset = new Vector2(len * Mathf.Cos(angle), len * Mathf.Sin(angle));

        middlePos = offset + origin;

        // don't move by velocity, rather by MoveTowards
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        timeStart = Time.time;
        moving = true;

        transform.position = origin;
    }

    protected override void Update()
    {
        // if no enemies in the room
        if (!moving)
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
            Vector2 c2 = target;
            Vector2 p01, p12, p012;
            p01 = (1 - u) * c0 + u * c1;
            p12 = (1 - u) * c1 + u * c2;
            p012 = (1 - u) * p01 + u * p12;

            transform.position = p012;
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        EnemyBase eb = other.gameObject.GetComponent<EnemyBase>();
        if (eb)
        {
            if (!eb.HasStatusEffect(StatusEffect.Ignite))
            {
                eb.AddStatusEffect(StatusEffect.Ignite);
                var burnerInstance = Instantiate(burnerPrefab, eb.transform);
                burnerInstance.GetComponent<DamageOverTime>().Init(0.5f, effectlifeTime, 1);
            }

            DealDamageTo(eb);
            numEnemiesHit++;
        }

        // destroy after hitting three enemies
        if (numEnemiesHit > 3)
        {
            Destroy(gameObject);
        }
    }
}
