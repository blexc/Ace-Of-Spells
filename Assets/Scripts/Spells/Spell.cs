using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    //spell rigidbody reference
    private Rigidbody2D spellRigidbody;

    [SerializeField] float spellLifetime = 10f; // in seconds
    [SerializeField] int effectlifeTime = 3; // duration of effect in seconds

    public float spellDamage = 1f;
    [SerializeField] private float spellSpeed = 20f;

    public bool applyIgniteEffect;
    public bool applyFreezeEffect;
    public bool applyShockEffect;
    public bool applyRotEffect;

    public bool applyChainLightning;
    public int numIceCounters; 

    public Collider2D explosionCollider;

    public Collider2D eruptionCollider;

    public float explosionDamage;

    public LayerMask layerMask;

    public bool hit;

    private List<Transform> targets = new List<Transform>();

    public GameObject enemyHit;

    // these object will be spawned upon an enemy if the spell causes ignite or bramble
    public GameObject burnerPrefab;
    public GameObject chainLightningPrefab;

    private void Awake()
    {
        //get rigidbody
        spellRigidbody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    private void Start()
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

    // should NOT be on collision enter, as it will propel
    // the enemies backwards upon contact
    // we should only have TRIGGER colliders
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        //if hits an enemy
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyHit = other.gameObject;
            var eb = enemyHit.GetComponent<EnemyBase>();
            eb.TakeDamage((int)spellDamage);

            if (applyChainLightning) Chain();
            if (numIceCounters > 0) eb.AddIceCounters(numIceCounters);

            #region apply spell effects
            if (applyShockEffect)
            {
                eb.AddStatusEffect(StatusEffect.Shock, 3);
            }
            else if (applyIgniteEffect)
            {
                eb.AddStatusEffect(StatusEffect.Ignite, effectlifeTime);
                var burnerInstance = Instantiate(burnerPrefab, eb.transform);
                burnerInstance.GetComponent<DamageOverTime>().Init(0.5f, effectlifeTime, 1);
            }
            else if (applyFreezeEffect)
            {
                eb.AddStatusEffect(StatusEffect.Freeze, effectlifeTime);
                eb.FreezeCharacter(effectlifeTime);
            }
            else if (applyRotEffect)
            {
                // shadow effects last longer
                effectlifeTime = 6;
                eb.AddStatusEffect(StatusEffect.Rot, effectlifeTime);
            }
            #endregion
        }

        // delete spell if hits ANYTHING
        // should not be left, since it could hit multiple enemies
        Destroy(gameObject);
    }

    public void Chain()
    {
        if (chainLightningPrefab)
        {
            Instantiate(chainLightningPrefab, enemyHit.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
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
}



