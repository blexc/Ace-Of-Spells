using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// shoot a flurry of projectiles that move through
// enemies. applies ignite.
public class FlamethrowerSpell : Spell
{
    [SerializeField] GameObject flamethrowerProjectilePrefab = null;

    public override void InitSpell()
    {
        if (!flamethrowerProjectilePrefab)
        {
            Debug.LogError("projectile not attached");
            return;
        }
        StartCoroutine(ProjectFlames());
    }

    IEnumerator ProjectFlames()
    {
        while (true)
        {
            var proj = Instantiate(flamethrowerProjectilePrefab);
            proj.GetComponent<Spell>().InitSpell();
            yield return new WaitForSeconds(0.05f); 
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        // DO NOTHING.
    }
}
