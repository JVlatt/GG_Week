using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script;

public class UIManager : MonoBehaviour
{
    public GameObject health1;
    public GameObject health2;
    public GameObject health3;

    private void Awake()
    {
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
}
