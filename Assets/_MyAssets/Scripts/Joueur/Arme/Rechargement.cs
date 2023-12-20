using System.Collections;
using UnityEngine;

public class Rechargement : MonoBehaviour
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
    }
}
