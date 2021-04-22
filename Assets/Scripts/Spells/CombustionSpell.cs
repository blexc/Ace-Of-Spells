using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* apply ignite to all enemies on screen
*/
public class CombustionSpell : Spell
{
    public override void InitSpell()
    {
        EnemyBase[] enemies = FindObjectsOfType<EnemyBase>();
        int debugCount = 0;
        foreach (EnemyBase e in enemies)
        {
            if (e.GetComponent<Renderer>().isVisible)
            {
                e.AddStatusEffect(StatusEffect.Ignite);
                debugCount++;
            }
        }

        print(debugCount);

        // TODO visual effect
    }
}
