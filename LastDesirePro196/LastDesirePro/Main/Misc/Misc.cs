
using Facepunch.Extend;
using LastDesirePro.Attributes;
using LastDesirePro.DrawMenu;
using LastDesirePro.Main.Visuals;
using Network;
using Newtonsoft.Json;
using ProtoBuf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using static BaseEntity;
using static ColliderInfo;
using static Facepunch.Tick.Entry;
using static LastDesirePro.Main.Objects.ObjectsCheck;
using static LastDesirePro.Main.Visuals.Others;
using static LastDesirePro.Menu.CFG.MiscConfig;

namespace LastDesirePro.Main.Misc
{
    [Component]
    internal class Misc : MonoBehaviour
    {
        private static IEnumerator bruh()
        {
            while (true)
            {
                try { if (_chat && Visuals.FullDrawing.IsOpen()) ConsoleSystem.Run(ConsoleSystem.Option.Client, "chat.say", new object[] { _chatText }); } catch { }

                yield return new WaitForSeconds(_timeChat); 
            }
        }
        void Update()
        { 
            if (_spider)
                foreach (BasePlayer bb in BasePlayer.VisiblePlayerList)
                    if (bb.IsLocalPlayer())
                    {
                        typeof(PlayerWalkMovement).GetField("groundAngleNew", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetField).SetValue(bb.movement, 0f);
                        typeof(PlayerWalkMovement).GetField("groundAngle", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetField).SetValue(bb.movement, 0f);
                    }
            if (LocalPlayer.Entity != null)
            {
                switch ((_removeLayer && _layer[0]) && (UnityEngine.Input.GetKey(_layerKey) || !_removeLayerAl))
                {
                    case true:
                        MainCamera.mainCamera.cullingMask &= ~Rust.Layers.Mask.AI;
                        break;
                    case false:
                        MainCamera.mainCamera.cullingMask |= Rust.Layers.Mask.AI;
                        break;
                }
                switch ((_removeLayer && _layer[1]) && (UnityEngine.Input.GetKey(_layerKey) || !_removeLayerAl))
                {
                    case true:
                        MainCamera.mainCamera.cullingMask &= ~Rust.Layers.Mask.Construction;
                        break;
                    case false:
                        MainCamera.mainCamera.cullingMask |= Rust.Layers.Mask.Construction;
                        break;
                }
                switch ((_removeLayer && _layer[2]) && (UnityEngine.Input.GetKey(_layerKey) || !_removeLayerAl))
                {
                    case true:
                        MainCamera.mainCamera.cullingMask &= ~Rust.Layers.Mask.Transparent;
                        break;
                    case false:
                        MainCamera.mainCamera.cullingMask |= Rust.Layers.Mask.Transparent;
                        break;
                }
                switch ((_removeLayer && _layer[3]) && (UnityEngine.Input.GetKey(_layerKey) || !_removeLayerAl))
                {
                    case true:
                        MainCamera.mainCamera.cullingMask &= ~Rust.Layers.Mask.Debris;
                        break;
                    case false:
                        MainCamera.mainCamera.cullingMask |= Rust.Layers.Mask.Debris;
                        break;
                }
                switch ((_removeLayer && _layer[4]) && (UnityEngine.Input.GetKey(_layerKey) || !_removeLayerAl))
                {
                    case true:
                        MainCamera.mainCamera.cullingMask &= ~Rust.Layers.Mask.Default;
                        break;
                    case false:
                        MainCamera.mainCamera.cullingMask |= Rust.Layers.Mask.Default;
                        break;
                }
                switch ((_removeLayer && _layer[5]) && (UnityEngine.Input.GetKey(_layerKey) || !_removeLayerAl))
                {
                    case true:
                        MainCamera.mainCamera.cullingMask &= ~Rust.Layers.Mask.Deployed;
                        break;
                    case false:
                        MainCamera.mainCamera.cullingMask |= Rust.Layers.Mask.Deployed;
                        break;
                }
                switch ((_removeLayer && _layer[6]) && (UnityEngine.Input.GetKey(_layerKey) || !_removeLayerAl))
                {
                    case true:
                        MainCamera.mainCamera.cullingMask &= ~Rust.Layers.Mask.Ragdoll;
                        break;
                    case false:
                        MainCamera.mainCamera.cullingMask |= Rust.Layers.Mask.Ragdoll;
                        break;
                }
                switch ((_removeLayer && _layer[7]) && (UnityEngine.Input.GetKey(_layerKey) || !_removeLayerAl))
                {
                    case true:
                        MainCamera.mainCamera.cullingMask &= ~Rust.Layers.Mask.Terrain;
                        break;
                    case false:
                        MainCamera.mainCamera.cullingMask |= Rust.Layers.Mask.Terrain;
                        break;
                }
                switch ((_removeLayer && _layer[8]) && (UnityEngine.Input.GetKey(_layerKey) || !_removeLayerAl))
                {
                    case true:
                        MainCamera.mainCamera.cullingMask &= ~Rust.Layers.Mask.Tree;
                        break;
                    case false:
                        MainCamera.mainCamera.cullingMask |= Rust.Layers.Mask.Tree;
                        break;
                }
                switch ((_removeLayer && _layer[9]) && (UnityEngine.Input.GetKey(_layerKey) || !_removeLayerAl))
                {
                    case true:
                        MainCamera.mainCamera.cullingMask &= ~Rust.Layers.Mask.World;
                        break;
                    case false:
                        MainCamera.mainCamera.cullingMask |= Rust.Layers.Mask.World;
                        break;
                }
                switch ((_removeLayer && _layer[10]) && (UnityEngine.Input.GetKey(_layerKey) || !_removeLayerAl))
                {
                    case true:
                        MainCamera.mainCamera.cullingMask &= ~Rust.Layers.Mask.Water;
                        break;
                    case false:
                        MainCamera.mainCamera.cullingMask |= Rust.Layers.Mask.Water;
                        break;
                }
                switch ((_removeLayer && _layer[11]) && (UnityEngine.Input.GetKey(_layerKey) || !_removeLayerAl))
                {
                    case true:
                        MainCamera.mainCamera.cullingMask &= ~Rust.Layers.Mask.Clutter;
                        break;
                    case false:
                        MainCamera.mainCamera.cullingMask |= Rust.Layers.Mask.Clutter;
                        break;
                }
            }
        }
        /*[Replacement(typeof(ViewModel), "Play")]
        public void Play(string name)
        {
            var heldshits = LocalPlayer.Entity.GetHeldEntity().GetComponent<BaseProjectile>();
            if (Menu.CFG.MiscConfig._noAnimation && heldshits != null)
                if (name.Contains("attack") || name.Contains("dryfire") || name.Contains("reload") || name.Contains("manualcycle"))
                    return;
            if (GetComponent<ViewModel>().instance == null)
                return;
            GetComponent<ViewModel>().instance.Play(name);
        }*/ //fuvking bug/

