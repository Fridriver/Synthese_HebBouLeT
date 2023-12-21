using System.Collections;
using Unity.Netcode;
using UnityEngine;

public class GunInstantiatator : NetworkBehaviour
{
    [SerializeField] private GameObject ChargeurPrefab;
    [SerializeField] private GameObject GunPrefab;
    [SerializeField] private GameObject SocketGun;
    private GameObject Gun;
    private Vector3 GunPosition;
    private bool ChargerInitialized;

    private void Start()
    {
        InstantiateGunServerRpc();
        ChargerInitialized = false;
    }

    private void Update()
    {
        if (ChargerInitialized)
        {
            return;
        }
        InstantiateChargerServerRpc();
        ChargerInitialized = true;
    }

    [ServerRpc(RequireOwnership = false)]
    private void InstantiateGunServerRpc()
    {
        Gun = Instantiate(GunPrefab, SocketGun.transform.position, Quaternion.identity);
        Gun.GetComponent<NetworkObject>().Spawn(true);
    }

    [ServerRpc(RequireOwnership = false)]
    private void InstantiateChargerServerRpc()
    {
        StartCoroutine(InstantiateCharger());
    }

    private IEnumerator InstantiateCharger()
    {
        yield return new WaitForSeconds(0.5f);
        GunPosition = Gun.transform.Find("Socket").transform.position;
        GameObject charger = Instantiate(ChargeurPrefab, GunPosition, Gun.transform.Find("Socket").transform.rotation);
        charger.GetComponent<NetworkObject>().Spawn(true);
    }
}