using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//a spell that spawns many tiny projectiles over time that interpolate to their target.
public class FlurrySpell : Spell
{
    [SerializeField] GameObject flurrySpellProjectilePrefab;

    public override void InitSpell()
    {
        PlaceAtMousePos();

        InvokeRepeating(nameof(CastProjectile), 0f, 0.1f);
    }

    void CastProjectile()
    {
        GameObject go = Instantiate(flurrySpellProjectilePrefab, gameObject.transform, false);
        go.transform.parent = null;
    }
}
