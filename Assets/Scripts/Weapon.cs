using UnityEngine;
using System;
using Cinemachine;

public class Weapon : MonoBehaviour
{
    [SerializeField] ParticleSystem ShootVFX;
    [SerializeField] WeaponSO weaponSO;
    [SerializeField] LayerMask IgnoreMeWhileShooting;

    ActiveWeapon instance;

    void Start()
    {
        instance = ActiveWeapon.instance;
        CinemachineImpulseSource impulseSource = GetComponent<CinemachineImpulseSource>();

        ActiveWeapon.FireTimeIncrementor = weaponSO.FireRate;
        ActiveWeapon.IsAutomatic = weaponSO.IsAutomatic;
        ActiveWeapon.CanZoom = weaponSO.CanZoom;
        ActiveWeapon.fullAmmoCapacity = weaponSO.ammoAmount;

        ActiveWeapon.impulseSource = impulseSource;

        ActiveWeapon.AmmoCount = weaponSO.ammoAmount;
        instance.UpdateAmmoUI();
    }

    public void Shoot(String enemyTag)
    {
        ShootVFX.Play();

        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, ~IgnoreMeWhileShooting))
        {
            Collider hitObject = hit.collider;
            if (hitObject.tag == enemyTag) hitObject.GetComponent<EnemyHealth>().TakeDamage(weaponSO.Damage);

            Instantiate(weaponSO.HitVFX, hit.point, Quaternion.identity);
        }
    }
}
