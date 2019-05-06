using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerController : MonoBehaviour {
  
    public float _speedMoveX;
    public float _speedMoveY;
    public float _speedJump;
    public GameObject bulletPrefab;

    [SerializeField]
    private Vector3 _posCheck = new Vector3();

    private Rigidbody2D _myRg;
    private SpriteRenderer _mySpriteRend;
    private Animator _myAnim;


    private float _timer = 0;
    public float _cooldown = 0.2f;
    private int facingRight = 1;
    private float _bulletSpeed = 10;
    public float _offsetX;
    public float _offsetY;

    private void Awake()
    {
        _myRg = GetComponent<Rigidbody2D>();
        _mySpriteRend = GetComponent<SpriteRenderer>();
        _myAnim = GetComponent<Animator>();
    }

    void Update () {
        Mouvement();
        if(Input.GetButton("Fire1") && _timer > _cooldown)
        {
            Fire();
            _timer = 0;
        }
        _timer += Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        { 
            Debug.Log("Touché");
            Destroy(collision.gameObject);
        }
    }
    private void Fire()
    {
        _myAnim.SetTrigger("Fire");
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.position = new Vector2(transform.position.x + (_offsetX * facingRight), transform.position.y + _offsetY);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(facingRight * _bulletSpeed,0);
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
    /*
    private void Jump()
    {
        //Debug.Log(Input.GetAxis("Jump") + " : " + Input.GetAxisRaw("Jump"));
        //float directionY = 0;

        if (Input.GetButton("Jump") && Input.GetAxis("Jump") < 1 && !_isJumped)
        { 
            _myRg.velocity = new Vector2(0, Input.GetAxis("Jump") * _speedJump);
        }

        if (Input.GetButtonUp("Jump") || Input.GetAxis("Jump") >= 1)
            _isJumped = true;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            _isJumped = false;
        }
    }
    */
}
