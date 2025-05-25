using UnityEngine;
using System;
using System.Collections;

public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] String enemyTag;
    [SerializeField] Animator animator;

    const String SHOOT_STRING = "Shoot";
    public float FireAvailableTime = 0f;
    Weapon currentWeapon;
    public static float FireTimeIncrementor;
    public static bool IsAutomatic;

    private void Start()
    {
        currentWeapon = GetComponentInChildren<Weapon>();
    }

    void OnShoot()
    {
        if (IsAutomatic)
        {
            StartCoroutine(FireMachineGun());
        }
        else
        {
            if (Time.time < FireAvailableTime) return;

            animator.Play(SHOOT_STRING, 0, 0f);
            currentWeapon.Shoot(enemyTag);

            FireAvailableTime += FireTimeIncrementor;
        }
    }

    private IEnumerator FireMachineGun()
    {
        while (Input.GetKey(KeyCode.Mouse0))
        {
            animator.Play(SHOOT_STRING, 0, 0f);
            currentWeapon.Shoot(enemyTag);

            yield return new WaitForSeconds(FireTimeIncrementor);
        }

        yield return null;
    }
}
