using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public int healthMax;
    public int attackDmg;
    public int attackSpd; // in seconds
    public StatusEffect statusEffect;

    int health;
    float attackCooldownTimer;

    void Start()
    {
        health = healthMax;
        attackCooldownTimer = attackSpd;
    }

    void Update()
    {
        attackCooldownTimer -= Time.deltaTime;
        if (attackCooldownTimer < 0) Attack();

        if (health < 0) Destroy(gameObject);
    }

    // child enemy classes will override this function
    protected virtual void Attack()
    {
        print("parent attack"); 
    }

    public void ApplyStatusEffect(StatusEffect effect)
    {
        statusEffect = effect;
    }
}
