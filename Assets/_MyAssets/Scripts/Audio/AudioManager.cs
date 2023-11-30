using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private static float VOLUMEATSTART = 0f;

    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer audioMixer;

    [Header("Sliders")]
    [SerializeField] private Slider masterSlider;

    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    [Header("Toggles")]
    [SerializeField] private Toggle masterToggle;

    [SerializeField] private Toggle musicToggle;
    [SerializeField] private Toggle sfxToggle;

    private GameObject optionPanel;

    private void Awake()
    {
            LoadVolume("Master");
            LoadVolume("BGM");
            LoadVolume("SFX");
        
    }

    // Start is called before the first frame update
    private void Start()
    {
        masterSlider.onValueChanged.AddListener(delegate { SetVolume("Master", masterToggle.isOn, GetVolume(masterSlider)); });
        musicSlider.onValueChanged.AddListener(delegate { SetVolume("BGM", musicToggle.isOn, GetVolume(musicSlider)); });
        sfxSlider.onValueChanged.AddListener(delegate { SetVolume("SFX", sfxToggle.isOn, GetVolume(sfxSlider)); });

        masterToggle.onValueChanged.AddListener(delegate { SetVolume("Master", masterToggle.isOn, GetVolume(masterSlider)); });
        musicToggle.onValueChanged.AddListener(delegate { SetVolume("BGM", musicToggle.isOn, GetVolume(musicSlider)); });
        sfxToggle.onValueChanged.AddListener(delegate { SetVolume("SFX", sfxToggle.isOn, GetVolume(sfxSlider)); });

    }

    private void SetVolume(string mixerGroup, bool mute, float volume)
    {
        audioMixer.SetFloat(mixerGroup, mute ? -80 : volume);
        PlayerPrefs.SetFloat(mixerGroup, volume);
    }

    private float GetVolume(Slider slider)
    {
        return slider.value;
    }
    private void LoadVolume(string mixerGroup)
    {
        if(!PlayerPrefs.HasKey(mixerGroup))
        {
            PlayerPrefs.SetFloat(mixerGroup, VOLUMEATSTART);
        }
        
    }
}