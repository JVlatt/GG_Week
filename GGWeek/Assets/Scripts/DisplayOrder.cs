using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayOrder : MonoBehaviour
{
    public Transform _line1;
    public Transform _line2;
    public Transform _line3;

    SpriteRenderer _mySprite;
    private void Awake()
    {
        _mySprite = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (transform.position.y > _line1.position.y)
            _mySprite.sortingOrder = 9;
        if (transform.position.y > _line2.position.y)
            _mySprite.sortingOrder = 7;
        if (transform.position.y > _line3.position.y)
            _mySprite.sortingOrder = 5;
        if(transform.position.y < _line1.position.y)
            _mySprite.sortingOrder = 11;

    }
}
