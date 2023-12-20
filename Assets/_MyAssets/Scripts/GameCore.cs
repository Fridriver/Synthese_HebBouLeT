using UnityEngine;

public class GameCore : MonoBehaviour
{
    public static GameCore Instance { get; private set; }

    private int killCount;
    private int waveCount;

    private EnemiesSpawner EnemiesSpawner;

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
    }

    private void OnWaveUpdateEvent(int obj)
    {
        waveCount = obj;
    }

    // Update is called once per frame
    private void Update()
    {
    }
}