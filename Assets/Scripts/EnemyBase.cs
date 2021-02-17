using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public StatusEffect SetStatusEffect { set { statusEffect = value; } }

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
}
