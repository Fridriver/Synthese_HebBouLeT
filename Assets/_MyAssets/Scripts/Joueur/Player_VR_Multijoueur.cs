using System.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class Player_VR_Multijoueur : NetworkBehaviour
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

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        _sante = _maxSante;
    }

    private void Update()
    {
        if (!IsOwner)
        { return; }
        Maladie();
        Alert();
        SanteDead();
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
        if (_sante <= (0.25 * _maxSante))
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

    public void PlayerDead()
    {
        StartCoroutine(DeadWait());
    }

    private void SanteDead()
    {
        if (_sante <= 0)
        {
            PlayerDead();
        }
    }

    private IEnumerator DeadWait()
    {
        yield return new WaitForSeconds(1f);
        NetworkSceneTransition.Instance.ChargerScenePourTous("EndScene");
        //SceneLoaderManager.Instance.LoadScene("EndScene");
    }

    private IEnumerator DiseaseProgression()
    {
        yield return new WaitForSeconds(_santePerteSec);
        isProgression = false;
    }

    private IEnumerator AlertWait()
    {
        yield return new WaitForSeconds(alertHealthLow.length + 2);
        isAlert = false;
    }
}