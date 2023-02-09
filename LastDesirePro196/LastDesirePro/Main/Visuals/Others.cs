using Facepunch;
using LastDesirePro.Attributes;
using LastDesirePro.DrawMenu;
using LastDesirePro.Main.Objects;
using Network;
using ProtoBuf;
using Rust;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using static LastDesirePro.Menu.CFG.VisuаlCоnfig;
using static LastDesirePro.Menu.CFG.MiscConfig;
using static LastDesirePro.Main.Objects.ObjectsCheck;
using static TrackIR;
using static LastDesirePro.ColorPicker.GUIColorPicker;

namespace LastDesirePro.Main.Visuals
{
    [Component]
    class Others : MonoBehaviour
    {
        void OnGUI()
        {
            if (!DrawMenu.AssetsLoad.Loaded) return;
            GetHome(); Get();  
        }
        void GetHome()
        {
            try
            {
                if (_MonumentInfo)
                {
                    if (TerrainMeta.Path != null)
                    {
                        var list = (List<MonumentInfo>)typeof(TerrainPath).GetField("Monuments", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(TerrainMeta.Path);
                        for (int j = 0; j < list.Count; j++)
                        {
                            var info = list[j];
                            if (info.displayPhrase.token.Length > 0)
                            {
                                Vector3 vector = MainCamera.mainCamera.WorldToScreenPoint(info.transform.position);
                                if (vector.z > 0f)
                                {
                                    int Distance = (int)Vector3.Distance(MainCamera.mainCamera.transform.position, info.transform.position);
                                    vector.x += 3f;
                                    vector.y = UnityEngine.Screen.height - (vector.y + 1f);
                                    FullDrawing.String(new Vector2(vector.x, vector.y + 12), string.Format("{0} m.", Distance), _colorMonumentInfo, true, 10, FontStyle.Bold, 2);
                                    FullDrawing.String(new Vector2(vector.x, vector.y), string.Format("{0}", info.displayPhrase.english), _colorMonumentInfo, true, 10, FontStyle.Bold, 2);
                                }
                            }
                        }
                    }
                }
                if (_BaseHelicopter.Count > 0)
                {
                    foreach (var t in _BaseHelicopter.Values)
                    {
                        if (_Helicopter[0])
                        {
                            FullDrawing.String(new Vector2(Screen.width / 2, 3), "Heli Alert", Color.red, true, 11, FontStyle.Bold, 2, true, _colorFonHelicopter, _radiusFonHelicopter);
                            FullDrawing.Health(new Vector2(Screen.width / 2, 24), (int)t.health, 10000, 51, 7, 1f);
                        }
                        var vector = MainCamera.mainCamera.WorldToScreenPoint(t.position);
                        if (vector.z > 0f && Main.Visuals.FullDrawing.IsInScreen(t.position))
                        {
                            var resource_distance = (int)Vector3.Distance(MainCamera.mainCamera.transform.position, t.position);
                            vector.x += 3f; vector.y = Screen.height - (vector.y + 1f);
                            if (_Helicopter[0])
                            {
                                FullDrawing.Health(new Vector2(vector.x, vector.y - 17f), (int)t.MainRotor, 900, 30, 7, 1f);
                                FullDrawing.Health(new Vector2(vector.x, vector.y - 11f), (int)t.TailRotor, 500, 30, 7, 1f);
                                FullDrawing.String(new Vector2(vector.x, vector.y - 12), string.Format("{0}", "Helicopter"), _colorHelicopter, true, 10, FontStyle.Bold, 2, _Helicopter[1], _colorFonHelicopter, _radiusFonHelicopter);
                                FullDrawing.String(new Vector2(vector.x, vector.y), string.Format("{0} m.", resource_distance), _colorHelicopter, true, 10, FontStyle.Bold, 2, _Helicopter[1], _colorFonHelicopter, _radiusFonHelicopter);
                            }
                        }
                    }
                }

                if (LastDesirePro.Main.Objects.ObjectsCheck._BradleyAPC.Count > 0)
                {
                    foreach (var t in LastDesirePro.Main.Objects.ObjectsCheck._BradleyAPC.Values)
                    {
                        var vector = MainCamera.mainCamera.WorldToScreenPoint(t.position);
                        if (vector.z > 0f && Main.Visuals.FullDrawing.IsInScreen(t.position))
                        {
                            var resource_distance = (int)Vector3.Distance(MainCamera.mainCamera.transform.position, t.position);
                            vector.x += 3f; vector.y = Screen.height - (vector.y + 1f);
                            if (LastDesirePro.Menu.CFG.VisuаlCоnfig._BradleyAPC[0])
                            {
                                FullDrawing.Health(new Vector2(vector.x, vector.y - 11f), (int)t.health, 1000, 30, 7, 1f);
                                FullDrawing.String(new Vector2(vector.x, vector.y - 12), string.Format("{0}", "BradleyAPC"), _colorBradleyAPC, true, 10, FontStyle.Bold, 2, LastDesirePro.Menu.CFG.VisuаlCоnfig._BradleyAPC[1], _colorFonBradleyAPC, _radiusFonBradleyAPC);
                                FullDrawing.String(new Vector2(vector.x, vector.y), string.Format("{0} m.", resource_distance), _colorBradleyAPC, true, 10, FontStyle.Bold, 2, LastDesirePro.Menu.CFG.VisuаlCоnfig._BradleyAPC[1], _colorFonBradleyAPC, _radiusFonBradleyAPC);
                            }
                        }
                    }
                }
                if (_ContainerStorage.Count > 0)
                {
                    foreach (var t in _ContainerStorage.Values)
                    {
                        var vector = MainCamera.mainCamera.WorldToScreenPoint(t.position);
                        if (vector.z > 0f && Main.Visuals.FullDrawing.IsInScreen(t.position))
                        {
                            var _distance = (int)Vector3.Distance(MainCamera.mainCamera.transform.position, t.position);
                            vector.x += 3f; vector.y = Screen.height - (vector.y + 1f);
                            if (t.name.Contains("heli_crate.") && _Helicopter[2])
                            {
                                FullDrawing.String(new Vector2(vector.x, vector.y), string.Format("{0} m.", _distance), _colorheli_crate, true, 10, FontStyle.Bold, 2, _Helicopter[1], _colorFonHelicopter, _radiusFonHelicopter);
                                FullDrawing.String(new Vector2(vector.x, vector.y - 12f), string.Format("{0}", "Crate heli"), _colorheli_crate, true, 10, FontStyle.Bold, 2, _Helicopter[1], _colorFonHelicopter, _radiusFonHelicopter);
                                if (t.IsOnFire)
                                {
                                    FullDrawing.String(new Vector2(vector.x, vector.y - 24f), string.Format("{0}", "On Fire"), Color.red, true, 10, FontStyle.Bold, 2, _Helicopter[1], _colorFonHelicopter, _radiusFonHelicopter);
                                }
                            }
                            if (t.name.Contains("bradley_crate.") && LastDesirePro.Menu.CFG.VisuаlCоnfig._BradleyAPC[2])
                            {
                                FullDrawing.String(new Vector2(vector.x, vector.y), string.Format("{0} m.", _distance), _colorbradley_crate, true, 10, FontStyle.Bold, 2, LastDesirePro.Menu.CFG.VisuаlCоnfig._BradleyAPC[1], _colorFonBradleyAPC, _radiusFonBradleyAPC);
                                FullDrawing.String(new Vector2(vector.x, vector.y - 12f), string.Format("{0}", "Crate bradley"), _colorbradley_crate, true, 10, FontStyle.Bold, 2, LastDesirePro.Menu.CFG.VisuаlCоnfig._BradleyAPC[1], _colorFonBradleyAPC, _radiusFonBradleyAPC);
                                if (t.IsOnFire)
                                {
                                    FullDrawing.String(new Vector2(vector.x, vector.y - 24f), string.Format("{0}", "On Fire"), Color.red, true, 10, FontStyle.Bold, 2, LastDesirePro.Menu.CFG.VisuаlCоnfig._BradleyAPC[1], _colorFonBradleyAPC, _radiusFonBradleyAPC);
                                }
                            }
                        }
                    }
                }

                if (_SupplyDrop.Count > 0)
                {
                    foreach (var ta in _SupplyDrop.Values)
                    {
                        var vectora = MainCamera.mainCamera.WorldToScreenPoint(ta.position);
                        if (vectora.z > 0f && Main.Visuals.FullDrawing.IsInScreen(ta.position) && _Storage[13])
                        {
                            var _distancea = (int)Vector3.Distance(MainCamera.mainCamera.transform.position, ta.position);
                            vectora.x += 3f; vectora.y = Screen.height - (vectora.y + 1f);
                            FullDrawing.String(new Vector2(vectora.x, vectora.y), string.Format("{0} m.", _distancea), _colorStorage[12], true, 10, FontStyle.Bold, 2, _Storage[12], _colorFonStorage, _radiusFonStorage);
                            FullDrawing.String(new Vector2(vectora.x, vectora.y - 10f), "Supply Drop", _colorStorage[12], true, 10, FontStyle.Bold, 2, _Storage[12], _colorFonStorage, _radiusFonStorage);
                        }
                    }
                }
                if (_Container1.Count > 0)
                {
                    foreach (var t in _Container1.Values)
                    {
                        var vector = MainCamera.mainCamera.WorldToScreenPoint(t.position);
                        if (vector.z > 0f && Main.Visuals.FullDrawing.IsInScreen(t.position))
                        {
                            var _distance = (int)Vector3.Distance(MainCamera.mainCamera.transform.position, t.position);
                            vector.x += 3f; vector.y = Screen.height - (vector.y + 1f);
                            if (_distance <= _StorageDist)
                            {

                                if (t.name.Contains("crate_tools.") && _Storage[0])
                                {
                                    FullDrawing.String(new Vector2(vector.x, vector.y), string.Format("{0} m.", _distance), _colorStorage[0], true, 10, FontStyle.Bold, 2, _Storage[12], _colorFonStorage, _radiusFonStorage);
                                    FullDrawing.String(new Vector2(vector.x, vector.y - 10f), "Crate tools", _colorStorage[0], true, 10, FontStyle.Bold, 2, _Storage[12], _colorFonStorage, _radiusFonStorage);
                                }
                                if (t.name.Contains("crate_basic.") && _Storage[1])
                                {
                                    FullDrawing.String(new Vector2(vector.x, vector.y), string.Format("{0} m.", _distance), _colorStorage[1], true, 10, FontStyle.Bold, 2, _Storage[12], _colorFonStorage, _radiusFonStorage);
                                    FullDrawing.String(new Vector2(vector.x, vector.y - 10f), "Crate small", _colorStorage[1], true, 10, FontStyle.Bold, 2, _Storage[12], _colorFonStorage, _radiusFonStorage);
                                }
                                if (t.name.Contains("crate_normal_2.") && _Storage[2])
                                {
                                    FullDrawing.String(new Vector2(vector.x, vector.y), string.Format("{0} m.", _distance), _colorStorage[2], true, 10, FontStyle.Bold, 2, _Storage[12], _colorFonStorage, _radiusFonStorage);
                                    FullDrawing.String(new Vector2(vector.x, vector.y - 10f), "Crate normal", _colorStorage[2], true, 10, FontStyle.Bold, 2, _Storage[12], _colorFonStorage, _radiusFonStorage);
                                }
                                if ((t.name.Contains("crate_normal_2_food.") || t.entity.PrefabName.Contains("food") || t.entity.PrefabName.Contains("trash-pile-1")) && _Storage[3])
                                {
                                    FullDrawing.String(new Vector2(vector.x, vector.y), string.Format("{0} m.", _distance), _colorStorage[3], true, 10, FontStyle.Bold, 2, _Storage[12], _colorFonStorage, _radiusFonStorage);
                                    FullDrawing.String(new Vector2(vector.x, vector.y - 10f), "Crate normal food", _colorStorage[3], true, 10, FontStyle.Bold, 2, _Storage[12], _colorFonStorage, _radiusFonStorage);
                                }
                                if (t.name.Contains("crate_normal_2_medical.") && _Storage[4])
                                {
                                    FullDrawing.String(new Vector2(vector.x, vector.y), string.Format("{0} m.", _distance), _colorStorage[4], true, 10, FontStyle.Bold, 2, _Storage[12], _colorFonStorage, _radiusFonStorage);
                                    FullDrawing.String(new Vector2(vector.x, vector.y - 10f), "Crate normal medical", _colorStorage[4], true, 10, FontStyle.Bold, 2, _Storage[12], _colorFonStorage, _radiusFonStorage);
                                }
                                if (t.name.Contains("crate_mine.") && _Storage[5])
                                {
                                    FullDrawing.String(new Vector2(vector.x, vector.y), string.Format("{0} m.", _distance), _colorStorage[5], true, 10, FontStyle.Bold, 2, _Storage[12], _colorFonStorage, _radiusFonStorage);
                                    FullDrawing.String(new Vector2(vector.x, vector.y - 10f), "Crate mine", _colorStorage[5], true, 10, FontStyle.Bold, 2, _Storage[12], _colorFonStorage, _radiusFonStorage);
                                }
                                if (t.name.Contains("crate_normal.") && _Storage[6])
                                {
                                    FullDrawing.String(new Vector2(vector.x, vector.y), string.Format("{0} m.", _distance), _colorStorage[6], true, 10, FontStyle.Bold, 2, _Storage[12], _colorFonStorage, _radiusFonStorage);
                                    FullDrawing.String(new Vector2(vector.x, vector.y - 10f), "Crate military", _colorStorage[6], true, 10, FontStyle.Bold, 2, _Storage[12], _colorFonStorage, _radiusFonStorage);
                                }
                                if (t.name.Contains("crate_elite.") && _Storage[7])
                                {
                                    FullDrawing.String(new Vector2(vector.x, vector.y), string.Format("{0} m.", _distance), _colorStorage[7], true, 10, FontStyle.Bold, 2, _Storage[12], _colorFonStorage, _radiusFonStorage);
                                    FullDrawing.String(new Vector2(vector.x, vector.y - 10f), "Crate elite", _colorStorage[7], true, 10, FontStyle.Bold, 2, _Storage[12], _colorFonStorage, _radiusFonStorage);
                                }
                                if ((t.name.Contains("loot-barrel-1.") || t.name.Contains("loot-barrel-2.") || t.name.Contains("loot_barrel_1.") || t.name.Contains("loot_barrel_2.")) && _Storage[8])
                                {
                                    FullDrawing.String(new Vector2(vector.x, vector.y), string.Format("{0} m.", _distance), _colorStorage[8], true, 10, FontStyle.Bold, 2, _Storage[12], _colorFonStorage, _radiusFonStorage);
                                    FullDrawing.String(new Vector2(vector.x, vector.y - 10f), "Barrel", _colorStorage[8], true, 10, FontStyle.Bold, 2, _Storage[12], _colorFonStorage, _radiusFonStorage);
                                }
                                if (t.name.Contains("hobobarrel_static.") && _Storage[9])
                                {
                                    FullDrawing.String(new Vector2(vector.x, vector.y), string.Format("{0} m.", _distance), _colorStorage[9], true, 10, FontStyle.Bold, 2, _Storage[12], _colorFonStorage, _radiusFonStorage);
                                    FullDrawing.String(new Vector2(vector.x, vector.y - 10f), "Barrel fire", _colorStorage[9], true, 10, FontStyle.Bold, 2, _Storage[12], _colorFonStorage, _radiusFonStorage);
                                }
                                if (t.name.Contains("oil_barrel.") && _Storage[10])
                                {
                                    FullDrawing.String(new Vector2(vector.x, vector.y), string.Format("{0} m.", _distance), _colorStorage[10], true, 10, FontStyle.Bold, 2, _Storage[12], _colorFonStorage, _radiusFonStorage);
                                    FullDrawing.String(new Vector2(vector.x, vector.y - 10f), "Oil barrel", _colorStorage[10], true, 10, FontStyle.Bold, 2, _Storage[12], _colorFonStorage, _radiusFonStorage);
                                }
                                if (t.entity.PrefabName.Contains("recycler") && _Storage[11])
                                {
                                    FullDrawing.String(new Vector2(vector.x, vector.y), string.Format("{0} m.", _distance), _colorStorage[11], true, 10, FontStyle.Bold, 2, _Storage[12], _colorFonStorage, _radiusFonStorage);
                                    FullDrawing.String(new Vector2(vector.x, vector.y - 10f), "Recycler", _colorStorage[11], true, 10, FontStyle.Bold, 2, _Storage[12], _colorFonStorage, _radiusFonStorage);
                                }
                            }
                        }
                    }
                }
            }
            catch { }
        }
        void Get()
        {
            if (LocalPlayer.Entity != null)
                DrawMarkers();
            if (_antiFlyUI)
            {
                vew0 = GUI.Window(122, _flyCheckVew0, FlyCheck0, "","label");
                _flyCheckVew0.x = vew0.x;
                _flyCheckVew0.y = vew0.y;
                vew1 = GUI.Window(123, _flyCheckVew1, FlyCheck1, "", "label");
                _flyCheckVew1.x = vew1.x;
                _flyCheckVew1.y = vew1.y;
            }
            if (_debugCamera)
                FullDrawing.String(new Vector2(Screen.width / 2, Screen.height / 2 - 30), "Debug Camera", Color.red, true, 12, FontStyle.Bold, 2);
            if (_debugFly)
                FullDrawing.String(new Vector2(Screen.width / 2, Screen.height / 2 - 45), "Fly Hack", Color.red, true, 12, FontStyle.Bold, 2);
        }
        public static Rect vew0;
        public static Rect vew1;
        void FlyCheck0(int windowID)
        {
            FullDrawing.Indicator(new Vector2(100, 14), flyhack, maxfly, 200, 14, 1f, string.Format("{0:0.#}m", flyhack));
            GUI.DragWindow();
        }
        void FlyCheck1(int windowID)
        {
            FullDrawing.Indicator(new Vector2(100,14), fly, maxFly, 200, 14, 1f, string.Format("{0:0.#}m", fly));
            GUI.DragWindow();
        }
        public float pauseTime;
        public float flyhack = 0f;
        public float fly = 0f;
        public float maxfly = 0f;
        public float maxFly = 0f;
        public float flyVertical = 0f;
        public float flyHorizontal = 0f;
        public void CheckFlyhack() {
            try
            {
                if (!LocalPlayer.Entity.OnLadder() && !LocalPlayer.Entity.IsSleeping() && LocalPlayer.Entity != null && !LocalPlayer.Entity.IsDead())
                {
                    pauseTime = Math.Max(0f, pauseTime - Time.deltaTime);
                    var position = LocalPlayer.Entity.playerModel.position;
                    var vector = (LocalPlayer.Entity.lastSentTick.position + position) * 0.5f;
                    var vector2 = vector + new Vector3(0f, LocalPlayer.Entity.GetRadius() - 1.85f, 0f);
                    var vector3 = vector + new Vector3(0f, LocalPlayer.Entity.GetHeight(false) - LocalPlayer.Entity.GetRadius(), 0f);
                    var inAir = !Physics.CheckCapsule(vector2, vector3, LocalPlayer.Entity.GetRadius() - 0.05f, 1503731969, QueryTriggerInteraction.Ignore) && !WaterLevel.Test(vector - new Vector3(0f, 1.85f, 0f)) && !_debugCamera;
                    if (inAir)
                    {
                        var vector4 = position - LocalPlayer.Entity.lastSentTick.position;
                        var num3 = Mathf.Abs(vector4.y);
                        var num4 = vector4.Magnitude2D();
                        if (vector4.y >= 0f)
                            flyVertical += vector4.y;
                        if (num3 < num4)
                            flyHorizontal += num4;
                    }
                    else
                    {
                        flyHorizontal = 0f;
                        flyVertical = 0f;
                    }
                    var h0 = Math.Max((pauseTime > 0f) ? 10f : 1.5f, 0f);
                    var h = (LocalPlayer.Entity.GetJumpHeight() + h0) * 2.3f;
                    maxfly = h;
                    if (flyVertical <= h)
                        flyhack = flyVertical;
                    var w = Math.Max((pauseTime > 0f) ? 10f : 1.5f, 0f) * 4f;
                    var w0 = (5f + w);
                    maxFly = w0;
                    if (flyHorizontal <= w0)
                        fly = flyHorizontal;
                    if (flyVertical > h && !_debugCamera) 
                                antifly = true; else antifly = false; 
                    if (flyHorizontal > w0 && !_debugCamera) 
                                antiFly = true; else antiFly = false; 
                }
            }
            catch { }
        }
        //fly camera 
        public static bool antifly;
        public static bool antiFly;



        [Replacement(typeof(BasePlayer), "SendClientTick")]
        internal void SendClientTick()
        {
            LocalPlayer.Entity.lastSentTickTime = Time.realtimeSinceStartup;
            using (PlayerTick playerTick = Pool.Get<PlayerTick>())
            { 
                Item activeItem = LocalPlayer.Entity.Belt.GetActiveItem();
                playerTick.activeItem = ((activeItem == null) ? 0U : activeItem.uid);

                playerTick.inputState = LocalPlayer.Entity.input.state.current;
                try
                {
                    if (!LocalPlayer.Entity.IsSleeping())
                    {
                        var current = LocalPlayer.Entity.transform.position;
                        var old = LocalPlayer.Entity.lastSentTick.position;
                        var over = new Vector3(current.x, current.y, current.z);
                        if (Menu.CFG.MiscConfig._antiFly && antifly)
                            over = new Vector3(over.x, current.y < old.y ? (current.y - 0.3f) : old.y, over.z);
                        if (Menu.CFG.MiscConfig._antiFly && antiFly)
                            over = new Vector3(old.x, over.y, old.z);
                        if (Menu.CFG.MiscConfig._antiFly && antifly || antiFly)
                            if (over != current)
                                LocalPlayer.Entity.movement.TeleportTo(over, LocalPlayer.Entity);
                    }
                }
                catch { }
                if(_spine)
                playerTick.inputState.aimAngles = new Vector3(UnityEngine.Random.Range(-360f, 360f), UnityEngine.Random.Range(-360f, 360f), UnityEngine.Random.Range(-360f, 360f));
                if (_debugCamera)
                    playerTick.position = _lockPos;
                else
                    playerTick.position = LocalPlayer.Entity.transform.position;
                if (!_debugCamera)
                    _lockEyePos = playerTick.inputState.aimAngles;
                if (_debugCamera)
                    playerTick.inputState.aimAngles = _lockEyePos;
                if (playerTick.modelState == null)
                {
                    playerTick.modelState = Pool.Get<ModelState>();
                    playerTick.modelState.onground = true;
                }
                if (LocalPlayer.Entity.modelState != null)
                {
                    LocalPlayer.Entity.modelState.CopyTo(playerTick.modelState);
                }
                if (_noSteps)
                {
                    playerTick.modelState.onground = false;
                    playerTick.modelState.jumped = true;
                } 
                if (Net.cl.write.Start())
                {
                    Net.cl.write.PacketID(Message.Type.Tick);
                    playerTick.WriteToStreamDelta(Net.cl.write, LocalPlayer.Entity.lastSentTick);
                    Net.cl.write.Send(new SendInfo(Net.cl.Connection)
                    {
                        priority = Priority.Immediate
                    });
                }
                if (Net.cl.IsRecording)
                {
                    byte[] array = playerTick.ToProtoBytes();
                    Net.cl.ManualRecordPacket(15, array, array.Length);
                }
                if (LocalPlayer.Entity.lastSentTick == null)
                {
                    LocalPlayer.Entity.lastSentTick = Pool.Get<PlayerTick>();
                }
                playerTick.CopyTo(LocalPlayer.Entity.lastSentTick); 
            }
        }//   
        public static Vector3 _lockMainCameraPos = Vector3.zero;
        public static Vector3 _lockEyePos = Vector3.zero;
        public static Vector3 _lockPos = Vector3.zero;
        public static bool _debugCamera;
        public static bool _debugFly;
        public static bool _pressOff = false;
        public static float lastjump = 0;
        public static float lastJump = 0;
        public static Color colorHit = new Color(_hitMarkerColor.r, _hitMarkerColor.g, _hitMarkerColor.b, _hitMarkerColor.a);
        [Replacement(typeof(PlayerWalkMovement), "ClientInput")]
        public void fsf(InputState input, ModelState modelState)
        {
            //GameMenu.Option jumpTimeaa = typeof(StorageContainer).GetFieldValue<GameMenu.Option>(t, "__menuOption_Menu_OnFire");
            GetComponent<PlayerWalkMovement>().gravityMultiplier = 2.5f / (_lowGravity ? 2 :  1);
            if (UnityEngine.Input.GetKeyDown(_debugCamKey) && Visuals.FullDrawing.IsOpen() && _debugCam)
            { 
                _lockPos = LocalPlayer.Entity.transform.position;
                _debugCamera = !_debugCamera;
            }
            if (UnityEngine.Input.GetKeyDown(_debugFlyKey) && Visuals.FullDrawing.IsOpen() && _fly)
                _debugFly = !_debugFly;
            GetComponent<PlayerWalkMovement>().wasFlying = GetComponent<PlayerWalkMovement>().flying;
            GetComponent<PlayerWalkMovement>().flying = false;
            if (GetComponent<PlayerWalkMovement>().Owner.IsAdmin || GetComponent<PlayerWalkMovement>().Owner.IsDeveloper)
                GetComponent<PlayerWalkMovement>().flying = GetComponent<PlayerWalkMovement>().adminCheat;
            GetComponent<PlayerWalkMovement>().UpdateCurrentLadder(input, modelState);
            GetComponent<PlayerWalkMovement>().modify = GetComponent<PlayerWalkMovement>().Owner.GetMovementModify();
            modelState.jumped = false;
            modelState.onground = false;
            modelState.onLadder = false;
            modelState.flying = false; 
            modelState.sitting = false;
            if (input.IsDown(BUTTON.FORWARD) || input.IsDown(BUTTON.BACKWARD) || input.IsDown(BUTTON.LEFT) || input.IsDown(BUTTON.RIGHT) || input.IsDown(BUTTON.JUMP))
                _pressOff = false;
            if (UnityEngine.Input.GetKeyDown(Menu.CFG.AutomaticConfig._magnitKey) && Visuals.FullDrawing.IsOpen())
                _pressOff = true;
            GetComponent<PlayerWalkMovement>().body.constraints = _pressOff ? RigidbodyConstraints.FreezePositionY : RigidbodyConstraints.FreezeRotation;
            if (_pressOff && Menu.CFG.AutomaticConfig._magnitPlayer)
            {
                foreach (var basePlayer in BasePlayer.VisiblePlayerList)
                {
                    if (basePlayer != null && (int)Vector3.Distance(LocalPlayer.Entity.transform.position, basePlayer.transform.position) <= 3.5f && !basePlayer.IsLocalPlayer() && !basePlayer.IsSleeping() && !basePlayer.IsDead() && !basePlayer.IsWounded())
                    {
                        Vector3 position = basePlayer.playerModel.headBone.transform.position;
                        Vector3 position2 = new Vector3(position.x, position.y + 0.28f, position.z);
                        GetComponent<PlayerWalkMovement>().groundAngleNew = 0f;
                        GetComponent<PlayerWalkMovement>().grounded = true;
                        GetComponent<PlayerWalkMovement>().groundAngle = 0f;
                        GetComponent<PlayerWalkMovement>().body.velocity = new Vector3(0f, 0f, 0f);
                        GetComponent<PlayerWalkMovement>().transform.position = position2;
                    }
                }
            }
            if (input.IsDown(BUTTON.JUMP) && Menu.CFG.AutomaticConfig._autoBhop)
            {
                lastJump = (Time.time - lastjump);
                if (lastJump > 0.2f && !LocalPlayer.Entity.IsSleeping() && !LocalPlayer.Entity.IsReceivingSnapshot)
                {
                    if (GamePhysics.CheckCapsule(
                    ((LocalPlayer.Entity.lastSentTick.position + LocalPlayer.Entity.transform.position) * 0.5f + new Vector3(0f, LocalPlayer.Entity.GetRadius() - 0.25f, 0f)),
                    (LocalPlayer.Entity.lastSentTick.position + LocalPlayer.Entity.transform.position) * 0.5f + new Vector3(0f, LocalPlayer.Entity.GetHeight(false) - LocalPlayer.Entity.GetRadius(), 0f),
                    (LocalPlayer.Entity.GetRadius() - 0.05f),
                    1503731969,
                    QueryTriggerInteraction.Ignore) || GetComponent<PlayerWalkMovement>().CanJump())
                    {
                        lastjump = Time.time;
                        LocalPlayer.Entity.movement.GetComponent<PlayerWalkMovement>().Jump(LocalPlayer.Entity.modelState, false);
                    }
                }
            }
            //fly camera 
            try
            {
                if (_debugCamera && _debugCam)
                {
                    float speed = 0.5f;
                    if (input.IsDown(BUTTON.DUCK))
                        speed = 0.2f;
                    if (input.IsDown(BUTTON.SPRINT))
                        speed = 1.2f;
                    var pos = LocalPlayer.Entity.transform.position;
                    if (input.IsDown(BUTTON.FORWARD))
                        GetComponent<PlayerWalkMovement>().transform.position += GetComponent<PlayerWalkMovement>().Owner.eyes.BodyForward() / 2f * speed;
                    if (input.IsDown(BUTTON.BACKWARD))
                        GetComponent<PlayerWalkMovement>().transform.position -= GetComponent<PlayerWalkMovement>().Owner.eyes.BodyForward() / 2f * speed;
                    if (input.IsDown(BUTTON.LEFT))
                        GetComponent<PlayerWalkMovement>().transform.position -= GetComponent<PlayerWalkMovement>().Owner.eyes.BodyRight() / 2f * speed;
                    if (input.IsDown(BUTTON.RIGHT))
                        GetComponent<PlayerWalkMovement>().transform.position += GetComponent<PlayerWalkMovement>().Owner.eyes.BodyRight() / 2f * speed;
                    GetComponent<PlayerWalkMovement>().body.velocity = new Vector3(0f, 0f, 0f);
                    if (input.IsDown(BUTTON.JUMP))
                        GetComponent<PlayerWalkMovement>().transform.position = new Vector3(pos.x, pos.y + speed, pos.z);
                }
            }
            catch { }
                
            GetComponent<PlayerWalkMovement>().body.constraints = _debugCamera ? RigidbodyConstraints.FreezePositionY : RigidbodyConstraints.FreezeRotation; 
            GetComponent<PlayerWalkMovement>().body.detectCollisions = !_debugCamera;
            if (GetComponent<PlayerWalkMovement>().Owner.isMounted)
            {
                GetComponent<PlayerWalkMovement>().HandleDucking(modelState, false);
                GetComponent<PlayerWalkMovement>().Movement_Mounted(input, modelState);
                return;
            }
            if (GetComponent<PlayerWalkMovement>().flying)
            {
                GetComponent<PlayerWalkMovement>().Movement_Noclip(input, modelState);
                return;
            }
            if (GetComponent<PlayerWalkMovement>().ladder)
                GetComponent<PlayerWalkMovement>().Movement_Ladder(input, modelState);
            else if (GetComponent<PlayerWalkMovement>().swimming)
                GetComponent<PlayerWalkMovement>().Movement_Water(input, modelState);
            else
                GetComponent<PlayerWalkMovement>().Movement_Walking(input, modelState);
            modelState.sprinting = GetComponent<PlayerWalkMovement>().IsRunning;
        }
        class debug : BaseMovement
        {
            [Replacement(typeof(PlayerWalkMovement), "Movement_Walking")]
            public void Movement_Walking(InputState input, ModelState modelState)
            {
                var walk = GetComponent<PlayerWalkMovement>();
                bool flag = input.IsDown(BUTTON.FORWARD) && (!input.IsDown(BUTTON.LEFT) && !input.IsDown(BUTTON.BACKWARD)) && !input.IsDown(BUTTON.RIGHT);
                if (flag)
                {
                    if (input.IsDown(BUTTON.SPRINT))
                    {
                        if (!input.WasDown(BUTTON.SPRINT))
                        {
                            walk.lastSprintTime = Time.time;
                            if (walk.sprintForced)
                                walk.sprintForced = false;
                        }
                        if (Time.time - walk.lastSprintTime > 2f)
                            walk.sprintForced = true;
                    }
                }
                else
                {
                    walk.sprintForced = false;
                    walk.lastSprintTime = Time.time;
                }
                base.TargetMovement = Vector3.zero;
                if (!ProgressBarUI.IsOpen())
                {
                    if (input.IsDown(BUTTON.FORWARD))
                        base.TargetMovement += this.Owner.transform.forward;
                    if (input.IsDown(BUTTON.BACKWARD))
                        base.TargetMovement -= this.Owner.transform.forward;
                    if (input.IsDown(BUTTON.LEFT))
                        base.TargetMovement -= this.Owner.transform.right;
                    if (input.IsDown(BUTTON.RIGHT))
                        base.TargetMovement += this.Owner.transform.right;
                }
                if (walk.swimming || walk.jumping || (walk.falling && Time.time - walk.groundTime > 0.3f))
                    walk.HandleGrounded(modelState, false);
                else
                    walk.HandleGrounded(modelState, true);
                bool wantsRun = (input.IsDown(BUTTON.SPRINT) || walk.sprintForced) && flag && walk.CanSprint();
                bool wantsDuck = input.IsDown(BUTTON.DUCK);
                bool wantsJump = input.WasJustPressed(BUTTON.JUMP);
                walk.HandleRunning(modelState, wantsRun);
                walk.HandleDucking(modelState, wantsDuck);
                if (base.TargetMovement != Vector3.zero)
                    base.TargetMovement = base.TargetMovement.normalized * this.Owner.GetSpeed(base.Running, base.Ducking);
                float t = Mathf.Max(walk.modify.drag, this.Owner.clothingMoveSpeedReduction);
                base.TargetMovement = Vector3.Lerp(base.TargetMovement, Vector3.zero, t);
                if (base.TargetMovement.magnitude < 0.1f)
                    base.Running = 0f;
                walk.HandleJumping(modelState, wantsJump, false);
                if (_noSteps)
                {
                    modelState.onground = false;
                    modelState.jumped = true;
                }
                try
                {
                    if (_debugFly)
                    {
                        if (_flyNoCollision)
                            GetComponent<PlayerWalkMovement>().flying = true;
                        base.TargetMovement = Vector3.zero;
                        if (input.IsDown(BUTTON.FORWARD))
                            base.TargetMovement += this.Owner.eyes.BodyForward();
                        if (input.IsDown(BUTTON.BACKWARD))
                            base.TargetMovement -= this.Owner.eyes.BodyForward();
                        if (input.IsDown(BUTTON.LEFT))
                            base.TargetMovement -= this.Owner.eyes.BodyRight();
                        if (input.IsDown(BUTTON.RIGHT))
                            base.TargetMovement += this.Owner.eyes.BodyRight();
                        if (input.IsDown(BUTTON.JUMP))
                            base.TargetMovement += Vector3.up;
                        float d = 6f;
                        if (input.IsDown(BUTTON.DUCK))
                            d = 3f;
                        if (input.IsDown(BUTTON.SPRINT))
                            d = 6f;
                        if (base.TargetMovement != Vector3.zero)
                            base.TargetMovement = base.TargetMovement.normalized * d;
                    }
                }
                catch { }
            }
        }
        [Replacement(typeof(BaseCombatEntity), "DoHitNotify")]
        public void DoHitNotify(HitInfo info)
        { 
            using (TimeWarning.New("DoHitNotify", 0.1f))
            {
                if (GetComponent<BaseCombatEntity>().sendsHitNotification && !(info.Initiator == null) && info.Initiator is BasePlayer && !info.isHeadshot && !(this == info.Initiator) && UnityEngine.Time.frameCount != GetComponent<BaseCombatEntity>().lastNotifyFrame)
                {
                    GetComponent<BaseCombatEntity>().lastNotifyFrame = UnityEngine.Time.frameCount;
                    bool flag = info.Weapon is BaseMelee;
                    if (GetComponent<BaseNetworkable>().isClient && ConVar.hitnotify.notification_level == 1 && info.Initiator == LocalPlayer.Entity && (!flag || GetComponent<BaseCombatEntity>().sendsMeleeHitNotification))
                    {
                        if (_hitSound)
                            playSound();
                        if (_hitLog)
                        {
                            if (GetComponent<BaseCombatEntity>().ToPlayer())
                            {
                                var boneProperty = GetComponent<BaseCombatEntity>().skeletonProperties.FindBone(info.HitBone);
                                var bone = boneProperty.name.english;
                                HitLogs.Add(String.Format("[HitLogs] You Hit <color=red>{0}</color> at <color=red>{2}M</color> Away in <color=red>{1}</color> for <color=red>{3}</color>.", GetComponent<BasePlayer>().displayName, bone, (int)Vector2.Distance(LocalPlayer.Entity.transform.position, GetComponent<BasePlayer>().transform.position), $"-{(int)info.damageTypes.Total()}"), Menu.CFG.MiscConfig._hitLogTime);
                            }
                        }
                        Effect.client.Run("assets/bundled/prefabs/fx/hit_notify.prefab", default(Vector3), default(Vector3), default(Vector3));
                    }
                }
                if (GetComponent<BaseCombatEntity>().sendsHitNotification && !(info.Initiator == null) && info.Initiator is BasePlayer  && !(this == info.Initiator) && UnityEngine.Time.frameCount != GetComponent<BaseCombatEntity>().lastNotifyFrame)
                {
                    GetComponent<BaseCombatEntity>().lastNotifyFrame = UnityEngine.Time.frameCount;
                    bool flag = info.Weapon is BaseMelee;
                    if (GetComponent<BaseNetworkable>().isClient && ConVar.hitnotify.notification_level == 1 && info.Initiator == LocalPlayer.Entity && (!flag || GetComponent<BaseCombatEntity>().sendsMeleeHitNotification))
                    {
                        if (_hitSound)
                            playSound();
                        if (_hitLog)
                        {
                            if (GetComponent<BaseCombatEntity>().ToPlayer())
                            {
                                var boneProperty = GetComponent<BaseCombatEntity>().skeletonProperties.FindBone(info.HitBone);
                                var bone = boneProperty.name.english;
                                HitLogs.Add(String.Format("[HitLogs] You Hit <color=red>{0}</color> at <color=red>{2}M</color> Away in <color=red>{1}</color> for <color=red>{3}</color>.", GetComponent<BasePlayer>().displayName, bone, (int)Vector2.Distance(LocalPlayer.Entity.transform.position, GetComponent<BasePlayer>().transform.position), $"-{(int)info.damageTypes.Total()}"), Menu.CFG.MiscConfig._hitLogTime);
                            }
                        }
                    }
                }
            }
        }
        public static void playSound()
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer();
            SoundList(player);
            player.Play();
        }
        public static void SoundList(System.Media.SoundPlayer f)
        {
            switch (_hitSoundList)
            { 
                case 0:
                    f.Stream = LastDesirePro.Properties.Resources._8;
                    break; 
                case 1:
                    f.Stream = LastDesirePro.Properties.Resources._10;
                    break;
                case 2:
                    f.Stream = LastDesirePro.Properties.Resources._5;
                    break;
                case 3:
                    f.Stream = LastDesirePro.Properties.Resources.killsound;
                    break;
            }
        }
        [Replacement(typeof(EffectLibrary), "Run")]
        public static void Run(Effect fx)
        { 
            if(_hitSound)//hit sound
            if (fx.pooledString.Contains("hit_notify") || fx.pooledString.Contains("headshot"))
                return;
            if (Menu.CFG.AutomaticConfig._autoBhop)
                if (fx.pooledString.Contains("jump") || fx.pooledString.Contains("screen_land"))
                    return;
            if (fx.type == 0U)
            {
                if (Menu.CFG.VisuаlCоnfig._raid[8])
                {
                    switch ((fx.pooledString))
                    {
                        case ("assets/prefabs/tools/c4/effects/c4_explosion.prefab"):
                            if (Menu.CFG.VisuаlCоnfig._raid[0])
                                Raid.Add(fx.worldPos, fx.pooledString);
                            break;
                        case ("assets/prefabs/weapons/satchelcharge/effects/satchel-charge-explosion.prefab"):
                            if (Menu.CFG.VisuаlCоnfig._raid[1])
                                Raid.Add(fx.worldPos, fx.pooledString);
                            break;
                        case ("assets/prefabs/weapons/rocketlauncher/effects/rocket_explosion_incendiary.prefab"):
                            if (Menu.CFG.VisuаlCоnfig._raid[2])
                                Raid.Add(fx.worldPos, fx.pooledString);
                            break;
                        case ("assets/prefabs/weapons/rocketlauncher/effects/rocket_explosion.prefab"):
                            if (Menu.CFG.VisuаlCоnfig._raid[3])
                                Raid.Add(fx.worldPos, fx.pooledString);
                            break;
                        case ("assets/prefabs/weapons/beancan grenade/effects/beancan_grenade_explosion.prefab"):
                            if (Menu.CFG.VisuаlCоnfig._raid[4])
                                Raid.Add(fx.worldPos, fx.pooledString);
                            break;
                        case ("assets/prefabs/weapons/f1 grenade/effects/f1grenade_explosion.prefab"):
                            if (Menu.CFG.VisuаlCоnfig._raid[5])
                                Raid.Add(fx.worldPos, fx.pooledString);
                            break;
                        case ("assets/bundled/prefabs/fx/impacts/additive/explosion.prefab"):
                            if (Menu.CFG.VisuаlCоnfig._raid[6])
                                Raid.Add(fx.worldPos, fx.pooledString);
                            break;
                    }
                }
                EffectLibrary.GenericEffectSpawn(fx);
            }
            else if (fx.type == 1U)
            {
                EffectLibrary.ProjectileEffectSpawn(fx);
            }
        } 
        [Component]
        public class Raid : MonoBehaviour
        {
            private static List<RaidEsp> raid = new List<RaidEsp>();
            public static void Add(Vector3 vec, string text)
            {
                raid.Add(new RaidEsp()
                {
                    text = text,
                    time = DateTime.Now,
                    pos = vec
                });
            }
            void OnGUI()
            {
                if (Menu.CFG.VisuаlCоnfig._raid[8])
                {
                    var logs = raid;
                    for (int i = 0; i < logs.Count; i++)
                    {
                        var log = logs[i];
                        var vector = MainCamera.mainCamera.WorldToScreenPoint(log.pos);
                        if (vector.z > 0f && Main.Visuals.FullDrawing.IsInScreen(log.pos))
                        {
                            var _distance = (int)Vector3.Distance(MainCamera.mainCamera.transform.position, log.pos);
                            vector.x += 3f; vector.y = Screen.height - (vector.y + 1f);
                            if ((DateTime.Now - log.time).TotalSeconds > Menu.CFG.VisuаlCоnfig._secondRaid)
                            {
                                logs.RemoveAt(i);
                                continue;
                            }
                            var screen = new Vector2(Screen.width / 2, Screen.height / 2);
                            var check = new Vector2(vector.x, vector.y);
                            var radius = Mathf.Abs(Vector2.Distance(screen, check));
                            if (Menu.CFG.VisuаlCоnfig._raid[0] && log.text == "assets/prefabs/tools/c4/effects/c4_explosion.prefab")
                                FullDrawing.String(new Vector2(vector.x, vector.y), $"C4 ({_distance}m) ({(int)(DateTime.Now - log.time).TotalSeconds}s)", Menu.CFG.VisuаlCоnfig._colorRaid, true, (radius <= 30) ? 14 : 10, FontStyle.Bold, 2, Menu.CFG.VisuаlCоnfig._raid[7],
                                Menu.CFG.VisuаlCоnfig._colorFonRaid, Menu.CFG.VisuаlCоnfig._radiusFonRaid);
                            if (Menu.CFG.VisuаlCоnfig._raid[1] && log.text == "assets/prefabs/weapons/satchelcharge/effects/satchel-charge-explosion.prefab")
                                FullDrawing.String(new Vector2(vector.x, vector.y), $"Satchel ({_distance}m) ({(int)(DateTime.Now - log.time).TotalSeconds}s)", Menu.CFG.VisuаlCоnfig._colorRaid, true, (radius <= 30) ? 14 : 10, FontStyle.Bold, 2, Menu.CFG.VisuаlCоnfig._raid[7],
                                Menu.CFG.VisuаlCоnfig._colorFonRaid, Menu.CFG.VisuаlCоnfig._radiusFonRaid);
                            if (Menu.CFG.VisuаlCоnfig._raid[2] && log.text == "assets/prefabs/weapons/rocketlauncher/effects/rocket_explosion_incendiary.prefab")
                                FullDrawing.String(new Vector2(vector.x, vector.y), $"Rocket ({_distance}m) ({(int)(DateTime.Now - log.time).TotalSeconds}s)", Menu.CFG.VisuаlCоnfig._colorRaid, true, (radius <= 30) ? 14 : 10, FontStyle.Bold, 2, Menu.CFG.VisuаlCоnfig._raid[7],
                                Menu.CFG.VisuаlCоnfig._colorFonRaid, Menu.CFG.VisuаlCоnfig._radiusFonRaid);
                            if (Menu.CFG.VisuаlCоnfig._raid[3] && log.text == "assets/prefabs/weapons/rocketlauncher/effects/rocket_explosion.prefab")
                                FullDrawing.String(new Vector2(vector.x, vector.y), $"Rocket ({_distance}m) ({(int)(DateTime.Now - log.time).TotalSeconds}s)", Menu.CFG.VisuаlCоnfig._colorRaid, true, (radius <= 30) ? 14 : 10, FontStyle.Bold, 2, Menu.CFG.VisuаlCоnfig._raid[7],
                                Menu.CFG.VisuаlCоnfig._colorFonRaid, Menu.CFG.VisuаlCоnfig._radiusFonRaid);
                            if (Menu.CFG.VisuаlCоnfig._raid[4] && log.text == "assets/prefabs/weapons/beancan grenade/effects/beancan_grenade_explosion.prefab")
                                FullDrawing.String(new Vector2(vector.x, vector.y), $"Beancan ({_distance}m) ({(int)(DateTime.Now - log.time).TotalSeconds}s)", Menu.CFG.VisuаlCоnfig._colorRaid, true, (radius <= 30) ? 14 : 10, FontStyle.Bold, 2, Menu.CFG.VisuаlCоnfig._raid[7],
                                Menu.CFG.VisuаlCоnfig._colorFonRaid, Menu.CFG.VisuаlCоnfig._radiusFonRaid);
                            if (Menu.CFG.VisuаlCоnfig._raid[5] && log.text == "assets/prefabs/weapons/f1 grenade/effects/f1grenade_explosion.prefab")
                                FullDrawing.String(new Vector2(vector.x, vector.y), $"F1 ({_distance}m) ({(int)(DateTime.Now - log.time).TotalSeconds}s)", Menu.CFG.VisuаlCоnfig._colorRaid, true, (radius <= 30) ? 14 : 10, FontStyle.Bold, 2, Menu.CFG.VisuаlCоnfig._raid[7],
                                Menu.CFG.VisuаlCоnfig._colorFonRaid, Menu.CFG.VisuаlCоnfig._radiusFonRaid);
                            if (Menu.CFG.VisuаlCоnfig._raid[6] && log.text == "assets/bundled/prefabs/fx/impacts/additive/explosion.prefab")
                                FullDrawing.String(new Vector2(vector.x, vector.y), $"Exp. ({_distance}m) ({(int)(DateTime.Now - log.time).TotalSeconds}s)", Menu.CFG.VisuаlCоnfig._colorRaid, true, (radius <= 30) ? 14 : 10, FontStyle.Bold, 2, Menu.CFG.VisuаlCоnfig._raid[7],
                                Menu.CFG.VisuаlCоnfig._colorFonRaid, Menu.CFG.VisuаlCоnfig._radiusFonRaid);
                            logs[i] = log;
                        }
                    }
                    raid = logs;
                }
            }
            public struct RaidEsp
            {
                public string text;
                public DateTime time;
                public Vector3 pos;
            }
        }
        [Component]
        public class HitLogs : MonoBehaviour
        {
            private static List<ILog> logs = new List<ILog>();
            public static void Add(string text,float time)
            {
                logs.Add(new ILog()
                {
                    text = text,
                    times = time,
                    time = DateTime.Now,
                    pos = new Vector2(10, 0)
                });
            } 
            void OnGUI()
            {
                var currentLogs = logs;
                for (int i = 0; i < currentLogs.Count; i++)
                {
                    var log = currentLogs[i];
                    int a = 0;
                    if ((DateTime.Now - log.time).TotalSeconds > log.times)
                    {
                        a = 200;
                        if (log.pos.y < 0)
                        {
                            currentLogs.RemoveAt(i);
                            continue;
                        }
                    }
                    log.pos = Vector2.Lerp(log.pos, new Vector2(10, 10 + (i * 20) - a), Time.deltaTime * 1f); 
                    FullDrawing.LogString(new Vector2(30, log.pos.y), $"{log.text}", Color.green, false,12,FontStyle.Bold,true,Color.black,0, (float)(DateTime.Now - log.time).TotalSeconds, log.times);
                    currentLogs[i] = log;
                }
                logs = currentLogs;
            } 
            public struct ILog
            {
                public string text;
                public float times;
                public DateTime time;
                public Vector2 pos;
            }
        } 
        private void DrawMarkers()
        {
            if (_markerSystem && LocalPlayer.Entity != null)
            {
                for (int i = 0; i < MarkerSystem._marker.Count; i++)
                {
                    var vector = MainCamera.mainCamera.WorldToScreenPoint(MarkerSystem._marker[i].vec);
                    var num = Vector3.Distance(LocalPlayer.Entity.transform.position, MarkerSystem._marker[i].vec);
                    if (vector.z > 0f)
                    {
                        vector.x += 3f;
                        vector.y = (float)Screen.height - (vector.y + 1f);
                        FullDrawing.String(new Vector2(vector.x, vector.y), string.Format("{0}", (int)num), _colorMarkerSystem, true, 10, FontStyle.Bold, 2, _markerSystemFon, _colorFonMarkerSystem, 0f);
                        FullDrawing.String(new Vector2(vector.x, vector.y - 10), string.Format("{0}", MarkerSystem._marker[i].name), _colorMarkerSystem, true, 10, FontStyle.Bold, 2, _markerSystemFon, _colorFonMarkerSystem, 0f);
                    }
                }
            }
        }
        [Component]
        public class MarkerSystem : MonoBehaviour
        {
           [Save] public static List<Info> _marker = new List<Info>();
            public static void Add(Vector3 vec, string name) => _marker.Add(new Info()
            {
                vec = vec,
                name = name
            });
            public static void Remove(Vector3 vec, string name) => _marker.Remove(new Info()
            {
                vec = vec,
                name = name
            });
            public static bool Contains(Vector3 vec)
            {
                var c = _marker;
                for (int i = 0; i < c.Count; i++)
                    if (c[i].vec == vec)
                    return true;
                return false;
            }
            public struct Info
            {
                public Vector3 vec;
                public string name; 
            }
        }
        public static void AddCords(BasePlayer player,string name)
        { 
            var pos = player.transform.position;
            if (!MarkerSystem.Contains(pos)) MarkerSystem.Add(pos, name);
        }
        public static void RemoveCords(Vector3 marker,string name)
        {
            var pos = marker;
            if (MarkerSystem.Contains(pos)) MarkerSystem.Remove(pos, name);
        }
        public static bool IsMarker(Vector3 player) => MarkerSystem.Contains(player); 
        private void Update()
        {
            if (_debugCamera)
            {
                typeof(PlayerWalkMovement).GetField("groundAngleNew", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetField).SetValue(LocalPlayer.Entity.movement, 0f);
                typeof(PlayerWalkMovement).GetField("groundAngle", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetField).SetValue(LocalPlayer.Entity.movement, 0f);
            }
            if (_debugFly)
            {
                typeof(PlayerWalkMovement).GetField("flying", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetField).SetValue(LocalPlayer.Entity.movement, true);
                typeof(PlayerWalkMovement).GetField("groundAngleNew", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetField).SetValue(LocalPlayer.Entity.movement, 0f);
                typeof(PlayerWalkMovement).GetField("groundAngle", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetField).SetValue(LocalPlayer.Entity.movement, 0f);
            } 
            if (LocalPlayer.Entity != null)
            { 
                //LocalPlayer.Entity.ServerRPC<int, int, float, int>("PerformanceReport", 999999999, 999999999, 33999999999999999999999999999999999999f, 999999999);
            }//spam console server (50/50 work)
            if (LocalPlayer.Entity != null && !LocalPlayer.Entity.IsSleeping() && _antiFlyUI)
                CheckFlyhack();
            /*foreach (var aa in BaseEntity.clientEntities)
            {
                var t = aa as BaseProjectile;
                if (t != null)
                {
                    if(t.recoil != null)
                    t.recoil = null;
                }
            }*/

        }
    }
} 