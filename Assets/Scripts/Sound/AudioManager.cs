using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;
    public static AudioManager instance = null;

    // properties that need to be set from the main menu or the escape menu
    private static float effectsVolume = 1;
    private static float musicVolume = 1;
    private static bool musicEnabled = true;
    private static bool effectsEnabled = true;
    private static bool skipMusicToggle = false;
    private static bool skipEffectsToggle = false;

    private Sound currentMusic;

    // Start is called before the first frame update
    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            // we have set a music volume for each invidual sound in the audio manager. because we can set
            // the volume in the main and escape menu also, we need to multiply the volume with the overall volume
            // that was set in one of those menues
            if (s.isMusic)
            {
                s.source.volume = s.volume * musicVolume;
            } 
            else
            {
                s.source.volume = s.volume * effectsVolume;
            }
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

        }
        // check in which scene we are right now
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            getGlobalVariables();
            Play("menu_music");
        }
        else if (SceneManager.GetActiveScene().name == "MainScene")
        {
            // get settings from global variables (maybe the user has changed music volume in menu, etc.)
            getGlobalVariables();
            Play("ingame_music");
        }
    }

    public void PlayClickSound()
    {
        this.Play("click_sound");
    }

    public void PlayMenuMusic()
    {
        this.Play("menu_music");
    }

    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        // set volume
        if (s.isMusic)
        {
            s.source.volume = s.volume * musicVolume;
            currentMusic = s; 
        }
        else
        {
            s.source.volume = s.volume * effectsVolume;
        }

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "not found!");
            return;
        }
        if(!s.source.isPlaying)
        {
            // check whether it is allowed to play the music or the effect
            if (s.isMusic && musicEnabled)
            {
                s.source.Play();
            }
            else if (s.isEffect && effectsEnabled)
            {
                s.source.Play();
            }
        }
    }

    public void Stop (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "not found!");
            return;
        }
            s.source.Stop();
    }

    public void toggleMusic()
    {
        if (skipMusicToggle)
        {
            skipMusicToggle = false;
            return;
        }

        if (!musicEnabled)  // Music was disabled and now got enabled by the user
        {
            Debug.Log("Enable Music");
            musicEnabled = true;
            if (SceneManager.GetActiveScene().name == "Menu")
            {
                Play("menu_music");
            }
            else if (SceneManager.GetActiveScene().name == "MainScene")
            {
                Play("ingame_music");
            }
        }
        else if (musicEnabled)  // Music was enabled and now got disabled by the user
        {
            Debug.Log("Disable Music");
            musicEnabled = false;
            if (SceneManager.GetActiveScene().name == "Menu")
            {
                Stop("menu_music");
            }
            else if (SceneManager.GetActiveScene().name == "MainScene")
            {
                Stop("ingame_music");
            }

        }
        updateGlobalVariables();
    }

    public void toggleEffects()
    {
        if (skipEffectsToggle)
        {
            skipEffectsToggle = false;
            return;
        }

        if (effectsEnabled)  // Sound effects were enabled and now got disabled by the user
        {
            Debug.Log("Disable Effect Sounds");
            effectsEnabled = false;
        }
        else if (!effectsEnabled)  // Sound effects were disabled and now got enabled by the user
        {
            Debug.Log("Enable Effect Sounds");
            effectsEnabled = true;
        }
        updateGlobalVariables();
    }

    public void setMusicVol(Slider slider)
    {
        musicVolume = slider.value;
        currentMusic.source.volume = musicVolume;
        Debug.Log(slider.value);
        updateGlobalVariables();
    }

    public void setEffectsVol(Slider slider)
    {
        effectsVolume = slider.value;
        updateGlobalVariables();
    }

    public static void updateGlobalVariables()
    {
        GlobalVariables.musicEnabled = musicEnabled;
        GlobalVariables.effectsEnabled = effectsEnabled;
        GlobalVariables.musicVol = musicVolume;
        GlobalVariables.effectsVol = effectsVolume;
    }

    public static void getGlobalVariables()
    {
        if (GlobalVariables.musicEnabled == false)
            skipMusicToggle = true;
        if (GlobalVariables.effectsEnabled == false)
            skipEffectsToggle = true;

        musicEnabled = GlobalVariables.musicEnabled;
        effectsEnabled = GlobalVariables.effectsEnabled;
        musicVolume = GlobalVariables.musicVol;
        effectsVolume = GlobalVariables.effectsVol;
    }
}
