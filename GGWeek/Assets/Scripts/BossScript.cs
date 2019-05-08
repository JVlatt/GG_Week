using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script;

public class BossScript : MonoBehaviour
{
    public Transform _dest;
    private Animator _myAnim;
    private Rigidbody2D _rb;
    private SpriteRenderer _sp;
    public float _speed = 5.0f;
    private int _hp = 100;
    private bool _isAlive = true;
    float animTimer = 0;

    public float cooldown = 10.0f;
    private float _timer = 0;

    private bool _pair = true;
    public float _knifeDuration = 5.0f;
    public float _timeBetweenKnifes = 1.0f;
    public float _offsetX;
    private float _knifeDurationTimer;
    private float _betweenKnifeTimer;
    public GameObject _knife;


    private GameObject _shield;
    public float _shieldDuration = 5.0f;
    private float _shieldTimer = 0;
    private bool _shieldPhase = true;
    private bool _shootPhase = false;
    private float _shootDuration = 5.0f;
    private float _shootTimer = 0;
    private float _betweenShootTimer = 0;

    bool movingLeft = true;
    private float posX;

    public LifeBarController _lifebar;

    private enum BOSS_STATE {
        INIT,
        FIGHT,
        DEATH
    };
    private enum BOSS_ABILITY
    {
        KNIFE,
        ROLL,
        VACCUM
    };

    private bool _wait = true;

    BOSS_STATE _state = BOSS_STATE.INIT;
    BOSS_ABILITY _ability = BOSS_ABILITY.KNIFE;
    
    private void Awake()
    {
        _myAnim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _sp = GetComponent<SpriteRenderer>();
        _shield = transform.GetChild(0).gameObject;
        _shield.SetActive(false);
    }
    private void Start()
    {
        _betweenKnifeTimer = _timeBetweenKnifes;
        _lifebar._lifeMax = _hp;
    }
    void Update()
    {
        if (_hp <= 0)
        { 
            _isAlive = false;
            _state = BOSS_STATE.DEATH;
        }
        if (_isAlive)
        {
            if(_state == BOSS_STATE.INIT)
                MoveToDest();
            if (_state == BOSS_STATE.FIGHT)
                FightLoop();
        }
        if(_state == BOSS_STATE.DEATH && !_isAlive)
        {
            _myAnim.SetTrigger("Death");
            GameManager.GetManager()._myCamera._target = GameManager.GetManager()._myPlayer.gameObject.transform;
            Debug.Log("Victory");
        }

    }

