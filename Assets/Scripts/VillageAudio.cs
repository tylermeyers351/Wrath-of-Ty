using UnityEngine;

public class VillageAudio : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    private bool hasHappened = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent<Health>(out Health health) && !hasHappened)
        {
            audioSource.Play();
            hasHappened = true;
        }
    }
}