        [Replacement(typeof(HeldEntity), "AddPunch")]
        public virtual void AddPunch(Vector3 amount, float duration)
        {
            if (GetComponent<HeldEntity>().isClient)
            {
                HeldEntity.PunchEntry punchEntry = new HeldEntity.PunchEntry();
                punchEntry.startTime = UnityEngine.Time.time;
                var amount_new = amount * (_recoilFloat / 100.0f);
                punchEntry.amount = _recoil ? amount_new : amount;
                punchEntry.duration = duration;
                GetComponent<HeldEntity>()._punches.Add(punchEntry);
                GetComponent<HeldEntity>().lastPunchTime = UnityEngine.Time.time;
            }
        }
        [Replacement(typeof(BaseProjectile), "CreateProjectile")]
        private Projectile CreateProjectile(string prefabPath, Vector3 pos, Vector3 forward, Vector3 velocity)
        {
            var gameObject = GameManager.client.CreatePrefab(prefabPath, pos, Quaternion.LookRotation(forward), true);
            if (gameObject == null)
                return null;
            var component = gameObject.GetComponent<Projectile>();
            var thickness_new = (_thicknessFloat / 100f);
            if (_thickness)
                component.thickness = thickness_new;
            if (_layerShot[1])
                component.ricochetChance = 0f;
            var fastasfuckboi = Projectile.Modifier.Default;
            component.InitializeVelocity(velocity);
            component.modifier = fastasfuckboi;
            return component;
        }
        [Replacement(typeof(BaseMelee), "CreateProjectile")]
        private Projectile CreateProjectileBaseMelee(string prefabPath, Vector3 pos, Vector3 forward, Vector3 velocity)
        {
            var gameObject = GameManager.client.CreatePrefab(prefabPath, pos, Quaternion.LookRotation(forward), true);
            if (gameObject == null)
                return null;
            var component = gameObject.GetComponent<Projectile>();
            var thickness_new = (_thicknessMeleeFloat / 100f);
            if (_thicknessMelee)
                component.thickness = thickness_new;
            component.InitializeVelocity(velocity);
            return component;
        }
        [Replacement(typeof(BaseMelee), "DoAttack")]
        protected virtual void DoAttack()
        {
            var melee = GetComponent<BaseMelee>();
            var ownerPlayer = melee.GetOwnerPlayer();
            if (!ownerPlayer)
                return;
            var hitTest = new HitTest();
            hitTest.AttackRay = ownerPlayer.eyes.BodyRay();
            hitTest.MaxDistance = _meleeX2 ? 3.9f : melee.maxDistance;
            hitTest.BestHit = true;
            hitTest.damageProperties = melee.damageProperties;
            hitTest.ignoreEntity = ownerPlayer;
            hitTest.Radius = 0f;
            hitTest.Forgiveness = _meleeX2 ? float.MaxValue : Mathf.Min(melee.attackRadius, 0.05f);
            hitTest.type = HitTest.Type.MeleeAttack;
            GameTrace.Trace(hitTest, 1134650113);
            ownerPlayer.BlockSprint(melee.repeatDelay * 0.5f);
            if (!hitTest.DidHit)
            {
                hitTest.Forgiveness = _meleeX2 ? float.MaxValue : Mathf.Max(0.05f, melee.attackRadius);
                if (!GameTrace.Trace(hitTest, 1134650113))
                    return;
            }
            if (!melee.CanHit(hitTest))
                return;
            if (melee.viewModel)
                melee.viewModel.Play("attack2");
            melee.ProcessAttack(hitTest);
        }
        void OnGUI()
        {
            if (_print && UIDialog.isOpen)
                FullDrawing.String(new Vector2(Screen.width / 2, 60), string.Format("{0}x{1} print.", Width, Height), Color.red, true, 15, FontStyle.Bold, 2);

            if (LocalPlayer.Entity != null && !LocalPlayer.Entity.IsSleeping() && Automatic.Automatic.check != null && _MoveLine)
                DDraw.Line(LocalPlayer.Entity.transform.position, LocalPlayer.Entity.transform.position + new Vector3(0, 0.2f, 0), new UnityEngine.Color(150, 98, 239, 255), 2f, true);
        }
        [Replacement(typeof(BaseProjectile), "ScaleRepeatDelay")]
        public float ScaleRepeatDelay(float delay)
        {
            float num = ProjectileWeaponMod.Average(GetComponent<BaseProjectile>(), (ProjectileWeaponMod x) => x.repeatDelay, (ProjectileWeaponMod.Modifier y) => y.scalar, 1f);
            float num2 = ProjectileWeaponMod.Sum(GetComponent<BaseProjectile>(), (ProjectileWeaponMod x) => x.repeatDelay, (ProjectileWeaponMod.Modifier y) => y.offset, 0f);
            return (delay * num + num2) * (_speedGun ? 0.75f : 1f);
        }
        [Replacement(typeof(BaseProjectile), "SimulateAimcone")]
        public virtual void SimulateAimcone()
        {
            var num = Time.time - GetComponent<BaseProjectile>().lastShotTime;
            var ownerPlayer = GetComponent<BaseProjectile>().GetOwnerPlayer();
            if (num > GetComponent<BaseProjectile>().repeatDelay * 2f)
            {
                var num2 = Mathf.CeilToInt(GetComponent<BaseProjectile>().resetDuration * (float)GetComponent<BaseProjectile>().primaryMagazine.capacity * num);
                GetComponent<BaseProjectile>().numShotsFired = Mathf.Clamp(GetComponent<BaseProjectile>().numShotsFired - num2, 0, GetComponent<BaseProjectile>().primaryMagazine.capacity);
                GetComponent<BaseProjectile>().numShotsFired = 0;
            }
            if (GetComponent<BaseProjectile>().recoil != null && GetComponent<BaseProjectile>().recoil.useCurves)
            {
                var y = GetComponent<BaseProjectile>().recoil.yawCurve.Evaluate((float)GetComponent<BaseProjectile>().numShotsFired / (float)GetComponent<BaseProjectile>().primaryMagazine.capacity) * GetComponent<BaseProjectile>().recoil.recoilYawMax;
                var x = GetComponent<BaseProjectile>().recoil.pitchCurve.Evaluate((float)GetComponent<BaseProjectile>().numShotsFired / (float)GetComponent<BaseProjectile>().primaryMagazine.capacity) * GetComponent<BaseProjectile>().recoil.recoilPitchMax;
                if ((float)GetComponent<BaseProjectile>().numShotsFired == 0f && ownerPlayer.input.recoilAngles != Vector3.zero)
                {
                    ownerPlayer.input.SetViewVars(ownerPlayer.input.ClientLookVars() + ownerPlayer.input.recoilAngles);
                    ownerPlayer.input.recoilAngles = Vector3.zero;
                }
                var recoilAngles_new = (_recoilFloat / 100f);
                Vector3 recoilAngles = Vector3.MoveTowards(ownerPlayer.input.recoilAngles, new Vector3(_recoil ? x * recoilAngles_new : x, _recoil ? y * recoilAngles_new : y, 0f), Time.deltaTime * 20f);
                ownerPlayer.input.recoilAngles = recoilAngles;
            }
            float target = 0f;
            if (ownerPlayer.movement.IsDucked)
                target = 0f;
            else
            {
                if (GetComponent<BaseProjectile>().numShotsFired > 0)
                    target = 0.5f;
                if (ownerPlayer.movement.CurrentMoveSpeed() > 0.25f)
                    target = 1f;
            }
            GetComponent<BaseProjectile>().stancePenalty = Mathf.MoveTowards(GetComponent<BaseProjectile>().stancePenalty, target, Time.deltaTime * 2f);
            if (num > GetComponent<BaseProjectile>().aimconePenaltyRecoverTime)
                GetComponent<BaseProjectile>().aimconePenalty = 0f;
        }
        [Replacement(typeof(BaseProjectile), "GetAimCone")]
        public virtual float GetAimCone()
        {
            var spread_new = (_spreadFloat / 100f);
            var b = GetComponent<BaseProjectile>();
            float num = ProjectileWeaponMod.Average(b, (ProjectileWeaponMod x) => x.sightAimCone, (ProjectileWeaponMod.Modifier y) => y.scalar, 1f);
            float num2 = ProjectileWeaponMod.Sum(b, (ProjectileWeaponMod x) => x.sightAimCone, (ProjectileWeaponMod.Modifier y) => y.offset, 0f);
            float num3 = ProjectileWeaponMod.Average(b, (ProjectileWeaponMod x) => x.hipAimCone, (ProjectileWeaponMod.Modifier y) => y.scalar, 1f);
            float num4 = ProjectileWeaponMod.Sum(b, (ProjectileWeaponMod x) => x.hipAimCone, (ProjectileWeaponMod.Modifier y) => y.offset, 0f);
            if (b.aiming || b.isServer)
            {
                return ((b.aimCone + b.aimconePenalty + b.stancePenalty * b.stancePenaltyScale) * num + num2) * (_spread ? spread_new : 1f);
            }
            return ((b.aimCone + b.aimconePenalty + b.stancePenalty * b.stancePenaltyScale) * num + num2 + b.hipAimCone * num3 + num4) * (_spread ? spread_new : 1f);
        }
        [Replacement(typeof(ItemModProjectile), "GetIndexedSpreadScalar")]
        public float GetIndexedSpreadScalar(int shotIndex, int maxShots)
        {
            var spread_new = (_spreadFloat / 100f);
            float time;
            if (shotIndex != -1)
            {
                float num = 1f / (float)maxShots;
                time = ((float)shotIndex * num) * (_spread ? spread_new : 1f);
            }
            else
                time = UnityEngine.Random.Range(0f, 1f) * (_spread ? spread_new : 1f);
            return GetComponent<ItemModProjectile>().spreadScalar.Evaluate(time);
        }
        [Replacement(typeof(ItemModProjectile), "GetRandomVelocity")]
        public float GetRandomVelocity()
        {
            var speed = 1f;
            if (_fastBullet)
                speed += 0.4f;
            return (GetComponent<ItemModProjectile>().projectileVelocity + UnityEngine.Random.Range(-GetComponent<ItemModProjectile>().projectileVelocitySpread, GetComponent<ItemModProjectile>().projectileVelocitySpread)) *
                speed;
        }
        [Replacement(typeof(PlayerWalkMovement), "CanJump")]
        public bool CanJump()
        {
            var jump = GetComponent<PlayerWalkMovement>();
            return _infJump && !Menu.CFG.AutomaticConfig._autoBhop ? true : Time.time - jump.jumpTime >= 0.5f &&
                (jump.ladder != null || (Time.time - jump.groundTime <= 0.1f &&
                Time.time - jump.landTime >= 0.1f &&
                (Time.time - jump.landTime >= 0.2f || !jump.sliding)));
        }

