using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.Networking;


public class GunInstantiatator : NetworkBehaviour
{
    [SerializeField] private GameObject ChargeurPrefab;
    [SerializeField] private GameObject GunPrefab;
    [SerializeField] private GameObject SocketGun;
    private GameObject Gun;
    private Vector3 GunPosition;
    private bool ChargerInitialized;

    void Start()
    {
        InstantiateGunClientRpc();
        ChargerInitialized = false;
    }

    private void Update()
    {
        if (ChargerInitialized)
        {
            return;
        }
        InstantiateChargerClientRpc();
    }

    [ClientRpc]
    private void InstantiateGunClientRpc()
    {
        Gun = Instantiate(GunPrefab, SocketGun.transform.position, Quaternion.identity);
        Gun.GetComponent<NetworkObject>().Spawn(true);
    }

    [ClientRpc]
    private void InstantiateChargerClientRpc()
    {
        StartCoroutine(InstantiateCharger());
    }

    IEnumerator InstantiateCharger()
    {
        ChargerInitialized = true;
        yield return new WaitForSeconds(0.5f);
        GunPosition = Gun.transform.Find("Socket").transform.position;
        GameObject charger =Instantiate(ChargeurPrefab, GunPosition, Gun.transform.Find("Socket").transform.rotation);
        charger.GetComponent<NetworkObject>().Spawn(true);
    }
}
