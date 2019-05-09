using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script
{
    public class GameManager
    {
        static GameManager _manger = null;

        public static GameManager GetManager()
        {
            if (_manger == null)
            {
                _manger = new GameManager(); // patern singleton
            }
            return _manger;
        }

        public PlayerController _myPlayer;
        public int killCount = 0;
        public UIManager _myUI;
        public CameraController _myCamera;

        public float _line1Top;
        public float _line1Center;
        public float _line1Bottom;
        public float _line2Top;
        public float _line2Center;
        public float _line2Bottom;
        public float _line3Top;
        public float _line3Center;
        public float _line3Bottom;
        public float _TopLimit;
        public float _BottomLimit;

        public void InitPlayer(PlayerController pc)
        {
            _myPlayer = pc;
        }
        public void InitUI(UIManager UI)
        {
            _myUI = UI;
        }
        public void InitCamera(CameraController camera)
        {
            _myCamera = camera;
        }
        public void FreezePlayer(bool state)
        {
            _myPlayer._canDoShit = state;
            _myPlayer.gameObject.GetComponent<Animator>().SetBool("Moving", false);
        }

        public void HurtPlayer(int amount)
        {
            _myPlayer._hp -= amount;
        }
    }
}
