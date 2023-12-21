using System;
using System.Collections;
using Unity.Netcode;
using UnityEngine;

public class MagazineMultiplayer : NetworkBehaviour
{
    [SerializeField] public int maxBallesChargeur { get; private set; } = 19;
    public int nbBallesChargeur;
    private AmmunitionPanelManagerMultijoueur ammunitionPanelManager;

    public event Action<int> EventNombreDeBalles;

    private void Start()
    {
        nbBallesChargeur = maxBallesChargeur;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Environnement")
        {
            StartCoroutine(Delay());
            DestroyMagServerRPC(NetworkManager.LocalClientId);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void DestroyMagServerRPC(ulong sender)
    {
        DestroyMagClientRPC(sender);
    }

    [ClientRpc]
    public void DestroyMagClientRPC(ulong sender)
    {
        if (NetworkManager.LocalClientId != sender)
        {
            StartCoroutine(Delay());
        }
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}