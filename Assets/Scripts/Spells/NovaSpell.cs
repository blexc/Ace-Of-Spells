using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//spell that casts around the player and freezes enemies around the player.
public class NovaSpell : Spell
{
    /// <summary>
    /// stick with the player
    /// </summary>
    public override void InitSpell()
    {
        var pa = FindObjectOfType<PlayerAttack>();
        if (pa) transform.parent = pa.transform;
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        //if hits an enemy
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyHit = other.gameObject;
            var eb = enemyHit.GetComponent<EnemyBase>();
            eb.AddStatusEffect(StatusEffect.Freeze, effectlifeTime);
            eb.FreezeCharacter(effectlifeTime);
        }
        else
        {
            // delete spell if hits anything other than an enemy
            Destroy(gameObject);
        }
    }
}
