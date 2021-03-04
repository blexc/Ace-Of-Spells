using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainLightning : MonoBehaviour
{
    public GameObject spellPrefab;

    public List<GameObject> triggerList = new List<GameObject>();

    public int spellDamage;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Destroy());

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {

            //other.gameObject.GetComponent<EnemyStats>().enemyHealth -= 10;

            if (!triggerList.Contains(other.gameObject))
            {
                triggerList.Add(other.gameObject);
                if (triggerList.Count > 0)
                {

                    Instantiate(spellPrefab, other.gameObject.transform.position, Quaternion.identity);
                    //other.gameObject.GetComponent<EnemyBase>().health -= spellDamage;

                }

            }


        }
    }

    public IEnumerator Destroy()
    {
        yield return new WaitForSeconds(.2f);
        Destroy(this.gameObject);

    }

    private void OnTriggerStay(Collider other)
    {

    }

    private void OnCollisionEnter2D(Collision2D other)
    {

    }
}