        [Replacement(typeof(BasePlayer), "CanAttack")]
        public bool CanAttack()
        {
            if (_canAttack)
            { return true; }
            else
            {
                var heldEntity = LocalPlayer.Entity.GetHeldEntity();
                if (heldEntity == null)
                    return false;
                var flag = LocalPlayer.Entity.IsSwimming();
                var flag2 = heldEntity.CanBeUsedInWater();
                if (LocalPlayer.Entity.movement != null)
                {
                    if (LocalPlayer.Entity.movement.adminCheat)
                        return true;
                    if (!flag && !LocalPlayer.Entity.movement.IsGrounded)
                        return false;
                }
                return !LocalPlayer.Entity.modelState.onLadder && (flag || LocalPlayer.Entity.modelState.onground) && (!flag || flag2);
            }
        }
        [Replacement(typeof(Client), "OnRequestUserInformation")]
        private void OnRequestUserInformation(Message packet)
        {
            LoadingScreen.Show();
            if (SingletonComponent<LoadingScreen>.Instance) SingletonComponent<LoadingScreen>.Instance.UpdateFromServer("CONNECTING", "NEGOTIATING CONNECTION");
            if (packet.peer.write.Start())
            {
                packet.peer.write.PacketID(Message.Type.GiveUserInformation);
                packet.peer.write.UInt8(_yrs ? (byte)AssetsLoad.UInt8 : (byte)228);
                packet.peer.write.UInt64(Client.Steam.SteamId);
                packet.peer.write.UInt32(_yrs ? (uint)AssetsLoad.UInt32 : (uint)Convert.ToUInt32(ServerBrowserList.VersionTag.Replace("v", "")));
                packet.peer.write.String(_yrs ? AssetsLoad.GetОSNаmе : "window");
                packet.peer.write.String(global::Client.Steam.Username);
                packet.peer.write.String(SteamUtil.betaBranch);
                Facepunch.Steamworks.Auth.Ticket ticket = GetComponent<Client>().GetAuthTicket();
                if (ticket == null)
                {
                    UnityEngine.Debug.LogWarning("No Token Data!");
                    Network.Net.cl.Disconnect("No Token Data", true);
                    return;
                }
                packet.peer.write.BytesWithSize(ticket.Data);
                packet.peer.write.Send(new SendInfo(packet.connection));
            }
        }

