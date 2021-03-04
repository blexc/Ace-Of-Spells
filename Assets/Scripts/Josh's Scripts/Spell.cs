using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    //spell rigidbody reference
    private Rigidbody2D spellRigidbody;

    [SerializeField] float spellLifetime = 10f; // in seconds

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

    // these object will be spawned upon an enemy if the spell causes ignite or bramble
    public GameObject burnerPrefab;
    public GameObject bramblePrefab;

    public GameObject chainLightningPrefab;

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

    private void Update()
    {
        // delete projectile if spell lifetime is 0
        spellLifetime -= Time.deltaTime;
        if (spellLifetime < 0) Destroy(gameObject);
    }



    private void OnCollisionEnter2D(Collision2D other)
    {
        //if hits an enemy
        if (other.gameObject.tag == "Enemy")
        {
            enemyHit = other.gameObject;
            var eb = enemyHit.GetComponent<EnemyBase>();
            eb.TakeDamage((int)spellDamage);


            float lifeTime = 3f;
            if (lightning)
            {
                Chain();
                eb.AddStatusEffect(StatusEffect.Shock, 3);
            }

            if (fire)
            {
                eb.AddStatusEffect(StatusEffect.Ignite, (int)lifeTime);
                var burnerInstance = Instantiate(burnerPrefab, eb.transform);
                burnerInstance.GetComponent<DamageOverTime>().Init(0.5f, lifeTime, 1);
            }

            if (frost)
            {
                eb.AddStatusEffect(StatusEffect.Freeze, (int)lifeTime);
                eb.FreezeCharacter(lifeTime);
            }

            if (nature)
            {
                eb.AddStatusEffect(StatusEffect.Bramble, (int)lifeTime);
                eb.FreezeCharacter(lifeTime);
                var brambleInstance = Instantiate(bramblePrefab, eb.transform);
                brambleInstance.GetComponent<DamageOverTime>().Init(0.5f, lifeTime, 1);
            }

            if (shadow)
            {
                // shadow effects last longer
                lifeTime = 6;
                eb.AddStatusEffect(StatusEffect.Rot, (int)lifeTime);
            }

            //GameObject roomManager = other.gameObject.transform.parent.gameObject;
            //RoomManager roomScript = roomManager.GetComponent<RoomManager>();
            //roomScript.enemiesRemaining--;

            
        }

        // delete spell if hits anything
        Destroy(this.gameObject);
        

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

        Instantiate(chainLightningPrefab, enemyHit.transform.position, Quaternion.identity);
        Destroy(this.gameObject);

    }

    public void Explosion()
    {
        //screen size collider
        explosionCollider.enabled = true;
    }

    public void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    public void Erupt()
    {
        //room size collider
        eruptionCollider.enabled = true;

    }

    public void ApplyStatusEffect()
    {
        //status effect
    }
}



