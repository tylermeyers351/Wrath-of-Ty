using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] AudioSource damageAudioSource;
    [SerializeField] AudioSource hurtAudioSource;
    [SerializeField] AudioSource deathAudioSource;
    [SerializeField] AudioSource blockAudioSource;

    float vignetteFlashIncrease = 0.08f;

    private int health;
    private bool canDamage;

    public event Action OnTakeDamage;

    public event Action OnDeath;

    public bool IsDead => health == 0;

    [SerializeField] private Volume volume;
    private Vignette vignette;

    private void Start()
    {
        health = maxHealth;
        if (volume != null && volume.profile != null)
        {
            if (!volume.profile.TryGet(out vignette))
            {
                Debug.LogError("Vignette not found in the Volume profile.");
                return;
            }
        }
        // Debug.Log("Intensity is set to: " + vignette.intensity);
    }

    public void SetDamageable(bool canDamage)
    {
        this.canDamage = canDamage;
    }

    public void DealDamage(int damageAmount)
    {
        if (health <= 0) { return; }
        if (canDamage)
        {
            blockAudioSource.Play();
            return;
        }
        health = (int)MathF.Max(health - damageAmount, 0);
        OnTakeDamage?.Invoke();

        damageAudioSource.Play();
        hurtAudioSource.Play();
        // Debug.Log($"{gameObject.name} Health: " + health);

        if (CompareTag("Player"))
        {
            Debug.Log("Parent has the Player tag!");
            IncreaseVignetteIntensity(vignetteFlashIncrease, 1f);  // 1 second duration fade-in
        }

        if (health == 0)
        {
            hurtAudioSource.Stop();
            OnDeath?.Invoke();
            deathAudioSource.Play();
            vignette.intensity.value = Mathf.Clamp01(vignette.intensity.value + 0.4f);
            // Debug.Log($"{gameObject.name} died...");
        }
    }

    private Coroutine vignetteCoroutine;

    public void IncreaseVignetteIntensity(float amount, float duration)
    {
        if (vignetteCoroutine != null)
            StopCoroutine(vignetteCoroutine);

        vignetteCoroutine = StartCoroutine(IncreaseVignetteCoroutine(amount, duration));
    }

    private IEnumerator IncreaseVignetteCoroutine(float amount, float duration)
    {
        float startIntensity = vignette.intensity.value;
        float targetIntensity = Mathf.Clamp01(startIntensity + amount);
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            vignette.intensity.value = Mathf.Lerp(startIntensity, targetIntensity, elapsed / duration);
            yield return null;
        }

        vignette.intensity.value = targetIntensity;
    }

}
