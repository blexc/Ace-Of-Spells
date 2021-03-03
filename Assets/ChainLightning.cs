using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainLightning : MonoBehaviour
{
    public GameObject spellPrefab;

    public List<GameObject> triggerList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);

            //other.gameObject.GetComponent<EnemyStats>().enemyHealth -= 10;

            if (!triggerList.Contains(other.gameObject))
            {
                triggerList.Add(other.gameObject);
                if (triggerList.Count > 1)
                {

                    Instantiate(spellPrefab, other.gameObject.transform.position, Quaternion.identity);

                }
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

    }
}
