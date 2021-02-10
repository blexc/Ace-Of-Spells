using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float moveSpeed = 1;
    GameObject player;
    private Rigidbody2D enemyRB;
    private Vector2 movement;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        Vector3 direction = (player.transform.position - transform.position) * moveSpeed;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        enemyRB.rotation = angle;
        direction.Normalize();
        movement = direction;

        MoveCharacter(movement);
    }

    public void MoveCharacter(Vector2 direction)
    {
        enemyRB.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }
}
