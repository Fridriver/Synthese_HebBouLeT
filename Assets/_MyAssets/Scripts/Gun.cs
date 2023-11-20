using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform _firePoint;
    public event Action<int> EventBallonTouche;

    public void Shooting()
    {
        this.GetComponent<AudioSource>().Play();

        RaycastHit hit;

        if (Physics.Raycast(_firePoint.position, transform.TransformDirection(Vector3.forward), out hit, 10))
        {
            
        }
    }
}
