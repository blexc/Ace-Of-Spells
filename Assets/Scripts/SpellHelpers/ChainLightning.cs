using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainLightning : Spell
{
    public GameObject spellPrefab;

    public List<GameObject> triggerList = new List<GameObject>();


    protected override void Start()
    {
        spellDamage = 1;
        StartCoroutine(Destroy());
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (!triggerList.Contains(other.gameObject))
            {
                triggerList.Add(other.gameObject);
                if (triggerList.Count > 0)
                {
                    Instantiate(spellPrefab, other.gameObject.transform.position, Quaternion.identity);
                    
                    DealDamageTo(other.gameObject.GetComponent<EnemyBase>());
                }
            }
        }
    }

    public IEnumerator Destroy()
    {
        yield return new WaitForSeconds(.2f);
        Destroy(this.gameObject);
    }
}
