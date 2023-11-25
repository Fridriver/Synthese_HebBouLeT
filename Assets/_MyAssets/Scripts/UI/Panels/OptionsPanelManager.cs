using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsPanelManager : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Toggle muteToggle;
    private float volume;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GameObject.Find("AudioManager").GetComponent<AudioSource>();
        volume = audioSource.volume;
        volumeSlider.value = volume;
        volumeSlider.onValueChanged.AddListener(delegate { ChangeVolume(); });
        muteToggle.onValueChanged.AddListener(delegate { Mute(); });
    }

    public void ChangeVolume()
    {
        volume = volumeSlider.value;
        audioSource.volume = volume;
    }

    public void Mute()
    {
        audioSource.mute = muteToggle.isOn;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
