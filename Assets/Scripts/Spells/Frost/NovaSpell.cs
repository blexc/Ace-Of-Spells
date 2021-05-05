using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//spell that casts around the player and freezes enemies around the player.
public class NovaSpell : Spell
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

        FadeOutAlpha();

        if (playerTransform)
            transform.position = playerTransform.position;
    }

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
