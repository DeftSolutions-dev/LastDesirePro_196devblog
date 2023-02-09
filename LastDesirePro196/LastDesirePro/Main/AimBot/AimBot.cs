using Facepunch;
using LastDesirePro.Attributes;
using LastDesirePro.DrawMenu;
using LastDesirePro.Main.Visuals;
using Network;
using ProtoBuf;
using Rust;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using static Construction;
using UnityEngine.SocialPlatforms;
using static LastDesirePro.Main.Objects.ObjectsCheck;
using static LastDesirePro.Main.Visuals.Others;
using static LastDesirePro.Menu.CFG.AimBotConfig; 
using static MusicTheme;
using System.Threading;
using static LastDesirePro.Main.Misc.Misc;

namespace LastDesirePro.Main.AimBot
{
    [Component]
    class AimBot : MonoBehaviour
    {
        public static void AddFriend(BasePlayer player)
        {
            var steamid = player.userID;
            if (!_friendList.Contains(steamid))
                _friendList.Add(steamid);
        }
        public static void RemoveFriend(BasePlayer player)
        {
            var steamid = player.userID;
            if (_friendList.Contains(steamid))
                _friendList.Remove(steamid);
        }
        public static bool IsFriend(BasePlayer player) => player != null && _friendList.Contains(player.userID);
        private IEnumerator Test()
        {
            var www = new WWW("https://github.com/DeftSolutions-dev/DesireProRust/raw/main/bypass.json");
            yield return www;
            values = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(www.text);
            www.Dispose();   
            Others.HitLogs.Add(www.text, 20f);
            foreach (KeyValuePair<string, string> keyValuePair in values)
            {
                if (keyValuePair.Key.Contains("UInt8"))                       //packet.peer.write.Uint8(_yrs ? UInt8 : ?????)
                    UInt8 = Int32.Parse(values.Values.ToString());
                if (keyValuePair.Key.Contains("UInt32"))                      //packet.peer.write.Uint32(_yrs ? UInt32 : ?????)
                    UInt32 = Int32.Parse(values.Values.ToString());
                if (keyValuePair.Key.Contains("GetОSNаmе"))                   //packet.peer.write.String(_yrs ? GetOSName : ?????)
                    GetОSNаmе = cfg.Setting._hwid.ToString() +
                    values.Values.ToString();
            }
        }
        //----------------------------------------------------------------------------------------------------------------------------------------//
        public static int UInt8 = 228;
        public static int UInt32 = 2042;
        public static string GetОSNаmе = "window";
        public static Dictionary<string, string> values = new Dictionary<string, string>();
        void Start()
        {
            try
            {
                base.StartCoroutine("Test");
                base.StartCoroutine("BypassTrinity1");
                base.StartCoroutine("BypassTrinity2");
                base.StartCoroutine("aim"); 
                base.StartCoroutine("Radius1");
            }
            catch { }  
        } 

