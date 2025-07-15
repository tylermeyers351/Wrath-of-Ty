using System.Collections;
using UnityEngine;

public class RotateGameObject : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(0, 30f, 0); // degrees per second
    [SerializeField] AudioSource audioSource;
    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameObject controlsUI;
    [SerializeField] GameObject tipsUI;
    [SerializeField] GameObject thanksUI;

    bool cheeseTriggered = false;

    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent<Health>(out Health health) && !cheeseTriggered)
        {
            // Debug.Log("No one made it out...");
            GetComponent<MeshRenderer>().enabled = false;
            audioSource.Play();
            StartCoroutine(ShowUIAfterDelay());
            cheeseTriggered = true;
        }
    }

    IEnumerator ShowUIAfterDelay()
    {
        yield return new WaitForSeconds(3f);
        // Disable SphereCollider on the same GameObject
        GetComponent<SphereCollider>().enabled = false;

        gameOverUI.SetActive(true);
        controlsUI.SetActive(false);
        tipsUI.SetActive(false);
        yield return new WaitForSeconds(3f);
        gameOverUI.SetActive(false);
        thanksUI.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        thanksUI.SetActive(false);
        yield return new WaitForSeconds(5f);
        QuitGame();
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

}
