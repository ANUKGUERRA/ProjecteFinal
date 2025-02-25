using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    [SerializeField] private SpellSystem spellSystem;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log($"Enemy took {damage} damage. Current health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Enemy died.");
        Destroy(gameObject); // Destroy the enemy when health reaches 0
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FireBall"))
        {
            float damage = spellSystem.fireballDamage;
            TakeDamage(damage);
            other.gameObject.SetActive(false);
            Debug.Log($"Enemy hit by fireball. Damage taken: {damage}");
        }
    }
}
