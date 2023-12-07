using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class MenuManager : MonoBehaviour
{
    public GameObject playButton;
    public GameObject ControlsMenu;
    public AudioSource soundSource;
    public AudioClip soundClip;


    public void Start()
    {
        playButton.SetActive(true);
        ControlsMenu.SetActive(true);
    }

    public void OnPlayButton()
    {
        PlaySound();

        SceneManager.LoadScene(0);
    }

    private void PlaySound()
    {
        soundSource.clip = soundClip;
        soundSource.Play(); //play sound
    }

    public void OnControlsNutton()
    {
        PlaySound();
        soundSource.Play(); //play sound
        SceneManager.LoadScene("Controls");
    }

    public void OnQuitButton()
    {
        PlaySound();
        playButton.SetActive(false);
        ControlsMenu.SetActive(false);
        Application.Quit();
    }
}
