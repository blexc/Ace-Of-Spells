using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    //spell rigidbody reference
    private Rigidbody2D spellRigidbody;

    //spell speed
    [SerializeField]
    private float spellSpeed;
   
    private void Awake()
    {
        //get rigidbody
        spellRigidbody = GetComponent<Rigidbody2D>();
    }


    // Start is called before the first frame update
    void Start()
    {
        //move spell 
        spellRigidbody.velocity = transform.right * spellSpeed;

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //if hits an enemy
        if (other.gameObject.tag == "Enemy")
        {
            //destroy enemy
            Destroy(other.gameObject);
            Destroy(this.gameObject);

            GameObject roomManager = other.gameObject.transform.parent.gameObject;
            RoomManager roomScript = roomManager.GetComponent<RoomManager>();
            roomScript.enemiesRemaining--;

        }
    }

}
