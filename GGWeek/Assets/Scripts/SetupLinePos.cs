using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script;

public class SetupLinePos : MonoBehaviour
{
    public Transform _line1Top;
    public Transform _line1Bottom;
    public Transform _line2Top;
    public Transform _line2Bottom;
    public Transform _line3Top;
    public Transform _line3Bottom;
    public Transform _line4Top;
    public Transform _line4Bottom;
    public Transform _line5Top;
    public Transform _line5Bottom;

    public Transform _TopLimit;
    public Transform _BottomLimit;

    private void Start()
    {
        GameManager.GetManager()._line1Center = (_line1Top.position.y + _line1Bottom.position.y) /2;
        GameManager.GetManager()._line1Top = _line1Bottom.position.y;
        GameManager.GetManager()._line1Bottom = _line1Bottom.position.y;

        GameManager.GetManager()._line2Top = _line2Top.position.y;
        GameManager.GetManager()._line2Center = (_line2Top.position.y + _line2Bottom.position.y) / 2;
        GameManager.GetManager()._line2Bottom = _line2Bottom.position.y;

        GameManager.GetManager()._line3Top = _line3Top.position.y;
        GameManager.GetManager()._line3Center = (_line3Top.position.y + _line3Bottom.position.y) / 2;
        GameManager.GetManager()._line3Bottom = _line3Bottom.position.y;

        GameManager.GetManager()._line4Top = _line4Top.position.y;
        GameManager.GetManager()._line4Center = (_line4Top.position.y + _line4Bottom.position.y) / 2;
        GameManager.GetManager()._line4Bottom = _line4Bottom.position.y;

        GameManager.GetManager()._line5Top = _line5Top.position.y;
        GameManager.GetManager()._line5Center = (_line5Top.position.y + _line5Bottom.position.y) / 2;
        GameManager.GetManager()._line5Bottom = _line5Bottom.position.y;

        GameManager.GetManager()._TopLimit = _TopLimit.position.y;
        GameManager.GetManager()._BottomLimit = _BottomLimit.position.y;
    }
}
