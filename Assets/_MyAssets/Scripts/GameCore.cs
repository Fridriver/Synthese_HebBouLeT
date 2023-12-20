using UnityEngine;

public class GameCore : MonoBehaviour
{
    public static GameCore Instance { get; private set; }

    private int killCount;
    public int waveCount { get; private set; }

    private EnemiesSpawner EnemiesSpawner;
    private Gun gun;

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
        EnemiesSpawner = FindObjectOfType<EnemiesSpawner>();
        EnemiesSpawner.waveUpdateEvent += OnWaveUpdateEvent;
        gun = FindObjectOfType<Gun>();
        gun.OnKillCountChangeEvent += OnKillCountChangeEvent;
        killCount = 0;
        waveCount = 0;
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
    }
}