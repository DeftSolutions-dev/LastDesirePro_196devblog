using LastDesirePro.Attributes;
using System.Collections.Generic;
using UnityEngine;

namespace LastDesirePro.Menu.CFG
{
    internal class AimBotConfig
    {

        [Save]
        public static HashSet<ulong> _friendList = new HashSet<ulong>();
        [Save]
        public static HashSet<ulong> _target = new HashSet<ulong>();

        [Save] public static bool _maxDamage;

        [Save] public static bool _aim;
        [Save] public static bool _aimPred;
        [Save] public static bool _aimFov;
        [Save] public static Color32 _colorFov = new Color32(255, 255, 255, 255);
        [Save] public static float _aimfov = 120f;
        [Save] public static int _aimBodyList = 0;
        [Save] public static KeyCode _aimkey = KeyCode.None;


         
        [Save] public static bool _hitBoxPlayer;
        [Save] public static bool _hitBoxPlayerGlass;
        [Save] public static int _hitBoxPlayerList = 0;
        [Save] public static float _hitBoxPlayerRadius = 1.5f;

        [Save] public static bool _hitBoxHelicopter; 
        [Save] public static float _hitBoxHelicopterRadius = 30f;


        [Save] public static bool _noDamageFriend;

        [Save] public static bool _hitRotor;
        [Save] public static bool _hitPlayer;
        [Save] public static int _hitPlayerList = 0;





        [Save] public static bool _silentPlayerPredict;
        [Save] public static bool _silentPlayerPredictWall;
        [Save] public static bool _silentPlayerPredictFov;
        [Save] public static Color32 _silentPlayerPredictFovColor = new Color32(255, 255, 255, 255);
        [Save] public static bool _silentHeliPredict;
        [Save] public static float _silent = 120f;


        [Save] public static bool _pAim;
        [Save] public static bool _pAimDub;
        [Save] public static bool _pAimTarget;
        [Save] public static Color32 _pAimTargetColor = new Color32(255, 0, 0, 255);
        [Save] public static bool _pAimFov;
        [Save] public static Color32 _colorpAimFov = new Color32(255, 255, 255, 255);
        [Save] public static float _pAimfov = 120f;
        [Save] public static int _pAimBodyList = 0;
        [Save] public static KeyCode _pAimkey = KeyCode.None;





        [Save] public static bool _pAimHeli;
        [Save] public static KeyCode _pAimHelikey = KeyCode.None;



    }
}
