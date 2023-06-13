using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //Singleton
    public static SoundManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    public AudioSource audiSource;

    public void PlaySFX(AudioClip clip)
    {
        audiSource.PlayOneShot(clip);
    }

    internal void PlaySFX(object keyUnlockClip)
    {
        throw new NotImplementedException();
    }
}
