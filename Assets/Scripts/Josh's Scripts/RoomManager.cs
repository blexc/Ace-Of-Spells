using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    //public int currentEnemies;
    public int enemiesRemaining;

    public GameObject exit;

    public bool isPlayerInRoom;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.tag == "Enemy")
            {
                enemiesRemaining++;
                Debug.Log(enemiesRemaining);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(enemiesRemaining <= 0)
        {
            exit.SetActive(false);
        }
    }

    public IEnumerator CheckEnemies()
    {
        foreach(Transform child in transform)
        {
            if(child.tag == "Enemy")
            {
                enemiesRemaining+= enemiesRemaining;
                Debug.Log(enemiesRemaining);
            }
        }
        yield return new WaitForSeconds(.5f);
        StartCoroutine(CheckEnemies());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            isPlayerInRoom = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {

       // if (other.gameObject.tag == "Player")
        {
           // isPlayerInRoom = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPlayerInRoom = false;
        }
    }
}
