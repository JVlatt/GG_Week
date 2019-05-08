using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script;

public class EventWave : MonoBehaviour
{
    public GameObject _pos;
    public int _nbMob;
    public int _nbWaves;
    private int _waveCount;
    private int _mobCount;
    public GameObject _enemyPrefab;
    public Transform _spawnPos;
    private float _timer = 0;
    public float _spawnSpeed = 2.0f;

    private enum EVENT_STATE
    {
        INIT,
        WAVE,
        WAIT,
        END
    };

    EVENT_STATE _myState = EVENT_STATE.INIT;
    private bool _isLaunched = false;

    private void Update()
    {  
        if (_isLaunched)
            EventLoop();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            _isLaunched = true;
        }
    }

    private void EventLoop()
    {
        Debug.Log(GameManager.GetManager().killCount);
        switch(_myState)
        {
            case EVENT_STATE.INIT:
                GameManager.GetManager()._myCamera._target = _pos.transform;
                GameManager.GetManager().killCount = 0;
                _myState = EVENT_STATE.WAVE;
                break;
            case EVENT_STATE.WAVE:
                _timer += Time.deltaTime;
                if(_mobCount<_nbMob && _timer >= _spawnSpeed)
                {
                    Instantiate(_enemyPrefab, _spawnPos);
                    _timer = 0;
                    _mobCount += 1;
                }
                if(GameManager.GetManager().killCount >= _nbMob)
                {
                    _myState = EVENT_STATE.WAIT;
                }
                break;
            case EVENT_STATE.WAIT:

                if(_waveCount<_nbWaves )
                {
                    _waveCount += 1;
                    _mobCount = 0;
                    GameManager.GetManager().killCount = 0;
                    _myState = EVENT_STATE.WAVE;

                }
                else
                {
                    _myState = EVENT_STATE.END;
                }
                break;
            case EVENT_STATE.END:
                    GameManager.GetManager()._myCamera._target = GameManager.GetManager()._myPlayer.transform;
                    GameManager.GetManager()._myCamera._smoothCam = true;
                break;
        }
    }
}
