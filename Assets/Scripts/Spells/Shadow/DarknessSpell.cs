using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Spawns X cone projectiles, where X is the number of cards in discard.
public class DarknessSpell : Spell
{
    [SerializeField] GameObject darknessProjectilePrefab = null;

    float timeBetweenShots;
    int numProjectiles;

    public override void InitSpell()
    {
        numProjectiles = Deck.instance.NumCardsInDiscard + 1;
        timeBetweenShots = spellLifetime / numProjectiles;
        StartCoroutine(FireProjectile());
    }

    IEnumerator FireProjectile()
    {
        for (int i = 0; i < numProjectiles; i++)
        {
            var pa = FindObjectOfType<PlayerAttack>();
            var proj = Instantiate(darknessProjectilePrefab, pa.transform.position, pa.transform.rotation);
            yield return new WaitForSeconds(timeBetweenShots);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        // DO NOTHING.
    }
}
