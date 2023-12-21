using System.Collections;
using UnityEngine;
using Unity.Netcode;

public class RechargementMultiplayer : NetworkBehaviour
{
    [SerializeField] private GameObject ChargeurPrefab;
    [SerializeField] private GameObject Socket;

    [ClientRpc]
    public void RechargementMagazineClientRpc()
    {
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2f);
        GameObject chargeur = Instantiate(ChargeurPrefab, Socket.transform.position, Quaternion.identity);
        chargeur.GetComponent<NetworkObject>().Spawn(true);
    }
}
