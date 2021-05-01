using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//a slow-moving orb that does damage to units around it every second.
// every "damageTimerStart" seconds, deal damage to a random enemy
public class OrbSpell : Spell
{
    [SerializeField] GameObject trailObject = null, trailTarget = null;

    // all enemies in the orb circle
    [SerializeField] List<EnemyBase> enemies = new List<EnemyBase>();

    float damageTimer, damageTimerStart;

    public override void InitSpell()
    {
        PlaceAtMousePos();
        damageTimerStart = 0.25f;
        damageTimer = damageTimerStart;
    }

    protected override void Update()
    {
        base.Update();

        // apply damage to all enemies every second
        damageTimer -= Time.deltaTime;
        if (damageTimer <= 0f && enemies.Count > 0)
        {
            DealDamageToAll();
            damageTimer = damageTimerStart;
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

        print("enemies: " + enemies.Count);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            var eb = other.gameObject.GetComponent<EnemyBase>();
            enemies.Add(eb);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            var eb = other.gameObject.GetComponent<EnemyBase>();
            enemies.Remove(eb);
        }
    }

    /// <summary>
    /// deal damage to a random enemy in the enemies list
    /// set target of trail renderer to be that enemy
    /// </summary>
    void DealDamageToAll()
    {
        int r = Random.Range(0, enemies.Count);
        if (enemies[r]!=null)
        {
            enemies[r].TakeDamage((int)spellDamage);
            trailTarget = enemies[r].gameObject;
        }

    }

}
