using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// project embers around player that will seek
// a target over time. applies ignite.
public class EmbersSpell : Spell
{
    [SerializeField] GameObject emberProjectilePrefab = null;
    public override void InitSpell()
    {
        if (!emberProjectilePrefab)
        {
            Debug.LogError("projectile not attached");
            return;
        }
        float dist = 5f;
        Transform playerTransform = FindObjectOfType<PlayerMovement>().gameObject.transform;
        
        // spawn 360 tiny projectiles around the player and hurle them towards the enemies
        for (int theta = 0; theta < 360; theta+=10)
        {
            var proj = Instantiate(emberProjectilePrefab, playerTransform, false);

            float yComp = Mathf.Sin(theta * Mathf.Deg2Rad) * dist;
            float xComp = Mathf.Cos(theta * Mathf.Deg2Rad) * dist;

            proj.transform.position += new Vector3(xComp, yComp);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        // DO NOTHING.
    }
}
