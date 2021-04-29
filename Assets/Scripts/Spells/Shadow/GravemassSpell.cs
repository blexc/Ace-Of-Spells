using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Slow moving large projectiles that deals more damage if you have cards in your discard.
public class GravemassSpell : Spell
{
    int finalDamage;

    protected override void Start()
    {
        int multiplier = Mathf.Max(1, Deck.instance.NumCardsInDiscard + 1);
        finalDamage = multiplier * (int)spellDamage;
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        var eb = other.gameObject.GetComponent<EnemyBase>();
        if (eb)
        {
            eb.TakeDamage(finalDamage);
            Destroy(gameObject);
        }
    }
}
