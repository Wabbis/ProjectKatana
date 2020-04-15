using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public string target;
    public Slider slider;

    private void Start()
    {
        slider.value = PlayerPrefs.GetFloat(target, 0.75f);
    }
    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat(target, Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat(target, sliderValue);
    }
}