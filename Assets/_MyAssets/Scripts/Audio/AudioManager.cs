using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private static float VOLUMEATSTART = -4f;

    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer audioMixer;

    [Header("Sliders")]
    [SerializeField] private Slider MasterSlider;

    [SerializeField] private Slider BGMSlider;
    [SerializeField] private Slider sfxSlider;

    [Header("Toggles")]
    [SerializeField] private Toggle MasterToggle;

    [SerializeField] private Toggle BGMToggle;
    [SerializeField] private Toggle sfxToggle;

    private GameObject optionPanel;

    private void Awake()
    {
        LoadVolume();
    }

    // Start is called before the first frame update
    private void Start()
    {
        MasterSlider.onValueChanged.AddListener(delegate { SetVolume("Master", MasterToggle.isOn, GetVolume(MasterSlider)); });
        BGMSlider.onValueChanged.AddListener(delegate { SetVolume("BGM", BGMToggle.isOn, GetVolume(BGMSlider)); });
        sfxSlider.onValueChanged.AddListener(delegate { SetVolume("SFX", sfxToggle.isOn, GetVolume(sfxSlider)); });

        MasterToggle.onValueChanged.AddListener(delegate { SetVolume("Master", MasterToggle.isOn, GetVolume(MasterSlider)); });
        BGMToggle.onValueChanged.AddListener(delegate { SetVolume("BGM", BGMToggle.isOn, GetVolume(BGMSlider)); });
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

    private void SetSliderValue(Slider slider, float value)
    {
        slider.value = value;
    }

    private void LoadVolume()
    {
        LoadVolume("Master");
        LoadVolume("BGM");
        LoadVolume("SFX");
    }

    private void LoadVolume(string mixerGroup)
    {
        if (!PlayerPrefs.HasKey(mixerGroup))
        {
            PlayerPrefs.SetFloat(mixerGroup, VOLUMEATSTART);
            audioMixer.SetFloat(mixerGroup, VOLUMEATSTART);
            SetSliderValue(mixerGroup == "Master" ? MasterSlider : mixerGroup == "BGM" ? BGMSlider : sfxSlider, VOLUMEATSTART);
        }
        else
        {
            audioMixer.SetFloat(mixerGroup, PlayerPrefs.GetFloat(mixerGroup));
            SetSliderValue(mixerGroup == "Master" ? MasterSlider : mixerGroup == "BGM" ? BGMSlider : sfxSlider, PlayerPrefs.GetFloat(mixerGroup));
        }
    }
}