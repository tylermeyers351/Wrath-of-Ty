using UnityEngine;

public class VillageAudio : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent<Health>(out Health health))
        {
            audioSource.Play();
        }
    }
}
