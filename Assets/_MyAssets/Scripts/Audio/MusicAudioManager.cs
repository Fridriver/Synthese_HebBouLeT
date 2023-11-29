using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicAudioManager : MonoBehaviour
{
    public static MusicAudioManager Instance { get; private set; }

    public float musicVolume { get; set; }
    public bool musicPlaying { get; set; }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
