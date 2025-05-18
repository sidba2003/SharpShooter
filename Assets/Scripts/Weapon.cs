using UnityEngine;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;

public class Weapon : MonoBehaviour
{
    [SerializeField] String enemyTag;
    [SerializeField] int dealDamage;

    void OnShoot()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity))
        {
            Collider hitObject = hit.collider;
            if (hitObject.tag == enemyTag) hitObject.GetComponent<EnemyHealth>().TakeDamage(dealDamage);
        }
    }
}
