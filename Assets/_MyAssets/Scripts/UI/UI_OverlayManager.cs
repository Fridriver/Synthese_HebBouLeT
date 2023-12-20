using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_OverlayManager : MonoBehaviour
{
    [SerializeField] private TMP_Text waveTxt;
    [SerializeField] private Image background;
    private EnemiesSpawner EnemiesSpawner;

    // Start is called before the first frame update
    private void Start()
    {
        EnemiesSpawner = FindObjectOfType<EnemiesSpawner>();
        EnemiesSpawner.waveUpdateEvent += OnWaveUpdateEvent;
        waveTxt.enabled = false;
        background.enabled = false;
        waveTxt.color = new Color(waveTxt.color.r, waveTxt.color.g, waveTxt.color.b, 0f);
        background.color = new Color(background.color.r, background.color.g, background.color.b, 0f);
    }

    private void OnWaveUpdateEvent(int obj)
    {
        waveTxt.text = "Vague " + obj.ToString();
        StartCoroutine(ShowWaveTxt());
    }

    private IEnumerator ShowWaveTxt()
    {

        //yield return new WaitForSeconds(1f);
        waveTxt.enabled = true;
        background.enabled = true;
        StartCoroutine(Fade(0, 1));

        yield return new WaitForSeconds(3f);
        StartCoroutine(Fade(1, 0));

        yield return new WaitForSeconds(2.1f);
        waveTxt.enabled = false;
        background.enabled = false;
    }

    private IEnumerator Fade(float a, float b)
    {
        float fadeTime = 2f;
        float alpha = 1f;

        for (float t = 0.0f; t < fadeTime; t += Time.deltaTime)
        {
            alpha = Mathf.Lerp(a, b, t / fadeTime);
            waveTxt.color = new Color(waveTxt.color.r, waveTxt.color.g, waveTxt.color.b, alpha);
            background.color = new Color(background.color.r, background.color.g, background.color.b, alpha);
            yield return null;
        }
    }

}