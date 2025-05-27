using UnityEngine;
using System;
using System.Collections;
using UnityEditor.Animations;

public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] String enemyTag;
    [SerializeField] Animator animator;

    const String SHOOT_STRING = "Shoot";
    public float FireAvailableTime = 0f;
    Weapon currentWeapon;
    public static float FireTimeIncrementor;
    public static bool IsAutomatic;
    public static ActiveWeapon instance;
    static float newAutomaticFireTime = 0f;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        currentWeapon = GetComponentInChildren<Weapon>();
    }

    void OnShoot()
    {
        if (IsAutomatic & Time.time >= newAutomaticFireTime)
        {
            newAutomaticFireTime = Time.time + FireTimeIncrementor;
            StartCoroutine(FireMachineGun());
        }
        else
        {
            if (Time.time < FireAvailableTime) return;

            animator.Play(SHOOT_STRING, 0, 0f);
            currentWeapon.Shoot(enemyTag);

            FireAvailableTime = Time.time + FireTimeIncrementor;
        }
    }

    public GameObject GetActiveWeaponDirectory()
    {
        // Gets the Camera Root
        GameObject cameraRoot = GetComponent<Transform>().GetChild(0).gameObject;

        // Returns the Active Weapon Directory Game Object
        return cameraRoot.GetComponent<Transform>().GetChild(0).gameObject;
    }

    public void SetNewWeaponReference(Weapon newWeaponReference)
    {
        currentWeapon = newWeaponReference;
        StopAllCoroutines();
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
