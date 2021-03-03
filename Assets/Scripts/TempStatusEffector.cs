using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempStatusEffector : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            int r = Random.Range(0, 5);
            int duration = 3; // seconds
            FindObjectOfType<EnemyBase>().AddStatusEffect(r + StatusEffect.Freeze, duration);
        }
    }
}
