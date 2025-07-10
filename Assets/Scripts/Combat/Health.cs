using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] AudioSource damageAudioSource;
    [SerializeField] AudioSource hurtAudioSource;
    [SerializeField] AudioSource deathAudioSource;

    private int health;
    private bool canDamage;

    public event Action OnTakeDamage;

    public event Action OnDeath;

    public bool IsDead => health == 0;

    private void Start()
    {
        health = maxHealth;
    }

    public void SetDamageable(bool canDamage)
    {
        this.canDamage = canDamage;
    }

    public void DealDamage(int damageAmount)
    {
        if (health <= 0) { return; }
        if(canDamage) { return; }
        health = (int)MathF.Max(health - damageAmount, 0);
        OnTakeDamage?.Invoke();

        damageAudioSource.Play();
        hurtAudioSource.Play();
        Debug.Log($"{gameObject.name} Health: " + health);

        if (health == 0)
        {
            OnDeath?.Invoke();
            deathAudioSource.Play();
            Debug.Log($"{gameObject.name} died...");
        }
    }
}
