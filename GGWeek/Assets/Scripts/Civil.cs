using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Civil : MonoBehaviour
{
    public Transform pos1;
    public Transform pos2;
    private int dirX = -1;
    private SpriteRenderer _mySprite;
    public float _speed = 5.0f;
    public int _hp = 1;
    private bool _isAlive = true;
    private Animator _myAnim;
    private Collider2D _myCollider;

    void Awake()
    {
        _mySprite = GetComponent<SpriteRenderer>();
        _myAnim = GetComponent<Animator>();
        _myCollider = GetComponent<Collider2D>();
    }
    void Update()
    {
        if(_isAlive)
            Move();
        if (_hp == 0)
            Death();
    }

    void Move()
    {
        if(transform.position.x >= pos2.position.x)
        {
            dirX = -1;
            _mySprite.flipX = false;
        }
        if(transform.position.x <= pos1.position.x)
        {
            dirX = 1;
            _mySprite.flipX = true;
        }
        transform.Translate(dirX*(Time.deltaTime * _speed),0,0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet") && _isAlive)
            _hp -= 1;
    }
    private void Death()
    {
        _isAlive = false;
        _myCollider.enabled = false;
        _myAnim.SetTrigger("Death");
    }
}
