using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header ("Health")]
    public float maxHealth;
    public float currentHealth;

    [Header("Speed")]
    public float moveSpeed;

    [Header("Time")]
    public float spellCooldown;
    public float timeControl;
    public float cd;

    public bool discardCard;
    public SpriteRenderer sprite;
    [SerializeField] private GameObject ScriptManager = null;
    PlayerMovement playerMovement;
    [SerializeField] float invincibleTimer, invincibleTimerStart = 3f; // time the player in invunerable

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cd > 0)
        {
            cd -= Time.deltaTime * 1f;
        }

        // decrement invincible timer
        invincibleTimer = Mathf.Clamp(invincibleTimer - Time.deltaTime,
            -1f, invincibleTimerStart);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            TakeDamage(other.gameObject.GetComponent<EnemyBase>().attackDmg);
        }

        if (other.gameObject.tag == "Projectile")
        {
            TakeDamage(3);
        }
    }

    public void TakeDamage(int damage)
    {
        // don't take damage, if the player is invunerable
        if (invincibleTimer > 0f) return;

        currentHealth -= damage;

        // ensure no null references
        if (ScriptManager)
            ScriptManager.GetComponent<GameplayUI>().HealthUpdate(currentHealth);
        else
            Debug.LogWarning("null reference in PlayerStats.cs");
        StartCoroutine(Flash(Color.red));
    }

    public void Heal(int amount)
    {
        currentHealth += (float)amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // ensure no null references
        if (ScriptManager)
            ScriptManager.GetComponent<GameplayUI>().HealthUpdate(currentHealth);
        else
            Debug.LogWarning("null reference in PlayerStats.cs");
        StartCoroutine(Flash(Color.green));
    }

    public IEnumerator Flash(Color color)
    {
        int count = 2;
        while (count-- > 0)
        {
            sprite.color = color;
            yield return new WaitForSeconds(.125f);
            sprite.color = Color.white;
            yield return new WaitForSeconds(.125f);
        }
    }

    public IEnumerator SlowPlayer(float slowTime, float slowAmount)
    {
        playerMovement.CurMoveSpeed = playerMovement.MoveSpeed * slowAmount;
        yield return new WaitForSeconds(slowTime);
        playerMovement.CurMoveSpeed = playerMovement.MoveSpeed; 
    }

    /// <summary>
    /// make the player invincible for a couple seconds (see invinsibleTimerStart)
    /// </summary>
    public void MakePlayerInvunerable()
    {
        invincibleTimer = invincibleTimerStart;
    }
}
