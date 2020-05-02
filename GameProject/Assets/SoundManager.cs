using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    // Mixers 
    public AudioMixer audioMixer;

    public void SetVolume(AudioMixerGroup targetGroup, float value)
    {
        audioMixer.SetFloat(targetGroup.name, Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat(targetGroup.name, value);
    }

}