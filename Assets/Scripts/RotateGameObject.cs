using UnityEngine;

public class RotateGameObject : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(0, 30f, 0); // degrees per second

    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent<Health>(out Health health))
        {
            Debug.Log("No one made it out...");
        }
    }

}
