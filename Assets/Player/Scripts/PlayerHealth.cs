using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : Health
{
    public HealthBar _healthBar;

    protected override void SetMaxHealth(int amount)
    {
        _healthBar.SetMaxHealth(amount);
    }

    protected override void SetCurrentHealth(int amount)
    {
        _healthBar.SetCurrentHealth(amount);
    }

    protected override void Die()
    {
        SceneManager.LoadScene("Game Over");
    }
}
