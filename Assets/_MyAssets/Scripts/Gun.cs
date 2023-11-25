using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    

    private bool isShooting = false;
    private bool isCharge = false;

    [SerializeField] private Transform raycastOrigin;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private ParticleSystem hitEffect;
    [SerializeField] private TrailRenderer tracerEffect;
    [SerializeField] private Collider magazine;

    Ray ray;
    RaycastHit hit;

    public void Shooting()
    {
        //if (isCharge)
        //{
            Debug.Log("chargé !");
            this.GetComponent<AudioSource>().Play();

            isShooting = true;
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
        //}
        //else
        //{
        //    Debug.Log("Pas de chargeur !");
        //}
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    // Vérifiez si l'objet a le tag "Magazine"
    //    if (other.gameObject.CompareTag("Magazine"))
    //    {
    //        Debug.Log("Chargé !");
    //        isCharge = true;
    //    }
    //    else
    //    {
    //        Debug.Log("Pas de chargeur !");
    //        isCharge = false;
    //    }
    //}
}