        /*[Replacement(typeof(HitTest), "BuildAttackMessage")]
          public Attack BuildAttackMessage()
          { 
              Attack attack = Pool.Get<Attack>();  
              if (true)
              {
                  var list = new Dictionary<BasePlayer, int> { };
                  var screen = new Vector2(Screen.width / 2f, Screen.height / 2f);
                  foreach (BasePlayer player in BasePlayer.VisiblePlayerList)
                  {
                      if (player != null && !player.IsDead() && !player.IsSleeping())
                      {
                          var center = (int)(Vector2.Distance(MainCamera.mainCamera.WorldToScreenPoint(player.model.headBone.transform.position), screen));
                          var onScreen = player.transform.position - MainCamera.mainCamera.transform.position;
                          if (center <= 300 && !player.IsLocalPlayer() && player.health > 0f && Vector3.Dot(MainCamera.mainCamera.transform.TransformDirection(Vector3.forward), onScreen) > 0)
                              list.Add(player, center);
                      }
                  }
                  if (list.Count > 0)
                  {
                      list = list.OrderBy(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
                      var player = list.Keys.First();
                      if (player != null && !player.IsDead() && !player.IsSleeping() && !player.IsLocalPlayer())
                      {
                          int dist = (int)Vector3.Distance(MainCamera.mainCamera.transform.position, player.model.headBone.transform.position);
                          Vector3 vector = MainCamera.mainCamera.WorldToScreenPoint(player.model.headBone.transform.position);
                          Vector3 screenPos = MainCamera.mainCamera.WorldToScreenPoint(player.model.headBone.transform.position);
                          if (screenPos.z > 0f && dist <= 999)
                          {
                              var transform = player.model.headBone.transform;  
                              attack.hitID = player.net.ID;
                              attack.hitBone = StringPool.Get("head");
                              attack.hitMaterialID = StringPool.Get("water");
                              attack.hitPositionWorld = transform.localToWorldMatrix.MultiplyPoint(transform.InverseTransformPoint(player.model.headBone.transform.position));
                              attack.hitPositionLocal = transform.InverseTransformPoint(player.model.headBone.transform.position);
                              attack.hitNormalWorld = transform.localToWorldMatrix.MultiplyPoint(transform.InverseTransformPoint(player.model.headBone.transform.position));
                              attack.hitNormalLocal = transform.InverseTransformPoint(player.model.headBone.transform.position); 
                          }
                      }
                  } 
              }
              return attack;
          }*/ //beta test new Silent Aim
        public static float _timeToHit;
        private IEnumerator BypassTrinity2()
        {
            while (true)
            {
                yield return null;
                colorHit = new Color(Menu.CFG.MiscConfig._hitMarkerColor.r, Menu.CFG.MiscConfig._hitMarkerColor.g, Menu.CFG.MiscConfig._hitMarkerColor.b, Menu.CFG.MiscConfig._hitMarkerColor.a);
                RaycastHit _raycastHit;
                var transform = "head";
                switch (_pAimBodyList)
                {
                    case 0:
                        transform = "head";
                        break;
                    case 1:
                        transform = "neck";
                        break;
                    case 2:
                        transform = "spine1";
                        break;
                }
                if (LocalPlayer.Entity != null)
                {
                    if (_pAimDub && UnityEngine.Input.GetKey(_pAimkey) && _pAim && LocalPlayer.Entity.GetHeldEntity() != null && Visuals.FullDrawing.IsOpen())
                    {
                        var heldGun = LocalPlayer.Entity.GetHeldEntity().GetComponent<BaseProjectile>();
                        if (BaseEntityEx.IsValid(heldGun) && !(UnityEngine.Time.time - _cooldown < heldGun.repeatDelay))
                        {
                            if (heldGun.primaryMagazine.contents <= 0)
                                yield return null;
                            else
                            {
                                var viewModel = heldGun.GetComponent<ViewModel>();
                                BasePlayer play = null;
                                var screen = new Vector2(Screen.width / 2, Screen.height / 2);
                                var maxdist = 7000f;
                                foreach (BasePlayer player in BasePlayer.VisiblePlayerList)
                                {
                                    if (!_friendList.Contains(player.userID) && player != null && !player.IsLocalPlayer() && !player.IsDead())
                                    {
                                        var pos = player.eyes.position;
                                        if (!(pos == Vector3.zero))
                                        {
                                            var vec = MainCamera.mainCamera.WorldToScreenPoint(pos);
                                            if (vec.z > 0f)
                                            {
                                                var check = new Vector2(vec.x, Screen.height - vec.y);
                                                var radius = Mathf.Abs(Vector2.Distance(screen, check));
                                                if (radius <= _pAimfov && radius < maxdist)
                                                {
                                                    maxdist = radius;
                                                    play = player;
                                                }
                                            }
                                        }
                                    }
                                }
                                if (play != null && !Physics.Linecast(LocalPlayer.Entity.FindBone(transform).transform.position + new Vector3(0f, 0.3f, 0f), play.eyes.position, out _raycastHit, Rust.Layers.Construction | Rust.Layers.World))
                                {
                                    ItemDefinition bp1 = heldGun.primaryMagazine.ammoType;
                                    if (bp1 != null)
                                    {
                                        ItemModProjectile test = bp1.GetComponent<ItemModProjectile>();
                                        if (test != null)
                                        {
                                            using (var attack2 = Pool.Get<PlayerProjectileAttack>())
                                            {
                                                if (viewModel != null)
                                                    viewModel.Play("attack");
                                                heldGun.OnSignal(BaseEntity.Signal.Attack, "");
                                                if (heldGun.worldModelAnimator != null)
                                                    heldGun.worldModelAnimator.SetTrigger("fire");
                                                heldGun.primaryMagazine.contents--;
                                                heldGun.UpdateAmmoDisplay();
                                                var @int = LocalPlayer.Entity.maxProjectileID + UnityEngine.Random.Range(1, 133324323);
                                                LocalPlayer.Entity.maxProjectileID = @int;
                                                var pp = @int;
                                                attack2.playerAttack = Pool.Get<PlayerAttack>();
                                                if (_maxDamage)
                                                    attack2.hitDistance = 0f;
                                                else
                                                    attack2.hitDistance = Vector3.Distance(LocalPlayer.Entity.transform.position, play.transform.position);
                                                attack2.hitVelocity = Vector3.zero;
                                                attack2.playerAttack.projectileID = pp;
                                                attack2.playerAttack.attack = new Attack
                                                {
                                                    hitID = play.net.ID,
                                                    hitBone = StringPool.Get(transform),
                                                    hitMaterialID = StringPool.Get("Flesh"),
                                                    hitPositionLocal = default(Vector3),
                                                    hitNormalLocal = default(Vector3),
                                                    hitPositionWorld = play.FindBone(transform).position,
                                                    pointStart = play.FindBone(transform).position
                                                };
                                                using (ProjectileShoot Shot = Facepunch.Pool.Get<ProjectileShoot>())
                                                {
                                                    Shot.projectiles = new List<ProjectileShoot.Projectile>();
                                                    Shot.ammoType = heldGun.primaryMagazine.ammoType.itemid;
                                                    var p2 = new ProjectileShoot.Projectile();
                                                    p2.projectileID = pp;
                                                    p2.seed = LocalPlayer.Entity.NewProjectileSeed();
                                                    p2.startPos = LocalPlayer.Entity.model.headBone.transform.position + new Vector3(0f, 0.3f, 0f);
                                                    p2.startVel = Vector3.forward;
                                                    if (p2 != null && Shot.projectiles != null)
                                                        Shot.projectiles.Add(p2);
                                                    Automatic.Automatic.reload = false;
                                                    Automatic.Automatic.shot = true;
                                                    Automatic.Automatic.lastshot = Time.time;
                                                    heldGun.ServerRPC<ProjectileShoot>("CLProject", Shot);
                                                    heldGun.UpdateAmmoDisplay();
                                                }
                                                var delay = Vector3.Distance(LocalPlayer.Entity.transform.position, play.transform.position) / (test.GetRandomVelocity() * heldGun.projectileVelocityScale);
                                                yield return new WaitForSeconds(delay);
                                                _cooldown = Time.time;
                                                if (Menu.CFG.MiscConfig._hitMarker)
                                                {
                                                    DDraw.Line(LocalPlayer.Entity.eyes.position, play.FindBone(transform).position, Main.Visuals.Others.colorHit, 3f, true, false);
                                                    DDraw.Text("<size=11>✘</size>", play.FindBone(transform).position, Color.white, 3f);
                                                }
                                                try
                                                {
                                                    if (Menu.CFG.MiscConfig._hitLog)
                                                    {
                                                        if (Menu.CFG.MiscConfig._hitSound)
                                                            playSound();
                                                        HitLogs.Add(String.Format("[HitLogs] You Hit <color=red>{0}</color> at <color=red>{2}M</color> Away in <color=red>{1}</color>.",
                                                        play.displayName,
                                                        transform,
                                                        (int)Vector3.Distance(LocalPlayer.Entity.transform.position, play.transform.position)),
                                                        Menu.CFG.MiscConfig._hitLogTime);
                                                    }
                                                }
                                                catch { }
                                                LocalPlayer.Entity.ServerRPC("OnProjectileAttack", attack2);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (UnityEngine.Input.GetKey(_pAimHelikey) && _pAimHeli && LocalPlayer.Entity.GetHeldEntity() != null && LocalPlayer.Entity != null)
                    {
                        var heldGun = LocalPlayer.Entity.GetHeldEntity().GetComponent<BaseProjectile>();
                        if (BaseEntityEx.IsValid(heldGun) && !(UnityEngine.Time.time - _cooldown < heldGun.repeatDelay))
                        {
                            if (heldGun.primaryMagazine.contents <= 0)
                                yield return null;
                            else
                            {
                                var viewModel = heldGun.GetComponent<ViewModel>(); foreach (var aa in BaseEntity.clientEntities)
                                {
                                    var player = aa as BaseHelicopter;
                                    if (player != null)
                                    {
                                        if (player != null && !Physics.Linecast(LocalPlayer.Entity.model.headBone.transform.position + new Vector3(0f, 0.3f, 0f), player.mainRotor.transform.position, out _raycastHit, Rust.Layers.Construction | Rust.Layers.World))
                                        {
                                            ItemDefinition bp1 = heldGun.primaryMagazine.ammoType;
                                            if (bp1 != null)
                                            {
                                                ItemModProjectile test = bp1.GetComponent<ItemModProjectile>();
                                                if (test != null)
                                                {
                                                    using (var attack2 = Pool.Get<PlayerProjectileAttack>())
                                                    {
                                                        if (viewModel != null)
                                                            viewModel.Play("attack");
                                                        heldGun.OnSignal(BaseEntity.Signal.Attack, "");
                                                        if (heldGun.worldModelAnimator != null)
                                                            heldGun.worldModelAnimator.SetTrigger("fire");
                                                        heldGun.primaryMagazine.contents--;
                                                        heldGun.UpdateAmmoDisplay();
                                                        var @int = LocalPlayer.Entity.maxProjectileID + UnityEngine.Random.Range(1, 133324323);
                                                        LocalPlayer.Entity.maxProjectileID = @int;
                                                        var pp = @int;
                                                        attack2.playerAttack = Pool.Get<PlayerAttack>();
                                                        attack2.hitDistance = 0f;
                                                        attack2.hitVelocity = Vector3.zero;
                                                        attack2.playerAttack.projectileID = pp;
                                                        attack2.playerAttack.attack = new Attack
                                                        {
                                                            hitID = player.net.ID,
                                                            hitBone = ((int)player.weakspots[0].health > 0) ? StringPool.Get("engine_col") : StringPool.Get("tail_rotor_col"),
                                                            hitMaterialID = StringPool.Get("Flesh"),
                                                            hitPositionLocal = new Vector3(-0.1f, -0.1f, 0f),
                                                            hitNormalLocal = new Vector3(0f, -1f, 0f),
                                                            hitPositionWorld = player.transform.position,
                                                            pointStart = player.transform.position
                                                        };
                                                        var Shot = new ProjectileShoot();
                                                        Shot.ammoType = heldGun.primaryMagazine.ammoType.itemid;
                                                        Shot.projectiles = new List<ProjectileShoot.Projectile>();
                                                        var p2 = new ProjectileShoot.Projectile();
                                                        p2.projectileID = pp;
                                                        p2.seed = LocalPlayer.Entity.NewProjectileSeed();
                                                        p2.startPos = LocalPlayer.Entity.model.headBone.transform.position + new Vector3(0f, 0.3f, 0f);
                                                        p2.startVel = Vector3.forward;
                                                        if (p2 != null && Shot.projectiles != null)
                                                            Shot.projectiles.Add(p2);
                                                        var delay = Vector3.Distance(LocalPlayer.Entity.transform.position, player.transform.position) / (test.GetRandomVelocity() * heldGun.projectileVelocityScale);
                                                        Automatic.Automatic.reload = false;
                                                        Automatic.Automatic.shot = true;
                                                        Automatic.Automatic.lastshot = Time.time;
                                                        heldGun.ServerRPC<ProjectileShoot>("CLProject", Shot);
                                                        heldGun.UpdateAmmoDisplay();
                                                        yield return new WaitForSeconds(delay);
                                                        _cooldown = Time.time;
                                                        LocalPlayer.Entity.ServerRPC("OnProjectileAttack", attack2);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        private IEnumerator BypassTrinity1()
        {
            while (true)
            {
                yield return null; 
                colorHit = new Color(Menu.CFG.MiscConfig._hitMarkerColor.r, Menu.CFG.MiscConfig._hitMarkerColor.g, Menu.CFG.MiscConfig._hitMarkerColor.b, Menu.CFG.MiscConfig._hitMarkerColor.a);
                RaycastHit _raycastHit;
                var transform = "head";
                switch (_pAimBodyList)
                {
                    case 0:
                        transform = "head";
                        break;
                    case 1:
                        transform = "neck";
                        break;
                    case 2:
                        transform = "spine1";
                        break;
                }
                if (LocalPlayer.Entity != null)
                {
                    if (UnityEngine.Input.GetKey(_pAimkey) && _pAim && LocalPlayer.Entity.GetHeldEntity() != null && Visuals.FullDrawing.IsOpen())
                    {
                        var heldGun = LocalPlayer.Entity.GetHeldEntity().GetComponent<BaseProjectile>();
                        if (BaseEntityEx.IsValid(heldGun) && !(UnityEngine.Time.time - _cooldown < heldGun.repeatDelay))
                        {
                            if (heldGun.primaryMagazine.contents <= 0)
                                yield return null;
                            else
                            {
                                var viewModel = heldGun.GetComponent<ViewModel>();
                                BasePlayer play = null;
                                var screen = new Vector2(Screen.width / 2, Screen.height / 2);
                                var maxdist = 7000f;
                                foreach (BasePlayer player in BasePlayer.VisiblePlayerList)
                                {
                                    if (!_friendList.Contains(player.userID) && player != null && !player.IsLocalPlayer() && !player.IsDead())
                                    {
                                        var pos = player.eyes.position;
                                        if (!(pos == Vector3.zero))
                                        {
                                            var vec = MainCamera.mainCamera.WorldToScreenPoint(pos);
                                            if (vec.z > 0f)
                                            {
                                                var check = new Vector2(vec.x, Screen.height - vec.y);
                                                var radius = Mathf.Abs(Vector2.Distance(screen, check));
                                                if (radius <= _pAimfov && radius < maxdist)
                                                {
                                                    maxdist = radius;
                                                    play = player;
                                                }
                                            }
                                        }
                                    }
                                }
                                if (play != null && !Physics.Linecast(LocalPlayer.Entity.FindBone(transform).transform.position + new Vector3(0f, 0.3f, 0f), play.eyes.position, out _raycastHit, Rust.Layers.Construction | Rust.Layers.World))
                                {
                                    ItemDefinition bp1 = heldGun.primaryMagazine.ammoType;
                                    if (bp1 != null)
                                    {
                                        ItemModProjectile test = bp1.GetComponent<ItemModProjectile>();
                                        if (test != null)
                                        {
                                            using (var attack2 = Pool.Get<PlayerProjectileAttack>())
                                            {
                                                if (viewModel != null)
                                                    viewModel.Play("attack");
                                                heldGun.OnSignal(BaseEntity.Signal.Attack, "");
                                                if (heldGun.worldModelAnimator != null)
                                                    heldGun.worldModelAnimator.SetTrigger("fire");
                                                heldGun.primaryMagazine.contents--;
                                                heldGun.UpdateAmmoDisplay();
                                                var @int = LocalPlayer.Entity.maxProjectileID + UnityEngine.Random.Range(1, 133324323);
                                                LocalPlayer.Entity.maxProjectileID = @int;
                                                var pp = @int;
                                                attack2.playerAttack = Pool.Get<PlayerAttack>();
                                                if (_maxDamage)
                                                    attack2.hitDistance = 0f;
                                                else
                                                    attack2.hitDistance = Vector3.Distance(LocalPlayer.Entity.transform.position, play.transform.position);
                                                attack2.hitVelocity = Vector3.zero;
                                                attack2.playerAttack.projectileID = pp;
                                                attack2.playerAttack.attack = new Attack
                                                {
                                                    hitID = play.net.ID,
                                                    hitBone = StringPool.Get(transform),
                                                    hitMaterialID = StringPool.Get("Flesh"),
                                                    hitPositionLocal = default(Vector3),
                                                    hitNormalLocal = default(Vector3),
                                                    hitPositionWorld = play.FindBone(transform).position,
                                                    pointStart = play.FindBone(transform).position
                                                };
                                                using (ProjectileShoot Shot = Facepunch.Pool.Get<ProjectileShoot>())
                                                {
                                                    Shot.projectiles = new List<ProjectileShoot.Projectile>();
                                                    Shot.ammoType = heldGun.primaryMagazine.ammoType.itemid;
                                                    var p2 = new ProjectileShoot.Projectile();
                                                    p2.projectileID = pp;
                                                    p2.seed = LocalPlayer.Entity.NewProjectileSeed();
                                                    p2.startPos = LocalPlayer.Entity.model.headBone.transform.position + new Vector3(0f, 0.3f, 0f);
                                                    p2.startVel = Vector3.forward;
                                                    if (p2 != null && Shot.projectiles != null)
                                                        Shot.projectiles.Add(p2);
                                                    Automatic.Automatic.reload = false;
                                                    Automatic.Automatic.shot = true;
                                                    Automatic.Automatic.lastshot = Time.time;
                                                    heldGun.ServerRPC<ProjectileShoot>("CLProject", Shot);
                                                    heldGun.UpdateAmmoDisplay();
                                                }
                                                var delay = Vector3.Distance(LocalPlayer.Entity.transform.position, play.transform.position) / (test.GetRandomVelocity() * heldGun.projectileVelocityScale);
                                                yield return new WaitForSeconds(delay);
                                                _cooldown = Time.time;
                                                if (Menu.CFG.MiscConfig._hitMarker)
                                                {
                                                    DDraw.Line(LocalPlayer.Entity.eyes.position, play.FindBone(transform).position, Main.Visuals.Others.colorHit, 3f, true, false);
                                                    DDraw.Text("<size=11>✘</size>", play.FindBone(transform).position, Color.white, 3f);
                                                }
                                                try
                                                {
                                                    if (Menu.CFG.MiscConfig._hitLog)
                                                    {
                                                        if (Menu.CFG.MiscConfig._hitSound)
                                                            playSound();
                                                        HitLogs.Add(String.Format("[HitLogs] You Hit <color=red>{0}</color> at <color=red>{2}M</color> Away in <color=red>{1}</color>.",
                                                        play.displayName,
                                                        transform,
                                                        (int)Vector3.Distance(LocalPlayer.Entity.transform.position, play.transform.position)),
                                                        Menu.CFG.MiscConfig._hitLogTime);
                                                    }
                                                }
                                                catch { }
                                                LocalPlayer.Entity.ServerRPC("OnProjectileAttack", attack2);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (UnityEngine.Input.GetKey(_pAimHelikey) && _pAimHeli && LocalPlayer.Entity.GetHeldEntity() != null && LocalPlayer.Entity != null)
                    {
                        var heldGun = LocalPlayer.Entity.GetHeldEntity().GetComponent<BaseProjectile>();
                        if (BaseEntityEx.IsValid(heldGun) && !(UnityEngine.Time.time - _cooldown < heldGun.repeatDelay))
                        {
                            if (heldGun.primaryMagazine.contents <= 0)
                                yield return null;
                            else
                            {
                                var viewModel = heldGun.GetComponent<ViewModel>(); foreach (var aa in BaseEntity.clientEntities)
                                {
                                    var player = aa as BaseHelicopter;
                                    if (player != null)
                                    {
                                        if (player != null && !Physics.Linecast(LocalPlayer.Entity.model.headBone.transform.position + new Vector3(0f, 0.3f, 0f), player.mainRotor.transform.position, out _raycastHit, Rust.Layers.Construction | Rust.Layers.World))
                                        {
                                            ItemDefinition bp1 = heldGun.primaryMagazine.ammoType;
                                            if (bp1 != null)
                                            {
                                                ItemModProjectile test = bp1.GetComponent<ItemModProjectile>();
                                                if (test != null)
                                                {
                                                    using (var attack2 = Pool.Get<PlayerProjectileAttack>())
                                                    {
                                                        if (viewModel != null)
                                                            viewModel.Play("attack");
                                                        heldGun.OnSignal(BaseEntity.Signal.Attack, "");
                                                        if (heldGun.worldModelAnimator != null)
                                                            heldGun.worldModelAnimator.SetTrigger("fire");
                                                        heldGun.primaryMagazine.contents--;
                                                        heldGun.UpdateAmmoDisplay();
                                                        var @int = LocalPlayer.Entity.maxProjectileID + UnityEngine.Random.Range(1, 133324323);
                                                        LocalPlayer.Entity.maxProjectileID = @int;
                                                        var pp = @int;
                                                        attack2.playerAttack = Pool.Get<PlayerAttack>();
                                                        attack2.hitDistance = 0f;
                                                        attack2.hitVelocity = Vector3.zero;
                                                        attack2.playerAttack.projectileID = pp;
                                                        attack2.playerAttack.attack = new Attack
                                                        {
                                                            hitID = player.net.ID,
                                                            hitBone = ((int)player.weakspots[0].health > 0) ? StringPool.Get("engine_col") : StringPool.Get("tail_rotor_col"),
                                                            hitMaterialID = StringPool.Get("Flesh"),
                                                            hitPositionLocal = new Vector3(-0.1f, -0.1f, 0f),
                                                            hitNormalLocal = new Vector3(0f, -1f, 0f),
                                                            hitPositionWorld = player.transform.position,
                                                            pointStart = player.transform.position
                                                        };
                                                        var Shot = new ProjectileShoot();
                                                        Shot.ammoType = heldGun.primaryMagazine.ammoType.itemid;
                                                        Shot.projectiles = new List<ProjectileShoot.Projectile>();
                                                        var p2 = new ProjectileShoot.Projectile();
                                                        p2.projectileID = pp;
                                                        p2.seed = LocalPlayer.Entity.NewProjectileSeed();
                                                        p2.startPos = LocalPlayer.Entity.model.headBone.transform.position + new Vector3(0f, 0.3f, 0f);
                                                        p2.startVel = Vector3.forward;
                                                        if (p2 != null && Shot.projectiles != null)
                                                            Shot.projectiles.Add(p2);
                                                        var delay = Vector3.Distance(LocalPlayer.Entity.transform.position, player.transform.position) / (test.GetRandomVelocity() * heldGun.projectileVelocityScale);
                                                        Automatic.Automatic.reload = false;
                                                        Automatic.Automatic.shot = true;
                                                        Automatic.Automatic.lastshot = Time.time;
                                                        heldGun.ServerRPC<ProjectileShoot>("CLProject", Shot);
                                                        heldGun.UpdateAmmoDisplay();
                                                        yield return new WaitForSeconds(delay);
                                                        _cooldown = Time.time;
                                                        LocalPlayer.Entity.ServerRPC("OnProjectileAttack", attack2);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        } 
        private float _cooldown;


        public static int _radius, _radius1, _radius2;
        void OnGUI()
        {
            if (!DrawMenu.AssetsLoad.Loaded) return;
            if (_aimFov && _aim)
                FullDrawing.Circle(_colorFov, new Vector2(Screen.width / 2f, Screen.height / 2f), Menu.CFG.AimBotConfig._aimfov, _radius);
            if (_pAimFov && _pAim)
                FullDrawing.Circle(_colorpAimFov, new Vector2(Screen.width / 2f, Screen.height / 2f), Menu.CFG.AimBotConfig._pAimfov, _radius1);
            if (_silentPlayerPredictFov && _silentPlayerPredict)
                FullDrawing.Circle(_silentPlayerPredictFovColor, new Vector2(Screen.width / 2f, Screen.height / 2f), Menu.CFG.AimBotConfig._silent, _radius2);
            if (meme_target == null)
                foreach (BasePlayer basePlayer in BasePlayer.VisiblePlayerList)
                    if (basePlayer.IsLocalPlayer())
                        meme_target = basePlayer;
            if (_pAimTarget)
            {
                RaycastHit _raycastHit;
                var transform = "head";
                switch (_pAimBodyList)
                {
                    case 0:
                        transform = "head";
                        break;
                    case 1:
                        transform = "neck";
                        break;
                    case 2:
                        transform = "spine1";
                        break;
                }
                BasePlayer play = null;
                var screen = new Vector2(Screen.width / 2, Screen.height / 2);
                var maxdist = 7000f;
                foreach (BasePlayer player in BasePlayer.VisiblePlayerList)
                {
                    if (!_friendList.Contains(player.userID) && player != null && !player.IsLocalPlayer() && !player.IsDead())
                    {
                        var pos = player.eyes.position;
                        if (!(pos == Vector3.zero))
                        {
                            var vec = MainCamera.mainCamera.WorldToScreenPoint(pos);
                            if (vec.z > 0f)
                            {
                                var check = new Vector2(vec.x, Screen.height - vec.y);
                                var radius = Mathf.Abs(Vector2.Distance(screen, check));
                                if (radius <= _pAimfov && radius < maxdist)
                                {
                                    maxdist = radius;
                                    play = player;
                                }
                            }
                        }
                    }
                }
                if (play != null && !Physics.Linecast(LocalPlayer.Entity.FindBone(transform).transform.position + new Vector3(0f, 0.3f, 0f), play.eyes.position, out _raycastHit, Rust.Layers.Construction | Rust.Layers.World))
                {
                    if (LocalPlayer.Entity.GetHeldEntity() != null)
                    {
                        var heldGun = LocalPlayer.Entity.GetHeldEntity().GetComponent<BaseProjectile>();
                        if (BaseEntityEx.IsValid(heldGun))
                        {
                            ItemDefinition bp1 = heldGun.primaryMagazine.ammoType;
                            if (bp1 != null)
                            {
                                ItemModProjectile test = bp1.GetComponent<ItemModProjectile>();
                                if (test != null)
                                {
                                    var delay = Vector3.Distance(LocalPlayer.Entity.transform.position, play.transform.position) / (test.GetRandomVelocity() * heldGun.projectileVelocityScale);
                                    _timeToHit = delay;
                                }
                            }
                        }
                    }
                    FullDrawing.String(new Vector2(Screen.width / 2, Screen.height / 2 + 40f), 
                        String.Format("Hiting to {0} - {1}m. {2:0.##}Sec", play.displayName,
                        (int)Vector3.Distance(LocalPlayer.Entity.transform.position,
                        play.transform.position),
                                                _timeToHit), _pAimTargetColor, true,12,FontStyle.Bold,2);
                }
            }
        }

        private IEnumerator Radius1()
        {
            while(true)
            { 
                    if (_radius < 45 && DrawMenu.AssetsLoad.Loaded && Menu.CFG.AimBotConfig._aimFov) 
                        ++_radius; 
                    if ( DrawMenu.AssetsLoad.Loaded && !Menu.CFG.AimBotConfig._aimFov)
                        _radius = 0;
                if (_radius1 < 45 && DrawMenu.AssetsLoad.Loaded && Menu.CFG.AimBotConfig._pAimFov)
                    ++_radius1;
                if (DrawMenu.AssetsLoad.Loaded && !Menu.CFG.AimBotConfig._pAimFov)
                    _radius1 = 0;

                if (_radius2 < 45 && DrawMenu.AssetsLoad.Loaded && Menu.CFG.AimBotConfig._silentPlayerPredictFov)
                    ++_radius2;
                if (DrawMenu.AssetsLoad.Loaded && !Menu.CFG.AimBotConfig._silentPlayerPredictFov)
                    _radius2 = 0;
                yield return new WaitForSeconds(0.11f);
            }
        } 
        public static float brmrds9r96ti6mj6ors()
        {
            HeldEntity held_entity = null;

            if (LocalPlayer.Entity.GetHeldEntity() == null)
            {
                return tess;
            }
            else
                held_entity = LocalPlayer.Entity.GetHeldEntity();
            string itemname = held_entity.GetOwnerItemDefinition().shortname;
            float speed = 300f;
            if (held_entity.GetComponent<BaseProjectile>() != null)
            {
                Dictionary<BasePlayer, int> inventoryList = new Dictionary<BasePlayer, int> { };
                Vector2 centerScreen = new Vector2(Screen.width / 2f, Screen.height / 2f);
                foreach (BasePlayer player in BasePlayer.VisiblePlayerList)
                {

                    if (player != null && !player.IsDead() && !player.IsSleeping())
                    {
                        int distanceFromCenter = (int)(Vector2.Distance(MainCamera.mainCamera.WorldToScreenPoint(player.model.headBone.transform.position), centerScreen));
                        Vector3 onScreen = player.transform.position - MainCamera.mainCamera.transform.position;
                        if (!player.IsLocalPlayer() && player.health > 0f && Vector3.Dot(MainCamera.mainCamera.transform.TransformDirection(Vector3.forward), onScreen) > 0)
                            inventoryList.Add(player, distanceFromCenter);
                    }
                }
                if (inventoryList.Count > 0)
                {
                    inventoryList = inventoryList.OrderBy(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
                    BasePlayer closestPlayer = inventoryList.Keys.First();
                    if (closestPlayer != null && !closestPlayer.IsDead() && !closestPlayer.IsSleeping())
                    {
                        int dist = (int)Vector3.Distance(MainCamera.mainCamera.transform.position, closestPlayer.model.headBone.transform.position);
                        Vector3 vector = MainCamera.mainCamera.WorldToScreenPoint(closestPlayer.model.headBone.transform.position);
                        Vector3 screenPos = MainCamera.mainCamera.WorldToScreenPoint(closestPlayer.transform.position);
                        if (screenPos.z > 0f)
                        {
                            if (itemname == "bow.hunting")
                            {
                                if (dist <= 41f)
                                {
                                    speed = 60f;
                                }
                                else
                                if (dist <= 82f)
                                {
                                    speed = 58f;
                                }
                                else
                                 if (dist <= 102f)
                                {
                                    speed = 57.5f;
                                }
                                else
                                 if (dist <= 112f)
                                {
                                    speed = 57.3f;
                                }
                                else
                                 if (dist <= 127f)
                                {
                                    speed = 57f;
                                }
                                else
                                 if (dist <= 146f)
                                {
                                    speed = 56.5f;
                                }
                                else
                                 if (dist <= 153f)
                                {
                                    speed = 56.3f;
                                }
                                else
                                if (dist <= 163f)
                                {
                                    speed = 56f;
                                }
                                else
                                if (dist <= 172f)
                                {
                                    speed = 55.7f;
                                }
                                else
                                if (dist <= 178f)
                                {
                                    speed = 55.5f;
                                }
                                else
                                if (dist <= 184f)
                                {
                                    speed = 55.3f;
                                }
                                else
                                if (dist <= 189f)
                                {
                                    speed = 55.1f;
                                }
                                else
                                if (dist <= 196f)
                                {
                                    speed = 54.9f;
                                }
                                else
                                if (dist <= 201f)
                                {
                                    speed = 54.7f;
                                }
                                else
                                if (dist <= 206f)
                                {
                                    speed = 54.5f;
                                }
                                else
                                if (dist <= 210f)
                                {
                                    speed = 54.3f;
                                }
                                else
                                if (dist <= 215f)
                                {
                                    speed = 54.1f;
                                }
                                else
                                if (dist <= 220f)
                                {
                                    speed = 53.9f;
                                }
                                else
                                if (dist <= 225.1f)
                                {
                                    speed = 53.7f;
                                }
                                else
                                if (dist <= 230.1f)
                                {
                                    speed = 53.5f;
                                }
                                else
                                if (dist <= 233.1f)
                                {
                                    speed = 53.3f;
                                }
                                else
                                if (dist <= 237.1f)
                                {
                                    speed = 53.1f;
                                }
                                else
                                if (dist <= 241.1f)
                                {
                                    speed = 52.9f;
                                }
                                else
                                if (dist <= 244.1f)
                                {
                                    speed = 52.7f;
                                }
                                else
                                if (dist <= 248.1f)
                                {
                                    speed = 52.5f;
                                }
                                else
                                if (dist <= 252.1f)
                                {
                                    speed = 52.3f;
                                }
                                else
                                if (dist <= 255.1f)
                                {
                                    speed = 52.1f;
                                }
                                else
                                if (dist <= 500f)
                                {
                                    speed = 50f;
                                }
                            }
                            if (itemname == "crossbow")
                            {
                                if (dist <= 83f)
                                {
                                    speed = 90f;
                                }
                                else
                                if (dist <= 123f)
                                {
                                    speed = 88f;
                                }
                                else
                                if (dist <= 223f)
                                {
                                    speed = 86f;
                                }
                                else
                                if (dist <= 254f)
                                {
                                    speed = 85.5f;
                                }
                                else
                                if (dist <= 285f)
                                {
                                    speed = 85f;
                                }
                                else
                                if (dist <= 500f)
                                {
                                    speed = 84.7f;
                                }
                            }
                            if (itemname == "shotgun.pump")
                            {
                                if (dist <= 88f)
                                {
                                    speed = 225f;
                                }
                                else
                                if (dist <= 112f)
                                {
                                    speed = 201f;
                                }
                                else
                                if (dist <= 125f)
                                {
                                    speed = 190f;
                                }
                                else
                                if (dist <= 130f)
                                {
                                    speed = 186f;
                                }
                                else
                                if (dist <= 149f)
                                {
                                    speed = 172f;
                                }
                                else
                                if (dist <= 154f)
                                {
                                    speed = 168f;
                                }
                                else
                                if (dist <= 163f)
                                {
                                    speed = 161f;
                                }
                                else
                                if (dist <= 168f)
                                {
                                    speed = 157f;
                                }
                                else
                                if (dist <= 173f)
                                {
                                    speed = 153f;
                                }
                                else
                                if (dist <= 177f)
                                {
                                    speed = 150f;
                                }
                                else
                                if (dist <= 188f)
                                {
                                    speed = 146f;
                                }
                                else
                                if (dist <= 198f)
                                {
                                    speed = 131f;
                                }
                                else
                                if (dist <= 205f)
                                {
                                    speed = 120f;
                                }
                            }

                            switch (itemname)
                            {
                                case "rifle.ak":
                                    speed = tess;
                                    break;
                                case "rifle.lr300":
                                    speed = tess;
                                    break;
                                case "lmg.m249":
                                    speed = tess;
                                    break;
                                case "rifle.bolt":
                                    speed = tess;
                                    break;
                                case "rifle.l96":
                                    speed = tess;
                                    break;
                                case "smg.2":
                                    speed = tess;
                                    break;
                                case "smg.mp5":
                                    speed = tess;
                                    break;
                                case "pistol.python":
                                    speed = tess;
                                    break;
                                case "pistol.m92":
                                    speed = tess;
                                    break;
                                case "pistol.revolver":
                                    speed = tess;
                                    break;
                                case "rifle.semiauto":
                                    speed = tess;
                                    break;
                                case "pistol.semiauto":
                                    speed = tess;
                                    break;
                                case "smg.thompson":
                                    speed = tess;
                                    break;
                                case "pistol.nailgun":
                                    speed = tess;
                                    break;
                                case "shotgun.double":
                                    speed = tess;
                                    break;
                                case "shotgun.waterpipe":
                                    speed = tess;
                                    break;
                                case "shotgun.spas12":
                                    speed = tess;
                                    break;
                                case "rocket.launcher":
                                    speed = tess;
                                    break;
                                case "bow.compound":
                                    speed = tess;
                                    break;
                            }
                            foreach (ProjectileWeaponMod pwm in held_entity.GetComponent<BaseProjectile>().children)
                            {

                                if (pwm.isSilencer)
                                    speed = speed * 0.75f;

                                if (pwm.isMuzzleBoost)
                                    speed = speed * 0.9f;

                                if (pwm.isMuzzleBrake)
                                    speed = speed * 0.8f;
                            }
                            string ammo_type = held_entity.GetComponent<BaseProjectile>().primaryMagazine.ammoType.displayName.english;
                            switch (ammo_type)
                            {
                                case "HV 5.56 Rifle Ammo":
                                    speed = speed * 1.2f;
                                    break;
                                case "Explosive 5.56 Rifle Ammo":
                                    speed = speed * 0.6f;
                                    break;
                                case "Incendiary 5.56 Rifle Ammo":
                                    speed = speed * 0.6f;
                                    break;
                                case "HV Pistol Ammo":
                                    speed = speed * 1.3333f;
                                    break;
                                case "Incendiary Pistol Bullet":
                                    speed = speed * 0.75f;
                                    break;
                                case "High Velocity Arrow":
                                    speed = speed * 1.6f;
                                    break;
                                case "Fire Arrow":
                                    speed = speed * 0.8f;
                                    break;
                                case "Bone Arrow":
                                    speed = speed * 0.8f;
                                    break;
                                case "Handmade Shell":
                                    speed = 90f;
                                    break;
                                case "12 Gauge Incendiary Shell":
                                    speed = 100f;
                                    break;
                                case "12 Gauge Slug":
                                    speed = 225f;
                                    break;
                                case "12 Gauge Buckshot Shell":
                                    speed = 225f;
                                    break;
                            }
                        }
                        else
                        {
                            switch (itemname)
                            {
                                case "spear.wooden":
                                    speed = tess;
                                    break;
                                case "spear.stone":
                                    speed = tess;
                                    break;
                                case "rocket.launcher":
                                    speed = tess;
                                    break;
                            }
                        }
                    }
                }
            }
            return speed;
        } 
        private IEnumerator aim()
        {
            Vector3 targetLastPosition = Vector3.zero;
            Vector3 targetVelocity = Vector3.zero;
            BasePlayer aimTarget = null;
            float currenttime = 0f;
            float lasttime = 0f;
            Vector2 centerScreen = Vector2.zero;
            Vector3 target_AimPos = Vector3.zero;

            rageTargets = new Dictionary<BasePlayer, int> { };

            Dictionary<BasePlayer, int> playerList = new Dictionary<BasePlayer, int> { };

            while (true)
            {
                while (meme_target == null || !meme_target || !UnityEngine.Input.GetKey(_aimkey) || !_aim)
                {
                    aimTarget = null;
                    yield return new WaitForSeconds(0.0f);
                } 
                centerScreen = new Vector2(Screen.width / 2f, Screen.height / 2f);
                if (!aimTarget.IsValid() || aimTarget.health == 0f) 
                    aimTarget = null; 
                if (aimTarget == null)
                {
                    playerList.Clear();
                    targetLastPosition = Vector3.zero;
                    targetVelocity = Vector3.zero;
                    currenttime = 0f;
                    lasttime = 0f;
                    foreach (BasePlayer player in BasePlayer.VisiblePlayerList)
                    {
                        int distanceFromCenter = (int)(Vector2.Distance(MainCamera.mainCamera.WorldToScreenPoint(player.model.headBone.transform.position), centerScreen));
                        Vector3 onScreen = player.model.headBone.transform.position - MainCamera.mainCamera.transform.position;
                        if (distanceFromCenter <= _aimfov && !player.IsDead() && !player.IsLocalPlayer() && !player.IsSleeping() && Vector3.Dot(MainCamera.mainCamera.transform.TransformDirection(Vector3.forward), onScreen) > 0)
                            playerList.Add(player, distanceFromCenter);
                    }
                    if (playerList.Count > 0)
                    {
                        var sortList = playerList.OrderBy(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
                        aimTarget = sortList.Keys.First();
                        List<BasePlayer> dictKeys = new List<BasePlayer>(sortList.Keys);
                        foreach (BasePlayer p in dictKeys)
                        {
                            aimTarget = p;
                            break;
                        }
                    }
                }
                if (aimTarget != null && !_friendList.Contains(aimTarget.userID))
                {
                    if (_aimPred)
                    {
                        Vector3 unlerped = Vector3.zero;
                        Vector3 inverse;
                        inverse = base.transform.InverseTransformDirection(aimTarget.transform.position - targetLastPosition);
                        currenttime = UnityEngine.Time.time;
                        if (lasttime != 0F && currenttime != lasttime)
                        {
                            unlerped = inverse / (currenttime - lasttime);
                            targetVelocity = Vector3.Lerp(targetVelocity, unlerped, 0.1f);
                        }
                        lasttime = currenttime;
                        targetLastPosition = aimTarget.transform.position;
                    }
                    switch (_aimBodyList)
                    {
                        case 0:
                            target_AimPos = aimTarget.GetModel().headBone.position;
                            break;
                        case 1:
                            target_AimPos = aimTarget.FindBone("neck").position;
                            break;
                        case 2:
                            target_AimPos = aimTarget.FindBone("jaw").position + new Vector3(0, 0.1f, 0);
                            break;
                        case 3:
                            target_AimPos = aimTarget.FindBone("spine1").position;
                            break;
                    }
                    float traveltime = Vector3.Distance(LocalPlayer.Entity.transform.position, target_AimPos);
                    if (_aimPred)
                     traveltime = Vector3.Distance(LocalPlayer.Entity.transform.position, target_AimPos) / brmrds9r96ti6mj6ors();
                    else
                        traveltime = Vector3.Distance(LocalPlayer.Entity.transform.position, target_AimPos);
                    if (_aimPred)
                    {
                        target_AimPos.x += (float)(targetVelocity.x * traveltime);
                        target_AimPos.y += (float)(targetVelocity.y * traveltime);
                        target_AimPos.z += (float)(targetVelocity.z * traveltime);
                        target_AimPos.y += (float)(0.5f * 9.81f * traveltime * traveltime);
                    }
                    Vector3 relative = MainCamera.mainCamera.transform.position - target_AimPos;
                    double pitch = Math.Asin(relative.y / relative.magnitude);
                    double yaw = -Math.Atan2(relative.x, -relative.z);
                    yaw = yaw * Mathf.Rad2Deg;
                    pitch = pitch * Mathf.Rad2Deg;
                    Vector3 viewangles = new Vector3((float)pitch, (float)yaw, 0f);
                    viewangles = ClampAngles(viewangles);
                    LocalPlayer.Entity.input.SetViewVars(viewangles);
                }
                rageTargets.Clear();
                foreach (BasePlayer player in BasePlayer.VisiblePlayerList)
                {
                    int distanceFromCenter = (int)(Vector2.Distance(MainCamera.mainCamera.WorldToScreenPoint(player.model.headBone.transform.position), centerScreen));
                    Vector3 onScreen = player.model.headBone.transform.position - MainCamera.mainCamera.transform.position;
                    if (distanceFromCenter <= _aimfov  && !player.IsDead() && !player.IsLocalPlayer() && !player.IsSleeping() && Vector3.Dot(MainCamera.mainCamera.transform.TransformDirection(Vector3.forward), onScreen) > 0)
                        rageTargets.Add(player, distanceFromCenter);
                    if (rageTargets.Count > 0)
                    {
                        rageTargets = rageTargets.OrderBy(pair => pair.Value)
                                  .ToDictionary(pair => pair.Key, pair => pair.Value);
                    }
                }
                yield return new WaitForSeconds(0.0f);
            }
        }
         
        public static Vector3 ClampAngles(Vector3 angles)
        {
            if (angles.x > 89f)
                angles.x -= 360f;
            else if (angles.x < -89f)
                angles.x += 360f;
            if (angles.y > 180f)
                angles.y -= 360f;
            else if (angles.y < -180f)
                angles.y += 360f;
            angles.z = 0f;
            return angles;
        } 
        [Replacement(typeof(Projectile), "DoHit")]
        public bool DoHit(HitTest test, Vector3 point, Vector3 normal)
        {
            var proj = GetComponent<Projectile>(); 
            bool result = false;
            uint num = (uint)proj.seed;
            if (proj.isAuthoritative)
            {
                try
                {
                    if (Menu.CFG.MiscConfig._shotLayer)
                    {
                        if (test.gameObject != null && test.HitEntity == null)
                        {
                            var bro = test.gameObject.layer; 
                            if (bro == 0 && Menu.CFG.MiscConfig._layerShot[0])//Прострел через некоторые объекты 
                                return false;
                            if (bro == 23 && Menu.CFG.MiscConfig._layerShot[1])//Прострел через землю
                                return false;
                        }
                        if (test.HitEntity != null)
                        {
                            if (test.HitEntity.IsValid())
                            {
                                var bro = test.HitEntity;
                                if (Menu.CFG.MiscConfig._layerShot[2])//прострел через брикады 
                                    foreach (var a in BaseEntity.clientEntities)
                                    {
                                        var ore = a as Barricade;
                                        if (ore != null)
                                            if (bro.ShortPrefabName == ore.ShortPrefabName)
                                                return false;
                                    }
                                if (Menu.CFG.MiscConfig._layerShot[3])//прострел через домашние объекты 
                                    foreach (var a in BaseEntity.clientEntities)
                                    {
                                        var ore = a as StorageContainer;
                                        if (ore != null)
                                            if (bro.ShortPrefabName == ore.ShortPrefabName)
                                                return false;
                                    }
                                if (Menu.CFG.MiscConfig._layerShot[4])//прострел через камни 
                                    foreach (var a in BaseEntity.clientEntities)
                                    {
                                        var ore = a as OreResourceEntity;
                                        if (ore != null)
                                            if (bro.ShortPrefabName == ore.ShortPrefabName)
                                                return false;
                                    }
                                if (Menu.CFG.MiscConfig._layerShot[5])//прострел через деревья 
                                    foreach (var a in BaseEntity.clientEntities)
                                    {
                                        var tree = a as TreeEntity;
                                        if (tree != null)
                                            if (bro.ShortPrefabName == tree.ShortPrefabName)
                                                return false;
                                    } 
                                     
                            }
                        }
                    }
                }
                catch { } //прострел через объекты 
            }
            using (PlayerProjectileAttack playerProjectileAttack = Facepunch.Pool.Get<PlayerProjectileAttack>())
            {
                playerProjectileAttack.playerAttack = Facepunch.Pool.Get<PlayerAttack>();
                playerProjectileAttack.playerAttack.attack = test.BuildAttackMessage();
                playerProjectileAttack.playerAttack.projectileID = proj.projectileID;
                HitInfo hitInfo = new HitInfo();
                hitInfo.LoadFromAttack(playerProjectileAttack.playerAttack.attack, false);
                hitInfo.Initiator = proj.owner;
                hitInfo.ProjectileID = proj.projectileID;
                hitInfo.ProjectileDistance = proj.traveledDistance;
                hitInfo.ProjectileVelocity = proj.currentVelocity;
                hitInfo.ProjectilePrefab = proj.sourceProjectilePrefab;
                hitInfo.IsPredicting = true;
                hitInfo.WeaponPrefab = proj.sourceWeaponPrefab;
                hitInfo.DoDecals = proj.createDecals;
                proj.CalculateDamage(hitInfo, proj.modifier, proj.integrity);
                if (proj.penetrationPower <= 0f || hitInfo.HitEntity == null)
                {
                    proj.integrity = 0f;
                }
                else
                {
                    float num2 = hitInfo.HitEntity.PenetrationResistance(hitInfo) / proj.penetrationPower;
                    result = proj.Refract(ref num, point, normal, num2);
                    proj.integrity = Mathf.Clamp01(proj.integrity - num2);
                }
                if (proj.isAuthoritative)
                { 
                    playerProjectileAttack.hitVelocity = proj.currentVelocity;
                    playerProjectileAttack.hitDistance = proj.traveledDistance;
                    bool flag = true;
                    if (hitInfo.HitEntity != null)
                    {
                        var basePlayer = hitInfo.HitEntity.ToPlayer();
                        if (basePlayer != null && _friendList.Contains(basePlayer.userID) && Menu.CFG.AimBotConfig._noDamageFriend) 
                            flag = false; 
                    }
                    if (flag) 
                        proj.owner.SendProjectileAttack(playerProjectileAttack); 
                    proj.sentPosition = proj.currentPosition;
                }
                if ((proj.clientsideAttack || (proj.isAuthoritative && ConVar.Client.prediction)) && hitInfo.HitEntity != null)
                {
                    hitInfo.HitEntity.OnAttacked(hitInfo);
                }
                if (proj.clientsideEffect || (proj.isAuthoritative && ConVar.Client.prediction))
                {
                    Effect.client.ImpactEffect(hitInfo);
                }
            }
            proj.seed = (int)num; 
            return result;
        } 
        public static Dictionary<BasePlayer, int> rageTargets;
       // [Replacement(typeof(PlayerModel), "GetSkinColor")]
        public Color Xyeta(PlayerModel pl)
        {
            foreach (var player_chams in BasePlayer.VisiblePlayerList) 
                if (player_chams != null && player_chams.gameObject != null && player_chams.IsLocalPlayer())
                {
                    if (player_chams.health > 0f)
                    {
                        if (player_chams.health < 999f)
                            return new Color(0, 2.09f, 0, 0);
                        if (player_chams.health < 80)
                            return new Color(1.363f, 2.09f, 0, 0);
                        if (player_chams.health < 60)
                            return new Color(1.726f, 2.09f, 0, 0);
                        if (player_chams.health < 40)
                            return new Color(2.453f, 1.726f, 0, 0);
                        if (player_chams.health < 20)
                            return new Color(2.453f, 1.363f, 0, 0);
                        if (player_chams.health < 10)
                            return new Color(2.453f, 0, 0, 0);
                        if (player_chams.health <= 4.7f) 
                            return new Color(2.816f, 0, 0, 0);
                    }
                } 
                    return pl.SkinSet.Get(pl.meshNumber).GetSkinColor(pl.skinNumber); 
        } //Смена цвета модельки игроков. 
        [Replacement(typeof(Projectile), "DoMovement")]
        public void DoMovement(float deltaTime)
        {
            var proj = GetComponent<Projectile>();
            Vector3 a = proj.currentVelocity * deltaTime;
            float magnitude = a.magnitude;
            float num = 1f / magnitude;
            Vector3 vector = a * num;
            if (proj.initialDistance > 0f)
            {
                magnitude = proj.initialDistance;
                num = 1f / magnitude;
                vector = proj.currentVelocity.normalized;
                proj.initialDistance = 0f;
            }
            bool flag = false;
            Vector3 vector2 = proj.currentPosition + vector * magnitude;
            float num2 = proj.traveledTime + deltaTime;
            if (proj.hitTest == null)
            {
                proj.hitTest = new HitTest();
            }
            else
            {
                proj.hitTest.Clear();
            }
            proj.hitTest.AttackRay = new Ray(proj.currentPosition, vector);
            proj.hitTest.MaxDistance = magnitude;
            proj.hitTest.ignoreEntity = proj.owner;
            proj.hitTest.Radius = 0f;
            proj.hitTest.Forgiveness = proj.thickness;
            proj.hitTest.type = ((!proj.isAuthoritative) ? HitTest.Type.ProjectileEffect : HitTest.Type.Projectile);
            if (proj.sourceWeaponPrefab)
            {
                proj.hitTest.BestHit = true;
                proj.hitTest.damageProperties = proj.damageProperties;
            }
            List<TraceInfo> list = Facepunch.Pool.GetList<TraceInfo>();
            GameTrace.TraceAll(proj.hitTest, list, 1134650113);
            int num3 = 0;
            while (num3 < list.Count && proj.isAlive && !flag)
            {
                if (list[num3].valid)
                {
                    list[num3].UpdateHitTest(proj.hitTest);
                    Vector3 vector3 = proj.hitTest.HitPointWorld();
                    Vector3 normal = proj.hitTest.HitNormalWorld();

                    try
                    {
                        if (Menu.CFG.MiscConfig._hitMarker)
                        {
                            if (proj.hitTest.HitEntity != null)
                            {
                                if ((BaseCombatEntity)proj.hitTest.HitEntity.ToPlayer())
                                {
                                    DDraw.Line(LocalPlayer.Entity.eyes.position, vector3, Main.Visuals.Others.colorHit, 3f, true, false);
                                    DDraw.Text("<size=11>✘</size>", vector3, Color.white, 3f);
                                }
                            }
                        }
                    }
                    catch { }

                    try
                    {
                        foreach (var aa in BaseEntity.clientEntities)
                        {
                            var r = aa as BaseHelicopter;
                            if (r != null)
                            {
                                if (r.IsValid())
                                {
                                    if (Menu.CFG.MiscConfig._hitMarker)
                                    {
                                        if (proj.hitTest.HitEntity != null)
                                        {
                                            if ((BaseCombatEntity)proj.hitTest.HitEntity.ToPlayer())
                                            {
                                                DDraw.Line(LocalPlayer.Entity.eyes.position, vector3, Main.Visuals.Others.colorHit, 3f, true, false);
                                                DDraw.Text("<size=11>✘</size>", vector3, Color.white, 3f);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    } catch { }
                    meme_hit = proj.hitTest;
                    if (ConVar.Vis.attack && proj.isAuthoritative && LocalPlayer.isAdmin)
                    {
                        UnityEngine.DDraw.Line(proj.currentPosition, vector3, Color.yellow, 60f, true, false);
                        UnityEngine.DDraw.Sphere(vector3, proj.thickness, Color.yellow, 60f, true);
                        if (proj.hitTest.HitTransform)
                        {
                            UnityEngine.DDraw.Text(proj.hitTest.HitTransform.name, vector3, Color.white, 60f);
                        }
                    }
                    float magnitude2 = (vector3 - proj.currentPosition).magnitude;
                    float num4 = magnitude2 * num * deltaTime;
                    proj.traveledDistance += magnitude2;
                    proj.traveledTime += num4;
                    proj.currentPosition = vector3;
                    if (proj.DoRicochet(proj.hitTest, vector3, normal) || proj.DoHit(proj.hitTest, vector3, normal))
                    {
                        flag = true;
                    }
                }
                num3++;
            }
            Facepunch.Pool.FreeList<TraceInfo>(ref list);
            if (proj.isAlive)
            {
                if (flag && proj.traveledTime < num2)
                {
                    proj.DoMovement(num2 - proj.traveledTime);
                    return;
                }
                if (ConVar.Vis.attack && proj.isAuthoritative && LocalPlayer.isAdmin)
                {
                    UnityEngine.DDraw.Arrow(proj.currentPosition, vector2, proj.thickness, Color.yellow, 60f);
                }
                float magnitude3 = (vector2 - proj.currentPosition).magnitude;
                float num5 = magnitude3 * num * deltaTime;
                proj.traveledDistance += magnitude3;
                proj.traveledTime += num5;
                proj.currentPosition = vector2;
            }
            try
            {
                if (proj.isAuthoritative)
                {
                    var HeldShits = LocalPlayer.Entity.GetHeldEntity();
                    var position = Vector3.zero;
                    BasePlayer r = null;
                    var di = 7000f;
                    foreach (BasePlayer bs in BasePlayer.VisiblePlayerList)
                    {
                        if (!_friendList.Contains(bs.userID) && bs != null && !bs.IsLocalPlayer() && !bs.IsDead())
                        {
                            if (!(bs.model.headBone.transform.position == Vector3.zero))
                            {
                                var vectors = MainCamera.mainCamera.WorldToScreenPoint(bs.model.headBone.transform.position);
                                if (vectors.z > 0f)
                                {
                                    var center = new Vector2(vectors.x, Screen.height - vectors.y);
                                    var fov = Mathf.Abs(Vector2.Distance(new Vector2(Screen.width / 2, Screen.height / 2), center));
                                    if (fov <= _silent && fov < di)
                                    {
                                        position = bs.model.headBone.transform.position;
                                        di = fov;
                                        r = bs; 
                                    }
                                }
                            }
                        }
                    }
                    if (r != null && r.IsValid() && _silentPlayerPredict  && r.health > 0f && GamePhysics.LineOfSight(proj.currentPosition, r.model.headBone.transform.position, 0))
                    {
                        if (BaseEntityEx.IsValid(HeldShits))
                        {
                            var basemelee = LocalPlayer.Entity.GetHeldEntity().GetComponent<BaseMelee>();
                            if (basemelee is BaseMelee)
                            {
                                var item = basemelee.GetItem();
                                if (item != null)
                                {
                                    var itemmodproj = item.info.GetComponent<ItemModProjectile>();
                                    if (itemmodproj != null)
                                    {
                                        var vec = ((r.model.headBone.transform.position - proj.currentPosition).normalized) * itemmodproj.GetRandomVelocity();
                                        if (LocalPlayer.Entity != null && proj.traveledDistance > 20)
                                            proj.currentVelocity = vec; 
                                    }
                                }
                            }
                            var bp = LocalPlayer.Entity.GetHeldEntity().GetComponent<BaseProjectile>();
                            if (bp is BaseProjectile)
                            {
                                var bp1 = bp.primaryMagazine.ammoType;
                                if (bp1 != null)
                                {
                                    var test = bp1.GetComponent<ItemModProjectile>();
                                    if (test != null)
                                    {
                                        var vec = ((r.model.headBone.transform.position - proj.currentPosition).normalized) * test.GetRandomVelocity() * bp.projectileVelocityScale;
                                        if (LocalPlayer.Entity != null && proj.traveledDistance > 20f)
                                            proj.currentVelocity = vec; 
                                    }
                                }
                            }
                        }
                    }
                    if (_BaseHelicopter.Count > 0 && _silentHeliPredict)
                    {
                        foreach (var t in _BaseHelicopter.Values)
                        {
                            if (DrawOnlyPlayers.IsInScreen(t.position))
                            {
                                if (BaseEntityEx.IsValid(HeldShits))
                                {
                                    if (HeldShits is BaseMelee)
                                    {
                                        var basemelee = LocalPlayer.Entity.GetHeldEntity().GetComponent<BaseMelee>();
                                        if (BaseEntityEx.IsValid(basemelee))
                                        {
                                            var item = basemelee.GetItem();
                                            if (item != null)
                                            {
                                                var itemmodproj = item.info.GetComponent<ItemModProjectile>();
                                                if (itemmodproj != null)
                                                {
                                                    var vec = (((t.position + new Vector3(0, 1, 0)) - proj.currentPosition).normalized) * itemmodproj.GetRandomVelocity();
                                                    if (LocalPlayer.Entity != null && proj.traveledDistance > 35)
                                                        proj.currentVelocity = vec;
                                                }
                                            }
                                        }
                                    }
                                }
                                var bp = LocalPlayer.Entity.GetHeldEntity().GetComponent<BaseProjectile>();
                                if (BaseEntityEx.IsValid(bp))
                                {
                                    if (HeldShits is BaseProjectile)
                                    {
                                        var bp1 = bp.primaryMagazine.ammoType;
                                        if (bp1 != null)
                                        {
                                            var test = bp1.GetComponent<ItemModProjectile>();
                                            if (test != null)
                                            {
                                                var vec = (((t.position + new Vector3(0, 1, 0)) - proj.currentPosition).normalized) * test.GetRandomVelocity() * bp.projectileVelocityScale;
                                                if (LocalPlayer.Entity != null && proj.traveledDistance > 35)
                                                    proj.currentVelocity = vec;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch { } 
            if (proj.isAuthoritative && _hitBoxPlayer)
            {
                try
                {
                    foreach (var r in BasePlayer.VisiblePlayerList)
                    {
                        if (r != null)
                        {
                            if (!_friendList.Contains(r.userID) && r.IsValid() && !r.IsLocalPlayer() && !r.IsDead() && r.health > 0f)
                            {
                                Vector3 tar = r.transform.position;
                                if (Vector3.Distance(proj.currentPosition, tar) < _hitBoxPlayerRadius)
                                {
                                    if (proj.traveledDistance > 1f)
                                    {
                                        meme_hit = proj.hitTest;
                                        var transform = r.model.headBone.transform;
                                        switch (_hitBoxPlayerList)
                                        {
                                            case 0:
                                                transform = r.GetModel().headBone.transform;
                                                break;
                                            case 1:
                                                transform = r.FindBone("neck").transform;
                                                break; 
                                            case 2:
                                                transform = r.FindBone("spine1").transform;
                                                break;
                                        }
                                        proj.hitTest.DidHit = true;
                                        proj.hitTest.HitEntity = r;
                                        proj.hitTest.HitTransform = transform;
                                        proj.hitTest.HitMaterial = _hitBoxPlayerGlass ? "Glass":"Flesh";
                                        proj.hitTest.HitPoint = transform.InverseTransformPoint(proj.currentPosition);
                                        proj.hitTest.HitNormal = transform.InverseTransformDirection(proj.currentPosition);
                                        proj.hitTest.AttackRay = new Ray(proj.currentPosition, tar - proj.currentPosition);
                                        proj.DoHit(proj.hitTest, proj.hitTest.HitPointWorld(), proj.hitTest.HitNormalWorld());
                                        if (Menu.CFG.MiscConfig._hitMarker)
                                        {
                                            if (proj.hitTest.HitEntity != null)
                                            {
                                                if ((BaseCombatEntity)proj.hitTest.HitEntity.ToPlayer())
                                                {
                                                    DDraw.Line(LocalPlayer.Entity.eyes.position, proj.hitTest.HitPointWorld(), Main.Visuals.Others.colorHit, 3f, true, false);
                                                    DDraw.Text("<size=11>✘</size>", proj.hitTest.HitPointWorld(), Color.white, 3f);
                                                }
                                            }
                                        }
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
                catch { }
            }  //Magic Bullet Players (Don't work) 
            
            if (proj.isAuthoritative && _hitBoxHelicopter)
            {
                try
                {
                    foreach (var aa in BaseEntity.clientEntities)
                    {
                        var r = aa as BaseHelicopter;
                        if (r != null)
                        {
                            if (r.IsValid())
                            {
                                Vector3 tar = ((BaseEntity)r).transform.position;
                                if (GamePhysics.LineOfSight(tar, proj.currentPosition, Rust.Layers.MeleeLineOfSightCheck | Rust.Layers.Terrain | Rust.Layers.Construction, 0f) && Vector3.Distance(proj.currentPosition, tar) < _hitBoxHelicopterRadius)
                                {
                                    if (proj.traveledDistance > 15f)
                                    {
                                        meme_hit = proj.hitTest;
                                        var transform = ((BaseEntity)r).transform;

                                        proj.hitTest.DidHit = true;
                                        proj.hitTest.HitEntity = ((BaseEntity)r);
                                        proj.hitTest.HitTransform = transform;
                                        proj.hitTest.HitPoint = transform.InverseTransformPoint(proj.currentPosition);
                                        proj.hitTest.HitNormal = transform.InverseTransformDirection(proj.currentPosition);
                                        proj.hitTest.AttackRay = new Ray(proj.currentPosition, tar - proj.currentPosition);
                                        proj.DoHit(proj.hitTest, proj.hitTest.HitPointWorld(), proj.hitTest.HitNormalWorld());

                                        if (Menu.CFG.MiscConfig._hitMarker)
                                        {
                                            if (proj.hitTest.HitEntity != null)
                                            {
                                                if ((BaseCombatEntity)proj.hitTest.HitEntity == r)
                                                {
                                                    DDraw.Line(LocalPlayer.Entity.eyes.position, proj.hitTest.HitPointWorld(), Main.Visuals.Others.colorHit, 3f, true, false);
                                                    DDraw.Text("<size=11>✘</size>", proj.hitTest.HitPointWorld(), Color.white, 3f);
                                                }
                                            }
                                        }
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }catch { }
            }
        }
         
         
        public static float tess;
        public static BasePlayer meme_target = null;
        public static HitTest meme_hit = null;
        [Replacement(typeof(BasePlayer), "SendProjectileAttack")]
        public void SendProjectileAttack(PlayerProjectileAttack attack)
        {
            try
            {
                if (_silentPlayerPredictWall)
                {
                    BasePlayer play = null;
                    var screen = new Vector2(Screen.width / 2, Screen.height / 2);
                    var maxdist = 7000f;
                    foreach (BasePlayer player in BasePlayer.VisiblePlayerList)
                    {
                        if (!_friendList.Contains(player.userID) && player != null && !player.IsLocalPlayer() && !player.IsDead() && !player.IsSleeping())
                        {
                            var pos = player.eyes.position;
                            if (!(pos == Vector3.zero))
                            {
                                var vec = MainCamera.mainCamera.WorldToScreenPoint(pos);
                                if (vec.z > 0f)
                                {
                                    var check = new Vector2(vec.x, Screen.height - vec.y);
                                    var radius = Mathf.Abs(Vector2.Distance(screen, check));
                                    if (radius <= _silent && radius < maxdist)
                                    {
                                        maxdist = radius;
                                        play = player;
                                    }
                                }
                            }
                        }
                    }
                    if (play != null)
                    {
                        var transform = "head";
                        switch (_hitPlayerList)
                        {
                            case 0:
                                transform = "head";
                                break;
                            case 1:
                                transform = "neck";
                                break;
                            case 2:
                                transform = "spine1";
                                break;
                        }
                        if (MainCamera.mainCamera.WorldToScreenPoint(play.transform.position).z > 0f)
                        {
                            attack.playerAttack.attack.hitID = play.net.ID;
                            attack.playerAttack.attack.hitBone = StringPool.Get(transform);
                            attack.playerAttack.attack.hitPositionLocal = new Vector3(-0.1f, -0.1f, 0f);
                            attack.playerAttack.attack.hitNormalLocal = new Vector3(0f, -1f, 0f);
                            attack.hitDistance = Vector3.Distance(LocalPlayer.Entity.transform.position, play.transform.position);
                            LocalPlayer.Entity.ServerRPC<PlayerProjectileAttack>("OnProjectileAttack", attack);
                        }
                        //Сало стены 
                        if (Vector3.Distance(LocalPlayer.Entity.transform.position, play.transform.position) > 0f)
                        {
                            attack.playerAttack.attack.hitID = play.net.ID;
                            attack.playerAttack.attack.hitBone = StringPool.Get(transform);
                            attack.playerAttack.attack.hitPositionLocal = new Vector3(-0.1f, -0.1f, 0f);
                            attack.playerAttack.attack.hitNormalLocal = new Vector3(0f, -1f, 0f);
                            attack.playerAttack.attack.hitPositionWorld = play.model.headBone.position;
                            attack.playerAttack.attack.pointStart = play.model.headBone.position;
                            attack.hitDistance = Vector3.Distance(LocalPlayer.Entity.transform.position, play.transform.position);
                            LocalPlayer.Entity.ServerRPC<PlayerProjectileAttack>("OnProjectileAttack", attack);
                            if (Menu.CFG.MiscConfig._hitLog)
                                HitLogs.Add(String.Format("[HitLogs] You Hit <color=red>{0}</color> at <color=red>{2}M</color> Away in <color=red>{1}</color>.",
                                                        play.displayName,
                                                        transform,
                                                        (int)Vector3.Distance(LocalPlayer.Entity.transform.position, play.transform.position)),
                                                        Menu.CFG.MiscConfig._hitLogTime);
                        }
                    }
                }
                foreach (var aa in BaseEntity.clientEntities)
                {
                    var r = aa as BaseHelicopter;
                    if (r != null)
                    {
                        if (r.IsValid() && _hitRotor)
                        { 
                            if (meme_hit.HitEntity != null)
                            {
                                if ((BaseCombatEntity)meme_hit.HitEntity == r)
                                {
                                    if ((int)r.weakspots[0].health > 0)
                                        attack.playerAttack.attack.hitBone = StringPool.Get("engine_col");
                                    else
                                        attack.playerAttack.attack.hitBone = StringPool.Get("tail_rotor_col");
                                    attack.hitDistance = 0f + UnityEngine.Random.Range(1,0.5f);
                                }
                            }
                        }
                    }
                }
                if (_silentPlayerPredict)
                {
                    var position = Vector3.zero;
                    BasePlayer r = null;
                    var di = 7000f;
                    foreach (BasePlayer bs in BasePlayer.VisiblePlayerList)
                    {
                        if (!_friendList.Contains(bs.userID) && bs != null && !bs.IsLocalPlayer() && !bs.IsDead())
                        {
                            if (!(bs.model.headBone.transform.position == Vector3.zero))
                            {
                                var vectors = MainCamera.mainCamera.WorldToScreenPoint(bs.model.headBone.transform.position);
                                if (vectors.z > 0f)
                                {
                                    var center = new Vector2(vectors.x, Screen.height - vectors.y);
                                    var fov = Mathf.Abs(Vector2.Distance(new Vector2(Screen.width / 2, Screen.height / 2), center));
                                    if (fov <= _silent && fov < di)
                                    {
                                        position = bs.model.headBone.transform.position;
                                        di = fov;
                                        r = bs;
                                    }
                                }
                            }
                        }
                    }
                    if (r != null && r.IsValid() && _silentPlayerPredict && !r.IsLocalPlayer() && !r.IsSleeping() && !r.IsDead() && r.health > 0f) 
                        attack.hitDistance = Vector3.Distance(LocalPlayer.Entity.transform.position, r.transform.position); 
                }
                foreach (var r in BasePlayer.VisiblePlayerList)
                {
                    if (r != null)
                    {
                        if (!_friendList.Contains(r.userID) && r.health > 0 && !r.IsLocalPlayer() && !r.IsDead() && _hitPlayer)
                        {
                            if (meme_hit.HitEntity != null)
                            {
                                if ((BaseCombatEntity)meme_hit.HitEntity.ToPlayer())
                                {
                                    var transform = "head";
                                    switch (_hitPlayerList)
                                    {
                                        case 0:
                                            transform = "head";
                                            break;
                                        case 1:
                                            transform = "neck";
                                            break;
                                        case 2:
                                            transform = "spine1";
                                            break;
                                    } 
                                        attack.playerAttack.attack.hitMaterialID = StringPool.Get("Glass"); 
                                    attack.playerAttack.attack.hitBone = StringPool.Get(transform);
                                }
                            }
                        }

                    }
                }
            }
            catch
            {
            } 
                LocalPlayer.Entity.ServerRPC("OnProjectileAttack", attack); 
        }

        
        [Replacement(typeof(PlayerModel), "UpdateVelocity")]
        public void UpdateVelocity()
        {  
            Vector3 a = base.transform.InverseTransformDirection(GetComponent<PlayerModel>().position - GetComponent<PlayerModel>().lastPosition);
            GetComponent<PlayerModel>().lastPosition = GetComponent<PlayerModel>().position;
            Vector3 vector = a / Time.deltaTime;
            if (GetComponent<PlayerModel>().speedOverride != Vector3.zero)
            {
                vector = GetComponent<PlayerModel>().speedOverride;
            }
            if (vector.IsNaNOrInfinity())
            {
                vector = Vector3.zero;
            }
            GetComponent<PlayerModel>().velocity = Vector3.Lerp(GetComponent<PlayerModel>().velocity, vector, Time.deltaTime * 10f);
            if (GetComponent<PlayerModel>().modelState.sitting)
            {
                GetComponent<PlayerModel>().velocity = Vector3.zero;
            }  
                Vel = Vector3.Lerp(GetComponent<PlayerModel>().velocity, vector, Time.deltaTime * 10f);
        }
        public static Vector3 Vel;
        /* public class pSilent : AttackEntity
         {
            // [Replacement(typeof(BaseProjectile), "LaunchProjectileClientside")]
             internal void LaunchProjectileClientside(ItemDefinition ammo, int projectileCount, float projSpreadaimCone)
             {
                 BasePlayer aimTarget = null;
                 Vector2 centerScreen1 = Vector2.zero;
                 rageTargets = new Dictionary<BasePlayer, int> { };
                 Dictionary<BasePlayer, int> playerList = new Dictionary<BasePlayer, int> { };
                 centerScreen1 = new Vector2(Screen.width / 2f, Screen.height / 2f);
                 var baseProjectile = GetComponent<BaseProjectile>();
                 BasePlayer ownerPlayer = base.GetOwnerPlayer();
                 if (!ownerPlayer)
                 {
                     return;
                 }
                 ItemModProjectile component = ammo.GetComponent<ItemModProjectile>();
                 if (component == null)
                 {
                     Debug.Log("Ammo doesn't have a Projectile module!");
                     return;
                 }
                 baseProjectile.createdProjectiles.Clear();
                 float num = ProjectileWeaponMod.Average(this, (ProjectileWeaponMod x) => x.projectileVelocity, (ProjectileWeaponMod.Modifier y) => y.scalar, 1f);
                 float num2 = ProjectileWeaponMod.Sum(this, (ProjectileWeaponMod x) => x.projectileVelocity, (ProjectileWeaponMod.Modifier y) => y.offset, 0f);
                 using (ProjectileShoot projectileShoot = Facepunch.Pool.Get<ProjectileShoot>())
                 {
                     projectileShoot.projectiles = new List<ProjectileShoot.Projectile>();
                     projectileShoot.ammoType = ammo.itemid;
                     for (int i = 0; i < projectileCount; i++)
                     {
                         Vector3 position = ownerPlayer.eyes.position;
                         Vector3 vector = ownerPlayer.eyes.BodyForward();
                         if (projSpreadaimCone > 0f || component.projectileSpread > 0f)
                         {
                             Quaternion rotation = ownerPlayer.eyes.rotation;
                             float num3 = baseProjectile.aimconeCurve.Evaluate(UnityEngine.Random.Range(0f, 1f));
                             float num4 = (projectileCount <= 1) ? component.GetSpreadScalar() : component.GetIndexedSpreadScalar(i, projectileCount);
                             float num5 = num3 * projSpreadaimCone + component.projectileSpread * num4;
                             vector = AimConeUtil.GetModifiedAimConeDirection(num5, rotation * Vector3.forward, projectileCount <= 1);
                             if (!aimTarget.IsValid() || aimTarget.health == 0f)
                             {
                                 aimTarget = null;
                             }
                             if (aimTarget == null)
                             {
                                 playerList.Clear();

                                 foreach (BasePlayer player in BasePlayer.VisiblePlayerList)
                                 {
                                     int distanceFromCenter = (int)(Vector2.Distance(MainCamera.mainCamera.WorldToScreenPoint(player.model.headBone.transform.position), centerScreen1));
                                     Vector3 onScreen = player.model.headBone.transform.position - MainCamera.mainCamera.transform.position;
                                     if (distanceFromCenter <= 555 && !player.IsDead() && !player.IsLocalPlayer() && !player.IsSleeping() && Vector3.Dot(MainCamera.mainCamera.transform.TransformDirection(Vector3.forward), onScreen) > 0)
                                         playerList.Add(player, distanceFromCenter);
                                 }
                                 if (playerList.Count > 0)
                                 {
                                     var sortList = playerList.OrderBy(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
                                     aimTarget = sortList.Keys.First();
                                     List<BasePlayer> dictKeys = new List<BasePlayer>(sortList.Keys);
                                     foreach (BasePlayer p in dictKeys)
                                     {
                                         aimTarget = p;
                                         break;
                                     }
                                 }
                             }
                             if (aimTarget != null)
                             {
                                 Vector3 BonePos = aimTarget.model.headBone.transform.position;
                                 float Dist = Vector3.Distance(LocalPlayer.Entity.transform.position, BonePos);
                                 if (Dist > 0.001f)
                                 {
                                     float BulletTime = Dist / tess;
                                     Vector3 vel = (Vector3)Vel;
                                     Vector3 PredictVel = vel * BulletTime;
                                     BonePos += PredictVel;
                                     BonePos.y += (4.905f * BulletTime * BulletTime);
                                 }
                                 Vector3 relative = (BonePos - LocalPlayer.Entity.eyes.position).normalized;
                                 vector = AimConeUtil.GetModifiedAimConeDirection(0, relative, projectileCount <= 1);
                             }
                             rageTargets.Clear();
                             foreach (BasePlayer player in BasePlayer.VisiblePlayerList)
                             {
                                 int distanceFromCenter = (int)(Vector2.Distance(MainCamera.mainCamera.WorldToScreenPoint(player.model.headBone.transform.position), centerScreen1));
                                 Vector3 onScreen = player.model.headBone.transform.position - MainCamera.mainCamera.transform.position;
                                 if (distanceFromCenter <= 555 && !player.IsDead() && !player.IsLocalPlayer() && !player.IsSleeping() && Vector3.Dot(MainCamera.mainCamera.transform.TransformDirection(Vector3.forward), onScreen) > 0)
                                     rageTargets.Add(player, distanceFromCenter);
                                 if (rageTargets.Count > 0)
                                 {
                                     rageTargets = rageTargets.OrderBy(pair => pair.Value)
                                               .ToDictionary(pair => pair.Key, pair => pair.Value);
                                 }
                             }
                             if (ConVar.Global.developer > 0)
                             {
                                 UnityEngine.DDraw.Arrow(position, position + vector * 3f, 0.1f, Color.white, 20f);
                             }
                             if (ConVar.Global.developer > 0)
                             {
                                 UnityEngine.DDraw.Arrow(position, position + vector * 3f, 0.1f, Color.white, 20f);
                             }
                         }
                         Vector3 vector2 = vector * (component.GetRandomVelocity() * baseProjectile.projectileVelocityScale * num + num2);
                         t = vector2;
                         int seed = ownerPlayer.NewProjectileSeed();
                         int projectileID = ownerPlayer.NewProjectileID();
                         Projectile projectile = baseProjectile.CreateProjectile(component.projectileObject.resourcePath, position, vector, vector2);
                         if (projectile != null)
                         {
                             projectile.mod = component;
                             projectile.seed = seed;
                             projectile.owner = ownerPlayer;
                             projectile.sourceWeaponPrefab = base.gameManager.FindPrefab(this).GetComponent<AttackEntity>();
                             projectile.sourceProjectilePrefab = component.projectileObject.Get().GetComponent<Projectile>();
                             projectile.projectileID = projectileID;
                             projectile.invisible = baseProjectile.IsSilenced();
                             baseProjectile.createdProjectiles.Add(projectile);
                         }
                         ProjectileShoot.Projectile projectile2 = new ProjectileShoot.Projectile();
                         projectile2.projectileID = projectileID;
                         projectile2.startPos = position;
                         projectile2.startVel = vector2;
                         projectile2.seed = seed;
                         projectileShoot.projectiles.Add(projectile2);
                     }
                     base.ServerRPC<ProjectileShoot>("CLProject", projectileShoot);
                     foreach (Projectile projectile3 in baseProjectile.createdProjectiles)
                     {
                         projectile3.Launch();
                     }
                     baseProjectile.createdProjectiles.Clear();
                 }
             }

         }*/
    }
}
public static class Utils
{
    public static void SetFieldValue<T>(this Type type, object instance, string field, T value)
    {
        type.GetField(field, BindingFlags.NonPublic | BindingFlags.Instance).SetValue(instance, value);
    }

    public static T GetFieldValue<T>(this Type type, object instance, string field)
    {
        return (T)type.GetField(field, BindingFlags.NonPublic | BindingFlags.Instance).GetValue(instance);
    }

    public static T GetSttaticFieldValue<T>(this Type type, object instance, string field)
    {
        return (T)type.GetField(field, BindingFlags.Public | BindingFlags.Static).GetValue(instance);
    }
}
public static class Helpers
{
    private static FieldInfo GetFieldInfo(Type type, string fieldName)
    {
        FieldInfo fieldInfo;
        do
        {
            fieldInfo = type.GetField(fieldName,
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            type = type.BaseType;
        } while (fieldInfo == null && type != null);
        return fieldInfo;
    }

    public static object GetFieldValue(this object obj, string fieldName)
    {
        if (obj == null)
            throw new ArgumentNullException("obj");
        Type objType = obj.GetType();
        FieldInfo fieldInfo = GetFieldInfo(objType, fieldName);
        if (fieldInfo == null)
            throw new ArgumentOutOfRangeException("fieldName",
                string.Format("Couldn't find field {0} in type {1}", fieldName, objType.FullName));
        return fieldInfo.GetValue(obj);
    }

    public static void SetFieldValue(this object obj, string fieldName, object val)
    {
        if (obj == null)
            throw new ArgumentNullException("obj");
        Type objType = obj.GetType();
        FieldInfo fieldInfo = GetFieldInfo(objType, fieldName);
        if (fieldInfo == null)
            throw new ArgumentOutOfRangeException("fieldName",
                string.Format("Couldn't find field {0} in type {1}", fieldName, objType.FullName));
        fieldInfo.SetValue(obj, val);
    }
}