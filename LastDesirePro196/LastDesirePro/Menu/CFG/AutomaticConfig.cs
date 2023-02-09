using LastDesirePro.Attributes;
using UnityEngine;
 

namespace LastDesirePro.Menu.CFG
{
    internal class AutomaticConfig
    {
        [Save] public static bool _farmOreBonus;
        [Save] public static bool _farmTreeBonus;
        [Save]
        public static bool[] _farmBot = new bool[] {
            false, //On/Off
            false, //stone
            false, //metal
            false, //sulfur
            false, //tree
            false, //OnLadder 
        };
        [Save]
        public static bool _silentMelee;
        [Save]
        public static bool _silentMeleeNpc; 
        [Save] public static int _silentMeleeHit = 0;
        [Save] public static float _silentMeleeObjectSpeed = 0;
        [Save] public static float _farmBotSpeed;
         
        [Save]
        public static bool[] _autoPickup = new bool[] {
            false, //hemp
            false, //stone
            false, //metal
            false, //sulfur
            false, //wood
            false, //corn 
            false, //pumpkin 
            false, //mushroom 
            false, //item 
            false, //Взрывчатка 
            false, //Land mine 
            false, //Plants Hemp
            false,//Plants Hemp
            false,//Plants Hemp
        };


        [Save] public static bool _autoDrink;
        [Save] public static bool _autoIgnite;
        [Save] public static bool _autoBhop;
        [Save] public static bool _autoSuicide;
        [Save] public static bool _alwaysSuicide;
        [Save] public static bool _spamSuicide;
        [Save] public static KeyCode _spamKey;

        [Save] public static bool _autoHeal;
        [Save] public static bool _autoHealFriend;
        [Save] public static bool _autoAssist;

        [Save] public static bool _autoAuthBuild;
        [Save] public static bool _rotateBuild;
        [Save] public static bool _autoAuthTurret;

        [Save] public static bool _autoDoThrow;
        [Save] public static KeyCode _doThrowKey;

        [Save] public static bool _autoDoDrop;
        [Save] public static KeyCode _doDropKey;

        [Save] public static bool _offRecycler; 

        [Save] public static bool _autoSpamGuitar;
        [Save] public static bool _autoGrade;
        [Save] public static bool _Grade;
        [Save] public static int _numGrade = 0;
        [Save] public static float _buildGradeDist = 2;
        [Save] public static KeyCode _keyGradeBuild;

        [Save] public static bool _autoOpen;

        [Save] public static KeyCode _suicideKey;
        [Save] public static bool _magnitPlayer;
        [Save] public static KeyCode _magnitKey;
        [Save] public static bool _autoReload;
        [Save] public static KeyCode _doortyk;
        [Save] public static bool _tyktyk;
        [Save] public static bool _autoLockCodeLock;
        [Save] public static int codeKey = 1312;
        [Save] public static bool _autoUnLockCodeLock;
        [Save] public static int _codeKey = 1312;
        [Save] public static KeyCode _unlockcode;


        [Save] public static bool _spamGlassHammer;


        [Save] public static bool _fieldOfView;
        [Save] public static float _fieldOfViewRadius = 10;
    }
}
