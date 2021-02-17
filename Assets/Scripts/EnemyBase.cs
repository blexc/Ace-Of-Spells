using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public StatusEffect SetStatusEffect
    {
        set
        {
            statusEffect = value;
            ChangeColor();
        }
    }

    public int healthMax;
    public int attackDmg;
    public int attackSpd; // in seconds
    StatusEffect statusEffect;

    int health;
    float attackCooldownTimer;

    void Start()
    {
        statusEffect = StatusEffect.Normal;
        health = healthMax;
        attackCooldownTimer = attackSpd;
    }

    void Update()
    {
        // attack in intervals
        attackCooldownTimer -= Time.deltaTime;
        if (attackCooldownTimer < 0) Attack();

        // enemy dies when 0 or less health
        if (health <= 0) Destroy(gameObject);
    }

    // child enemy classes will override this function
    protected virtual void Attack()
    {
        print("parent attack"); 
    }

    void ChangeColor()
    {
        Color c;

        switch (statusEffect)
        {
            case StatusEffect.Normal:   c = Color.white; break;
            case StatusEffect.Freeze:   c = Color.blue; break;
            case StatusEffect.Rot:      c = Color.green; break;
            case StatusEffect.Ignite:   c = Color.red; break;
            case StatusEffect.Sap:      c = Color.gray; break;
            case StatusEffect.Shock:    c = Color.yellow; break;
            default: c = Color.white; break;
        }

        GetComponent<SpriteRenderer>().color = c;
    }
}
