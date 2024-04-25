using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowSounds : MonoBehaviour
{
    public AudioClip[] bowSounds;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void playLoadSound()
    {
        audioSource.Stop();
        audioSource.clip = bowSounds[0];
        audioSource.Play();
    }

    public void playReleaseSound()
    {
        audioSource.Stop();
        audioSource.clip = bowSounds[1];
        audioSource.Play();
    }
}
