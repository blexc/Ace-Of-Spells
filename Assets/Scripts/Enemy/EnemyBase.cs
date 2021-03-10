using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public bool IsFrozen { get { return frozenTimer > 0; } }

    public int healthMax;
    public int attackDmg;
    public int attackSpd; // in seconds

    // key=>status effect
    // value=>life time
    public List<KeyValuePair<StatusEffect, float>> statusEffects =
        new List<KeyValuePair<StatusEffect, float>>();

    int health;
    float attackCooldownTimer;
    Color originalColor;

    // if greater than 0, then you cannot move
    public float frozenTimer;

    void Start()
    {
        health = healthMax;
        GetComponentInChildren<EnemyUI>().HPMax = healthMax;
        attackCooldownTimer = attackSpd;
        originalColor = GetComponent<SpriteRenderer>().color;

        frozenTimer = -1f;
    }

    void Update()
    {
        // if the freeze timer isn't inactive
        if (!Mathf.Approximately(-1f, frozenTimer))
        {
            // decrement timer
            frozenTimer = Mathf.Max(0f, frozenTimer - Time.deltaTime);

            // if the timer hits 0, unfreeze the enemy and
            // set the timer to be inactive
            if (Mathf.Approximately(frozenTimer, 0f))
            {
                // "thaw" the enemy
                frozenTimer = -1f;
                if (HasStatusEffect(StatusEffect.Freeze))
                {
                    // deal percentage damage to self once unthawed from freeze
                    int finalDmg = (int)(healthMax * 0.3f);
                    if (HasStatusEffect(StatusEffect.Rot))
                        finalDmg *= 2;
                    TakeDamage(finalDmg);
                }
            }
        }

        // if the enemy is using AI pathfinding and their frozen, stop them
        if (GetComponent<Pathfinding.AIPath>())
            GetComponent<Pathfinding.AIPath>().canMove = !IsFrozen;


        // attack in intervals
        attackCooldownTimer -= Time.deltaTime;
        if (attackCooldownTimer < 0) Attack();

        // enemy dies when 0 or less health
        if (health <= 0) Destroy(gameObject);

        // iterate through all status effects
        // decrement the life time of each status effect
        // remove it if the time is less than or equal to 0
        for (int i = statusEffects.Count; --i >= 0;)
        {
            float newTime = statusEffects[i].Value - Time.deltaTime;

            if (newTime > 0)
            {
                // decrement the time, if the effect still lingers...
                var temp = new KeyValuePair<StatusEffect, float>(
                    statusEffects[i].Key, newTime);

                statusEffects[i] = temp;
            }
            else
            {
                statusEffects.RemoveAt(i);
                ChangeColor();
            }
        }
        //PrintStatusEffectList();
    }

    // child enemy classes will override this function
    protected virtual void Attack()
    {
        //print("parent attack");
    }

    void ChangeColor()
    {
        Color c = originalColor;

        if (statusEffects.Count == 0)
        {
            GetComponent<SpriteRenderer>().color = c;
            return;
        }

        switch (statusEffects[0].Key)
        {
            case StatusEffect.Freeze:   c = Color.cyan; break;
            case StatusEffect.Rot:      c = Color.magenta; break;
            case StatusEffect.Ignite:   c = Color.red; break;
            case StatusEffect.Bramble:  c = Color.green; break;
            case StatusEffect.Shock:    c = Color.yellow; break;
            default: c = originalColor; break;
        }

        GetComponent<SpriteRenderer>().color = c;
    }

    public void AddStatusEffect(StatusEffect effect, int durationSeconds)
    {
        var pair = new KeyValuePair<StatusEffect, float>(effect, durationSeconds);
        statusEffects.Add(pair);
        ChangeColor();
    }

    public void TakeDamage(int amount)
    {
        // take more damage if shocked
        if (HasStatusEffect(StatusEffect.Shock))
        {
            // mutiply damage by two
            amount *= 2;

            // remove that shock from the list
            RemoveEffect(StatusEffect.Shock);
        }

        health -= amount;

        print(gameObject.name + ": took" + amount + " damage | " + health + " / " + healthMax);
        GetComponentInChildren<EnemyUI>().damagePopUP(amount); //Calls the spawn of the enemy damage text in the UI script - AHL (3/9/21)
        GetComponentInChildren<EnemyUI>().enemyHPUpdate(health); //Adjusts the enemey HP bar in the UI script - AHL (3/3/21)
    }

    void PrintStatusEffectList()
    {
        string msg = "";

        for (int i = statusEffects.Count; --i >= 0;)
            msg += i + ": " + statusEffects[i].Key + " " + statusEffects[i].Value + " | ";

        print(msg);
    }

    // will return whether or not this enemy has a certain
    // status effect applied to it
    public bool HasStatusEffect(StatusEffect se)
    {
        // scan the list of status effects..
        for (int i = statusEffects.Count; --i >= 0;)
        {
            if (statusEffects[i].Key == se)
                return true;
        }

        return false;
    }

    // return true if remove effect, false otherwise
    // removes ONE effect of a specific type (not all stacked types)
    bool RemoveEffect(StatusEffect se)
    {
        // scan the list of status effects..
        for (int i = statusEffects.Count; --i >= 0;)
        {
            if (statusEffects[i].Key == se)
            {
                statusEffects.RemoveAt(i);
                print("removed effect from list");
                ChangeColor();
                return true;
            }
        }

        return false;
    }

    // begins to freeze the enemy for a specified amount of time
    public void FreezeCharacter(float amountSec = 1)
    {
        frozenTimer = amountSec;
    }
}
