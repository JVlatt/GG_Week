using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public GameObject _myAudio;
    private Animator _myAnimator;
    public GameObject _logo;

    private void Start()
    {
        _myAnimator = gameObject.GetComponent<Animator>();
    }


    public void PlayButton()
    {
        DontDestroyOnLoad(_myAudio);
        SoundControler._soundControler.PlaySound(SoundControler._soundControler._click);
        StartCoroutine("Wait");
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void OptionButton()
    {
        _logo.SetActive(false);
        _myAnimator.SetTrigger("option");
        SoundControler._soundControler.PlaySound(SoundControler._soundControler._transi1);
    }

    public void Backbutton()
    {
        _logo.SetActive(true);
        _myAnimator.SetTrigger("main");
        SoundControler._soundControler.PlaySound(SoundControler._soundControler._transi2);
    }
    IEnumerator Wait()
    {      
        yield return new WaitForSecondsRealtime(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
