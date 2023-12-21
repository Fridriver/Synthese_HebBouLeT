using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCoreMultijoueur : NetworkBehaviour
{
    public static GameCoreMultijoueur Instance { get; private set; }

    private int killCount;
    public int waveCount { get; private set; }

    private EnemiesSpawnerMultijoueur EnemiesSpawner;
    private Gun gun;
    private bool isAlreadyInSceneNiveau = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            killCount = 0;
            waveCount = 0;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        EventLoad();
    }

    private void OnWaveUpdateEvent(int obj)
    {
        waveCount = obj;
    }
    private void OnKillCountChangeEvent(int obj)
    {
        killCount = obj;
    }


    // Update is called once per frame
    private void Update()
    {
        if(SceneManager.GetActiveScene().name == "EndScene")
        {
            EventUnload();
            isAlreadyInSceneNiveau = false;
        }
        if (SceneManager.GetActiveScene().name == "NiveauMultijoueur" && !isAlreadyInSceneNiveau)
        {
            EventLoad();
            isAlreadyInSceneNiveau = true;
        }

    }

    private void EventLoad()
    {
        EnemiesSpawner = FindObjectOfType<EnemiesSpawnerMultijoueur>();
        EnemiesSpawner.waveUpdateEvent += OnWaveUpdateEvent;
        gun = FindObjectOfType<Gun>();
        gun.OnKillCountChangeEvent += OnKillCountChangeEvent;
        killCount = 0;
        waveCount = 0;
    }


    private void EventUnload()
    {
        EnemiesSpawner.waveUpdateEvent -= OnWaveUpdateEvent;
        gun.OnKillCountChangeEvent -= OnKillCountChangeEvent;
    }

    

}