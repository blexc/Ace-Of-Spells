using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmberSpellProjectile : Spell
{
    float timer = 3f;
    float speed = 40f;
    Transform target = null;

    public override void InitSpell()
    {
        // do nothing.
    }

    protected override void Update()
    {
        base.Update();

        // wait timer seconds to seek target
        if (!Mathf.Approximately(timer, -1f))
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                timer = -1;
                target = GetClosestEnemy(25f);
            }
        }
        else if (target)
        {
            // MoveTowardsNearestEnemy
            Vector3 pos = transform.position;
            pos = Vector3.MoveTowards(pos, target.position, Time.deltaTime * speed);
            transform.position = pos;
        }
        else
        {
            // if you don't have a target, die
            Destroy(gameObject);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        EnemyBase eb = other.gameObject.GetComponent<EnemyBase>();
        if (eb)
        {
            eb.AddStatusEffect(StatusEffect.Ignite);
            eb.TakeDamage((int)spellDamage);
            Destroy(gameObject);
        }
    }
}
