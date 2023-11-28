using System;
using UnityEngine;

public class Magazine : MonoBehaviour
{
    [SerializeField] public int maxBallesChargeur { get; private set; } = 9;
    public int nbBallesChargeur;

    public event Action<int> EventNombreDeBalles;

    private void Start()
    {
        nbBallesChargeur = maxBallesChargeur;
    }
}