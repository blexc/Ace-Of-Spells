using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlurrySpellProjectile : Spell
{
    Transform target;

    protected override void Start()
    {
        target = GetClosestEnemy();        

        // don't move by velocity, rather by MoveTowards
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }

    protected override void Update()
    {
        base.Update();


        // seek enemy, if one exists
        if (target)
        {
            Vector3 pos = transform.position;
            pos = Vector3.MoveTowards(pos, target.position, Time.deltaTime * spellSpeed);
            transform.position = pos;
        }
    }
    
    Transform GetClosestEnemy()
    {
        EnemyBase[] enemies = FindObjectsOfType<EnemyBase>();

        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (EnemyBase t in enemies)
        {
            float dist = Vector3.Distance(t.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = t.transform;
                minDist = dist;
            }
        }
        return tMin;
    }

}
