using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script;

public class BossEvent : MonoBehaviour
{
    public GameObject _cameraTarget;
    public Transform _spawnPos;
    public Transform _rangeBoss;
    public GameObject _boss;
    public Transform _destination;
    private bool _hasSpawn = false;
    public UIManager _uiManager;

    private void Start()
    {
        _boss.GetComponent<BossScript>()._dest = _destination;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !_hasSpawn)
        {
            _hasSpawn = true;
            GameManager.GetManager()._myCamera._target = _cameraTarget.transform;
            GameManager.GetManager().killCount = 0;
            GameManager.GetManager().FreezePlayer(false);
            _boss.GetComponent<BossScript>()._dest = _destination;
            _boss.GetComponent<BossScript>()._ui = _uiManager;
            _boss.GetComponent<BossScript>()._range = _rangeBoss;
            Instantiate(_boss, _spawnPos);
            
        }
    }
}