    private void MoveToDest()
    {
        
        if (transform.position.x >= _dest.position.x)
        {
            transform.Translate(new Vector2(-1 * (_speed * Time.deltaTime), 0));
            _myAnim.SetBool("isMoving", true);
        }
        else
        {
            animTimer += Time.deltaTime;
            _myAnim.SetBool("isMoving", false);
            _myAnim.SetTrigger("Taunt");
            if(animTimer <= 3.0f)
            { 
                _state = BOSS_STATE.FIGHT;
                GameManager.GetManager().FreezePlayer(true);
                posX = transform.position.x;
                _wait = false;
                _timer = 9.0f;
            }
        }
    }
    private void FightLoop()
    {
        Debug.Log("BOSS HP : " + _hp);
        if(cooldown <= _timer)
        {
            switch(_ability)
            {
                case BOSS_ABILITY.KNIFE:
                    _wait = true;

                    if (_knifeDuration >= _knifeDurationTimer)
                    {
                        _knifeDurationTimer += Time.deltaTime;
                        _betweenKnifeTimer += Time.deltaTime;

                        if (_timeBetweenKnifes <= _betweenKnifeTimer)
                        {
                            _betweenKnifeTimer = 0;
                            if (_pair)
                            {
                                _myAnim.SetTrigger("fire");

                                GameObject knife1 = Instantiate(_knife);
                                GameObject knife2 = Instantiate(_knife);

                                knife1.transform.position = new Vector2(transform.position.x - _offsetX, GameManager.GetManager()._line2Center);
                                knife2.transform.position = new Vector2(transform.position.x - _offsetX, GameManager.GetManager()._line4Center);

                                Rigidbody2D rb1 = knife1.GetComponent<Rigidbody2D>();
                                Rigidbody2D rb2 = knife2.GetComponent<Rigidbody2D>();

                                rb1.velocity = new Vector2(-10, 0);
                                rb2.velocity = new Vector2(-10, 0);

                                _pair = false;
                                break;
                            }
                            if (!_pair)
                            {
                                _myAnim.SetTrigger("fire");

                                GameObject knife1 = Instantiate(_knife);
                                GameObject knife2 = Instantiate(_knife);
                                GameObject knife3 = Instantiate(_knife);

                                knife1.transform.position = new Vector2(transform.position.x - _offsetX, GameManager.GetManager()._line1Center);
                                knife2.transform.position = new Vector2(transform.position.x - _offsetX, GameManager.GetManager()._line3Center);
                                knife3.transform.position = new Vector2(transform.position.x - _offsetX, GameManager.GetManager()._line5Center);

                                Rigidbody2D rb1 = knife1.GetComponent<Rigidbody2D>();
                                Rigidbody2D rb2 = knife2.GetComponent<Rigidbody2D>();
                                Rigidbody2D rb3 = knife3.GetComponent<Rigidbody2D>();

                                rb1.velocity = new Vector2(-10, 0);
                                rb2.velocity = new Vector2(-10, 0);
                                rb3.velocity = new Vector2(-10, 0);

                                _pair = true;
                                break;
                            }
                        }
                    }
                    else
                    {
                        _timer = 0;
                        _wait = false;
                        _ability = BOSS_ABILITY.VACCUM;
                        _shieldPhase = true;
                        _shieldTimer = 0;
                        _shootTimer = 0;
                        _shootPhase = false;
                    }
                    break;
                case BOSS_ABILITY.VACCUM:
                    _wait = true;
                    if(_shieldPhase)
                    {
                        _shieldTimer += Time.deltaTime;
                        _shield.SetActive(true);

                        if (_shieldTimer >= _shieldDuration)
                        { 
                            _shield.SetActive(false);
                            _shieldPhase = false;
                            _shootPhase = true;
                        }
                    }
                    if (_shootPhase)
                    {
                        if(_shootTimer <= _shootDuration)
                        {
                            _shootTimer += Time.deltaTime;
                            _betweenShootTimer += Time.deltaTime;
                            if(_betweenShootTimer >= 0.2f)
                            {
                                _myAnim.SetTrigger("fire");
                                _betweenShootTimer = 0;
                                GameObject knife1 = Instantiate(_knife);
                                GameObject knife2 = Instantiate(_knife);
                                GameObject knife3 = Instantiate(_knife);

                                knife1.transform.position = new Vector2(transform.position.x - _offsetX, GameManager.GetManager()._line1Center);
                                knife2.transform.position = new Vector2(transform.position.x - _offsetX, GameManager.GetManager()._line3Center);
                                knife3.transform.position = new Vector2(transform.position.x - _offsetX, GameManager.GetManager()._line5Center);

                                Rigidbody2D rb1 = knife1.GetComponent<Rigidbody2D>();
                                Rigidbody2D rb2 = knife2.GetComponent<Rigidbody2D>();
                                Rigidbody2D rb3 = knife3.GetComponent<Rigidbody2D>();

                                rb1.velocity = (new Vector2 (70, GameManager.GetManager()._line1Center) - new Vector2(transform.position.x - _offsetX, GameManager.GetManager()._line3Center));
                                rb2.velocity = (new Vector2(70, GameManager.GetManager()._line3Center) - new Vector2(transform.position.x - _offsetX, GameManager.GetManager()._line3Center));
                                rb3.velocity = (new Vector2(70, GameManager.GetManager()._line5Center) - new Vector2(transform.position.x - _offsetX, GameManager.GetManager()._line3Center));
                            }
                        }
                        else
                        {
                            _timer = 0;
                            _wait = false;
                            _shootPhase = false;
                            movingLeft = true;
                            _ability = BOSS_ABILITY.ROLL;
                        }

                    }
                    break;
                case BOSS_ABILITY.ROLL:
                    _wait = true;
                    if(movingLeft)
                    {
                        if(transform.position.x >= 70)
                        {
                            _speed = 15;
                            _myAnim.SetBool("isMoving",true);
                            transform.Translate(-1 * (Time.deltaTime * _speed), 0, 0);
                        }
                        else
                        {
                            movingLeft = false;
                            _speed = 7;
                        }
                    }
                    if (!movingLeft)
                    {
                        _sp.flipX = true;
                        if(transform.position.x <= posX)
                        {
                            transform.Translate(1 * (Time.deltaTime * _speed), 0, 0);
                        }
                        else
                        {
                            _sp.flipX = false;
                            _myAnim.SetBool("isMoving", false);
                            _timer = 0;
                            _wait = false;
                            _knifeDurationTimer = 0;
                            _ability = BOSS_ABILITY.KNIFE;
                        }
                    }
                    break;
            }
        }

        if (!_wait)
            _timer += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet") && _isAlive)
        {
            _hp -= 1;
            _lifebar.SetLife(-1);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Player") && _isAlive)
        {
            GameManager.GetManager().HurtPlayer(1);
        }
    }
}
