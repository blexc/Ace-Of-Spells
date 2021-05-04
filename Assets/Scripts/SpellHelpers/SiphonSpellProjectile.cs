using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiphonSpellProjectile : Spell
{
    float speed = 25f;
    Transform target = null;
    int healAmount = 0;

    public override void InitSpell()
    {
        // target is the player
        target = FindObjectOfType<PlayerStats>().transform;
    }

    protected override void Update()
    {
        base.Update();
        if (target)
        {
            // MoveTowardsNearestEnemy
            Vector3 pos = transform.position;
            pos = Vector3.MoveTowards(pos, target.position, Time.deltaTime * speed);
            transform.position = pos;
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        EnemyBase eb = other.gameObject.GetComponent<EnemyBase>();
        if (eb)
        {
            healAmount++;
            DealDamageTo(eb);
            IncreaseAlpha(0.4f);
        }

        PlayerStats ps = other.gameObject.GetComponent<PlayerStats>();
        if (ps)
        {
            // once you hit the player...
            if (healAmount > 0) ps.Heal(healAmount);
            Destroy(gameObject);
        }
    }
}







