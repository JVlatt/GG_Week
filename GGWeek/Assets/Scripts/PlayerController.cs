using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using Assets.Script;


public class PlayerController : MonoBehaviour
{

    public float _speedMoveX;
    public float _speedMoveY;
    public float _speedJump;
    public GameObject bulletPrefab;

    private bool _isDashing = false;
    private float _dashLengthTimer;
    public float _dashLength = 0.5f;
    public int _hp = 3;
    private Collider2D _myCollider;

    [SerializeField]
    private Vector3 _posCheck = new Vector3();

    [SerializeField]
    private GameObject UIManager;

    private UIManager _myUI;

    private Rigidbody2D _myRg;
    private SpriteRenderer _mySpriteRend;
    private Animator _myAnim;

    public float _maxCooldown = 3.0f;
    public float _minCooldown = 0.1f;
    private float _timerBetweenShots = 0;
    public float _cooldownBetweenShots;
    public float _cooldownDecreaseSpeed = 0.2f;
    private float _timerDecreaseSpeed = 0;
    public float inputDecreaseValue = 0.1f;
    public float periodicDecreaseValue = 0.2f;

    private float _dashTimer;
    public float _dashCooldown = 2.0f;
    private int facingRight = 1;
    private float _bulletSpeed = 10;
    public float _offsetX;
    public float _offsetY;

    private bool isInvicible = false;
    private float invicibleTimer = 0;

    [System.NonSerialized]
    public bool _canDoShit = true;

    private void Awake()
    {
        _myRg = GetComponent<Rigidbody2D>();
        _mySpriteRend = GetComponent<SpriteRenderer>();
        _myAnim = GetComponent<Animator>();
        _myUI = UIManager.GetComponent<UIManager>();
        _myCollider = GetComponent<Collider2D>();
        GameManager.GetManager()._myPlayer = GetComponent<PlayerController>();
        _dashTimer = _dashCooldown;
        _cooldownBetweenShots = _maxCooldown;
    }

    void Update()
    {
        if (_canDoShit)
        {
            if (!_isDashing)
            {
                _myCollider.enabled = true;
                Mouvement();
            }


            if (_hp == 0)
            {
                Death();
            }
            if (!_isDashing && Input.GetButton("Jump") && _dashTimer >= _dashCooldown)
            {
                Dash();
                _dashLengthTimer = 0;
                _dashTimer = 0;
            }
            if (_dashLengthTimer >= _dashLength && _isDashing)
            {
                _myRg.velocity = Vector2.zero;
                _isDashing = false;
                Debug.Log("test2");
            }
            if (_isDashing)
            {
                _myCollider.enabled = false;
            }

            if (isInvicible)
            {
                _myCollider.enabled = false;
                invicibleTimer += Time.deltaTime;
            }
            if (invicibleTimer >= 3.0)
            {
                isInvicible = false;
                _myCollider.enabled = true;
            }
            if (_timerBetweenShots >= _cooldownBetweenShots)
            {
                Fire();
                _timerBetweenShots = 0;
            }
            if (_timerDecreaseSpeed >= _cooldownDecreaseSpeed)
            {
                _cooldownBetweenShots += periodicDecreaseValue;
                _cooldownBetweenShots = Mathf.Clamp(_cooldownBetweenShots, _minCooldown, _maxCooldown);
                _timerDecreaseSpeed = 0;
            }
            if (Input.GetButtonDown("Fire1"))
            {
                _cooldownBetweenShots -= inputDecreaseValue;
                _cooldownBetweenShots = Mathf.Clamp(_cooldownBetweenShots, _minCooldown, _maxCooldown);

            }
            if (Input.GetButtonDown("Fire2"))
            {
                _cooldownBetweenShots -= inputDecreaseValue;
                _cooldownBetweenShots = Mathf.Clamp(_cooldownBetweenShots, _minCooldown, _maxCooldown);

            }
            if (Input.GetButtonDown("Fire3"))
            {
                _cooldownBetweenShots -= inputDecreaseValue;
                _cooldownBetweenShots = Mathf.Clamp(_cooldownBetweenShots, _minCooldown, _maxCooldown);

            }
            _timerBetweenShots += Time.deltaTime;
            _timerDecreaseSpeed += Time.deltaTime;
        }
        Debug.Log(_cooldownBetweenShots);
        _dashLengthTimer += Time.deltaTime;
        _dashTimer += Time.deltaTime;
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet") && _hp > 0)
        {
            _hp -= 1;
            _myUI.UpdateHearts(_hp);
            Destroy(collision.gameObject);
            isInvicible = true;
            invicibleTimer = 0;
        }
    }
    private void Fire()
    {
        _myAnim.SetTrigger("Fire");
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.position = new Vector2(transform.position.x + (_offsetX * facingRight), transform.position.y + _offsetY);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(facingRight * _bulletSpeed, 0);
    }
    private void Mouvement()
    {
        float directionX = 0;
        float directionY = 0;

        //Debug.Log(Input.GetAxis("Horizontal") + " : " + Input.GetAxisRaw("Horizontal"));

        if (Input.GetButton("Horizontal"))
        {
            directionX = Input.GetAxis("Horizontal") * Time.deltaTime * _speedMoveX;
        }
        if (Input.GetButton("Vertical"))
        {
            directionY = Input.GetAxis("Vertical") * Time.deltaTime * _speedMoveY;
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            _mySpriteRend.flipX = true;
            facingRight = -1;
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            _mySpriteRend.flipX = false;
            facingRight = 1;
        }

        if (Math.Abs(directionX) >= 0.01 || Math.Abs(directionY) >= 0.01)
        {
            _myAnim.SetBool("Moving", true);
        }
        else
        {
            _myAnim.SetBool("Moving", false);
        }

        transform.Translate(directionX, directionY, 0);
    }
    private void Death()
    {
        Debug.Log("Defeat");
        _myAnim.SetTrigger("Death");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void Dash()
    {
        _isDashing = true;
        Vector2 dir = new Vector2(Input.GetAxis("Horizontal") * Time.deltaTime * _speedMoveX, Input.GetAxis("Vertical") * Time.deltaTime * _speedMoveX);
        dir.Normalize();
        _myRg.velocity = dir * _speedJump;
        Debug.Log("test");

    }
}
