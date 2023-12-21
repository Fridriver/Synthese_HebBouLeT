using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class AmmunitionPanelManagerMultijoueur : NetworkBehaviour
{
    [SerializeField] private TMP_Text ammunitionText;
    
    private GunMultiplayer gun;
    [SerializeField] private Color fullMagColor;
    [SerializeField] private Color emptyMagColor ;


    //Lier avec le pistol et le magasine
    // Start is called before the first frame update
    void Start()
    {
        gun = GetComponentInParent<GunMultiplayer>();
        gun.OnAmmoChangeEvent += OnAmmoChangeEvent;
        
    }

    private void OnAmmoChangeEvent(string nbBalles)
    {
        if(nbBalles == "0") {
            ammunitionText.color = emptyMagColor; }
        else {
            ammunitionText.color = fullMagColor;
        }
            

        ammunitionText.text = nbBalles;
    }

}
