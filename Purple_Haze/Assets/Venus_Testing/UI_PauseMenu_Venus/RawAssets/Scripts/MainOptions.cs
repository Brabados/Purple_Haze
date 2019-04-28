using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;

public class MainOptions : MonoBehaviour
{
    public static AudioMixer audioMixer;
    public event Action GoBack;
    public event Action<bool> Subs;

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

    public void FullScreenToggle(bool IsFullScr)
    {
        Screen.fullScreen = IsFullScr;
    }

    public void SubtitlesToggle(bool SubsOn)
    {
        Subs(SubsOn);
    }
}
