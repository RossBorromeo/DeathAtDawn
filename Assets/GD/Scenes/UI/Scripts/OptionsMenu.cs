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
    private GameObject _backButton;
    private void Start()
    {
        loadValues();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(01);
        }
        // //if user currently in mainlevel and presses escape, load main menu and pause game
        // and then when user presses escape again, unpause game and load mainlevel
        // if(SceneManager.GetActiveScene().buildIndex == 0)
        // {
        //     if (Input.GetKeyDown(KeyCode.Escape))
        //     {
        //         SceneManager.LoadScene(3);
        //     }
        // }
    }

    public void OnVolumeSliderChanged(float value)
    {
        _volumeText.text = _volumeSlider.value.ToString("0.0");
    }
    public void onSaveButton()
    {
        float volume = _volumeSlider.value;
        PlayerPrefs.SetFloat("Volume", volume);
        loadValues();
    }

    private void loadValues()
    {
        float volumeSliderValue = PlayerPrefs.GetFloat("Volume");
        _volumeSlider.value = volumeSliderValue;
        AudioListener.volume = volumeSliderValue;
    }
    public void onBackButton()
    {
        SceneManager.LoadScene(01);
        
    }
    
}
