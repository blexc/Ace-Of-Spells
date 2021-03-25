using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// assumes EnemyDetectionRadius.cs is attached to a child object
public class EnemyFollow : MonoBehaviour
{
    private bool IsFrozen { get { return GetComponent<EnemyBase>().IsFrozen; } }

    //enemy movement speed
    public float moveSpeed = 1;
    //enemy detection radius
    public int detectionRadius = 40;
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
        GetComponentInChildren<EnemyDetectionRadius>().SetRadius(detectionRadius);
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

        // if can follow and not frozen 
        if (canFollow && !IsFrozen)
        {
            //move enemy
            MoveCharacter(movementDirection);

        }
    }

    public void MoveCharacter(Vector2 direction)
    {
        //move enemy to player
        enemyRB.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.fixedDeltaTime));
    }
}
