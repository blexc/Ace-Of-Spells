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
    private SpriteRenderer sprite;

    private void Awake()
    {
        sprite = gameObject.GetComponent<PlayerMovement>().sprite;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        if (cd > 0)
        {
            cd -= Time.deltaTime * 1f;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            TakeDamage(other.gameObject.GetComponent<EnemyBase>().attackDmg);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        StartCoroutine(Flash());
    }

    public IEnumerator Flash()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(.125f);
        sprite.color = Color.white;

    }
}
