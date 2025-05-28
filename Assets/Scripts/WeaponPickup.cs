using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] GameObject MachineGunPrefab;
    [SerializeField] GameObject PistolPrefab;
    [SerializeField] GameObject SniperRiflePrefab;

    ActiveWeapon activeWeaponInstance;

    private void Start()
    {
        activeWeaponInstance = ActiveWeapon.instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);

        if (this.name == "Machine Gun Pickup")
        {
            ChangeWeapon(MachineGunPrefab);
        } else if (this.name == "Pistol Pickup")
        {
            ChangeWeapon(PistolPrefab);
        } else if (this.name == "Sniper Rifle Pickup")
        {
            ChangeWeapon(SniperRiflePrefab);
        }
    }

    private void ChangeWeapon(GameObject NewWeapon)
    {
        GameObject ActiveWeaponDirectory = activeWeaponInstance.GetActiveWeaponDirectory();
        Destroy(ActiveWeaponDirectory.GetComponent<Transform>().GetChild(0).gameObject);

        GameObject InstantiatedNewWeapon = Instantiate(NewWeapon, ActiveWeaponDirectory.transform);
        activeWeaponInstance.SetNewWeaponReference(InstantiatedNewWeapon.GetComponent<Weapon>());
    }
}
