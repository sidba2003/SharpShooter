using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int TotalHealth;
   
    public void TakeDamage(int amount)
    {
        TotalHealth -= amount;
        CheckHealth();
    }

    void CheckHealth()
    {
        if (TotalHealth <= 0) Destroy(gameObject);
    }
}
