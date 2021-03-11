using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    // Start is called before the first frame update
    public float time;
    void Start()
    {
        Destroy(this, time);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
}
