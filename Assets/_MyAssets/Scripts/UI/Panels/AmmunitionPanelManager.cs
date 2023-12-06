using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmunitionPanelManager : MonoBehaviour
{
    [SerializeField] private TMP_Text ammunitionText;
    
    private Gun gun;
    [SerializeField] private Color fullMagColor;
    [SerializeField] private Color emptyMagColor ;


    //Lier avec le pistol et le magasine
    // Start is called before the first frame update
    void Start()
    {
        gun = FindObjectOfType<Gun>();
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
