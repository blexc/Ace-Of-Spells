using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
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

    void Start()
    {
        health = healthMax;
        attackCooldownTimer = attackSpd;
        originalColor = GetComponent<SpriteRenderer>().color;
    }

    void Update()
    {
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
            case StatusEffect.Freeze:   c = Color.blue; break;
            case StatusEffect.Rot:      c = Color.green; break;
            case StatusEffect.Ignite:   c = Color.red; break;
            case StatusEffect.Sap:      c = Color.gray; break;
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
        // double the damage taken if shocked
        if (HasStatusEffect(StatusEffect.Shock)) amount *= 2;

        health -= amount;

        //print(gameObject.name + ": took" + amount + " damage | " + health + " / " + healthMax); //**AHL - Reference for enemy damage UI**
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
    bool HasStatusEffect(StatusEffect se)
    {
        // scan the list of status effects..
        for (int i = statusEffects.Count; --i >= 0;)
        {
            if (statusEffects[i].Key == se)
                return true;
        }

        return false;
    }
}
