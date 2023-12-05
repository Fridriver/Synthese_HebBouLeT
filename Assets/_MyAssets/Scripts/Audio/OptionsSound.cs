using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsSound : MonoBehaviour
{
    [SerializeField] private string typeOfSound;
    public string TypeOfSound { get { return typeOfSound; } }

    public Slider SoundSlider { get; private set; }
    public Toggle SoundToggle { get; private set; }

    private void Start()
    {
        SoundSlider = GetComponentInChildren<Slider>();
        SoundToggle = GetComponentInChildren<Toggle>();
    }





}
