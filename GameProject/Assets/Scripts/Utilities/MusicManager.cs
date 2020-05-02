using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static AudioClip ALARM, ARENEMY, PISTOLENEMY, EXPLODE, BOSSSHOT;
    static AudioSource audioSrc;

    void Start()
    {
        ALARM = Resources.Load<AudioClip>("ALARM");
        ARENEMY = Resources.Load<AudioClip>("ARENEMY");
        PISTOLENEMY = Resources.Load<AudioClip>("PISTOLENEMY");
        EXPLODE = Resources.Load<AudioClip>("EXPLODE");
        BOSSSHOT = Resources.Load<AudioClip>("BOSSSHOT");

        audioSrc = GetComponent<AudioSource>();
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "ALARM":
                audioSrc.Stop();
                audioSrc.loop = true;
                audioSrc.clip = ALARM;
                audioSrc.Play();
                break;
            case "ARENEMY":
                audioSrc.Stop();
                audioSrc.loop = false;
                audioSrc.clip = ARENEMY;
                audioSrc.Play();
                break;
            case "PISTOLENEMY":
                audioSrc.Stop();
                audioSrc.loop = false;
                audioSrc.clip = PISTOLENEMY;
                audioSrc.Play();
                break;
            case "EXPLODE":
                audioSrc.Stop();
                audioSrc.loop = false;
                audioSrc.clip = EXPLODE;
                audioSrc.Play();
                break;
            case "BOSSSHOT":
                audioSrc.Stop();
                audioSrc.loop = false;
                audioSrc.clip = BOSSSHOT;
                audioSrc.Play();
                break;

        }
    }

}
