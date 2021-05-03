using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//a large AoE spell that does damage every second and applys a slow to targets within the area.
public class BlizzardSpell : Spell
{
    // all enemies in the blizzard circle
    [SerializeField] List<EnemyBase> enemies = new List<EnemyBase>();

    float damageTimer;

    public override void InitSpell()
    {
        PlaceAtMousePos();
        damageTimer = 1f;
    }

    protected override void Update()
    {
        base.Update();

        // apply damage to all enemies every second
        damageTimer -= Time.deltaTime;
        if (damageTimer <= 0f && enemies.Count > 0)
        {
            DealDamageToAll();
            damageTimer = 1f;
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        //if hits an enemy
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyHit = other.gameObject;
            var eb = enemyHit.GetComponent<EnemyBase>();
            enemies.Add(eb);
            
            // if this is a unique enemy, apply damage to all enemies
            // (restart timer)
            if (!enemies.Contains(eb)) damageTimer = 0f;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            var eb = enemyHit.GetComponent<EnemyBase>();

            // if you're slow, stop being slow
            if (eb.IsSlowed) eb.RemoveEffect(StatusEffect.Slow);

            enemies.Remove(eb);
        }
    }

    /// <summary>
    /// deal damage to all enemies in the enemies list
    /// slows enemy (if not slowed already)
    /// </summary>
    void DealDamageToAll()
    {
        foreach (EnemyBase eb in enemies)
        {
            DealDamageTo(eb);

            if (!eb.IsSlowed) eb.AddStatusEffect(StatusEffect.Slow);
        }
    }
}
