using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamFollow : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;

    public GameObject playerGameObject;

    private void Awake()
    {
        playerGameObject = GameObject.FindGameObjectWithTag("Player");
    }

    // Start is called before the first frame update
    void Start()
    {
        vcam.Follow = playerGameObject.transform;
        vcam.LookAt = playerGameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
