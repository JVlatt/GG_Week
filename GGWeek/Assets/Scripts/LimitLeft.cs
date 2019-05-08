using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitLeft : MonoBehaviour
{
    private Transform _camPosition;
    public Vector2 _offset;
    public Vector2 _lastPosition;
    void Start()
    {
        _camPosition = Camera.main.transform;
        _lastPosition = _camPosition.position;
    }

    void Update()
    {
        if(_lastPosition.x < _camPosition.position.x)
        { 
            transform.position = new Vector2(_camPosition.position.x, _camPosition.position.y);
            _lastPosition = transform.position;
        }
    }
}
