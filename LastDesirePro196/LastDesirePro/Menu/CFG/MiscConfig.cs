using LastDesirePro.Attributes;
using System.Collections.Generic;
using UnityEngine;
 

namespace LastDesirePro.Menu.CFG
{
    internal class MiscConfig
    {
        [Save] public static bool _recoil;
        [Save] public static float _recoilFloat;

        [Save] public static bool _spread;
        [Save] public static float _spreadFloat;

        [Save] public static bool _fastBullet;
        [Save] public static bool _automatic;
        [Save] public static bool _sway;
        [Save] public static bool _canAttack;
        [Save] public static bool _eoka;
        [Save] public static bool _bow;
        [Save] public static bool _fakeShoot;
        [Save] public static bool _alwayFakeShoot;
        [Save] public static KeyCode _fakeShootKey;

        [Save] public static bool _silentShoot;
        [Save] public static KeyCode _silentShootKey;


        [Save] public static bool _shotTheArmor;
        [Save] public static bool _sprintAim;



        [Save] public static bool _thickness;
        [Save] public static float _thicknessFloat;
        [Save] public static bool _thicknessMelee;
        [Save] public static float _thicknessMeleeFloat;

        [Save] public static bool _meleeX2;
        [Save] public static bool _meleeFarmOnlyBonus;
         


        [Save] public static bool _removeLayer;
        [Save] public static bool _removeLayerAl;
        [Save] public static KeyCode _layerKey;

        [Save] public static bool[] _layer = new bool[]{
            false, //                                             0
            false, //                                             1
            false, //                                             2
            false, //                                             3
            false, //                                             4
            false, //                                             5
            false, //                                             6
            false, //                                             7
            false, //                                             8
            false, //                                             9
            false, //                                             10
            false //                                              11
        };
        [Save] public static bool _shotLayer;
        [Save] public static bool _yrs;
        [Save]
        public static bool[] _layerShot = new bool[]{
            false, //                                             0    
            false, //                                             1   
            false, //                                             2  
            false, //                                             3
            false, //                                             4
            false, //                                             5
            false, //                                             6
            false, //                                             7
            false, //                                             8
            false, //                                             9
            false, //                                             10
            false //                                              11
        };


        [Save] public static bool _x6Zoom;
        [Save] public static KeyCode _x6ZoomKey;
        [Save] public static bool _speedGun;

        [Save] public static bool _noBobbing; 

        [Save] public static bool _debugCam;
        [Save] public static KeyCode _debugCamKey;


        [Save] public static bool _collisionWater;
        [Save] public static bool _noCollisionTree;
        [Save] public static bool _noCollisionAI;


        [Save] public static bool _fly;
        [Save] public static bool _flyNoCollision;
        [Save] public static KeyCode _debugFlyKey;


        [Save] public static bool _speed;
        [Save] public static float _srint = 5.5f;
        [Save] public static float _move = 2.8f;
        [Save] public static float _siting = 1.7f;


        [Save] public static bool _spider;
        [Save] public static bool _time;
        [Save] public static float _timeScale = 1f;
        [Save] public static KeyCode _timeScaleKey;

        [Save] public static bool _viewMode; 
        [Save] public static KeyCode _viewModeeKey;

        [Save] public static bool _antiFlyUI;
        [Save] public static bool _antiFly;
        [Save] public static Rect _flyCheckVew0 = new Rect((float)Screen.width / 2 -100f, 30f, 210, 20);
        [Save] public static Rect _flyCheckVew1 = new Rect((float)Screen.width / 2 - 100f, 50f, 210, 20);



        [Save] public static bool _spine;

        [Save] public static bool _upEye;
        [Save] public static KeyCode _upEyeKey;


        [Save] public static bool _hitLog;
        [Save] public static Color32 _ColorHitLog = new Color32(0, 0, 0, 255);
        [Save] public static float _hitLogTime = 5f;


        [Save] public static bool _offDamageLand;



        [Save] public static bool _hitMarker;
        [Save] public static Color32 _hitMarkerColor = new Color32(150, 98, 239, 255);


        [Save] public static bool _hitSound;
        [Save] public static int _hitSoundList = 0;


        [Save] public static bool _cross;
        [Save] public static int _crossList = 0;

        [Save] public static string _prints = "unknown.png";
        [Save] public static bool _print;
        [Save] public static KeyCode _printKey;


        [Save] public static bool _noSteps;
        [Save] public static bool _MoveLine;



        [Save] public static string _chatText = "Omg refund nn hhhhhhhh!\nget gud, get my nonpasted desirepro.fun";
        [Save] public static bool _chat;
        [Save] public static float _timeChat = 4f;


        [Save] public static Color32 _colorFonMarkerSystem = new Color32(255, 255, 255, 255);
        [Save] public static Color32 _colorMarkerSystem = new Color32(255, 0, 0, 255);
        [Save] public static bool _markerSystem;
        [Save] public static bool _markerSystemFon;


        [Save] public static bool _backGroundMenu;


        [Save] public static bool _lowGravity;
        [Save] public static bool _infJump;
        [Save] public static KeyCode _menuKey = KeyCode.Insert;



        [Save] public static bool _timeDay;
        [Save] public static float _timeScrol = 7f;


        [Save] public static bool _customSky;
        [Save] public static float _star = 1f;
        [Save] public static float _atmosphere = 1f;
    }
}
