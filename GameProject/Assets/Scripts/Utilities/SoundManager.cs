using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static AudioClip ALARM, ARENEMY, PISTOLENEMY, EXPLODE, BOSSSHOT, CHARGEUP, ELEVATORMOVE, ELEVATOROPEN, MELEESTAB, MENUHOVER, MENUSELECT, PICKUP, ROBOTAIM, ROBOTMOVE, SWITCH, TELEPORT, WHOOSH, SWORD;
    static AudioSource audioSrc;
    public AudioMixer audioMixer;


    void Start()
    {
        ALARM = Resources.Load<AudioClip>("ALARM");
        ARENEMY = Resources.Load<AudioClip>("ARENEMY");
        PISTOLENEMY = Resources.Load<AudioClip>("PISTOLENEMY");
        EXPLODE = Resources.Load<AudioClip>("EXPLODE");
        TELEPORT = Resources.Load<AudioClip>("TELEPORT");
        BOSSSHOT = Resources.Load<AudioClip>("BOSSSHOT");
        CHARGEUP = Resources.Load<AudioClip>("CHARGEUP");
        ELEVATORMOVE = Resources.Load<AudioClip>("ELEVATORMOVE");
        ELEVATOROPEN = Resources.Load<AudioClip>("ELEVATOROPEN");
        MELEESTAB = Resources.Load<AudioClip>("MELEESTAB");
        MENUHOVER = Resources.Load<AudioClip>("MENUHOVER");
        MENUSELECT = Resources.Load<AudioClip>("MENUSELECT");
        PICKUP = Resources.Load<AudioClip>("PICKUP");
        ROBOTAIM = Resources.Load<AudioClip>("ROBOTAIM");
        ROBOTMOVE = Resources.Load<AudioClip>("ROBOTMOVE");
        SWITCH = Resources.Load<AudioClip>("SWITCH");
        WHOOSH = Resources.Load<AudioClip>("WHOOSH");
        SWORD = Resources.Load<AudioClip>("SWORD");

        audioSrc = GetComponent<AudioSource>();
    }

    public void SetVolume(AudioMixerGroup targetGroup, float value)
    {
        audioMixer.SetFloat(targetGroup.name, Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat(targetGroup.name, value);
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
            case "TELEPORT":
                audioSrc.Stop();
                audioSrc.loop = false;
                audioSrc.clip = TELEPORT;
                audioSrc.Play();
                break;
            case "BOSSSHOT":
                audioSrc.Stop();
                audioSrc.loop = false;
                audioSrc.clip = BOSSSHOT;
                audioSrc.Play();
                break;
            case "CHARGEUP":
                audioSrc.Stop();
                audioSrc.loop = false;
                audioSrc.clip = CHARGEUP;
                audioSrc.Play();
                break;
            case "ELEVATORMOVE":
                audioSrc.Stop();
                audioSrc.loop = false;
                audioSrc.clip = ELEVATORMOVE;
                audioSrc.Play();
                break;
            case "ELEVATOROPEN":
                audioSrc.Stop();
                audioSrc.loop = false;
                audioSrc.clip = ELEVATOROPEN;
                audioSrc.Play();
                break;
            case "MELEESTAB":
                audioSrc.Stop();
                audioSrc.loop = false;
                audioSrc.clip = MELEESTAB;
                audioSrc.Play();
                break;
            case "MENUHOVER":
                audioSrc.Stop();
                audioSrc.loop = false;
                audioSrc.clip = MENUHOVER;
                audioSrc.Play();
                break;
            case "MENUSELECT":
                audioSrc.Stop();
                audioSrc.loop = false;
                audioSrc.clip = MENUSELECT;
                audioSrc.Play();
                break;
            case "PICKUP":
                audioSrc.Stop();
                audioSrc.loop = false;
                audioSrc.clip = PICKUP;
                audioSrc.Play();
                break;
            case "ROBOTAIM":
                audioSrc.Stop();
                audioSrc.loop = false;
                audioSrc.clip = ROBOTAIM;
                audioSrc.Play();
                break;
            case "ROBOTMOVE":
                audioSrc.Stop();
                audioSrc.loop = false;
                audioSrc.clip = ROBOTMOVE;
                audioSrc.Play();
                break;
            case "SWITCH":
                audioSrc.Stop();
                audioSrc.loop = false;
                audioSrc.clip = SWITCH;
                audioSrc.Play();
                break;
            case "WHOOSH":
                audioSrc.Stop();
                audioSrc.loop = false;
                audioSrc.clip = WHOOSH;
                audioSrc.Play();
                break;
            case "SWORD":
                audioSrc.Stop();
                audioSrc.loop = false;
                audioSrc.clip = SWORD;
                audioSrc.Play();
                break;

        }
    }

}
