using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Target an enemy. Pull projectiles out of that enemy, and towards you.
// For each enemy hit by the projectile, heal 1 hp. 
public class SiphonSpell : Spell
{
    [SerializeField] GameObject siphonProjectile = null;
    int numProjectiles = 8;

    Transform target;

    /// <summary>
    /// stick with the player
    /// </summary>
    protected override void Update()
    {
        base.Update();

        if (target) transform.position = target.position;

        OscAlpha(0.1f);
    }

    public override void InitSpell()
    {
        Vector2 mousePos = PlaceAtMousePos();
        target = GetClosestEnemy(25f);
        transform.position = mousePos;
        StartCoroutine(SpawnProjectiles());
    }

    IEnumerator SpawnProjectiles()
    {
        for (int i = 0; i < numProjectiles; i++)
        {
            var proj = Instantiate(siphonProjectile);
            proj.transform.position = transform.position;
            proj.GetComponent<Spell>().InitSpell();
            yield return new WaitForSeconds(0.2f);
        }

        Destroy(gameObject);
    }
}
