using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// cast Lightning Strike on the cursor 5 times over the duration of the spell.
public class StormUnleashedSpell : Spell
{
    [SerializeField] GameObject lightningStrikeSpellPrefab;

    public override void InitSpell()
    {
        StartCoroutine(SpawnLightningStikes()); 
    }

    IEnumerator SpawnLightningStikes()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject go = Instantiate(lightningStrikeSpellPrefab);
            go.GetComponent<Spell>().InitSpell();
            yield return new WaitForSeconds(0.3f);
        }
    }
}
