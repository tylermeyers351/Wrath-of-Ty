using System.Collections;
using UnityEngine;
using TMPro;

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

    void Start()
    {
        blackPanel.SetActive(true);
        StartCoroutine(OpeningSequence());
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

    }
}
