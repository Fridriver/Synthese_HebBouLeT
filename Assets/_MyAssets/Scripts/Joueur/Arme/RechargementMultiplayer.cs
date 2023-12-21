using System.Collections;
using UnityEngine;
using Unity.Netcode;

public class RechargementMultiplayer : NetworkBehaviour
{
    [SerializeField] private GameObject Chargeur;
    [SerializeField] private GameObject Socket;

    public void RechargementMagazine()
    {
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2f);
        Instantiate(Chargeur, Socket.transform.position, Quaternion.identity);
        Chargeur.GetComponent<NetworkObject>().Spawn(true);
    }
}
