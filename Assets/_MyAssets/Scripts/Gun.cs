using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform _firePoint;

    private bool isShooting = false;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private ParticleSystem hitEffect;

    public void Shooting()
    {
        isShooting = true;
        muzzleFlash.Emit(1);

        RaycastHit hit;

        if (Physics.Raycast(_firePoint.position, _firePoint.transform.TransformDirection(Vector3.forward), out hit))
        {
            Debug.DrawRay(_firePoint.position, _firePoint.transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);

            hitEffect.transform.position = hit.point;
            hitEffect.transform.forward = hit.normal;
            hitEffect.Emit(1);

        }
    }
}
