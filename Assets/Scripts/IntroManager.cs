using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class IntroManager : MonoBehaviour
{

    [SerializeField] GameObject blackPanel;
    [SerializeField] GameObject controlsUI;

    [SerializeField] GameObject tipsUI;
    [SerializeField] AudioSource narrateAudio;
    [SerializeField] AudioSource musicAudio;

    [SerializeField] TextMeshProUGUI subtitleText;

    [SerializeField] float subtitleTime1 = 3f;
    [SerializeField] float subtitleTime2 = 3f;
    [SerializeField] float subtitleTime3 = 3f;
    [SerializeField] float subtitleTime4 = 3f;
    [SerializeField] float subtitleTime5 = 3f;
    [SerializeField] float finalDelay = 1f;

    [SerializeField] GameObject spacebarUI;
    [SerializeField] GameObject escapeUI;

    public static IntroManager Instance { get; private set; }
    public bool controlReady { get; private set; } = false;

    private bool hasStarted = false;

    private bool cursorUnlocked = true;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        blackPanel.SetActive(true);
        UnlockCursor();
    }

    void Update()
    {
        if (!hasStarted && Input.GetKeyDown(KeyCode.Space))
        {
            hasStarted = true;
            spacebarUI.SetActive(false);
            escapeUI.SetActive(false);
            StartCoroutine(OpeningSequence());
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnlockCursor();
        }

        if (cursorUnlocked && Input.GetMouseButtonDown(0))
        {
            LockCursor();
        }

    }

    private string[] lines = new string[]
    {
        "Elderglen was my home.",
        "Its peace held by a fragile alliance.",
        "My father's ambition invited ruin.",
        "He claimed the barbarians betrayed us.",
        "I knew better."
    };

    IEnumerator OpeningSequence()
    {
        yield return new WaitForSeconds(1f);

        narrateAudio.Play();

        subtitleText.text = lines[0];
        subtitleText.gameObject.SetActive(true);
        yield return new WaitForSeconds(subtitleTime1);
        subtitleText.gameObject.SetActive(false);

        subtitleText.text = lines[1];
        subtitleText.gameObject.SetActive(true);
        yield return new WaitForSeconds(subtitleTime2);
        subtitleText.gameObject.SetActive(false);

        subtitleText.text = lines[2];
        subtitleText.gameObject.SetActive(true);
        yield return new WaitForSeconds(subtitleTime3);
        subtitleText.gameObject.SetActive(false);

        subtitleText.text = lines[3];
        subtitleText.gameObject.SetActive(true);
        yield return new WaitForSeconds(subtitleTime4);
        subtitleText.gameObject.SetActive(false);

        subtitleText.text = lines[4];
        subtitleText.gameObject.SetActive(true);
        yield return new WaitForSeconds(subtitleTime5);
        subtitleText.gameObject.SetActive(false);

        yield return new WaitForSeconds(finalDelay);

        blackPanel.SetActive(false);
        musicAudio.Play();

        controlReady = true;
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cursorUnlocked = false;
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        cursorUnlocked = true;
    }
    

}
