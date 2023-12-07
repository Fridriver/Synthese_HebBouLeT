using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _containerBlob = default;
    [SerializeField] private GameObject _blob = default;
    [SerializeField] private Transform[] _spawnPoints = default;

    [SerializeField] private static int _ACTIVEBLOB = 3;


    private ArrayList randomlist = new ArrayList();

    void Update()
    {
        spawner();
    }

    private void spawner()
    {
        int random = Random.Range(0, _spawnPoints.Length);

        if (_containerBlob.transform.childCount < _ACTIVEBLOB)
        {
            if (!randomlist.Contains(random))
            {
                Transform randomSpawn = _spawnPoints[random];
                GameObject blob = Instantiate(_blob, randomSpawn.position, randomSpawn.rotation);
                blob.transform.parent = _containerBlob.transform;
                randomlist.Add(random);
            }
        }
        else
        {
            randomlist.Clear();
        }



    }
}
