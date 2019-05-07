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
    }
}
