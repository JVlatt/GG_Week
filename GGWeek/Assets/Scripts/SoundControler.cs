using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControler : MonoBehaviour {

    public AudioClip _musicIG;
    public AudioClip _transi1;
    public AudioClip _transi2;
    public AudioClip _click;

    public static SoundControler _soundControler;

    public AudioSource _musicSource;
    public AudioSource _fxSource;

    private void Awake()
    {
        if (_soundControler == null)
            _soundControler = this;
        else
            Destroy(this);
        
        _musicSource.clip = _musicIG;
        _musicSource.loop = true;
        _musicSource.Play();
    }

    public void PlaySound(AudioClip sound)
    {
        _fxSource.PlayOneShot(sound);
    }

}
