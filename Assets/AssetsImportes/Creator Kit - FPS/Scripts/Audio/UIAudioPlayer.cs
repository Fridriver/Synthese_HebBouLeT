using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAudioPlayer : MonoBehaviour
{
    public static UIAudioPlayer Instance { get; private set; }

    [SerializeField] private AudioClip PositiveSound;
    [SerializeField] private AudioClip NegativeSound;
    [SerializeField] private AudioClip SelectSound;
    
    AudioSource m_Source;

    void Awake()
    {
        m_Source = GetComponent<AudioSource>();
        Instance = this;
    }
    public static void Play(bool positive)
    {
        Instance.m_Source.PlayOneShot(positive ? Instance.PositiveSound : Instance.NegativeSound);
    }
    public static void PlayPositive()
    {
        Instance.m_Source.PlayOneShot(Instance.PositiveSound);
    }

    public static void PlayNegative()
    {
        Instance.m_Source.PlayOneShot(Instance.NegativeSound);
    }

    public static void PlaySelect()
    {
         Instance.m_Source.PlayOneShot(Instance.SelectSound);
    }
}
