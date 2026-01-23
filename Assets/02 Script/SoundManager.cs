using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] List<AudioClip> Sounds;
    [SerializeField] private AudioSource As;

    private void Awake()
    {
        As=GetComponent<AudioSource>();
       
    }
    public void UsePlaySound(int PlayNum)
    {
        PlaySound(PlayNum);
    }
    public void UseStopSound()
    {
        StopSound();
    }
    private void PlaySound(int PlayNum)
    {
        float volume = 1f;

        if(PlayNum == 2)
        {
            volume = 0.05f;
        }
        As.PlayOneShot(Sounds[PlayNum],volume);
        if(PlayNum == 10)
        {
            volume = 0.01f;
        }
    }
    private void StopSound()
    {
        As.Stop();
    }
}
