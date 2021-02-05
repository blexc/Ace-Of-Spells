using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public int minX;
    public int maxX;
    public int minY;
    public int maxY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x >= maxX)
        {
            transform.position = new Vector2(maxX, this.transform.position.y);
        }

        if (transform.position.x <= minX)
        {
            transform.position = new Vector2(minX, this.transform.position.y);
        }

        if (transform.position.x >= maxY)
        {
            transform.position = new Vector2(this.transform.position.x, maxY );
        }

        if (transform.position.x <= minY)
        {
            transform.position = new Vector2(this.transform.position.x, minY);
        }
    }
}
