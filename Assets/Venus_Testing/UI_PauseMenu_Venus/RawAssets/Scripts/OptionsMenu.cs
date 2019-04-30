using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public event Action GoBack;

    private Canvas OwnCanvas;

    // Start is called before the first frame update
    void Start()
    {
        OwnCanvas = GetComponent<Canvas>();
        OwnCanvas.enabled = false;
        FindObjectOfType<PauseMenu>().ToggleOptions += ShowOptions;
        //FindObjectOfType<PauseMenu>().ToggleOptions +=
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowOptions(string ToggleStr)
    {
        switch (ToggleStr)
        {
            case "On":
                OwnCanvas.enabled = true;
                break;
            case "Off":
                OwnCanvas.enabled = false;
                break;
        }
    }

    public void Back()
    {
        OwnCanvas.enabled = false;
        if (GoBack != null)
            GoBack();
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("MixerVolume", volume);
    }
}
