using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject snakeTarget;

    void Start()
    {
        snakeTarget = GameObject.Find("Snake Target");
    }

    // Update is called once per frame
    void Update()
    {
        if (snakeTarget!=null)
        transform.position = Vector3.MoveTowards(transform.position, snakeTarget.transform.position, 30f * Time.deltaTime);
    }
}
