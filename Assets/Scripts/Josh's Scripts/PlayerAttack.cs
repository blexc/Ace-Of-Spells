using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    //spell prefab array
    [SerializeField]
    private GameObject[] spellPrefab;

    //camera reference
    public Camera mainCamera;

   
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

    public void CastSpell(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            //spawn spell at my rotation
            Instantiate(spellPrefab[0], new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z), transform.rotation);
        }
    }
}
