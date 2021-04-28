using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Hurts X nearest enemies, where X is the number of cards discarded.
// Each enemy spawns 5 projectiles from them.
public class ImplodeSpell : Spell
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

        // all enemies on screen get in enemies list
        Collider2D[] colliders = Physics2D.OverlapAreaAll(topLeftWorld, bottomRightWorld);
        List<EnemyBase> enemies = new List<EnemyBase>();
        foreach (Collider2D c in colliders)
        {
            EnemyBase eb = c.GetComponent<EnemyBase>();
            if (eb) enemies.Add(eb);
        }
    }
}
