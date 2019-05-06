using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
    EnemyScript _enemy;
    Animator _enemyAnim;

    private void Awake()
    {
        _enemyAnim = GetComponentInParent<Animator>();
        _enemy = GetComponentInParent<EnemyScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && _enemy.isAlive)
        {
            _enemy._isAware = true;
            _enemyAnim.SetTrigger("detect");
            transform.gameObject.SetActive(false);
        }
    }
}
