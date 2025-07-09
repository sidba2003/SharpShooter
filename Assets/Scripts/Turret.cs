using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] Transform playerCameraRoot;
    [SerializeField] Transform turretHead;
    [SerializeField] ParticleSystem explosionVFX;
    [SerializeField] float projectileCooldown;
    [SerializeField] Transform ProjectileSpawnPoint;
    [SerializeField] GameObject ProjectileObject;
    [SerializeField] int health;
    [SerializeField] GameObject Shooter;

    PlayerHealth pHealth;

    private void Start()
    {
        pHealth = PlayerHealth.instance;

        StartCoroutine(GenerateProjectiles());
    }

    private void Update()
    {
        if (playerCameraRoot) turretHead.LookAt(playerCameraRoot.position);
    }

    public void TakeDamage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Instantiate(explosionVFX, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    IEnumerator GenerateProjectiles()
    {
        while (pHealth.GetPlayerHealth() > 0)
        {
            yield return new WaitForSeconds(projectileCooldown);
            Instantiate(ProjectileObject, ProjectileSpawnPoint.position, Shooter.transform.rotation);
        }

        yield return null;
    }
}
