using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointManager : MonoBehaviour
{
    [SerializeField] private Transform _player = default;
    [SerializeField] private Transform[] _spawnPoints = default;


    // Start is called before the first frame update
    void Start()
    {
        _player.position = GetSpawnPoint().position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected Transform GetSpawnPoint()
    {
        return _spawnPoints[Random.Range(0, _spawnPoints.Length)];
    }
}
