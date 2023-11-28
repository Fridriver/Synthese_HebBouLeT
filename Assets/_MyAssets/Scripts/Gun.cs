using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Gun : MonoBehaviour
{
    private GameObject magazine;

    private bool isCharge;
    private bool magazineIsLoaded = true;
    private XRSocketInteractor interactor = default;

    [SerializeField] private Transform raycastOrigin;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private ParticleSystem hitEffect;
    [SerializeField] private TrailRenderer tracerEffect;

    Ray ray;
    RaycastHit hit;

    private void Start()
    {
        
        
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
            Debug.Log("chargé !");

            Magazine_EventNombreDeBalles(1);
            this.GetComponent<AudioSource>().Play();

            
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
            Debug.Log("Pas de chargeur !");
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        magazine = collision.gameObject;
        magazine.GetComponent<Magazine>().EventNombreDeBalles += Magazine_EventNombreDeBalles;
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
