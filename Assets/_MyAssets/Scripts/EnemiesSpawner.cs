using TMPro.EditorUtilities;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _containerEnnemies = default;
    [SerializeField] private float _spawnTime = 4;
    [SerializeField] private Transform[] _spawnPoints = default;
    [SerializeField] private GameObject[] _enemiesList = default;
    private float _timer;

    private Gun gun;

    private void Start()
    {
        gun = FindObjectOfType<Gun>();
    }

    void Update()
    {
        spawner();
    }

    private void spawner()
    {
        if (_timer > _spawnTime)
        {
            if (gun.nbMort <= 0)
            {
                //Debug.Log("Rien. (" + gun.nbMort + ") vitesse : " + _spawnTime);
                _spawnTime = 4;
            }
            else if (gun.nbMort > 0 && gun.nbMort < 1)
            {
                //Debug.Log("First Blood ! (" + gun.nbMort + ") vitesse : " + _spawnTime);
                _spawnTime = 3;
            }
            else if (gun.nbMort >= 1 && gun.nbMort < 2)
            {
                //Debug.Log("Dead Dead Dead... (" + gun.nbMort + ") vitesse : " + _spawnTime);
                _spawnTime = 2;
            }
            else if (gun.nbMort >= 2)
            {
                //Debug.Log("AAAAAAAAAAAAAAAAAAAH !! (" + gun.nbMort + ") vitesse : " + _spawnTime);
                _spawnTime = 2;
            }
            GameObject randomEnemy = _enemiesList[Random.Range(0, _enemiesList.Length)];
            Transform randomSpawn = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
            GameObject enemie = Instantiate(randomEnemy, randomSpawn.position, randomSpawn.rotation);
            enemie.transform.parent = _containerEnnemies.transform;

            _timer = 0;
        }
        _timer += Time.deltaTime;
    }
}
