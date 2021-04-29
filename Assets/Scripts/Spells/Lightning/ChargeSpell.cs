using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChargeSpell : Spell
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        //if hits an enemy
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyHit = other.gameObject;
            var eb = enemyHit.GetComponent<EnemyBase>();
            eb.AddStatusEffect(StatusEffect.Shock, effectlifeTime);
        }
        else Destroy(gameObject);
    }

    /// <summary>
    /// set starting position to be the mouse position
    /// resets the velocity
    /// </summary>
    public override void InitSpell()
    {
        //get mouse position
        Vector3 mouse = Mouse.current.position.ReadValue();
        mouse.z = Mathf.Abs(Camera.main.transform.position.z);

        // get screen point
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(mouse);
        worldPoint.z = 0f;

        transform.position = worldPoint;
    }
}
