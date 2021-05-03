using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarknessSpellProjectile : Spell
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        EnemyBase eb = other.GetComponent<EnemyBase>();
        if (eb)
        {
            DealDamageTo(eb);
            Destroy(gameObject);
        }
    }
}
