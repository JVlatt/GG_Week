using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour {

    public AudioMixer _audioMixer;
    
    public void SetMusicVolume(float volume)
    {
        _audioMixer.SetFloat("MusicVol", volume);
    }
    public void SetMasterVolume(float volume)
    {
        _audioMixer.SetFloat("MasterVol", volume);
    }
    public void SetFxVolume(float volume)
    {
        _audioMixer.SetFloat("FxVol", volume);
    }
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
}
