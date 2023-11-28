using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magazine : MonoBehaviour
{
    [SerializeField] private int maxBallesChargeur = 9;
    public int nbBallesChargeur;

    public event Action<int> EventNombreDeBalles;

    private void Start()
    {
        nbBallesChargeur = maxBallesChargeur;
    }
}
