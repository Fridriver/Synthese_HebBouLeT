using System.Collections;
using UnityEngine;
using Unity.Netcode;

public class RechargementMultiplayer : NetworkBehaviour
{
    [SerializeField] private GameObject ChargeurPrefab;
    [SerializeField] private GameObject Socket;

    public void Reload()
    {
        StartCoroutine(ActualReload(Socket.transform.position, Quaternion.identity));
        ReloadMagServerRpc(Socket.transform.position, Quaternion.identity, NetworkManager.LocalClientId);
    }


    [ServerRpc(RequireOwnership = false)]
    public void ReloadMagServerRpc(Vector3 pos, Quaternion rot, ulong sender)
    {
        ReloadMagClientRpc(pos, rot, sender);
    }

    [ClientRpc]
    public void ReloadMagClientRpc(Vector3 pos, Quaternion rot, ulong sender)
    {
        if (NetworkManager.LocalClientId != sender)
            ActualReload(pos, rot);
    }

    IEnumerator ActualReload(Vector3 pos, Quaternion rot)
    {
        yield return new WaitForSeconds(2f);
        GameObject chargeur = Instantiate(ChargeurPrefab, pos, rot);
        chargeur.GetComponent<NetworkObject>().Spawn(true);
    }
}
