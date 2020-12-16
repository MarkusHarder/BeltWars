using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    private bool musicEnabled = true;
    public AudioSource audioSource;
    public AudioClip music;

    public void ToggleMusic(bool toggle)
    {
        if (toggle && !this.musicEnabled)  // Music was disabled and now got enabled by the user
        {
            Debug.Log("Enable Music");
            this.musicEnabled = true;
            this.audioSource.Play();
        }
        else if (this.musicEnabled && !toggle)  // Music was enabled and now got disabled by the user
        {
            Debug.Log("Disable Music");
            this.musicEnabled = false;
            this.audioSource.Pause();
        }
    }

    public void SetMusicVolume(float volume)
    {
        this.audioSource.volume = volume;
    }
}
