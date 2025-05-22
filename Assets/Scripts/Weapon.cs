using UnityEngine;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;

public class Weapon : MonoBehaviour
{
    [SerializeField] String enemyTag;
    [SerializeField] ParticleSystem ShootVFX;
    [SerializeField] Animator animator;
    [SerializeField] ParticleSystem HitVFX;
    [SerializeField] WeaponSO weaponSO;

    const String SHOOT_STRING = "Shoot";

    void OnShoot()
    {
        ShootVFX.Play();
        animator.Play(SHOOT_STRING, 0, 0f);

        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity))
        {
            Collider hitObject = hit.collider;
            if (hitObject.tag == enemyTag) hitObject.GetComponent<EnemyHealth>().TakeDamage(weaponSO.Damage);

            Instantiate(HitVFX, hit.point, Quaternion.identity);
        }
    }
}
