using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    //spell rigidbody reference
    private Rigidbody2D spellRigidbody;

    public float spellDamage;

    public bool fire;

    public bool frost;

    public bool nature;

    public bool lightning;

    public bool shadow;

    public Collider2D explosionCollider;

    public Collider2D eruptionCollider;

    public float explosionDamage;

    public LayerMask layerMask;

    public bool hit;

    private List<Transform> targets = new List<Transform>();
    //spell speed
    [SerializeField]
    private float spellSpeed;

    public GameObject enemyHit;

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
            enemyHit = other.gameObject;
            Destroy(other.gameObject);
            Destroy(this.gameObject);
            //GameObject roomManager = other.gameObject.transform.parent.gameObject;
            //RoomManager roomScript = roomManager.GetComponent<RoomManager>();
            //roomScript.enemiesRemaining--;

            
        }

        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       
    }

    public void Damage()
    {
        //damage enemy
        //enemyHit.GetComponent<EnemyStats>().enemyHealth -= spellDamage;
    }

    public void Chain()
    {
        

        
    }

    public void ApplyStatusEffect()
    {
        //status effect
    }

    public void Explosion()
    {
        explosionCollider.enabled = true;
    }

    public void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    public void Erupt()
    {
        eruptionCollider.enabled = true;

    }
}
