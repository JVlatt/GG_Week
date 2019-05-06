using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float _cooldown = 2.0f;
    private int _hp = 3;

    [System.NonSerialized]
    public bool isAlive = true;

    public bool _isAware = false;
    public GameObject _enemyBullet;
    private float _timer;
    private Animator _myAnim;

    private Collider2D _bulletCollider;
    private Collider2D _enemyCollider;
    private Collider2D _detectionCollider;

    public float _offsetX;
    public float _offsetY;
    private float _bulletSpeed = 10;


    void Awake()
    {
        _myAnim = GetComponent<Animator>();
        _bulletCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (_hp == 0)
            Death();
        if (_isAware && _cooldown < _timer && isAlive)
            Shoot();

        _timer += Time.deltaTime;
        Debug.Log("HP : " + _hp);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("PlayerBullet") && isAlive)
        {
            _hp -= 1;
            Destroy(collision.gameObject);
        }
        
    }

    private void Death()
    {
        isAlive = false;
        _myAnim.SetTrigger("death");
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

}
