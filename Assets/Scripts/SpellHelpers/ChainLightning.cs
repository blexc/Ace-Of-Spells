using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainLightning : MonoBehaviour
{
    public GameObject spellPrefab;

    public List<GameObject> triggerList = new List<GameObject>();

    public int spellDamage = 1;

    void Start()
    {
        StartCoroutine(Destroy());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (!triggerList.Contains(other.gameObject))
            {
                triggerList.Add(other.gameObject);
                if (triggerList.Count > 0)
                {
                    Instantiate(spellPrefab, other.gameObject.transform.position, Quaternion.identity);
                    
                    other.gameObject.GetComponent<EnemyBase>().TakeDamage(spellDamage);
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
