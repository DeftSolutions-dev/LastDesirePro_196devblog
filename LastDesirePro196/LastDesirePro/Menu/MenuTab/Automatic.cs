using static LastDesirePro.Menu.CFG.AutomaticConfig;
using UnityEngine;
using LastDesirePro.Main.Visuals;
using static LastDesirePro.DrawMenu.Prefab;
using System.IO;
using System.Collections.Generic;
using System;

namespace LastDesirePro.Menu.MenuTab
{
    internal class Automatic
    {
        public static Vector2 _AutoFarm, _Melee, _PickUp, _Auto, _Misc;
        public static float f = 0;
        public static float he = 100;
        public static int tab = 0;
        public static string[] tb = new string[] { "AutoFarm", "Silent Melee", "PickUp", "Auto", "Misc" };
        public static bool key,key1, key2, key3, key4, key5, key6, key7;
        public static void DoAutomatic()
        {
            tab = Tab(23, 60, 133, 100, tb, tab, 150);
            switch (tab)
            { 
                case 0:
                    {
                        ScrollViewMenu(new Rect(15, 85, 750, 415), ref _AutoFarm, () =>
                        {
                            GUILayout.BeginVertical(GUILayout.Width(180)); 
                            GUILayout.Space(5f);
                            Toggle("AutoFarm Tree Marker", ref _farmTreeBonus, 16);
                            Toggle("AutoFarm Ore Bonus", ref _farmOreBonus, 16);
                            Toggle("Farm Bot [Beta]", ref _farmBot[0], 16);
                            if (_farmBot[0])
                            {
                                Toggle("Stone", ref _farmBot[1], 16);
                                Toggle("Metal", ref _farmBot[2], 16);
                                Toggle("Sulfur", ref _farmBot[3], 16);
                                Toggle("Tree", ref _farmBot[4], 16);
                                Slider(0, 2f, ref _farmBotSpeed, 200, $"Speed Bot: {string.Format( "{0:0.#} m/sec",(float)_farmBotSpeed+4f)}");
                                Toggle("OnLadder", ref _farmBot[5], 16); 
                            }
                            GUILayout.EndVertical();
                        });
                        break;
                    }
                case 1:
                    {
                        ScrollViewMenu(new Rect(15, 85, 750, 415), ref _Melee, () =>
                        {
                            GUILayout.BeginVertical(GUILayout.Width(180));
                            GUILayout.Space(5f);
                            Toggle("Silent Melee to player [Normal Server Detect!]", ref _silentMelee, 16);
                            if (_silentMelee)
                            { 
                                GUILayout.Space(5f);
                                using (new GUILayout.HorizontalScope())
                                {
                                    GUIContent[] target = { new GUIContent("Head"), new GUIContent("Chest"), new GUIContent("Body") };
                                    if (Button("◄", 25))
                                    {
                                        _silentMeleeHit--;
                                        if (_silentMeleeHit == -1)
                                            _silentMeleeHit = 0;
                                    }
                                    GUILayout.Label(target[(int)_silentMeleeHit], _TextStyle1);
                                    if (Button("►", 25))
                                    {
                                        _silentMeleeHit++;
                                        if (_silentMeleeHit == 3)
                                            _silentMeleeHit = 2;
                                    }
                                }
                            }
                            Toggle("Silent Melee to Animal's", ref _silentMeleeNpc, 16); 
                            Toggle("Hammer Spam Glass to player", ref _spamGlassHammer, 16);
                            GUILayout.EndVertical();
                        });
                        break;
                    }
                case 2:
                    {
                        ScrollViewMenu(new Rect(15, 85, 750, 415), ref _PickUp, () =>
                        {
                            GUILayout.BeginVertical(GUILayout.Width(180));
                            GUILayout.Space(5f); 
                            Toggle("AutoPickup Hemp", ref _autoPickup[0], 16);
                            Toggle("AutoPickup Stone", ref _autoPickup[1], 16);
                            Toggle("AutoPickup Metal", ref _autoPickup[2], 16);
                            Toggle("AutoPickup Sulfur", ref _autoPickup[3], 16);
                            Toggle("AutoPickup Wood", ref _autoPickup[4], 16);
                            Toggle("AutoPickup Corn", ref _autoPickup[5], 16);
                            Toggle("AutoPickup Pumpkin", ref _autoPickup[6], 16);
                            Toggle("AutoPickup Mushroom", ref _autoPickup[7], 16);
                            Toggle("AutoPickup Dropped Item", ref _autoPickup[8], 16);
                            Toggle("AutoPickup Timed Explosive", ref _autoPickup[9], 16);
                            Toggle("AutoPickup Mine", ref _autoPickup[10], 16);
                            GUILayout.EndVertical();
                        });
                        break;
                    }
                case 3:
                    {
                        ScrollViewMenu(new Rect(15, 85, 750, 415), ref _Auto, () =>
                        {
                            GUILayout.BeginVertical(GUILayout.Width(180));
                            GUILayout.Space(5f);
                            Toggle("Auto Silent Reload", ref _autoReload, 16);
                            Toggle("Auto Jump [Bhop, press Jump]", ref _autoBhop, 16);
                            Toggle("Auto Heal", ref _autoHeal, 16);
                            Toggle("Auto Heal Friend", ref _autoHealFriend, 16);
                            Toggle("Auto Revive", ref _autoAssist, 16);
                            Toggle("Auto OnTorch", ref _autoIgnite, 16);
                            Toggle("Auto Drink", ref _autoDrink, 16); 
                            Toggle("Auto Knok", ref _tyktyk, 16);
                            if (_tyktyk)
                                _doortyk = Bind(_doortyk, "Spam Key: ", ref key);
                            Toggle("Auto Open", ref _autoOpen, 16);
                            Toggle("Spam Guitar", ref _autoSpamGuitar, 16); 
                            Toggle("Auto Lock CodeLock", ref _autoLockCodeLock, 16);
                            if (_autoLockCodeLock)
                                using (new GUILayout.HorizontalScope())
                                {
                                    GUILayout.Space(40);
                                    codeKey = TextField(codeKey, "", 50);
                                }
                            Toggle("Auto UnLock CodeLock", ref _autoUnLockCodeLock, 16);
                            if (_autoUnLockCodeLock)
                                using (new GUILayout.HorizontalScope())
                                {
                                    GUILayout.Space(40);
                                    _codeKey = TextField(_codeKey, "", 50);
                                    GUILayout.Space(5f);
                                    _unlockcode = Bind(_unlockcode, "Unlock Key: ", ref key1);
                                }
                            Toggle("Auto Auth Cupboard", ref _autoAuthBuild, 16);
                            Toggle("Auto Auth Turret", ref _autoAuthTurret, 16);
                            Toggle("Auto Off Recycler", ref _offRecycler, 16); 
                            GUILayout.EndVertical();
                        });
                        break; 
                    }
                case 4:
                    {
                        ScrollViewMenu(new Rect(15, 85, 750, 415), ref _Misc, () =>
                        { 
                            GUILayout.BeginVertical(GUILayout.Width(230));
                            GUILayout.Space(5f);
                            Toggle("Magnit Player", ref _magnitPlayer, 16);
                            if(_magnitPlayer)
                            _magnitKey = Bind(_magnitKey, "Magnit Key: ", ref key2);
                            Toggle("Suicide", ref _autoSuicide, 16);
                            if (_autoSuicide)
                            {
                                Toggle("Always Suicide", ref _alwaysSuicide, 16);
                                if(!_alwaysSuicide)
                                _suicideKey = Bind(_suicideKey, "Suicide Key: ", ref key3);
                            }
                            Toggle("Spam Suicide", ref _spamSuicide, 16);
                            if (_spamSuicide)
                                _spamKey = Bind(_spamKey, "Spam Suicide Key: ", ref key4);

                            Toggle("Fast Throwing Grenades", ref _autoDoThrow, 16);
                            if (_autoDoThrow)
                                _doThrowKey = Bind(_doThrowKey, "DoThrow Key: ", ref key5);

                            Toggle("Fast Drop Grenades", ref _autoDoDrop, 16);
                            if (_autoDoDrop)
                                _doDropKey = Bind(_doDropKey, "DoDrop Key: ", ref key6);
                            Toggle("Rotate Build [Key Arrows]", ref _rotateBuild, 16);
                            Toggle("UpGrade Build", ref _Grade, 16);
                            if (_Grade)
                            {
                                Slider(0f, 4f, ref _buildGradeDist, 200, $"{string.Format("{0}m.", (int)_buildGradeDist)}.");
                                Toggle("Always Upgrade", ref _autoGrade, 16);
                                GUILayout.Space(5f);
                                using (new GUILayout.HorizontalScope())
                                {
                                    GUIContent[] target = { new GUIContent("Wood"), new GUIContent("Stone"),
                                        new GUIContent("Metal"), new GUIContent("TopTier") };
                                    if (Button("◄", 25))
                                    {
                                        _numGrade--;
                                        if (_numGrade == -1)
                                            _numGrade = 0;
                                    }
                                    GUILayout.Label(target[(int)_numGrade], _TextStyle1);
                                    if (Button("►", 25))
                                    {
                                        _numGrade++;
                                        if (_numGrade == 4)
                                            _numGrade = 3;
                                    }
                                }
                                if (!_autoGrade)
                                    _keyGradeBuild = Bind(_keyGradeBuild, "UpGrade Key: ", ref key7);
                            } 
                            GUILayout.EndVertical();
                        }); 
                        break;
                    }
            }
        }
    }
}
