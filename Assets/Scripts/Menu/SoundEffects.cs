using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip soundEffect;
    public bool effectsEnabled = true;

    public void ToggleEffects(bool toggle)
    {
        if (this.effectsEnabled && !toggle)  // Sound effects were enabled and now got disabled by the user
        {
            this.effectsEnabled = false;
        }
        else if (!this.effectsEnabled && toggle)  // Sound effects were disabled and now got enabled by the user
        {
            this.effectsEnabled = true;
        }
    }

    public void PlayClickSound()
    {
        if (this.effectsEnabled)
        {
            audioSource.PlayOneShot(soundEffect);
        }
    }

    public void SetEffectsVolume(float volume)
    {
        this.audioSource.volume = volume;
    }
}
