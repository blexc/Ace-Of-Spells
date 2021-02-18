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

    // Start is called before the first frame update
    void Start()
    {
        //set detection radius
        GetComponent<CircleCollider2D>().radius = detectionRadius;
        //set player
        player = GameObject.FindGameObjectWithTag("Player");
        //get enemy rigidbody
        enemyRB = GetComponent<Rigidbody2D>();

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {

        //get player direction & rotate
        Vector3 direction = (player.transform.position - transform.position) * moveSpeed;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        enemyRB.rotation = angle;
        direction.Normalize();
        movementDirection = direction;

        if (canFollow)
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
}
