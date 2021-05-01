using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject FollowTarget;
    public GameObject snakeHead;
    public float delay;
    private float time;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (snakeHead== null)
        {
            Destroy(gameObject);
        }

        if (time < delay)
        {
            time += Time.deltaTime;
        }
        else
        {
            if (FollowTarget!=null)
            transform.position = Vector3.MoveTowards(transform.position, FollowTarget.transform.position, 25f * Time.deltaTime);
        }

       
    }

    public void TakeDamage(int damage)
    {
        snakeHead.GetComponent<EnemyBase>().TakeDamage(damage);
    }

}
