using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTransition : MonoBehaviour
{
    public GameObject room1Cam;

    public GameObject room2Cam;

    // Start is called before the first frame update
    void Start()
    {
        //room1Cam = GameObject.FindGameObjectWithTag("Room1");
        //room2Cam = GameObject.FindGameObjectWithTag("Room2");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Room1")
        {
            room1Cam.SetActive(true);
            room2Cam.SetActive(false);
        }

        if (other.tag == "Room2")
        {
            room1Cam.SetActive(false);
            room2Cam.SetActive(true);
        }
    }
}
