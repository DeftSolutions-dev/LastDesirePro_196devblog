using static LastDesirePro.Menu.CFG.MiscConfig;
using UnityEngine;
using LastDesirePro.Main.Visuals;
using static LastDesirePro.DrawMenu.Prefab;
using System.IO;
using System.Collections.Generic;
using System;
using Facepunch.Steamworks;
using static LastDesirePro.Main.Visuals.Others;

namespace LastDesirePro.Menu.MenuTab
{
    internal class Misc
    {
        public static Vector2 _Weapon, _Weapon1, _Movement, _Movement1, _Misc, _Misc1;
         
        public static int tab = 0;
        public static string[] tb = new string[] { "Weapon", "Movement", "Misc" };
        public static bool key,key1, key2, key3, key4, key5, key6, key7, key8, key9;

        public static Vector3 marker;
        public static string _nameMarkers;
        public static string _nameMarker = "Num 1";
        public static void DoMisc()
        {
            tab = Tab(23, 60, 200, 100, tb, tab, 266);
            switch (tab)
            { 

                case 0:
                    { 
                        ScrollViewMenu(new Rect(15, 85, 370, 415), ref _Weapon, () =>
                        {
                            GUILayout.BeginVertical(GUILayout.Width(180));
                            GUILayout.Space(5f);
                            Toggle("Recoil", ref _recoil, 16);
                            if (_recoil)
                                Slider(0f, 100f, ref _recoilFloat, 200, $"{string.Format("{0}", (int)_recoilFloat)}.");
                            Toggle("Spread", ref _spread, 16);
                            if (_spread)
                                Slider(0f, 100f, ref _spreadFloat, 200, $"{string.Format("{0}", (int)_spreadFloat)}.");
                            Toggle("Automatic", ref _automatic, 16);
                            Toggle("No Sway", ref _sway, 16);
                            Toggle("Insta eoka", ref _eoka, 16);
                            Toggle("Insta bow", ref _bow, 16);
                            Toggle("Fast Bullet", ref _fastBullet, 16);
                            Toggle("Thickness", ref _thickness, 16);
                            if(_thickness)
                                Slider(0f, 100f, ref _thicknessFloat, 200, $"{string.Format("{0}", (int)_thicknessFloat)}.");
                            Toggle("Thickness Melee", ref _thicknessMelee, 16);
                            if (_thicknessMelee)
                                Slider(0f, 100f, ref _thicknessMeleeFloat, 200, $"{string.Format("{0}", (int)_thicknessMeleeFloat)}.");
                            Toggle("Melee x2 Distance", ref _meleeX2, 16);
                            Toggle("Melee Farm only Bonus", ref _meleeFarmOnlyBonus, 16);
                            Toggle("Fake Shoot", ref _fakeShoot, 16);
                            if (_fakeShoot)
                            {
                                Toggle("Always Fake Shoot", ref _alwayFakeShoot, 16);
                                if (!_alwayFakeShoot)
                                    _fakeShootKey = Bind(_fakeShootKey, "Fake Shoot Key: ", ref key);
                            }
                            Toggle("Silent Shoot", ref _silentShoot, 16);
                            if (_silentShoot)
                            {
                                _silentShootKey = Bind(_silentShootKey, "Silent Shoot Key: ", ref key1);
                            }
                            Toggle("Pierce to Layer", ref _shotLayer, 16);
                            if (_shotLayer)
                            {
                                Toggle("Some Objects", ref _layerShot[0], 16);
                                Toggle("Terrain", ref _layerShot[1], 16);
                                Toggle("Barricade", ref _layerShot[2], 16);
                                Toggle("Storage Container", ref _layerShot[3], 16);
                                Toggle("Ore Resource", ref _layerShot[4], 16);
                                Toggle("Tree", ref _layerShot[5], 16);
                                }
                            GUILayout.EndVertical();
                        });
                        ScrollViewMenu(new Rect(395, 85, 370, 415), ref _Weapon1, () =>
                        { 
                            GUILayout.BeginVertical(GUILayout.Width(180));
                            GUILayout.Space(5f);
                            Toggle("Layer Mask", ref _removeLayer, 16);
                            if (_removeLayer)
                            {
                                Toggle("AI", ref _layer[0], 16);
                                Toggle("Construction", ref _layer[1], 16);
                                Toggle("Transparent", ref _layer[2], 16);
                                Toggle("Debris", ref _layer[3], 16);
                                Toggle("Default", ref _layer[4], 16);
                                Toggle("Deployed", ref _layer[5], 16);
                                Toggle("Ragdoll", ref _layer[6], 16);
                                Toggle("Terrain", ref _layer[7], 16);
                                Toggle("Tree", ref _layer[8], 16);
                                Toggle("World", ref _layer[9], 16);
                                Toggle("Water", ref _layer[10], 16);
                                Toggle("Clutter", ref _layer[11], 16);
                                Toggle("Remove Layer Bind", ref _removeLayerAl, 16);
                                if (_removeLayerAl)
                                    _layerKey = Bind(_layerKey, "LayerMask Key: ", ref key2);
                            }
                            Toggle("No Attack restriction", ref _canAttack, 16);
                            Toggle("Aim in Heavy Armor", ref _shotTheArmor, 16);
                            Toggle("Shoot while sprinting", ref _sprintAim, 16);
                            Toggle("Fast Gun", ref _speedGun, 16);
                            Toggle("No Bobbing", ref _noBobbing, 16); 
                            Toggle("x6 Zoom", ref _x6Zoom, 16);
                            if (_x6Zoom) 
                                _x6ZoomKey = Bind(_x6ZoomKey, "Zoom Key: ", ref key3);  
                            Toggle("Crosshair", ref _cross, 16);
                            if (_cross)
                            {
                                using (new GUILayout.HorizontalScope())
                                {
                                    GUIContent[] target = { new GUIContent("#1"), new GUIContent("#2")  };
                                    if (Button("◄", 25))
                                    {
                                        _crossList--;
                                        if (_crossList == -1)
                                            _crossList = 0;
                                    }
                                    GUILayout.Label(target[(int)_crossList], _TextStyle1);
                                    if (Button("►", 25))
                                    {
                                        _crossList++;
                                        if (_crossList == 2)
                                            _crossList = 1;
                                    }
                                } 
                            }
                            GUILayout.EndVertical();
                        }); 
                        break;
                    }
                case 1:
                    {
                        ScrollViewMenu(new Rect(15, 85, 370, 415), ref _Movement, () =>
                        {
                            GUILayout.BeginVertical(GUILayout.Width(180));
                            GUILayout.Space(5f);
                            Toggle("UI anti flyhack", ref _antiFlyUI, 16);
                            if (_antiFlyUI)
                                Toggle("Anti FlyHack", ref _antiFly, 16);
                            Toggle("DebugCam", ref _debugCam, 16);
                            if (_debugCam)
                                _debugCamKey = Bind(_debugCamKey, "DebugCam Key: ", ref key4);
                            Toggle("Fly", ref _fly, 16);
                            if (_fly)
                            {
                                Toggle("Fly No Collision", ref _flyNoCollision, 16);
                                _debugFlyKey = Bind(_debugFlyKey, "Fly Key: ", ref key5);
                            }
                            Toggle("Speed", ref _speed, 16);
                            if (_speed)
                            {
                                Slider(0f, 6f, ref _move, 200, $"{string.Format("Standing: {0:0.#}", (float)_move)}.");
                                Slider(0f, 6f, ref _srint, 200, $"{string.Format("Sprint: {0:0.#}", (float)_srint)}.");
                                Slider(0f, 6f, ref _siting, 200, $"{string.Format("Sitting: {0:0.#}", (float)_siting)}.");
                            }
                            Toggle("Speed Game", ref _time, 16);
                            if (_time)
                            {
                                Slider(0f, 20, ref _timeScale, 200, $"{string.Format("Speed: {0:0.#} Sec", (float)_timeScale)}.");
                                _timeScaleKey = Bind(_timeScaleKey, "Speed Key: ", ref key6);
                            }

                            Toggle("3 Eyes", ref _viewMode, 16);
                            if (_viewMode)
                                _viewModeeKey = Bind(_viewModeeKey, "3 Eyes Key: ", ref key7);
                            Toggle("Spine [Anti-Aim]", ref _spine, 16);
                            Toggle("No Steps", ref _noSteps, 16);
                            GUILayout.EndVertical();
                        });
                        ScrollViewMenu(new Rect(395, 85, 370, 415), ref _Movement1, () =>
                        {
                            GUILayout.BeginVertical(GUILayout.Width(180));
                            GUILayout.Space(5f);
                            Toggle("Walk on water", ref _collisionWater, 16);
                            Toggle("No Collision Tree", ref _noCollisionTree, 16);
                            Toggle("No Collision Players", ref _noCollisionAI, 16);
                            Toggle("No Damage Land", ref _offDamageLand, 16);
                            Toggle("Big Jump", ref _lowGravity, 16);
                            Toggle("Inf Jump [OFF Bhop]", ref _infJump, 16);
                            Toggle("Spider", ref _spider, 16);
                            Toggle("Up Eye", ref _upEye, 16);
                            if (_upEye)
                                _upEyeKey = Bind(_upEyeKey, "Up Eye Key: ", ref key8);
                            Toggle("Field Of View", ref Menu.CFG.AutomaticConfig._fieldOfView, 16);
                            if(CFG.AutomaticConfig._fieldOfView)
                                Slider(0f, 35f, ref CFG.AutomaticConfig._fieldOfViewRadius, 200, $"{string.Format("FOV: {0:0.#}", CFG.AutomaticConfig._fieldOfViewRadius)}.");

                            GUILayout.EndVertical();
                        });
                        break;
                    }
                case 2:
                    { 
                        ScrollViewMenu(new Rect(15, 85, 370, 415), ref _Misc, () =>
                        {
                            GUILayout.BeginVertical(GUILayout.Width(180));
                            GUILayout.Space(5f);
                            Toggle("Hit Logs", ref _hitLog, 16, _ColorHitLog, true, 94);
                            if (_hitLog)
                                Slider(0f, 10f, ref _hitLogTime, 200, $"{string.Format("Time Hit Log: {0:0.#}", _hitLogTime)}.");

                            Toggle("Hit Marker", ref _hitMarker, 16, _hitMarkerColor, true, 97);
                            if(_hitMarker)
                                if(Button("Default Color", 111)) _hitMarkerColor = new Color32(150, 98, 239, 255);

                            Toggle("Hit Sound", ref _hitSound, 16);
                            if (_hitSound)
                            {
                                using (new GUILayout.HorizontalScope())
                                {
                                    GUIContent[] target = { new GUIContent("#1"), new GUIContent("#2"), new GUIContent("#3"), new GUIContent("#4")};
                                    if (Button("◄", 25))
                                    {
                                        _hitSoundList--;
                                        if (_hitSoundList == -1)
                                            _hitSoundList = 0;
                                    }
                                    GUILayout.Label(target[(int)_hitSoundList], _TextStyle1);
                                    if (Button("►", 25))
                                    {
                                        _hitSoundList++;
                                        if (_hitSoundList == 4)
                                            _hitSoundList = 3;
                                    }
                                }
                                if (Button("Check Sound", 111)) Others.playSound();
                            }
                            Toggle("Move Line", ref _MoveLine, 16); 

                            GUILayout.EndVertical();
                        });
                        ScrollViewMenu(new Rect(395, 85, 370, 415), ref _Misc1, () =>
                        {
                            GUILayout.BeginVertical(GUILayout.Width(180));
                            GUILayout.Space(5f);

                            Toggle("Printing", ref _print, 16);
                            if (_print)
                            {
                                _printKey = Bind(_printKey, "Printing Key: ", ref key9);
                                using (new GUILayout.HorizontalScope())
                                {
                                    GUILayout.Space(5); 
                                    _prints = TextField(_prints, "Desktop:  ", 222);
                                }
                            }
                            Toggle("Chat Spam", ref _chat, 16);
                            if (_chat)
                            { 
                                using (new GUILayout.HorizontalScope())
                                {
                                    GUILayout.Space(5); 
                                    _chatText = TextField(_chatText, "Text:  ", 222);
                                }
                                Slider(0.5f, 6f, ref _timeChat, 200, $"{string.Format("Time: {0:0.#}",  _timeChat)}.");
                            }
                            Toggle("Time", ref _timeDay, 16);
                            if (_timeDay)
                                Slider(0f, 24f, ref _timeScrol, 200, $"{string.Format("Time: {0:0.#}Hour.", _timeScrol)}.");
                            Toggle("Custom Sky", ref _customSky, 16);
                            if (_customSky)
                            {
                                Slider(0f, 1000f, ref _star, 200, $"{string.Format("Star: {0}", (int)_star)}.");
                                Slider(1f, 20f, ref _atmosphere, 200, $"{string.Format("Atmosphere: {0}", (int)_atmosphere)}.");
                            } 
                            Toggle("System Marker", ref _markerSystem, 16, _colorMarkerSystem, true, 100);
                            if (_markerSystem)
                            {
                                Toggle("Text background", ref _markerSystemFon, 16, _colorFonMarkerSystem, true, 101);
                                GUILayout.Space(10);
                                _nameMarker = TextField(_nameMarker, "Name: ", 160);
                                GUILayout.Space(5);
                                try
                                {
                                    for (int i = 0; i < MarkerSystem._marker.Count; i++)
                                    {
                                        var markers = MarkerSystem._marker[i].vec;
                                        var Selected = markers == Misc.marker;
                                        var color = Selected ? "<color=red>" : ""; 
                                        if (Button((Selected ? "<b>" : "") + color + $"{MarkerSystem._marker[i].name}" + (Selected ? "</color>" : "") + (Selected ? "</b>" : ""), 200))
                                        {
                                            _nameMarkers = MarkerSystem._marker[i].name;
                                            Misc.marker = markers;
                                        }
                                    }
                                    GUILayout.Space(10);
                                    if (Main.Visuals.Others.IsMarker(marker))
                                    {
                                        if (Button("Remove Marker ", 150))
                                            Main.Visuals.Others.RemoveCords(marker, _nameMarkers);
                                    }
                                    else
                                    {
                                        if (Button("Add Marker ", 150))
                                            Main.Visuals.Others.AddCords(LocalPlayer.Entity, _nameMarker);
                                    }
                                }
                                catch { }
                            }
                            GUILayout.EndVertical();
                        });
                        break;
                    }
            }
        }
    }
}