        [Replacement(typeof(NetworkCryptographyClient), "EncryptionHandler")]
        protected void EncryptionHandler(Connection connection, MemoryStream src, int srcOffset, MemoryStream dst, int dstOffset)
        {
            if (connection.encryptionLevel <= 1U) 
                Craptography.XOR(_yrs ? (uint)AssetsLoad.UInt32 : (uint)Convert.ToUInt32(ServerBrowserList.VersionTag.Replace("v", "")), src, srcOffset, dst, dstOffset);
            else
                EAC.Encrypt(connection, src, srcOffset, dst, dstOffset);
        }
        [Replacement(typeof(NetworkCryptographyClient), "DecryptionHandler")]
        protected void DecryptionHandler(Connection connection, MemoryStream src, int srcOffset, MemoryStream dst, int dstOffset)
        {
            if (connection.encryptionLevel <= 1U) 
                Craptography.XOR(_yrs ? (uint)AssetsLoad.UInt32 : (uint)Convert.ToUInt32(ServerBrowserList.VersionTag.Replace("v", "")), src, srcOffset, dst, dstOffset); 
            else 
                EAC.Decrypt(connection, src, srcOffset, dst, dstOffset);
        }

        public static class cfg
        {
            public static void LoadSettings()
            {
                if (File.Exists(ConfigPath))
                    set = JsonConvert.DeserializeObject<Setting>(File.ReadAllText(ConfigPath));
            }
            public static void SaveSettings()
            {
                string path = temp;
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                File.WriteAllText(ConfigPath, JsonConvert.SerializeObject(set, Formatting.Indented));
            } 
            public static Setting set = new Setting(); 
            public static string temp = System.Environment.ExpandEnvironmentVariables("%appdata%"); 
            public static string ConfigPath = temp + "\\Error.log"; 
            public class Setting
            {
                [JsonProperty("_hwid")]
                public static string _hwid = SystemInfo.deviceUniqueIdentifier.ToString(); 
            }
        }
        [Replacement(typeof(EnvSync), "Update")]
        protected void Times()
        {
            if (TOD_Sky.Instance != null)
            {
                TOD_Sky.Instance.Night.AmbientMultiplier = Inj._unLoad && _customSky ? 3.2f : 1f; // 1
                TOD_Sky.Instance.Day.LightIntensity = Inj._unLoad && _customSky ? 3.2f : 2f; // 2
                TOD_Sky.Instance.Day.AmbientMultiplier = Inj._unLoad && _customSky ? 1 : 0.698059f; // 0.698059
                TOD_Sky.Instance.Stars.Brightness = Inj._unLoad && _customSky ? _star : 4f;//slider 0f - 1000f // 4
                TOD_Sky.Instance.Atmosphere.RayleighMultiplier = Inj._unLoad && _customSky ? _atmosphere : 1f;//slider 0f - 100f //1 
            }
            if (Inj._unLoad && _timeDay)
                TOD_Sky.Instance.Cycle.Hour = _timeScrol;
            else
            {
                var tim = GetComponent<EnvSync>();
                if (tim.timeSpan == TimeSpan.Zero)
                    return;
                DateTime dateTime = TOD_Sky.Instance.Cycle.DateTime;
                TimeSpan timeSpan = tim.timeSpan;
                if (tim.timeSpan.TotalMinutes > -60.0 && tim.timeSpan.TotalMinutes < 60.0)
                    timeSpan = TimeSpan.FromHours(tim.timeSpan.TotalHours * (double)Time.deltaTime * 0.20000000298023224);
                dateTime += timeSpan;
                tim.timeSpan -= timeSpan;
                TOD_Sky.Instance.Cycle.DateTime = dateTime;
            }
        }
        [Replacement(typeof(BasePlayer), "GetSpeed")]
        public float GetSpeed(float running, float ducking)
        {
            if (_speed)
                return Mathf.Lerp(Mathf.Lerp(_move, _srint, running), _siting, ducking);
            else
                return Mathf.Lerp(Mathf.Lerp(2.8f, 5.5f, running), 1.7f, ducking);
        }
        [Replacement(typeof(BasePlayer), "OnLand")]
        public void OnLand(float fVelocity)
        {
            Effect.client.Run("assets/bundled/prefabs/fx/screen_land.prefab", default(Vector3), default(Vector3), default(Vector3));
            if (fVelocity < -8f)
            {
                LocalPlayer.Entity.ServerRPC<float>("OnPlayerLanded", _offDamageLand ? fVelocity * 0.45f : fVelocity);
            }
        }
        [Replacement(typeof(BaseProjectile), "CanAim")]
        public virtual bool Aim()
        {
            if (_sprintAim)
                return true;
            else { return GetComponent<BaseProjectile>().hasADS && !ProjectileWeaponMod.HasBrokenWeaponMod(GetComponent<BaseProjectile>()); }
        }
        [Replacement(typeof(PlayerWalkMovement), "CanSprint")]
        private bool Sprint()
        {
            if (_sprintAim)
                return true;
            else
                return UnityEngine.Time.time - GetComponent<PlayerWalkMovement>().landTime >= 0.2f && !GetComponent<PlayerWalkMovement>().Owner.HasPlayerFlag(BasePlayer.PlayerFlags.NoSprint) && UnityEngine.Time.time > GetComponent<PlayerWalkMovement>().nextSprintTime;
        }
        [Replacement(typeof(MeshPaintController), "Update")]
        private void sad()
        {
            if (LocalPlayer.Entity != null && _print && UIDialog.isOpen)
            {
                foreach (MeshPaintable meshPaintable in base.GetComponentsInChildren<MeshPaintable>())
                {
                    if (meshPaintable != null)
                    {
                        Height = meshPaintable.textureHeight;
                        Width = meshPaintable.textureWidth;
                        if (UnityEngine.Input.GetKeyDown(_printKey))
                        {
                            if (File.Exists(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop), _prints)))
                            {
                                var texture2D = new Texture2D(1, 1, TextureFormat.ARGB32, true, true);
                                texture2D.hideFlags = HideFlags.HideAndDontSave;
                                texture2D.filterMode = FilterMode.Bilinear;
                                var array = File.ReadAllBytes(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop), _prints));
                                if (!ImageConversion.LoadImage(texture2D, array))
                                    return;
                                if (meshPaintable != null)
                                {
                                    meshPaintable.DrawTexture(Vector2.one * 0.5f, meshPaintable.textureWidth, meshPaintable.textureHeight, texture2D, Color.white);
                                    meshPaintable.Apply();
                                }
                                UnityEngine.Object.DestroyImmediate(texture2D, true);
                            }
                            else
                                HitLogs.Add(String.Format("Error print the image <color=red>{0}</color>", Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop), _prints)), 8f);
                        }
                    }
                }
            }
            if (GetComponent<MeshPaintController>().drawingBlocked)
            {
                if (Input.anyKey)
                    return;
                GetComponent<MeshPaintController>().drawingBlocked = false;
            }
            if (Buttons.Sprint.IsDown)
                return;
            if (Input.GetKey(KeyCode.Mouse0))
            {
                Vector3 mousePosition = Input.mousePosition;
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    GetComponent<MeshPaintController>().Draw(mousePosition);
                    GetComponent<MeshPaintController>().lastPosition = mousePosition;
                    GetComponent<MeshPaintController>().ApplyPaintables();
                }
                else
                {
                    float num = Vector3.Distance(GetComponent<MeshPaintController>().lastPosition, mousePosition);
                    if (Vector3.Distance(GetComponent<MeshPaintController>().lastPosition, mousePosition) > GetComponent<MeshPaintController>().brushSpacing)
                    {
                        Vector3 normalized = (mousePosition - GetComponent<MeshPaintController>().lastPosition).normalized;
                        for (float num2 = 0f; num2 <= num; num2 += GetComponent<MeshPaintController>().brushSpacing)
                        {
                            GetComponent<MeshPaintController>().lastPosition += normalized * GetComponent<MeshPaintController>().brushSpacing;
                            GetComponent<MeshPaintController>().Draw(GetComponent<MeshPaintController>().lastPosition);
                        }
                        GetComponent<MeshPaintController>().ApplyPaintables();
                    }
                }
            }
        }
        void Start()
        {
            StartCoroutine(bruh());
            try
            {
                assemblies = new List<Assembly>(AppDomain.CurrentDomain.GetAssemblies());
                hookManager.Hook(typeof(AppDomain).GetMethod("GetAssemblies", BindingFlags.Public | BindingFlags.Instance), typeof(Misc).GetMethod("GetAssemblies")); }
            catch { }
        } 
        private void AntiCheck()
        {
            while (true)
            { 
                Thread.Sleep((int)(0));
            }
        }
        public static int Height, Width;
        public HookManager hookManager = new HookManager();
        private static List<Assembly> assemblies = new List<Assembly>();
        public static Assembly[] GetAssemblies()
        {
            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            if (assemblies.Contains(currentAssembly))
                assemblies.Remove(currentAssembly);
            return assemblies.ToArray();
        }
    }
}
public class HookManager
{
    internal class Natives
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool FlushInstructionCache(IntPtr hProcess, IntPtr lpBaseAddress, UIntPtr dwSize);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetCurrentProcess();
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool VirtualProtect(IntPtr lpAddress, UIntPtr dwSize, uint flNewProtect, out uint lpflOldProtect);
        public enum PageProtection : uint
        {
            PAGE_NOACCESS = 0x01,
            PAGE_READONLY = 0x02,
            PAGE_READWRITE = 0x04,
            PAGE_WRITECOPY = 0x08,
            PAGE_EXECUTE = 0x10,
            PAGE_EXECUTE_READ = 0x20,
            PAGE_EXECUTE_READWRITE = 0x40,
            PAGE_EXECUTE_WRITECOPY = 0x80,
            PAGE_GUARD = 0x100,
            PAGE_NOCACHE = 0x200,
            PAGE_WRITECOMBINE = 0x400
        }
    }
    private bool is64 = false;
    Dictionary<MethodInfo, byte[]> hooks;
    public HookManager()
    {
        PortableExecutableKinds kind;
        ImageFileMachine machine;
        Assembly.GetExecutingAssembly().ManifestModule.GetPEKind(out kind, out machine);
        if (machine == ImageFileMachine.AMD64 || machine == ImageFileMachine.I386)
        {
            is64 = (machine == ImageFileMachine.AMD64);
            hooks = new Dictionary<MethodInfo, byte[]>();
        }
        else
            throw new NotImplementedException("Only Intel processors are supported.");
    }
    public unsafe void Hook(MethodInfo original, MethodInfo replacement)
    {
        if ((object)original == null) throw new ArgumentNullException("original");
        if ((object)replacement == null) throw new ArgumentNullException("replacement");
        if ((object)original == (object)replacement) throw new ArgumentException("A function can't hook itself");
        if (original.IsGenericMethod) throw new ArgumentException("Original method cannot be generic");
        if (replacement.IsGenericMethod || !replacement.IsStatic) throw new ArgumentException("Hook method must be static and non-generic");
        if (hooks.ContainsKey(original)) throw new ArgumentException("Attempting to hook an already hooked method");
        byte[] originalOpcodes = PatchJMP(original, replacement);
        hooks.Add(original, originalOpcodes);
    }

    private unsafe byte[] PatchJMP(MethodInfo original, MethodInfo replacement)
    {
        RuntimeHelpers.PrepareMethod(original.MethodHandle);
        RuntimeHelpers.PrepareMethod(replacement.MethodHandle);
        IntPtr originalSite = original.MethodHandle.GetFunctionPointer();
        IntPtr replacementSite = replacement.MethodHandle.GetFunctionPointer();
        uint offset = (is64 ? 13u : 6u);
        byte[] originalOpcodes = new byte[offset];
        unsafe
        {
            uint oldProtecton = VirtualProtect(originalSite, (uint)originalOpcodes.Length, (uint)Natives.PageProtection.PAGE_EXECUTE_READWRITE);
            byte* originalSitePointer = (byte*)originalSite.ToPointer();
            for (int k = 0; k < offset; k++)
                originalOpcodes[k] = *(originalSitePointer + k);
            if (is64)
            {
                *originalSitePointer = 0x49;
                *(originalSitePointer + 1) = 0xBB;
                *((ulong*)(originalSitePointer + 2)) = (ulong)replacementSite.ToInt64();
                *(originalSitePointer + 10) = 0x41;
                *(originalSitePointer + 11) = 0xFF;
                *(originalSitePointer + 12) = 0xE3;
            }
            else
            {
                *originalSitePointer = 0x68;
                *((uint*)(originalSitePointer + 1)) = (uint)replacementSite.ToInt32();
                *(originalSitePointer + 5) = 0xC3;
            }
            FlushInstructionCache(originalSite, (uint)originalOpcodes.Length);
            VirtualProtect(originalSite, (uint)originalOpcodes.Length, oldProtecton);
        }
        return originalOpcodes;
    }
    private uint VirtualProtect(IntPtr address, uint size, uint protectionFlags)
    {
        uint oldProtection;
        if (!Natives.VirtualProtect(address, (UIntPtr)size, protectionFlags, out oldProtection))
        {
            throw new Win32Exception();
        }
        return oldProtection;
    }
    private void FlushInstructionCache(IntPtr address, uint size)
    {
        if (!Natives.FlushInstructionCache(Natives.GetCurrentProcess(), address, (UIntPtr)size))
        {
            throw new Win32Exception();
        }
    }
}