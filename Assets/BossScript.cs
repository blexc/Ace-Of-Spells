using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public bool canAttack;
    public float attackCooldown;

    private float rotation;

    public GameObject fanAttackPrefab;

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
        if(canAttack)
        {
            canAttack = false;
            print("FAN ATTACK");
            Instantiate(fanAttackPrefab, transform.position, Quaternion.identity);

           
        }
        
        
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
        StartCoroutine(Swing());

    }

    public IEnumerator Swing()
    {
        if (canAttack)
        {
            canAttack = false;
            print("SWING ATTACK");

        }

        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
        StartCoroutine(Slap());

    }

    public IEnumerator Slap()
    {
        if (canAttack)
        {
            canAttack = false;
            print("SLAP ATTACK");

        }

        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
        StartCoroutine(FanAttack());

    }


}
