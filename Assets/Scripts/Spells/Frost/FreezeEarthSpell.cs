﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this spell will freeze enemies in its path
// and will die once over a certain amount of time
// or when it hits a wall
public class FreezeEarthSpell : Spell
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        //if hits an enemy
        if (other.gameObject.CompareTag("Enemy"))
        {
            var eb = other.gameObject.GetComponent<EnemyBase>();
            eb.AddStatusEffect(StatusEffect.Freeze, effectlifeTime);
            eb.FreezeCharacter(effectlifeTime);
        }
    }
}
