using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeCollider : MonoBehaviour
{
    public Transform snake;
    // Start is called before the first frame update
 

    // Update is called once per frame
    void Update()
    {
        transform.position = snake.transform.position;
    }
}
