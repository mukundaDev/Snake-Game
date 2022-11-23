using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager1 : MonoBehaviour
{
    private static AudioManager1 instance;
    public static AudioManager1 Instance { get { return instance; } }

    public AudioSource soundEffect;
    [SerializeField]

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void PlaySound(Sounds sound)
    {
        AudioClip clip = getSoundClip(sound);
        if(clip != null)
        {
            soundEffect.PlayOneShot(clip);
        }
    }
    private AudioClip getSoundClip(Sounds sound)
    {
        SoundType returnSound = Array.Find(Sounds, item => item.soundType == sound);
        if (returnSound != null)
            return returnSound.soundClip;
        return null;
    }
}

[Serializable]
public class SoundType
{
    public Sounds soundType;
    public AudioClip soundClip;
}

public enum Sounds { buttonClick, backGroundMusic, foodSound, gameOverSound, }
