using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigLoader : MonoBehaviour
{
    public Slider musicSlider;
    public Slider effectsSlider;
    public Toggle musicToggle;
    public Toggle effectsToggle;

    // Start is called before the first frame update
    void Start()
    {
        this.setMusicSliderValue();
        this.setEffectsSliderValue();
        this.setMusicToggle();
        this.setEffectsToggle();
    }

    public void setMusicSliderValue()
    {
        musicSlider.value = GlobalVariables.musicVol;
    }

    public void setEffectsSliderValue()
    {
        effectsSlider.value = GlobalVariables.effectsVol;
    }

    public void setMusicToggle()
    {
        musicToggle.isOn = GlobalVariables.musicEnabled;
    }

    public void setEffectsToggle()
    {
        effectsToggle.isOn = GlobalVariables.effectsEnabled;
    }
}
