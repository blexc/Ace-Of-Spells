using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffect : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform obj;
    private ParticleSystem ps;
    void Start()
    {
        ps = this.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (obj!=null)
        {
            transform.position = obj.position;
        }
        else
        {
            ps.Stop();
            Destroy(gameObject, 3f);
        }
  
    }

}
