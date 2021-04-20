using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TossObject : MonoBehaviour
{
    public GameObject objectPrefab;
    public Animator anim;
    public SpriteRenderer sR;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(Throw());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Throw()
    {
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(1.2f);
        Instantiate(objectPrefab, transform.position, Quaternion.identity);
        Vector2 dir = (player.transform.position - transform.position).normalized;
        Debug.Log(dir);
        if (dir.x <0)
        {
            sR.flipX = false;
        }
        else if (dir.x >= 0)
        {
            sR.flipX = true;
           
        }
        StartCoroutine(Throw());
    }
}
