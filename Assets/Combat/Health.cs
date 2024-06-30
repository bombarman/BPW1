using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    private int _currentHealth;

    private void Start()
    {
        _currentHealth = _maxHealth;
        SetMaxHealth(_maxHealth);
    }

    public void TakeDamage(int amount)
    {
        _currentHealth -= amount;

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            Die();
        }
        SetCurrentHealth(_currentHealth);
    }

    public void Heal(int amount)
    {
        _currentHealth += amount;
        if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }

        SetCurrentHealth(_currentHealth);
    }

    protected virtual void SetMaxHealth(int amount)
    {
        return;
    }

    protected virtual void SetCurrentHealth(int amount)
    {
        return;
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
