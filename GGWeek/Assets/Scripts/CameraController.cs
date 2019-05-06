using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform _target = null;
    public Vector2 _offsetPos = Vector2.zero;
    public bool _followOnX = false;
    public bool _followOnY = false;
    public bool _smoothCam = false;
    public float _speedSmooth = 0.1f;

    [Header("Limite Level")]
    public Transform _limiteMin;
    public Transform _limiteMax;

    private Vector2 _velocity = Vector2.zero;

    void Update()
    {
        Vector2 newPosCam;
        newPosCam.x = _followOnX ? _target.position.x + _offsetPos.x : _offsetPos.x;
        newPosCam.y = _followOnY ? _target.position.y + _offsetPos.y : _offsetPos.y;

        if (newPosCam.x > _limiteMin.position.x & newPosCam.x < _limiteMax.position.x || newPosCam.y > _limiteMin.position.y & newPosCam.y < _limiteMax.position.y)
        {
            if (_smoothCam)
                newPosCam = Vector2.SmoothDamp(transform.position, newPosCam, ref _velocity, _speedSmooth);

            transform.position = new Vector3(newPosCam.x, newPosCam.y, transform.position.z);
        }
        else
        {
            if (_followOnX)
            {
                if (Vector2.Distance(transform.position, _limiteMin.position) < 2F)
                {
                    if (Vector2.Distance(transform.position, _limiteMin.position) < 0.5F)
                        transform.position = new Vector3(_limiteMin.position.x, _limiteMin.position.y, transform.position.z);
                    else
                    {
                        newPosCam.x = Mathf.SmoothDamp(transform.position.x, _limiteMin.position.x, ref _velocity.x, _speedSmooth);
                        transform.position = new Vector3(newPosCam.x, newPosCam.y, transform.position.z);
                    }
                }

                if (Vector2.Distance(transform.position, _limiteMax.position) < 2F)
                {
                    if (Vector2.Distance(transform.position, _limiteMax.position) < 0.5F)
                        transform.position = new Vector3(_limiteMax.position.x, _limiteMax.position.y, transform.position.z);
                    else
                    {
                        newPosCam.x = Mathf.SmoothDamp(transform.position.x, _limiteMax.position.x, ref _velocity.x, _speedSmooth);
                        transform.position = new Vector3(newPosCam.x, newPosCam.y, transform.position.z);
                    }
                }
            }

            if (_followOnY)
            {
                if (Vector2.Distance(transform.position, _limiteMin.position) < 2F)
                {
                    if (Vector2.Distance(transform.position, _limiteMin.position) < 0.5F)
                        transform.position = new Vector3(_limiteMin.position.x, _limiteMin.position.y, transform.position.z);
                    else
                    {
                        newPosCam.y = Mathf.SmoothDamp(transform.position.y, _limiteMin.position.y, ref _velocity.y, _speedSmooth);
                        transform.position = new Vector3(newPosCam.x, newPosCam.y, transform.position.z);
                    }
                }

                if (Vector2.Distance(transform.position, _limiteMax.position) < 2F)
                {
                    if (Vector2.Distance(transform.position, _limiteMax.position) < 0.5F)
                        transform.position = new Vector3(_limiteMax.position.x, _limiteMax.position.y, transform.position.z);
                    else
                    {
                        newPosCam.y = Mathf.SmoothDamp(transform.position.y, _limiteMax.position.y, ref _velocity.y, _speedSmooth);
                        transform.position = new Vector3(newPosCam.x, newPosCam.y, transform.position.z);
                    }
                }
            }
        }
    }
}
