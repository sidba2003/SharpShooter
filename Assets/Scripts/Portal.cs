using System.Collections;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] int EnemySpawnCooldown;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject enemyParent;
    [SerializeField] ParticleSystem explosionVFX;

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Instantiate(explosionVFX, this.transform.position, explosionVFX.transform.rotation);
            Destroy(gameObject);
        }

    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            GameObject spawnedEnemy = Instantiate(enemy, transform.position, transform.rotation);
            spawnedEnemy.transform.SetParent(enemyParent.transform);

            yield return new WaitForSeconds(EnemySpawnCooldown);
        }
    }

}
