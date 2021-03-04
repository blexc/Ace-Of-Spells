using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this object will burn the ENEMY it's attached to for a certain amount of 
// ticks and for a certain amount of time
public class Burner : MonoBehaviour
{
    [SerializeField] float tickRate = 0.5f; // in seconds
    [SerializeField] float lifeTime = 5f; // in seconds
    [SerializeField] int damagePerTick = 1;
    float tick;
    EnemyBase myEnemy;

    /// <summary>
    /// setup the burner script, 
    /// tr = Tick Rate, 
    /// lt = Life Time, 
    /// dmg = Damage Per Tick
    /// </summary>
    /// <param name="tr">Tick Rate</param>
    /// <param name="lt">Life Time</param>
    /// <param name="dmg">Damage Per Tick</param>
    public void Init(float tr, float lt, int dmg)
    {
        tickRate = tr;
        lifeTime = lt;
        damagePerTick = dmg;
    }

    private void Start()
    {
        myEnemy = GetComponentInParent<EnemyBase>();
        tick = tickRate;
    }

    void Update()
    {
        // destroy object when time is up
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0) Destroy(gameObject);

        // apply damage and reset timer
        tick -= Time.deltaTime;
        if (tick < 0)
        {
            myEnemy.TakeDamage(damagePerTick);
            tick = tickRate;
        } 
    }
}
