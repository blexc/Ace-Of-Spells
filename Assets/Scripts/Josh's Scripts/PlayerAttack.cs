using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    //camera reference
    public Camera mainCamera;
    private GameObject par;

    private void Start()
    {
        par = transform.parent.gameObject;
    }


    // Update is called once per frame
    void Update()
    {
        //get mouse position
        Vector3 mouse = Mouse.current.position.ReadValue();
        // get screen point
        Vector3 screenPoint = mainCamera.WorldToScreenPoint(transform.position);

        Vector2 offset = new Vector2(mouse.x - screenPoint.x, mouse.y - screenPoint.y);
        //get angle
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        //rotate to angle
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    public void CastSpell(GameObject spellPrefab)
    {
        // if spellPrefab is not null...
        if (spellPrefab && par.GetComponent<PlayerStats>().cd <= 0f)
        {
            par.GetComponent<PlayerStats>().cd = par.GetComponent<PlayerStats>().spellCooldown;
            //spawn spell at my rotation
            //print("PlayerAttack: spawning spell: " + spellPrefab.name);
            Instantiate(spellPrefab, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z), transform.rotation);
            if (transform.eulerAngles.z <= 45f || transform.eulerAngles.z > 315f)
            {
                //face right
                Debug.Log("face right");
                //Debug.Log(transform.rotation.z);
                par.GetComponent<PlayerMovement>().animator.SetTrigger("SideCast");
            }

            if (transform.eulerAngles.z > 45f && transform.eulerAngles.z <= 135f)
            {
                //face up
                Debug.Log("face up");
                par.GetComponent<PlayerMovement>().animator.SetTrigger("BackCast");
                //Debug.Log(transform.rotation.z);
            }

            if (transform.eulerAngles.z > 135f && transform.eulerAngles.z <= 225f)
            {
                //face left
                Debug.Log("face left");
                par.GetComponent<PlayerMovement>().animator.SetTrigger("SideAltCast");
                

                //Debug.Log(transform.rotation.z);
            }

            if (transform.eulerAngles.z > 225f && transform.eulerAngles.z <= 315f)
            {
                //face down
                Debug.Log("face down");
                par.GetComponent<PlayerMovement>().animator.SetTrigger("FrontCast");
                //Debug.Log(transform.rotation.z);
            }
            Debug.Log(transform.eulerAngles.z);
        }
    }
}
