using Cinemachine;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int TotalHealth;
    [SerializeField] ParticleSystem explosionVFX;

    public static EnemyHealth instance;
    CinemachineImpulseSource impulseSource;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public void TakeDamage(int amount)
    {
        TotalHealth -= amount;
        CheckHealth();
    }

    void CheckHealth()
    {
        if (TotalHealth <= 0)
        {
            SelfDestruct();
        }
    }

    public void SelfDestruct()
    {
        impulseSource.GenerateImpulse();

        Instantiate(explosionVFX, this.transform.position, explosionVFX.transform.rotation);
        Destroy(gameObject);
    }
}
