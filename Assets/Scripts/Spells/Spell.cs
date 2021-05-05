using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // needed in PlaceAtMousePos()

/// <summary>
/// Base class for all (most) spells
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class Spell : MonoBehaviour
{
    //spell rigidbody reference
    private Rigidbody2D spellRigidbody;

    [SerializeField] protected float spellLifetime = 10f; // in seconds
    [SerializeField] protected int effectlifeTime = 3; // duration of effect in seconds
    [SerializeField] protected CardType cardType = CardType.NA;

    public float spellDamage = 1f;
    [SerializeField] protected float spellSpeed = 20f;

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

    public float AoETime;

    protected float spellLifetimeStart;
    protected SpriteRenderer sr;

    // these object will be spawned upon an enemy if the spell causes ignite or bramble
    public GameObject burnerPrefab;
    public GameObject chainLightningPrefab;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        //get rigidbody
        spellRigidbody = GetComponent<Rigidbody2D>();
        spellLifetimeStart = spellLifetime;

        //move spell 
        spellRigidbody.velocity = transform.right * spellSpeed;
    }

    protected virtual void Start()
    {
        // overridden by some spells
    }

    protected virtual void Update()
    {
        // delete projectile if spell lifetime is 0
        spellLifetime -= Time.deltaTime;
        if (spellLifetime < 0) Destroy(gameObject);
    }

    /// <summary>
    /// some setup for the spell, as each spell may want to setup differently
    /// sets the initial position of the spell to its desired place
    /// (player position by default)
    /// </summary>
    public virtual void InitSpell()
    {
        var pa = FindObjectOfType<PlayerAttack>();
        if (pa)
        {
            transform.position = pa.transform.position;
        }
    }

    /// <summary>
    /// should NOT be on collision enter, as it will propel
    /// the enemies backwards upon contact
    /// we should only have TRIGGER colliders
    /// </summary>
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        // don't do anything with spell
        if (other.gameObject.GetComponent<Spell>()) return;

        //if hits an enemy
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyHit = other.gameObject;
            var eb = enemyHit.GetComponent<EnemyBase>();
            DealDamageTo(eb);

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

        if(this.gameObject.tag == "AoE")
        {
            StartCoroutine(AoEWait());
        }
        else if (this.gameObject.tag == "AoESpawner")
        {
            GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(AoEWait());
        }
        else if (other.tag != "DetectionCircle" && other.tag != "Projectile")
        {
            // delete spell if hits ANYTHING
            // should not be left, since it could hit multiple enemies
            Destroy(gameObject);
        }
        
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

    public IEnumerator AoEWait()
    {
       
        yield return new WaitForSeconds(AoETime);
        Destroy(this.gameObject);
    }

    /// <summary>
    /// places object attached to whereever the mouse position is
    /// returns position
    /// </summary>
    protected Vector3 PlaceAtMousePos()
    {
        //get mouse position
        Vector3 mouse = Mouse.current.position.ReadValue();
        mouse.z = Mathf.Abs(Camera.main.transform.position.z);

        // get screen point
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(mouse);
        worldPoint.z = 0f;

        transform.position = worldPoint;
        return worldPoint;
    }

    protected Transform GetClosestEnemy(float minDist)
    {
        EnemyBase[] enemies = FindObjectsOfType<EnemyBase>();

        Transform tMin = null;
        Vector3 currentPos = transform.position;
        foreach (EnemyBase t in enemies)
        {
            float dist = Vector3.Distance(t.transform.position, currentPos);
            if (dist < minDist && dist > 5f)
            {
                tMin = t.transform;
                minDist = dist;
            }
        }
        return tMin;
    }

    // add this to update method to fade out as time of spell runs out
    protected void FadeOutAlpha()
    {
        Color c = sr.color;
        c.a = Mathf.Lerp(spellLifetime, spellLifetimeStart, 0.1f);
        c.a = Mathf.Clamp(c.a, 0, 0.8f);
        sr.color = c;
    }

    // add this to update method to oscillate
    protected void IncreaseAlpha(float amount)
    {
        Color c = sr.color;
        c.a += amount;
        sr.color = c;
    }

    // add this to update method to oscillate
    protected void OscAlpha(float period)
    {
        Color c = sr.color;
        c.a = 0.25f * Mathf.Sin(Mathf.PI * 2 * Time.time / period);
        sr.color = c;
    }

    // used to sort list of enemies by closeness to this spell
    // useage:
    // List<EnemyBase> enemies = new List<EnemyBase>();
    // ...add enemies to list...
    // enemies.Sort(SortByDistanceToMe);
    protected int SortByDistanceToMe(EnemyBase a, EnemyBase b)
    {
        float squaredRangeA = (a.transform.position - transform.position).sqrMagnitude;
        float squaredRangeB = (b.transform.position - transform.position).sqrMagnitude;
        return squaredRangeA.CompareTo(squaredRangeB);
    }

    /// <summary>
    /// deal damage to an enemy, dealing more with more cards of a specific element is in hand
    /// all spells should call this when they want to deal damage
    /// </summary>
    protected void DealDamageTo(EnemyBase eb)
    {
        if (eb)
        {
            int finalDamage;
            int numCardsOfElement = Deck.instance.NumOfTypeInHand(cardType) + 1; 
            float multiplier;

            if (numCardsOfElement > 2) multiplier = 2.0f;
            else if (numCardsOfElement == 2) multiplier = 1.5f;
            else multiplier = 1.0f;

            finalDamage = Mathf.FloorToInt(spellDamage * multiplier);
            //print("Dealing " + spellDamage + " * " + multiplier + " damage." + " Type: " + cardType);
            eb.TakeDamage(finalDamage);
        }
    }
}



