using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public bool canAttack;
    public float attackCooldown;

    private float rotation;

    public GameObject fanAttackPrefab;

    public GameObject table1;
    public GameObject table2;
    public GameObject table3;

    public int loopNumber;

    // Start is called before the first frame update
    void Start()
    {
        canAttack = true;
        StartCoroutine(FanAttack());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
    public IEnumerator FanAttack()
    {
        if(canAttack || loopNumber >= 3)
        {
            canAttack = false;
            //print("FAN ATTACK");
            Instantiate(fanAttackPrefab, transform.position, Quaternion.identity);

           
        }
        
        
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
        StartCoroutine(Swing());

    }

    public IEnumerator Swing()
    {
        if (canAttack || loopNumber >= 3)
        {
            canAttack = false;
            //print("SWING ATTACK");

            table1.GetComponent<SpriteRenderer>().color = Color.black;
            table2.GetComponent<SpriteRenderer>().color = Color.black;
            table3.GetComponent<SpriteRenderer>().color = Color.black;

            yield return new WaitForSeconds(1);

            table1.GetComponent<CircleCollider2D>().enabled = true;
            table2.GetComponent<CircleCollider2D>().enabled = true;
            table3.GetComponent<CircleCollider2D>().enabled = true;

            table1.GetComponent<SpriteRenderer>().color = Color.red;
            table2.GetComponent<SpriteRenderer>().color = Color.red;
            table3.GetComponent<SpriteRenderer>().color = Color.red;

            table1.GetComponent<ParticleSystem>().Play();
            table2.GetComponent<ParticleSystem>().Play();
            table3.GetComponent<ParticleSystem>().Play();

            yield return new WaitForSeconds(1);

            table1.GetComponent<SpriteRenderer>().color = Color.white;
            table2.GetComponent<SpriteRenderer>().color = Color.white;
            table3.GetComponent<SpriteRenderer>().color = Color.white;

            table1.GetComponent<CircleCollider2D>().enabled = false;
            table2.GetComponent<CircleCollider2D>().enabled = false;
            table3.GetComponent<CircleCollider2D>().enabled = false;
        }

        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
        StartCoroutine(Slap());

    }

    public IEnumerator Slap()
    {
        if (canAttack || loopNumber >= 3)
        {
            canAttack = false;
            //print("SLAP ATTACK");

            int randomInt = Random.Range(1, 4);
            if(randomInt == 1)
            {
                table1.GetComponent<SpriteRenderer>().color = Color.black;

              
                yield return new WaitForSeconds(1);


                table1.GetComponent<CircleCollider2D>().enabled = true;
          
                table1.GetComponent<SpriteRenderer>().color = Color.red;
              
                table1.GetComponent<ParticleSystem>().Play();

    
                yield return new WaitForSeconds(1);


                table1.GetComponent<SpriteRenderer>().color = Color.white;
          

                table1.GetComponent<CircleCollider2D>().enabled = false;
             
            }

            if(randomInt == 2)
            {
                table2.GetComponent<SpriteRenderer>().color = Color.black;


                yield return new WaitForSeconds(1);


                table2.GetComponent<CircleCollider2D>().enabled = true;

                table2.GetComponent<SpriteRenderer>().color = Color.red;

                table2.GetComponent<ParticleSystem>().Play();


                yield return new WaitForSeconds(1);


                table2.GetComponent<SpriteRenderer>().color = Color.white;


                table2.GetComponent<CircleCollider2D>().enabled = false;

            }

            if(randomInt == 3)
            {
                table3.GetComponent<SpriteRenderer>().color = Color.black;


                yield return new WaitForSeconds(1);


                table3.GetComponent<CircleCollider2D>().enabled = true;

                table3.GetComponent<SpriteRenderer>().color = Color.red;

                table3.GetComponent<ParticleSystem>().Play();


                yield return new WaitForSeconds(1);


                table3.GetComponent<SpriteRenderer>().color = Color.white;


                table3.GetComponent<CircleCollider2D>().enabled = false;

            }

        }

        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
        loopNumber++;
        StartCoroutine(FanAttack());
        
        if(loopNumber >= 3)
        {
            StartCoroutine(Enrage());

        }

    }

    public IEnumerator Enrage()
    {
        if (loopNumber >= 3)
        {
            canAttack = false;
            //print("Enrage");

            StartCoroutine(FanAttack());
          
        }

        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
        StartCoroutine(FanAttack());

    }


}
