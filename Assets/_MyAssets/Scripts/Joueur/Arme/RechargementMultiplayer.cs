using System.Collections;
using UnityEngine;
using Unity.Netcode;

public class RechargementMultiplayer : NetworkBehaviour
{
    [SerializeField] private GameObject ChargeurPrefab;
    [SerializeField] private GameObject Socket;

    public void Reload()
    {
        StartCoroutine(ActualReload());
        ReloadMagServerRpc(NetworkManager.LocalClientId);

    }


    [ServerRpc(RequireOwnership = false)]
    public void ReloadMagServerRpc(ulong sender)
    {
        ReloadMagClientRpc(sender);
    }

    [ClientRpc]
    public void ReloadMagClientRpc(ulong sender)
    {
        if (NetworkManager.LocalClientId != sender)
            ActualReload();
    }

    IEnumerator ActualReload()
    {
        yield return new WaitForSeconds(2f);
        GameObject chargeur = Instantiate(ChargeurPrefab, Socket.transform.position, Quaternion.identity);
        chargeur.GetComponent<NetworkObject>().Spawn(true);
    }
}
