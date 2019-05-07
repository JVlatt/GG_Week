using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script;

public class EnemyScript : MonoBehaviour
{
    public float _cooldown = 2.0f;
    private int _hp = 3;
    private Vector2 _pos;
    [System.NonSerialized]
    public bool isAlive = true;
    [System.NonSerialized]
    public bool _isAware = false;
    public bool _isMoving = false;
    public GameObject _enemyBullet;
    private float _timer;
    private Animator _myAnim;

    private Collider2D _bulletCollider;

    public float _offsetX;
    public float _offsetY;
    private float _bulletSpeed = 10;

    public float _speed = 7.0f;

    void Awake()
    {
        _myAnim = GetComponent<Animator>();
        _bulletCollider = GetComponent<Collider2D>();
        
    }
    private void Start()
    {
        float _randomY = Random.Range(-2.93f, -0.79f);
        float _randomX = Random.Range(2, 8);
        if(_isMoving)
        transform.position = new Vector2(transform.position.x + _randomX, _randomY);
        int _move = Random.Range(5, 10);
        _pos = new Vector2(transform.position.x - _move, transform.position.y);
        
    }

    void Update()
    {
        if (_hp == 0 && isAlive)
            Death();
        if (_isAware && _cooldown < _timer && isAlive && !_isMoving)
            Shoot();
        if (_isMoving && isAlive)
            Move();
        _timer += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("PlayerBullet") && isAlive)
        {
            _hp -= 1;
            _isAware = true;
            _myAnim.SetTrigger("detect");
            Destroy(collision.gameObject);
        }
        
    }

    private void Death()
    {
        isAlive = false;
        _myAnim.SetTrigger("death");
        GameManager.GetManager().killCount += 1;
    }

    private void Shoot()
    {
        _timer = 0;
        _myAnim.SetTrigger("fire");
        GameObject bullet = Instantiate(_enemyBullet);
        bullet.transform.position = new Vector2(transform.position.x - _offsetX, transform.position.y + _offsetY);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(- _bulletSpeed, 0);
    }

    private void Move()
    {
        if(transform.position.x > _pos.x)
        {
            _myAnim.SetTrigger("move");
            transform.Translate(-1 * (Time.deltaTime * _speed), 0, 0);
        }
        else
        {
            _isMoving = false;
            _isAware = true;
            _myAnim.SetTrigger("detect");
        }
    }

}
