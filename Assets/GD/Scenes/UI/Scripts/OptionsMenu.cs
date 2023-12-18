using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    
    [SerializeField]
    private Slider _volumeSlider;
    [SerializeField]
    public Text _volumeText;
    
    private void Start()
    {
        LoadValues();
    }
    
    public void OnVolumeSliderChanged(float value)
    {
        _volumeText.text = _volumeSlider.value.ToString("0.0");
    }
    public void OnSaveButton()
    {
        float volume = _volumeSlider.value;
        PlayerPrefs.SetFloat("Volume", volume);
        LoadValues();
    }

    private void LoadValues()
    {
        float volumeSliderValue = PlayerPrefs.GetFloat("Volume");
        _volumeSlider.value = volumeSliderValue;
        AudioListener.volume = volumeSliderValue;
    }
    
    
}
