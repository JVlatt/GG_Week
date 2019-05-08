using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBarController : MonoBehaviour
{
    public Image _myImage;
    public int _currentLife;
    public int _lifeMax;

    private void Start()
    {
        if (_currentLife >= _lifeMax)
            _currentLife = _lifeMax;

        RefreshLifeBar();
    }

    /// <summary>
    /// Add or remove life.
    /// </summary>
    /// <param name="value">(+/-)value</param>
    public void SetLife(int value)
    {
        if (_currentLife <= _lifeMax)
            _currentLife += value;

        if (_currentLife >= _lifeMax)
            _currentLife = _lifeMax;

        RefreshLifeBar();
    }

    private void RefreshLifeBar()
    {
        if (_myImage != null)
            _myImage.fillAmount = _currentLife / _lifeMax;
    }
}
