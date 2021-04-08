using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//a large AoE spell that does damage every second and applys a slow to targets within the area.
public class BlizzardSpell : Spell
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        // DO NOTHING
    }

    protected void OnTriggerStay2D(Collider2D other)
    {
        //if hits an enemy
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyHit = other.gameObject;
            var eb = enemyHit.GetComponent<EnemyBase>();
            StartCoroutine(DealDamage(eb));
        }
    }

    IEnumerator DealDamage(EnemyBase eb)
    {
        eb.TakeDamage((int)spellDamage);
        yield return new WaitForSeconds(1f);
    }
}
