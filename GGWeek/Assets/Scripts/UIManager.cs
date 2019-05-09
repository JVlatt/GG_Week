using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Script;

public class UIManager : MonoBehaviour
{
    public GameObject health1;
    public GameObject health2;
    public GameObject health3;
    public GameObject _lifeBossDisplay;
    private Slider _slider;

    private void Awake()
    {
        _slider = _lifeBossDisplay.GetComponentInChildren<Slider>();
        GameManager.GetManager()._myUI = GetComponent<UIManager>();
    }

    private void Start()
    {
        health1.SetActive(false);
        health2.SetActive(false);
        health3.SetActive(true);
    }

    public void UpdateHearts(int i)
    {
        switch (i)
        { 
            case 1:
                health1.SetActive(true);
                health2.SetActive(false);
                health3.SetActive(false);
                break;
            case 2:
                health1.SetActive(false);
                health2.SetActive(true);
                health3.SetActive(false);
                break;
            case 3:
                health1.SetActive(false);
                health2.SetActive(false);
                health3.SetActive(true);
                break;
        }
    }
    public void StateLifeBar(bool state)
    {
        _lifeBossDisplay.SetActive(state);
    }
    public void UpdateBossHP(int value)
    {
        _slider.value = value;
    }
}
