using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Teleport to the cursor. Deal damage between your start and new position.
public class LightningJumpSpell : Spell
{
    Vector3 origin, target;
    [SerializeField] float lightningSpellSpeed = 100f;

    public override void InitSpell()
    {
        origin = transform.position;
        target = PlaceAtMousePos();

        GameObject player = FindObjectOfType<PlayerMovement>().gameObject;

        origin = player.transform.position;
        target = transform.position;
        Vector2 dir = new Vector2(target.x - origin.x, target.y - origin.y).normalized;

        float distance = Vector2.Distance(origin, target);
        int wallLayerMask = LayerMask.NameToLayer("Wall");
        bool betweenWall = false;

        // ensure there is no wall between player and target
        RaycastHit2D[] hits = Physics2D.RaycastAll(origin, dir, distance);
        foreach (RaycastHit2D h in hits)
        {
            // you've hit a wall
            if (h.collider.gameObject.layer == wallLayerMask)
            {
                betweenWall = true;

                // teleport where it can...
                player.transform.position = h.point;
                break;
            }
        }

        // ... otherwise teleport player to target
        if (!betweenWall) player.transform.position = transform.position;

        // update the target, in case they hit a wall
        target = player.transform.position;

        // place the spell back to the original player position (before teleport)
        transform.position = origin;

        // make player invinsible
        player.GetComponent<PlayerStats>().MakePlayerInvunerable();
    }

    protected override void Update()
    {
        base.Update();

        // move the lightning spell object towards the target (where the player teleported)
        Vector3 pos = transform.position;
        pos = Vector3.Lerp(pos, target, Time.deltaTime * 1 / lightningSpellSpeed);
        transform.position = pos;

        // remove the object once it reaches position teleported
        if (Vector3.Distance(pos, target) < 0.01f) Destroy(gameObject);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            var eb = other.gameObject.GetComponent<EnemyBase>();
            DealDamageTo(eb);
        }
    }
}
