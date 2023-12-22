using System;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GunMultiplayer : NetworkBehaviour
{
    [Header("Blob")]
    private Player_VR_Multijoueur player;

    [SerializeField] private float _gainSante = 100f;

    [Header("Mag")]
    [SerializeField] private XRBaseInteractor socketInteractor = default;

    private MagazineMultiplayer magazine;
    private bool magazineIsInsert;
    private bool magazineIsLoaded = true;

    [Header("Gun")]
    [SerializeField] private Transform raycastOrigin;

    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private ParticleSystem hitEffect;
    [SerializeField] private TrailRenderer tracerEffect;

    private NetworkXRGrabInteractable gunInteractable;

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

    public event Action<GameObject, RaycastHit> OnEnemyHitEvent;

    public event Action<GameObject, RaycastHit> OnBlobHitEvent;

    public event Action<string> OnAmmoChangeEvent;


    private void Start()
    {

        player = FindObjectOfType<Player_VR_Multijoueur>();
        audioSource = GetComponent<AudioSource>();

        gunInteractable = GetComponent<NetworkXRGrabInteractable>();
        gunInteractable.selectEntered.AddListener(Loaded);
        gunInteractable.selectExited.AddListener(UnLoaded);
        gunInteractable.activated.AddListener(Shooting);

        socketInteractor.selectEntered.AddListener(AddMagazine);
        socketInteractor.selectExited.AddListener(RemoveMagazine);
    }


    public void AddMagazine(SelectEnterEventArgs interactor)
    {
        magazine = interactor.interactableObject.ConvertTo<MagazineMultiplayer>();
        magazine.EventNombreDeBalles += onEventNombreDeBalles;
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
        magazine.EventNombreDeBalles -= onEventNombreDeBalles;
        audioSource.PlayOneShot(removeMagSound);
        OnAmmoChangeEvent?.Invoke("0");
        magazine = null;
    }

    private void onEventNombreDeBalles(int obj)
    {
        if (magazine.nbBallesChargeur == 0)
        {
            magazineIsLoaded = false;
            return;
        }
        magazine.GetComponent<MagazineMultiplayer>().nbBallesChargeur -= obj;
        OnAmmoChangeEvent?.Invoke(magazine.nbBallesChargeur.ToString());
    }

    [ContextMenu("Shooting")]
    public void Shooting(ActivateEventArgs args)
    {
        if (magazineIsInsert && magazineIsLoaded)
        {
            Shoot();
            ShootingServerRPC(NetworkManager.LocalClientId);
        }
        else
        {
            audioSource.PlayOneShot(emptyMagSound);
        }
    }

    public void Shoot()
    {
        onEventNombreDeBalles(1);

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
                Debug.Log("Ennemi touché");
                OnEnemyHitEvent?.Invoke(hit.collider.gameObject,hit);
                
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

    [ClientRpc]
    public void ShootingClientRPC(ulong sender)
    {
        if (NetworkManager.LocalClientId != sender)
        {
            Shoot();
            
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void ShootingServerRPC(ulong sender)
    {
        ShootingClientRPC(sender);
        
    }

    public void Loaded(SelectEnterEventArgs args)
    {
        magazineIsInsert = true;
        foreach(var playerTemp in FindObjectsOfType<Player_VR_Multijoueur>())
        {
            if(playerTemp.IsLocalPlayer)
            {
                player = playerTemp;
            }
        }

    }

    public void UnLoaded(SelectExitEventArgs args)
    {
        magazineIsInsert = false;
        player = null;
    }
}