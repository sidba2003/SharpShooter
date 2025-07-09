using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float projectileSpeed;
    [SerializeField] float projectileDamage;
    [SerializeField] ParticleSystem hitVFX;

    PlayerHealth health;
    Rigidbody rigidBody;

    private void Start()
    {
        health = PlayerHealth.instance;
        rigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rigidBody.MovePosition(rigidBody.position + transform.forward * projectileSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            health.TakeDamage(projectileDamage);
        }

        Instantiate(hitVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
