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

    public event Action<int> EventNombreDeBalles;

    private void Start()
    {
        nbBallesChargeur = maxBallesChargeur;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!(collision.gameObject.tag == "SocketMag"))
        {
            StartCoroutine(Delay());
            
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
}