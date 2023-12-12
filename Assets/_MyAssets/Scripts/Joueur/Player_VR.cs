using System;
using System.Collections;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;

public class Player_VR : MonoBehaviour
{
    private AudioSource audioSource;

    [Header("Stats Joueur")]
    [SerializeField] private float _santePerteSec = 0.5f;
    [SerializeField] public float _maxSante = 1000f;
    [SerializeField] private Image healthBar;
    [SerializeField] private AudioClip alertHealthLow;
    [SerializeField] private GameObject LightHealthLow;
    public float _sante;
    private bool isProgression = false;
    private bool isAlert = false;

    [Header("Zone de mort")]
    [SerializeField] private Collider _zoneMort = default;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        _sante = _maxSante;
    }

    void Update()
    {
        Maladie();
        Alert();
    }


    private void Maladie()
    {
        if (!isProgression)
        {
            isProgression = true;
            if (_sante != 0)
            {
                StartCoroutine(DiseaseProgression());
                _sante--;
                healthBar.fillAmount = _sante / _maxSante;
            }
        }
    }

    private void Alert()
    {
        if (_sante <= (0.25 *_maxSante))
        {
            LightHealthLow.gameObject.SetActive(true);
            if (!isAlert)
            {
                isAlert = true;
                StartCoroutine(AlertWait());
                audioSource.PlayOneShot(alertHealthLow);
            }
        }
        else
        {
            LightHealthLow.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision");
        if (collision.gameObject.tag == "Ennemi")
        {
            PlayerDead();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ennemi")
        {
            PlayerDead();
        }
    }

    private void PlayerDead()
    {
        StartCoroutine(DeadWait());
        SceneLoaderManager.Instance.LoadScene("EndScene");
    }

    IEnumerator DeadWait()
    {
        yield return new WaitForSeconds(5f);
    }

    IEnumerator DiseaseProgression()
    {
        yield return new WaitForSeconds(_santePerteSec);
        isProgression = false;
    }

    IEnumerator AlertWait()
    {
        yield return new WaitForSeconds(alertHealthLow.length + 2);
        isAlert = false;
    }
}
