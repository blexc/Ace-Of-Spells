using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Circle that pulls enemies towards its center.
public class BlackHoleSpell : Spell
{
    Vector3 center;
    [SerializeField] float gravityStrength = 0.05f;

    public override void InitSpell()
    {
        PlaceAtMousePos();
        center = transform.position;
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        // DO NOTHING.
    }

    protected void OnTriggerStay2D(Collider2D other)
    {
        // pull enemies towards center
        var eb = other.gameObject.GetComponent<EnemyBase>();
        if (eb)
        {
            eb.FreezeCharacter(0.1f);
            Vector3 enemyPos = eb.transform.position;
            enemyPos = Vector3.MoveTowards(enemyPos, center, gravityStrength);
            eb.GetComponent<Rigidbody2D>().MovePosition(enemyPos);
        }
    }
}
