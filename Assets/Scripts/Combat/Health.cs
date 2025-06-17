using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;

    private int health;

    private void Start()
    {
        health = maxHealth;
    }

    public void DealDamage(int damageAmount)
    {
        if (health <= 0) { return; }
        health = (int)MathF.Max(health - damageAmount, 0);
        Debug.Log("Health: " + health);
    }
}
