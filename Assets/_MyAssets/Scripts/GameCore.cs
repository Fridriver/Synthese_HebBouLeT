using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameCore : MonoBehaviour
{
    public static GameCore Instance { get; private set; }

    private int killCount;
    private int waveCount;
    [SerializeField] private TMP_Text waveTxt;

    private EnemiesSpawner EnemiesSpawner;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            killCount = 0;
            waveCount = 0;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        EnemiesSpawner = FindObjectOfType<EnemiesSpawner>();
        EnemiesSpawner.waveUpdateEvent += OnWaveUpdateEvent;
        waveTxt.color = new Color(waveTxt.color.r, waveTxt.color.g, waveTxt.color.b, 0f);
    }

    private void OnWaveUpdateEvent(int obj)
    {
        waveCount = obj;
        waveTxt.text = "Vague " + waveCount.ToString();
        StartCoroutine(ShowWaveTxt());
    }

    IEnumerator ShowWaveTxt()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(Fade(0, 1));

        yield return new WaitForSeconds(3f);
        StartCoroutine(Fade(1, 0));
    }

    IEnumerator Fade(float a, float b)
    {
        float fadeTime = 2f;
        float alpha = 1f;

        for (float t = 0.0f; t < fadeTime; t += Time.deltaTime)
        {
            alpha = Mathf.Lerp(a, b, t / fadeTime);
            waveTxt.color = new Color(waveTxt.color.r, waveTxt.color.g, waveTxt.color.b, alpha);
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
