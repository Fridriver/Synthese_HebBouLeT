using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Gun : MonoBehaviour
{
   

    [Header("Mag")]
    [SerializeField] private XRBaseInteractor socketInteractor = default;
    private Magazine magazine;
    private bool isCharge;
    private bool magazineIsLoaded = true;

    [Header("Gun")]
    [SerializeField] private Transform raycastOrigin;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private ParticleSystem hitEffect;
    [SerializeField] private TrailRenderer tracerEffect;
    private Ray ray;
    private RaycastHit hit;

    [Header("Sounds")]
    [SerializeField] private AudioClip shotSound;
    [SerializeField] private AudioClip insertMagSound;
    [SerializeField] private AudioClip emptyMagSound;
    [SerializeField] private AudioClip removeMagSound;
    private AudioSource audioSource;

    private LiquidAmmoDisplay liquidAmmoDisplay;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        socketInteractor.selectEntered.AddListener(AddMagazine);
        socketInteractor.selectExited.AddListener(RemoveMagazine);

    }

    public void AddMagazine(SelectEnterEventArgs interactor)
    {
        magazine = interactor.interactableObject.ConvertTo<Magazine>();
        magazine.EventNombreDeBalles += Magazine_EventNombreDeBalles;
        magazineIsLoaded = true;
        audioSource.PlayOneShot(insertMagSound);
        
    }

    public void RemoveMagazine(SelectExitEventArgs interactor)
    {
        magazine.EventNombreDeBalles -= Magazine_EventNombreDeBalles;
        audioSource.PlayOneShot(removeMagSound);
        magazine = null;
    }

    private void Magazine_EventNombreDeBalles(int obj)
    {
        if (magazine.GetComponent<Magazine>().nbBallesChargeur == 0)
        {
            magazineIsLoaded = false;
            return;
        }
        magazine.GetComponent<Magazine>().nbBallesChargeur -= obj;
        
    }

    public void Shooting()
    {
        if (isCharge && magazineIsLoaded)
        {
            //Debug.Log("chargé !");

            Magazine_EventNombreDeBalles(1);
            audioSource.PlayOneShot(shotSound);

            muzzleFlash.Emit(1);

            ray.origin = raycastOrigin.position;
            ray.direction = raycastOrigin.forward;
            var tracer = Instantiate(tracerEffect, ray.origin, Quaternion.identity);

            tracer.AddPosition(ray.origin);
            if (Physics.Raycast(ray, out hit))
            {
                hitEffect.transform.position = hit.point;
                hitEffect.transform.forward = hit.normal;
                hitEffect.Emit(1);

                tracer.transform.position = hit.point;
            }
        }
        else
        {

            audioSource.PlayOneShot(emptyMagSound);
            //Jouer son de clic
        }
    }

    public void Loaded()
    {
        isCharge = true;
    }

    public void UnLoaded()
    {
        isCharge = false;
    }
}