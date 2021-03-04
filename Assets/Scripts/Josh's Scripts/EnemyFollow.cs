using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    //enemy movement speed
    public float moveSpeed = 1;
    //enemy detection radius
    public float detectionRadius = 1;
    //player reference
    GameObject player;
    //enemy rigidbody reference
    private Rigidbody2D enemyRB;
    //movement direcction
    private Vector2 movementDirection;
    //if enemy can follow player
    public bool canFollow = false;

    // if greater than 0, then you cannot move
    public float frozenTimer;

    // Start is called before the first frame update
    void Start()
    {
        //set detection radius
        GetComponent<CircleCollider2D>().radius = detectionRadius;
        //set player
        player = GameObject.FindGameObjectWithTag("Player");
        //get enemy rigidbody
        enemyRB = GetComponent<Rigidbody2D>();

        frozenTimer = -1f;
    }

    // Update is called once per frame
    void Update()
    {
        // if the freeze timer isn't inactive
        if (!Mathf.Approximately(-1f, frozenTimer))
        {
            // decrement timer
            frozenTimer = Mathf.Max(0f, frozenTimer - Time.deltaTime);

            // if the timer hits 0, unfreeze the enemy and
            // set the timer to be inactive
            if (Mathf.Approximately(frozenTimer, 0f))
            {
                frozenTimer = -1f;
            }
        }
    }

    private void FixedUpdate()
    {

        //get player direction & rotate
        Vector3 direction = (player.transform.position - transform.position) * moveSpeed;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        enemyRB.rotation = angle;
        direction.Normalize();
        movementDirection = direction;

        // if can follow and not frozen 
        if (canFollow && frozenTimer <= 0)
        {
            //move enemy
            MoveCharacter(movementDirection);

        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            canFollow = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            canFollow = false;
        }
    }

    public void MoveCharacter(Vector2 direction)
    {
        //move enemy to player
        enemyRB.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.fixedDeltaTime));
    }

    // begins to freeze the enemy for a specified amount of time
    public void FreezeCharacter(float amountSec = 1)
    {
        frozenTimer = amountSec;
    }
}
