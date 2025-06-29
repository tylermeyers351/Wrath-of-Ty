using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;

    private int health;

    public event Action OnTakeDamage;

    public event Action OnDeath;

    private void Start()
    {
        health = maxHealth;
    }

    public void DealDamage(int damageAmount)
    {
        if (health <= 0) { return; }
        health = (int)MathF.Max(health - damageAmount, 0);
        OnTakeDamage?.Invoke();
        // Debug.Log("Health: " + health);

        if (health == 0)
        {
            OnDeath?.Invoke();
            Debug.Log("Character died...");
        }
    }
}
