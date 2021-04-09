using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//a slow-moving orb that does damage to units around it every second.
public class OrbSpell : Spell
{
    [SerializeField] GameObject trailObject, trailTarget;

    // all enemies in the orb circle
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

        if (enemies.Count > 0)
        {
            trailObject.SetActive(true);
            if (trailTarget)
            {
                Vector3 trailPos = trailObject.transform.position;
                trailPos = Vector3.MoveTowards(trailPos, trailTarget.transform.position, 100 * Time.deltaTime);
                trailObject.transform.position = trailPos;
            }
        }
        else
        {
            trailObject.SetActive(false);
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

            enemies.Remove(eb);
        }
    }

    /// <summary>
    /// deal damage to all enemies in the enemies list
    /// set target of trail renderer to be a random enemy
    /// </summary>
    void DealDamageToAll()
    {
        foreach (EnemyBase eb in enemies)
        {
            eb.TakeDamage((int)spellDamage);
        }
        int r = Random.Range(0, enemies.Count);
        trailTarget = enemies[r].gameObject; 
    }

}
