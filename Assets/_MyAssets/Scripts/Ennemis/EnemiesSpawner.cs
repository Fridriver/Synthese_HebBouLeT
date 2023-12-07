using System.Collections;
using TMPro.EditorUtilities;
using Unity.XR.CoreUtils;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _containerEnnemies = default;
    [SerializeField] private float _spawnTime = 4;
    [SerializeField] private Transform[] _spawnPoints = default;
    [SerializeField] private GameObject[] _enemiesList = default;
    [SerializeField] private float _difficulte = 1.5f;
    private float _multiplicateur = 1;
    private bool isCoroutineRunning;
    public int _waveNumber { get; private set; }

    private void Update()
    {
        wave();
    }

    private void spawner()
    {
        GameObject randomEnemy = _enemiesList[Random.Range(0, _enemiesList.Length)];
        Transform randomSpawn = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
        GameObject enemie = Instantiate(randomEnemy, randomSpawn.position, randomSpawn.rotation);
        enemie.transform.parent = _containerEnnemies.transform;
    }

    private void wave()
    {
        if (_containerEnnemies.transform.childCount > 0 || isCoroutineRunning)
        {
            return;
        }

        StartCoroutine(SpawnDelay());
    }

    IEnumerator SpawnDelay()
    {
        isCoroutineRunning = true;

        yield return new WaitForSeconds(3f);

        _waveNumber++;

        for (int i = 0; i < _multiplicateur; i++)
        {
            spawner();
            yield return new WaitForSeconds(1f);
        }

        _multiplicateur = Mathf.Round((float)(_multiplicateur * _difficulte));
        isCoroutineRunning = false;
    }

}