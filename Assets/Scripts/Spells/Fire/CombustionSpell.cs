using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* apply ignite to all enemies on screen
*/
public class CombustionSpell : Spell
{
    Vector3 topLeftWorld = Vector3.zero;
    Vector3 bottomRightWorld = Vector3.zero;

    public override void InitSpell()
    {
        Camera cam = Camera.main;
        float camZ = Mathf.Abs(cam.transform.position.z);

        // get screen point of top left and bottom right corners
        float h = cam.pixelHeight;
        float w = cam.pixelWidth;
        Vector3 topLeftScreen = new Vector3(w, h, camZ);
        Vector3 bottomRightScreen = new Vector3(0, 0, camZ);

        // convert screen positions to world positions
        topLeftWorld = cam.ScreenToWorldPoint(topLeftScreen);
        topLeftWorld.z = 0f;
        bottomRightWorld = cam.ScreenToWorldPoint(bottomRightScreen);
        bottomRightWorld.z = 0f;

        // apply ignite to all enemies on screen
        Collider2D[] colliders = Physics2D.OverlapAreaAll(topLeftWorld, bottomRightWorld);
        foreach (Collider2D c in colliders)
        {
            EnemyBase eb = c.GetComponent<EnemyBase>();
            if (eb)
            {
                eb.AddStatusEffect(StatusEffect.Ignite);
            }
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        // DO NOTHING.
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(topLeftWorld, 3f);
        Gizmos.DrawSphere(bottomRightWorld, 3f);
    }
}
