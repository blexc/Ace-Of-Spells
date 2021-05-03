using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Constant damage around player for 3 seconds. Destroys enemy projectiles
public class ShroudSpell : Spell
{
    Transform playerTransform;

    protected override void Start()
    {
        playerTransform = FindObjectOfType<PlayerMovement>().transform;
    }

    /// <summary>
    /// stick with the player
    /// </summary>
    protected override void Update()
    {
        base.Update();

        if (playerTransform)
            transform.position = playerTransform.position;

        FadeOutAlpha();
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        //if hits an enemy
        var eb = other.gameObject.GetComponent<EnemyBase>();
        if (eb)
        {
            if (!eb.HasStatusEffect(StatusEffect.Rot))
               eb.AddStatusEffect(StatusEffect.Rot, effectlifeTime);
            DealDamageTo(eb);
        }
        else if (other.gameObject.CompareTag("Projectile"))
        {
            // destroy projectiles upon collision
            Destroy(other.gameObject);
        }
    }
}
