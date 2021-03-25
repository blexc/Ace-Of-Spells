using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// a spell that deals damage
// if the enemy is frozen deal double damage
public class IceSpear : Spell
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        //if hits an enemy
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyHit = other.gameObject;
            var eb = enemyHit.GetComponent<EnemyBase>();
            int finalDmg = (int)spellDamage;

            if (eb.HasStatusEffect(StatusEffect.Freeze))
                finalDmg *= 2;

            eb.TakeDamage(finalDmg);
        }

        // delete spell if hits ANYTHING
        // should not be left, since it could hit multiple enemies
        Destroy(gameObject);
    }
}
