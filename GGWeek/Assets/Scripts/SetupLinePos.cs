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

        GameManager.GetManager()._TopLimit = _TopLimit.position.y;
        GameManager.GetManager()._BottomLimit = _BottomLimit.position.y;
    }
}
