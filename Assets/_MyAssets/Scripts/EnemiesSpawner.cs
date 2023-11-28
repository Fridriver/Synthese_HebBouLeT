using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField] private float _spawnTime = 1;
    [SerializeField] private Transform[] _spawnPoints = default;
    [SerializeField] private GameObject[] _enemiesList = default;
    private float _timer;

    // Update is called once per frame
    void Update()
    {
        spawner();
    }

    private void spawner()
    {
        if (_timer > _spawnTime)
        {
            GameObject randomEnemy = _enemiesList[Random.Range(0, _enemiesList.Length)];
            Transform randomSpawn = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
            GameObject enemie = Instantiate(randomEnemy, randomSpawn.position, randomSpawn.rotation);

            _timer = 0;
        }
        _timer += Time.deltaTime;
    }


}
