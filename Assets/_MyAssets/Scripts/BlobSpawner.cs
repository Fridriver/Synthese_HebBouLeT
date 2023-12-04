using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _containerBlob = default;
    [SerializeField] private GameObject _blob = default;
    [SerializeField] private Transform[] _spawnPoints = default;
    [SerializeField] private static int _ACTIVEBLOB = 3;

    private int _nbBlobActive;

    void Update()
    {
        spawner();
    }

    private void spawner()
    {
        if (_nbBlobActive < _ACTIVEBLOB)
        {
            _nbBlobActive++;
            Transform randomSpawn = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
            GameObject blob = Instantiate(_blob, randomSpawn.position, randomSpawn.rotation);
            
            blob.transform.parent = _containerBlob.transform;
        }
    }
}
