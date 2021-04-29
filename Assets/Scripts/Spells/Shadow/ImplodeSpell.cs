using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Hurts X nearest enemies, where X is the number of cards discarded.
// Each enemy spawns 5 projectiles from them.
public class ImplodeSpell : Spell
{
    [SerializeField] GameObject projectilePrefab = null;
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

        // sort enemy list by how close they are to the spell (and therefore the mouse)
        PlaceAtMousePos();
        enemies.Sort(SortByDistanceToMe);

        int numEnemiesToImplode = Mathf.Max(1, Deck.instance.NumCardsInDiscard + 1);
        for (int i = 0; i < numEnemiesToImplode && i < enemies.Count; i++)
        {
            enemies[i].TakeDamage((int)spellDamage);
            StartCoroutine(ShootProjectiles(enemies[i]));
        }
    }

    IEnumerator ShootProjectiles(EnemyBase eb)
    {
        // shoot five projectiles in a circle outwards
        for (int theta = 0; theta < 360; theta+=72)
        {
            if (!eb) break;

            // find position with no collision with enemies
            Vector3 pos;

            float dist = 3f;
            float yComp = Mathf.Sin(theta * Mathf.Deg2Rad) * dist; 
            float xComp = Mathf.Cos(theta * Mathf.Deg2Rad) * dist;
            pos = eb.transform.position + new Vector3(xComp, yComp);

            Quaternion quaternion = Quaternion.Euler(0f, 0f, theta);
            var proj = Instantiate(projectilePrefab, pos, quaternion);

            proj.transform.localScale *= 0.5f;

            yield return new WaitForSeconds(0.1f);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        // DO NOTHING.
    }
}