using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GunMultiplayer : MonoBehaviour
{
    [Header("Blob")]
    private Player_VR player;
    [SerializeField] private float _gainSante = 100f;

    [Header("Mag")]
    [SerializeField] private XRBaseInteractor socketInteractor = default;

    private Magazine magazine;
    private bool magazineIsInsert;
    private bool magazineIsLoaded = true;


    [Header("Gun")]
    [SerializeField] private Transform raycastOrigin;

    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private ParticleSystem hitEffect;
    [SerializeField] private TrailRenderer tracerEffect;
    
    private int killCount;
    private Ray ray;
    private RaycastHit hit;

    [Header("Sounds")]
    [SerializeField] private AudioClip shotSound;

    [SerializeField] private AudioClip insertMagSound;
    [SerializeField] private AudioClip emptyMagSound;
    [SerializeField] private AudioClip removeMagSound;
    private AudioSource audioSource;

    private LiquidAmmoDisplay liquidAmmoDisplay;

    public event Action<GameObject,RaycastHit> OnEnemyHitEvent;
    public event Action<GameObject, RaycastHit> OnBlobHitEvent;
    public event Action<string> OnAmmoChangeEvent;
    public event Action<int> OnKillCountChangeEvent;

    private void Start()
    {
        player = FindObjectOfType<Player_VR>();
        audioSource = GetComponent<AudioSource>();
        socketInteractor.selectEntered.AddListener(AddMagazine);
        socketInteractor.selectExited.AddListener(RemoveMagazine);

    }

    public void AddMagazine(SelectEnterEventArgs interactor)
    {
        magazine = interactor.interactableObject.ConvertTo<Magazine>();
        magazine.EventNombreDeBalles += Magazine_EventNombreDeBalles;
        OnAmmoChangeEvent?.Invoke(magazine.nbBallesChargeur.ToString());
        if (magazine.nbBallesChargeur == 0)
        {
            magazineIsLoaded = false;
        }
        else
        {
            magazineIsLoaded = true;
        }

        audioSource.PlayOneShot(insertMagSound);
    }

    public void RemoveMagazine(SelectExitEventArgs interactor)
    {
        magazine.EventNombreDeBalles -= Magazine_EventNombreDeBalles;
        audioSource.PlayOneShot(removeMagSound);
        OnAmmoChangeEvent?.Invoke("0");
        magazine = null;
    }

    private void Magazine_EventNombreDeBalles(int obj)
    {
        if (magazine.nbBallesChargeur == 0)
        {
            magazineIsLoaded = false;
            return;
        }
        magazine.GetComponent<Magazine>().nbBallesChargeur -= obj;
        OnAmmoChangeEvent?.Invoke(magazine.nbBallesChargeur.ToString());
    }

    [ContextMenu("Shooting")]
    public void Shooting()
    {
        if (magazineIsInsert && magazineIsLoaded)
        {
            Magazine_EventNombreDeBalles(1);
            audioSource.PlayOneShot(shotSound);

            muzzleFlash.Emit(1);

            ray.origin = raycastOrigin.position;
            ray.direction = raycastOrigin.forward;
            var tracer = Instantiate(tracerEffect, ray.origin, Quaternion.identity);

            tracer.AddPosition(ray.origin);
            if (Physics.Raycast(ray, out hit))
            {


                tracer.transform.position = hit.point;

                if (hit.collider.tag == "Ennemi")
                {
                    OnEnemyHitEvent?.Invoke(hit.collider.gameObject, hit);
                    OnKillCountChangeEvent?.Invoke(killCount++);
                    
                }
                else if (hit.collider.tag == "Blob")
                {
                    OnBlobHitEvent?.Invoke(hit.collider.gameObject, hit);
                    if (player._maxSante - player._sante < _gainSante)
                    {
                        player._sante += player._maxSante - player._sante;
                    }
                    else
                    {
                        player._sante += _gainSante;
                    }
                    
                }
                else
                {
                    hitEffect.transform.position = hit.point;
                    hitEffect.transform.forward = hit.normal;
                    hitEffect.Emit(1);
                }
            }
        }
        else
        {
            audioSource.PlayOneShot(emptyMagSound);
        }
    }

    public void Loaded()
    {
        magazineIsInsert = true;
    }

    public void UnLoaded()
    {
        magazineIsInsert = false;
    }
}