using System;
using System.Collections;
using System.Linq;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Magazine : MonoBehaviour
{
    [SerializeField] public int maxBallesChargeur { get; private set; } = 19;
    public int nbBallesChargeur;
    private AmmunitionPanelManager ammunitionPanelManager;

    public event Action<int> EventNombreDeBalles;
   

    private void Start()
    {
        nbBallesChargeur = maxBallesChargeur;
        
        Collider collider = GetComponent<Collider>(); 
        Collider playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Collider>();
        Physics.IgnoreCollision(collider, playerCollider);
    }



    private void OnCollisionEnter(Collision collision)
    {
  
        
        if(collision.gameObject.tag == "Environnement")
        {
           
            StartCoroutine(Delay());
        }
   
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

}