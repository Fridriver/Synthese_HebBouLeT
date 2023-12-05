using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rechargement : MonoBehaviour
{
    [SerializeField] private GameObject Chargeur;
    [SerializeField] private Transform Socket;

    public void RechargementMagazine()
    {
        StartCoroutine(Delay());
    }
    
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2f);
        Instantiate(Chargeur, Socket.position, Quaternion.identity);
    }
}
