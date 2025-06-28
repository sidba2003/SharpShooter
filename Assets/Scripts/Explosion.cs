using UnityEngine;

public class RobotExplosion : MonoBehaviour
{
    [SerializeField] float radius = 1.5f;
    [SerializeField] float ExplosionDamage;

    PlayerHealth health;
    string playerTag = "Player";

    void Start()
    {
        health = PlayerHealth.instance;
        DamagePlayer();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, radius);
    }

    void DamagePlayer()
    {
        Collider[] collidingPhysics = Physics.OverlapSphere(this.transform.position, radius);

        for (int i = 0; i < collidingPhysics.Length; i++)
        {
            Collider body = collidingPhysics[i];

            if (body.CompareTag(playerTag))
            {
                health.TakeDamage(-ExplosionDamage);
            }
        }
    }
}
