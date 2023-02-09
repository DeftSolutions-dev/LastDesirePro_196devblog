 
using LastDesirePro.Attributes;
using LastDesirePro.Main.Visuals;
using Network;
using ProtoBuf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using UnityEngine;
using static LastDesirePro.Main.Objects.ObjectsCheck;
using static LastDesirePro.Main.Visuals.Others;
using static LastDesirePro.Menu.CFG.AutomaticConfig;

namespace LastDesirePro.Main.Automatic
{
    [Component]
    internal class Automatic : MonoBehaviour
    {
       
        void Start() {

            for (int i = 0; i < 20; i++)
                Forces.Add(new ForceInfo("ඞ"));
            for (int i = 0; i < 10; i++)
                Forces1.Add(new ForceInfo(""));
            try
            { 
                StartCoroutine(autopick());
                StartCoroutine("TreeSilentFarm");
                StartCoroutine("OreSilentFarm");
                StartCoroutine("silentMelee");
                StartCoroutine("guitar");
                StartCoroutine("upd");
            }
            catch { }
        } 
        private static IEnumerator autopick()
        {
            while (true)
            {
                try
                {
                    if (LocalPlayer.Entity != null)
                    {
                        if (_autoSuicide && Visuals.FullDrawing.IsOpen())
                            if (_alwaysSuicide || UnityEngine.Input.GetKey(_suicideKey))
                                for (int i = 0; i < 3; i++)
                                    LocalPlayer.Entity.ServerRPC<float>("OnPlayerLanded", -18);

                        if (_spamSuicide && UnityEngine.Input.GetKey(_spamKey) && Visuals.FullDrawing.IsOpen())
                        {
                            for (int i = 0; i < 3; i++)
                                LocalPlayer.Entity.ServerRPC<float>("OnPlayerLanded", -18);
                            SingletonComponent<UIDeathScreen>.Instance.OnRespawn();
                        }

                        if (_autoDoThrow && UnityEngine.Input.GetKey(_doThrowKey) && Visuals.FullDrawing.IsOpen())
                        {
                            var held = LocalPlayer.Entity.GetHeldEntity();
                            if (held is GrenadeWeapon && (int)Vector3.Distance(LocalPlayer.Entity.transform.position, held.transform.position) <= 2f)
                                held.ServerRPC<Vector3, Vector3, float>("DoThrow", LocalPlayer.Entity.eyes.position, LocalPlayer.Entity.eyes.BodyForward(), 1f);
                        }
                        if (_autoDoDrop && UnityEngine.Input.GetKey(_doDropKey) && Visuals.FullDrawing.IsOpen())
                        {
                            var held = LocalPlayer.Entity.GetHeldEntity();
                            if (held is GrenadeWeapon && (int)Vector3.Distance(LocalPlayer.Entity.transform.position, held.transform.position) <= 2f)
                                held.ServerRPC<Vector3, Vector3>("DoDrop", LocalPlayer.Entity.eyes.position, LocalPlayer.Entity.eyes.BodyForward());
                        }

                        if (_autoHeal)
                        {
                            lastHeal = (Time.time - lastheal);
                            if (lastHeal > 0.5f)
                            {
                                var activeItem = LocalPlayer.Entity.Belt.GetActiveItem();
                                var held = LocalPlayer.Entity.GetHeldEntity();
                                if (held is MedicalTool && LocalPlayer.Entity.IsLocalPlayer() && (activeItem.info.shortname.Contains("syringe.medical") && LocalPlayer.Entity.health < 87f) ||
                                    (activeItem.info.shortname.Contains("bandage") && LocalPlayer.Entity.health < 99f) &&
                                    (int)Vector3.Distance(LocalPlayer.Entity.transform.position, held.transform.position) <= 2f)
                                    held.ServerRPC("UseSelf");
                                lastheal = Time.time;
                            }
                        }
                        if (_autoHealFriend)
                        {
                            lastHealFriend = (Time.time - lasthealFriend);
                            if (lastHealFriend > 0.5f)
                            {
                                foreach (var player in BasePlayer.VisiblePlayerList)
                                {
                                    if (player != null && player.gameObject != null && (int)Vector3.Distance(LocalPlayer.Entity.transform.position, player.transform.position) <= 1.5f)
                                    {
                                        var activeItem = LocalPlayer.Entity.Belt.GetActiveItem();
                                        var held = LocalPlayer.Entity.GetHeldEntity();
                                        if (held is MedicalTool && !player.IsLocalPlayer() && (activeItem.info.shortname.Contains("syringe.medical") && player.health < 87f) ||
                                            (activeItem.info.shortname.Contains("bandage") && player.health < 99f))
                                            held.ServerRPC<uint>("UseOther", player.net.ID);
                                        lasthealFriend = Time.time;
                                    }
                                }
                            }
                        }
                        if (_autoAuthBuild && _Cupboard.Count > 0)
                        {
                            foreach (var aa in BaseEntity.clientEntities)
                            {
                                var t = aa as BuildingPrivlidge;
                                if (t != null && !t.IsDead() && Vector3.Distance(LocalPlayer.Entity.transform.position, t.transform.position) <= 4f && !t.IsAuthed(LocalPlayer.Entity))
                                    t.ServerRPC("AddSelfAuthorize");
                            }
                        }
                        if (_autoAuthTurret && _AutoTurret.Count > 0)
                        {
                            foreach (var aa in BaseEntity.clientEntities)
                            {
                                var t = aa as AutoTurret;
                                if (t != null && !t.IsDead() && Vector3.Distance(LocalPlayer.Entity.transform.position, t.transform.position) <= 4f && !t.IsAuthed(LocalPlayer.Entity))
                                    t.ServerRPC("AddSelfAuthorize");
                            }
                        }
                        if (_autoIgnite)
                        {
                            var bufferList = Networkable();
                            for (int i = 0; i < bufferList.Count; i++)
                            {
                                var dor = bufferList[i] as TorchWeapon;
                                if (dor is TorchWeapon && Vector3.Distance(LocalPlayer.Entity.transform.position, dor.transform.position) <= 2f && !dor.IsOnFire() && !dor.IsOn())
                                    dor.ServerRPC("Ignite");
                            }
                        }
                        if (_autoAssist)
                        {
                            foreach (var player in BasePlayer.VisiblePlayerList)
                                if (player != null)
                                    if (player != null && player.HasPlayerFlag(BasePlayer.PlayerFlags.Wounded) && Vector3.Distance(LocalPlayer.Entity.transform.position, player.transform.position) <= 7f)
                                    {
                                        player.ServerRPC("RPC_KeepAlive");
                                        player.ServerRPC("RPC_Assist");
                                    }
                        }

                        if (_offRecycler)
                        {
                            var bufferList = Networkable();
                            for (int i = 0; i < bufferList.Count; i++)
                            {
                                var baseoven = bufferList[i] as Recycler;
                                if (baseoven != null && baseoven.IsOn() && (int)Vector3.Distance(LocalPlayer.Entity.transform.position, baseoven.transform.position) <= 3f)
                                    baseoven.ServerRPC<bool>("SVSwitch", false);
                            }
                        }

                        if (_PlantEntity.Count > 0)
                            foreach (var r in _PlantEntity.Values)
                            {
                                if ((int)Vector3.Distance(LocalPlayer.Entity.transform.position, r.position) <= 2f)
                                {
                                    if (_autoPickup[0] && r.name.Contains("hemp.entity"))
                                        r.entity.ServerRPC("RPC_PickFruit");
                                    if (_autoPickup[5] && r.name.Contains("corn.entity"))
                                        r.entity.ServerRPC("RPC_PickFruit");
                                    if (_autoPickup[6] && r.name.Contains("pumpkin.entity"))
                                        r.entity.ServerRPC("RPC_PickFruit");
                                }
                            }
                        var bruh = _CollectibleEntity;
                        if (_autoPickup[0] && bruh.Count > 0)
                            foreach (var r in bruh.Values)
                                if (r.name.Contains("hemp"))
                                    if ((int)Vector3.Distance(LocalPlayer.Entity.transform.position, r.position) <= 4f)
                                        r.entity.ServerRPC("pickup");
                        if (_autoPickup[1] && bruh.Count > 0)
                            foreach (var r in bruh.Values)
                                if (r.name.Contains("stone-collectable"))
                                    if ((int)Vector3.Distance(LocalPlayer.Entity.transform.position, r.position) <= 4f)
                                        r.entity.ServerRPC("pickup");
                        if (_autoPickup[2] && bruh.Count > 0)
                            foreach (var r in bruh.Values)
                                if (r.name.Contains("metal"))
                                    if ((int)Vector3.Distance(LocalPlayer.Entity.transform.position, r.position) <= 4f)
                                        r.entity.ServerRPC("pickup");
                        if (_autoPickup[3] && bruh.Count > 0)
                            foreach (var r in bruh.Values)
                                if (r.name.Contains("sulfur"))
                                    if ((int)Vector3.Distance(LocalPlayer.Entity.transform.position, r.position) <= 4f)
                                        r.entity.ServerRPC("pickup");
                        if (_autoPickup[4] && bruh.Count > 0)
                            foreach (var r in bruh.Values)
                                if (r.name.Contains("wood"))
                                    if ((int)Vector3.Distance(LocalPlayer.Entity.transform.position, r.position) <= 4f)
                                        r.entity.ServerRPC("pickup");
                        if (_autoPickup[5] && bruh.Count > 0)
                            foreach (var r in bruh.Values)
                                if (r.name.Contains("corn"))
                                    if ((int)Vector3.Distance(LocalPlayer.Entity.transform.position, r.position) <= 4f)
                                        r.entity.ServerRPC("pickup");
                        if (_autoPickup[6] && bruh.Count > 0)
                            foreach (var r in bruh.Values)
                                if (r.name.Contains("pumpkin"))
                                    if ((int)Vector3.Distance(LocalPlayer.Entity.transform.position, r.position) <= 4f)
                                        r.entity.ServerRPC("pickup");
                        if (_autoPickup[7] && bruh.Count > 0)
                            foreach (var r in bruh.Values)
                                if (r.name.Contains("mushroom"))
                                    if ((int)Vector3.Distance(LocalPlayer.Entity.transform.position, r.position) <= 4f)
                                        r.entity.ServerRPC("pickup");
                        if (_autoPickup[8] && Visuals.FullDrawing.IsOpen())
                        {
                            var bufferList = Networkable();
                            for (int i = 0; i < bufferList.Count; i++)
                            {
                                var Item = bufferList[i] as WorldItem;
                                if (Item != null && Item.IsValid() && (int)Vector3.Distance(LocalPlayer.Entity.transform.position, Item.transform.position) <= 4f)
                                    Item.ServerRPC("Pickup");
                            }
                        }
                        if (_autoPickup[9])
                        {
                            var bufferList = Networkable();
                            for (int i = 0; i < bufferList.Count; i++)
                            {
                                var Timed = bufferList[i] as TimedExplosive;
                                if (Timed != null && (int)Vector3.Distance(LocalPlayer.Entity.transform.position, Timed.transform.position) <= 4f)
                                    Timed.ServerRPC("RPC_Pickup");
                            }
                        }
                        if (_autoPickup[10] && _Landmine.Count > 0)
                            foreach (var r in _Landmine.Values)
                                if ((int)Vector3.Distance(LocalPlayer.Entity.transform.position, r.position) <= 1.5f)
                                    r.entity.ServerRPC("RPC_Disarm");

                        if (_autoPickup[10] && _BearTrap.Count > 0)
                            foreach (var r in _BearTrap.Values)
                                if (Vector3.Distance(LocalPlayer.Entity.transform.position, r.position) <= 1.5f)
                                    r.entity.ServerRPC("RPC_PickupStart");

                        if (_autoDrink)
                        {
                            var bufferList = Networkable();
                            for (int i = 0; i < bufferList.Count; i++)
                            {
                                var drink = bufferList[i] as BaseLiquidVessel;
                                if (drink != null && (LocalPlayer.Entity.metabolism.hydration.max - 30) > LocalPlayer.Entity.metabolism.hydration.value)
                                    if (drink != null && (int)Vector3.Distance(LocalPlayer.Entity.eyes.position, drink.transform.position) <= 2f)
                                        drink.ServerRPC("DoDrink");
                            }
                        }

                        if (_autoLockCodeLock && Visuals.FullDrawing.IsOpen())
                        {
                            var bufferList = Networkable();
                            for (int i = 0; i < bufferList.Count; i++)
                            {
                                var dor = bufferList[i] as CodeLock;
                                if (dor != null && !dor.IsLocked())
                                    if (dor != null && (int)Vector3.Distance(LocalPlayer.Entity.transform.position, dor.transform.position) <= 4f)
                                        dor.ServerRPC("RPC_ChangeCode", codeKey.ToString(), false);
                            }
                        }
                    }
                }
                catch { }
                yield return new WaitForSeconds(0.09f);
            }
        }

        private IEnumerator TreeSilentFarm()
        {
            while (true)
            {
                try
                {
                    if (LocalPlayer.Entity != null)
                    {
                        var activeItem = LocalPlayer.Entity.Belt.GetActiveItem();
                        if (LocalPlayer.Entity != null && BaseEntityEx.IsValid(LocalPlayer.Entity) && _farmTreeBonus && (activeItem.info.shortname.Contains("axe.salvaged") || activeItem.info.shortname.Contains("bone.club")
                                            || activeItem.info.shortname.Contains("cakefiveyear") || activeItem.info.shortname.Contains("chainsaw") || activeItem.info.shortname.Contains("candycaneclub")
                                            || activeItem.info.shortname.Contains("hammer.salvaged") || activeItem.info.shortname.Contains("hatchet")
                                            || activeItem.info.shortname.Contains("rock") || activeItem.info.shortname.Contains("sickle") || activeItem.info.shortname.Contains("stonehatchet")))
                        {
                            var vector = LocalPlayer.Entity.eyes.position;
                            var heldEntity = LocalPlayer.Entity.GetHeldEntity();
                            if (heldEntity != null)
                            {
                                BaseEntity Entity = null;
                                BaseEntity Entity0 = null;
                                var num = float.MaxValue;
                                var num0 = float.MaxValue;
                                var bufferList = Networkable();
                                for (int i = 0; i < bufferList.Count; i++)
                                {
                                    var Entity1 = bufferList[i] as BaseEntity;
                                    if (!(Entity1 == null) && BaseEntityEx.IsValid(Entity1))
                                    {

                                        if (Entity1 is TreeMarker)
                                        {
                                            var num1 = Vector3.Distance(vector, Entity1.transform.position);
                                            if (num1 <= 3.5f && num1 < num)
                                            {
                                                Entity = Entity1;
                                                num = num1;
                                            }
                                        }
                                        else if (Entity1 is TreeEntity)
                                        {
                                            var position = Entity1.transform.position;
                                            position.y = vector.y;
                                            var nume2 = Vector3.Distance(vector, position);
                                            if (nume2 <= 2.5f && nume2 < num0)
                                            {
                                                Entity0 = Entity1;
                                                num0 = nume2;
                                            }
                                        }
                                    }
                                }
                                if (Entity0 != null)
                                {
                                    Vector3 vector0;
                                    RaycastHit raycastHit;
                                    if (Entity != null)
                                    {
                                        var vec = Entity0.transform.position - Entity.transform.position;
                                        vec.y = 0f;
                                        vec.Normalize();
                                        vector = Entity.transform.position - vec;
                                        vector0 = Entity.transform.position;
                                    }
                                    else if (GamePhysics.Trace(new Ray(LocalPlayer.Entity.eyes.position, (Entity0.transform.position - LocalPlayer.Entity.eyes.position).normalized), 0f, out raycastHit, 15f, 1101212417, 0))
                                        vector0 = raycastHit.point;
                                    else
                                        vector0 = Entity0.transform.position;
                                    var attEnt = heldEntity as BaseMelee;
                                    if (attEnt != null && !(UnityEngine.Time.time - time < attEnt.repeatDelay))
                                    {
                                        var ray = new Ray(vector, (vector0 - vector).normalized);
                                        using (var plAtt = Facepunch.Pool.Get<PlayerAttack>())
                                        {
                                            using (var attack = Facepunch.Pool.Get<Attack>())
                                            {
                                                plAtt.attack = attack;
                                                plAtt.attack.hitID = Entity0.net.ID;
                                                plAtt.attack.hitMaterialID = StringPool.Get("wood");
                                                plAtt.attack.hitPositionWorld = vector0;
                                                plAtt.attack.hitNormalWorld = ray.direction;
                                                plAtt.attack.pointStart = vector;
                                                plAtt.attack.pointEnd = vector0;
                                                attEnt.ServerRPC<PlayerAttack>("PlayerAttack", plAtt);
                                            }
                                        }
                                        time = Time.time;
                                    }
                                }

                            }
                        }
                    }
                } catch { }
                yield return new WaitForSeconds(0f);
            }
        }
        private IEnumerator OreSilentFarm()
        {
            while (true)
            {
                try
                {
                    if (LocalPlayer.Entity != null)
                    {
                        var activeItem = LocalPlayer.Entity.Belt.GetActiveItem();
                        if (BaseEntityEx.IsValid(LocalPlayer.Entity) && _farmOreBonus && (activeItem.info.shortname.Contains("cakefiveyear") || activeItem.info.shortname.Contains("hammer.salvaged")
                                            || activeItem.info.shortname.Contains("icepick.salvaged") || activeItem.info.shortname.Contains("jackhammer")
                                            || activeItem.info.shortname.Contains("pickaxe") || activeItem.info.shortname.Contains("rock")
                                            || activeItem.info.shortname.Contains("stone.pickaxe") || activeItem.info.shortname.Contains("bone.club") || activeItem.info.shortname.Contains("candycaneclub")))
                        {
                            var vector = LocalPlayer.Entity.eyes.position;
                            var heldEntity = LocalPlayer.Entity.GetHeldEntity();
                            if (heldEntity != null)
                            {
                                BaseEntity Entity = null;
                                BaseEntity Entity0 = null;
                                var num = float.MaxValue;
                                var num0 = float.MaxValue;
                                var bufferList = Networkable();
                                for (int i = 0; i < bufferList.Count; i++)
                                {
                                    var Entity1 = bufferList[i] as BaseEntity;
                                    if (!(Entity1 == null) && BaseEntityEx.IsValid(Entity1))
                                    {
                                        if (Entity1 is OreHotSpot)
                                        {
                                            var num1 = Vector3.Distance(vector, Entity1.transform.position);
                                            if (num1 <= 3.5f && num1 < num)
                                            {
                                                Entity = Entity1;
                                                num = num1;
                                            }
                                        }
                                        else if (Entity1 is OreResourceEntity)
                                        {
                                            var position = Entity1.transform.position;
                                            position.y = vector.y;
                                            var num2 = Vector3.Distance(vector, position);
                                            if (num2 <= 2.5f && num2 < num0)
                                            {
                                                Entity0 = Entity1;
                                                num0 = num2;
                                            }
                                        }
                                    }
                                }
                                if (Entity0 != null)
                                {
                                    Vector3 vector0;
                                    RaycastHit raycastHit;
                                    if (Entity != null)
                                    {
                                        var vec = Entity0.transform.position - Entity.transform.position;
                                        vec.y = 0f;
                                        vec.Normalize();
                                        vector = Entity.transform.position - vec;
                                        vector0 = Entity.transform.position;
                                    }
                                    else if (GamePhysics.Trace(new Ray(LocalPlayer.Entity.eyes.position, (Entity0.transform.position - LocalPlayer.Entity.eyes.position).normalized), 0f, out raycastHit, 15f, 1101212417, 0))
                                        vector0 = raycastHit.point;
                                    else
                                        vector0 = Entity0.transform.position;
                                    var attEnt = heldEntity as BaseMelee;
                                    if (attEnt != null && !(UnityEngine.Time.time - time < attEnt.repeatDelay))
                                    {
                                        var ray = new Ray(vector, (vector0 - vector).normalized);
                                        using (var plAtt = Facepunch.Pool.Get<PlayerAttack>())
                                        {
                                            using (var attack = Facepunch.Pool.Get<Attack>())
                                            {
                                                plAtt.attack = attack;
                                                plAtt.attack.hitID = Entity0.net.ID;
                                                plAtt.attack.hitMaterialID = StringPool.Get("stones");
                                                plAtt.attack.hitPositionWorld = vector0;
                                                plAtt.attack.hitNormalWorld = ray.direction;
                                                plAtt.attack.pointStart = vector;
                                                plAtt.attack.pointEnd = vector0;
                                                attEnt.ServerRPC<PlayerAttack>("PlayerAttack", plAtt);
                                            }
                                        }
                                        if (activeItem.info.shortname.Contains("jackhammer"))
                                            time = Time.time + 0.115f;
                                        else
                                            time = Time.time;
                                    }
                                }

                            }
                        }
                    }
                }
                catch { }
                yield return new WaitForSeconds(0f);
            }
        }
        private IEnumerator silentMelee()
        {
            while (true)
            {
                try
                {
                    /*if (LocalPlayer.Entity != null && BaseEntityEx.IsValid(LocalPlayer.Entity) && _silentMeleeObject && farment != null)
                    {
                        var activeItem = LocalPlayer.Entity.Belt.GetActiveItem();
                        var heldEntity = LocalPlayer.Entity.GetHeldEntity();
                        var attEnt = heldEntity as BaseMelee;
                        float tim = UnityEngine.Time.time - time + _silentMeleeObjectSpeed;
                        if ((int)Vector3.Distance(MainCamera.mainCamera.transform.position, farment.transform.position) < 4f && attEnt != null && !(tim < attEnt.repeatDelay))
                        { 
                            var ray = new Ray(LocalPlayer.Entity.eyes.transform.position, (farment.transform.position - farment.transform.position).normalized);
                            using (var playAtt = Facepunch.Pool.Get<PlayerAttack>())
                            {
                                playAtt.attack = new Attack
                                {
                                    hitID = farment.net.ID,
                                    hitBone = hits,
                                    hitMaterialID =   StringPool.Get("flesh"),
                                    hitPositionWorld = LocalPlayer.Entity.eyes.transform.position,
                                    hitPositionLocal = LocalPlayer.Entity.eyes.transform.position,
                                    hitNormalLocal = LocalPlayer.Entity.eyes.transform.position,
                                    hitNormalWorld = LocalPlayer.Entity.eyes.transform.position,
                                    pointStart = LocalPlayer.Entity.eyes.transform.position + Vector3.up,
                                    pointEnd = LocalPlayer.Entity.eyes.transform.position + Vector3.up,
                                    ShouldPool = true
                                };
                                attEnt.ServerRPC("PlayerAttack", playAtt);
                                using (var attack = Facepunch.Pool.Get<Attack>())
                                {
                                    playAtt.attack = attack;
                                    playAtt.attack.hitID = farment.net.ID;
                                    playAtt.attack.hitBone = hits;
                                    playAtt.attack.hitMaterialID =   StringPool.Get("flesh");
                                    playAtt.attack.hitPositionWorld = farment.transform.position;
                                    playAtt.attack.hitNormalWorld = ray.direction;
                                    playAtt.attack.pointStart = LocalPlayer.Entity.eyes.transform.position;
                                    playAtt.attack.pointEnd = farment.transform.position;
                                    attEnt.ServerRPC<PlayerAttack>("PlayerAttack", playAtt);
                                }
                            }
                            time = Time.time;
                        }
                    }*/
                }
                catch { }
                try
                { 
                    if (LocalPlayer.Entity != null && BaseEntityEx.IsValid(LocalPlayer.Entity) && _silentMeleeNpc)
                    {
                        var heldEntity = LocalPlayer.Entity.GetHeldEntity();
                        var attEnt = heldEntity as BaseMelee;
                        foreach (var player in BaseNpc.VisibleNpcList)
                        {
                            if (player != null && !player.IsDead() && BaseEntityEx.IsValid(player) && BaseEntityEx.IsValid(attEnt))
                            { 
                                if ((int)Vector3.Distance(MainCamera.mainCamera.transform.position, player.FindBone("head").position) < 4f && attEnt != null && !(UnityEngine.Time.time - time < attEnt.repeatDelay))
                                {
                                    var ray = new Ray(LocalPlayer.Entity.eyes.transform.position, (player.FindBone("head").position - player.FindBone("head").position).normalized);
                                    using (var plAtt = Facepunch.Pool.Get<PlayerAttack>())
                                    {
                                        using (var attack = Facepunch.Pool.Get<Attack>())
                                        {
                                            plAtt.attack = attack;
                                            plAtt.attack.hitID = player.net.ID;
                                            plAtt.attack.hitBone = StringPool.Get("head");
                                            plAtt.attack.hitMaterialID = StringPool.Get("flesh");
                                            plAtt.attack.hitPositionWorld = player.FindBone("head").position;
                                            plAtt.attack.hitNormalWorld = ray.direction;
                                            plAtt.attack.pointStart = LocalPlayer.Entity.eyes.position;
                                            plAtt.attack.pointEnd = player.FindBone("head").position;
                                            attEnt.ServerRPC<PlayerAttack>("PlayerAttack", plAtt); 
                                        }
                                    };
                                    time = Time.time; 
                                }
                            }
                        }
                    }
                }
                catch { }
                try
                { 
                    if (LocalPlayer.Entity != null && BaseEntityEx.IsValid(LocalPlayer.Entity) && _silentMelee)
                    {
                        var heldEntity = LocalPlayer.Entity.GetHeldEntity();
                        var attEnt = heldEntity as BaseMelee;
                        foreach (BasePlayer player in BasePlayer.VisiblePlayerList)
                        {
                            if (!Menu.CFG.AimBotConfig._friendList.Contains(player.userID) && player != null && BaseEntityEx.IsValid(player) && BaseEntityEx.IsValid(attEnt) && !player.IsLocalPlayer() && !player.IsDead() && !player.IsWounded())
                            {
                                int dist = (int)Vector3.Distance(MainCamera.mainCamera.transform.position, player.model.headBone.transform.position + new Vector3(0f, 0.04f, 0.04f));
                                if (dist < 4f && attEnt != null && !(UnityEngine.Time.time - time < attEnt.repeatDelay))
                                {
                                    if (player != null)
                                    {
                                        string bone = "head";
                                        switch (_silentMeleeHit)
                                        {
                                            case 0:
                                                bone = "head";
                                                break;
                                            case 1:
                                                bone = "neck";
                                                break;
                                            case 2:
                                                bone = "spine1";
                                                break;
                                        }
                                        Ray ray = new Ray(LocalPlayer.Entity.eyes.transform.position, (player.FindBone("head").position - player.FindBone("head").position).normalized);
                                        using (PlayerAttack playerAttack = Facepunch.Pool.Get<PlayerAttack>())
                                        {
                                            playerAttack.attack = new Attack
                                            {
                                                hitID = player.net.ID,
                                                hitBone = StringPool.Get(bone),
                                                hitMaterialID = StringPool.Get("Flesh"),
                                                hitPositionWorld = LocalPlayer.Entity.eyes.transform.position,
                                                hitPositionLocal = LocalPlayer.Entity.eyes.transform.position,
                                                hitNormalLocal = LocalPlayer.Entity.eyes.transform.position,
                                                hitNormalWorld = LocalPlayer.Entity.eyes.transform.position,
                                                pointStart = LocalPlayer.Entity.eyes.transform.position + Vector3.up,
                                                pointEnd = LocalPlayer.Entity.eyes.transform.position + Vector3.up,
                                                ShouldPool = true
                                            };
                                            attEnt.ServerRPC("PlayerAttack", playerAttack); 
                                        }
                                        using (PlayerAttack playerAttack = Facepunch.Pool.Get<PlayerAttack>())
                                        {
                                            using (Attack attack = Facepunch.Pool.Get<Attack>())
                                            {
                                                playerAttack.attack = attack;
                                                playerAttack.attack.hitID = player.net.ID;
                                                playerAttack.attack.hitBone = StringPool.Get(bone);
                                                playerAttack.attack.hitMaterialID = StringPool.Get("Flesh");
                                                playerAttack.attack.hitPositionWorld = player.FindBone("head").position;
                                                playerAttack.attack.hitNormalWorld = ray.direction;
                                                playerAttack.attack.pointStart = LocalPlayer.Entity.eyes.position;
                                                playerAttack.attack.hitPositionLocal = new Vector3(-0.1f, -1f, 0f);
                                                playerAttack.attack.hitNormalLocal = new Vector3(0f, -1f, 0f);
                                                playerAttack.attack.pointEnd = player.FindBone("head").position;
                                                attEnt.ServerRPC<PlayerAttack>("PlayerAttack", playerAttack);
                                            }
                                        };
                                         
                                        time = Time.time;
                                    }
                                }
                            }
                        }
                    }
                }
                catch { }
                yield return new WaitForSeconds(0);
            }
        }
        private IEnumerator guitar()
        {
            while (true)
            { 
                try
                {
                    if (_autoSpamGuitar)
                    {
                        var activeItem = LocalPlayer.Entity.Belt.GetActiveItem();
                        if (activeItem != null && activeItem.info.shortname.Contains("fun.guitar"))
                        {
                            var bufferList = Networkable();
                            for (int i = 0; i < bufferList.Count; i++)
                            {
                                var drink = bufferList[i] as InstrumentTool;
                                if (drink != null)
                                {
                                    drink.ServerRPC<byte, float>("SVPlayNote", 0, 2);
                                    drink.ServerRPC<byte, float>("SVPlayNote", 0, -2);
                                    drink.ServerRPC<byte, float>("SVPlayNote", 1, 2);
                                    drink.ServerRPC<byte, float>("SVPlayNote", 1, -2);
                                    LocalPlayer.Entity.SendSignalBroadcast(BaseEntity.Signal.Alt_Attack, string.Empty);
                                }
                            }
                        }
                    }
                }
                catch { }
                yield return new WaitForSeconds(0.1f);
            }
        }
        public static BaseEntity farment = null;
        public static uint hits = 0; 
        public static Vector3 farmpos = Vector3.zero;
        [Replacement(typeof(BaseMelee), "ProcessAttack")]
        protected virtual void ProcessAttack(HitTest hit)
        {
            try
            {
                /*if (_silentMeleeObject)
                {
                    farment = hit.HitEntity;
                    farmpos = hit.HitPointWorld(); 
                }*/
            }
            catch { }
            using (PlayerAttack attack = Facepunch.Pool.Get<PlayerAttack>())
            { 
                //Debug.LogError( hit.HitPart);
                attack.attack = hit.BuildAttackMessage();
                var Looking_Entity = LocalPlayer.Entity.lookingAtEntity;
                var bm = LocalPlayer.Entity.GetHeldEntity().GetComponent<BaseMelee>(); 
                if (Menu.CFG.MiscConfig._meleeFarmOnlyBonus && bm != null)
                {
                    float weaponMaxDistance = Menu.CFG.MiscConfig._meleeX2? 3.9f: bm.maxDistance;
                    if (Looking_Entity.GetComponent<OreResourceEntity>() != null)
                    {
                        float distance;
                        var distancetobeat = 999f;
                        var hsList = _OreHotSpot.Values.ToList();
                        var attackPoint = Vector3.zero;
                        foreach (var h in hsList)
                        {
                            if (h.entity != null)
                            {
                                distance = Vector3.Distance(Looking_Entity.transform.position, h.position);
                                if (distance < distancetobeat)
                                {
                                    distancetobeat = distance;
                                    attackPoint = h.position;
                                }
                            }
                        }
                        if (attackPoint != Vector3.zero)
                            attack.attack.hitPositionWorld = attackPoint;
                    }
                    else
                    {
                        if (attack.attack.hitMaterialID == StringPool.Get("wood"))
                        {
                            var tree = BaseNetworkable.clientEntities.Find(attack.attack.hitID) as BaseEntity;
                            var t_ent = tree.GetComponent<TreeEntity>();
                            float distance;
                            var distancetobeat = 999f;
                            var hsList = _marker.Values.ToList();
                            var attackPoint = Vector3.zero;
                            var newStart = Vector3.zero;
                            var newEnd = Vector3.zero; 
                            foreach (var h in hsList)
                            {
                                if (h.entity != null)
                                {
                                    distance = Vector3.Distance(attack.attack.hitPositionWorld, h.position);
                                    if (distance < distancetobeat && distance < 2)
                                    {
                                        distancetobeat = distance;
                                        attackPoint = h.position;
                                        Vector3 fixedDirection = Vector3Ex.Direction2D(t_ent.transform.position, h.position);
                                        newStart = h.position - fixedDirection * 0.05f;
                                        newEnd = newStart + fixedDirection * weaponMaxDistance;
                                    }
                                }
                            }
                            if (attackPoint != Vector3.zero)
                            {
                                attack.attack.hitPositionWorld = attackPoint;
                                attack.attack.pointStart = newStart;
                                attack.attack.pointEnd = newEnd;
                            }
                        }
                    }
                }
                GetComponent<BaseMelee>().ServerRPC<PlayerAttack>("PlayerAttack", attack);
                HitInfo hitInfo = new HitInfo(); 
                /*if (_silentMeleeObject)
                    hits = attack.attack.hitBone;*/
                hitInfo.LoadFromAttack(attack.attack, GetComponent<BaseMelee>().isServer);
                hitInfo.Initiator = GetComponent<BaseMelee>().GetOwnerPlayer();
                hitInfo.Weapon = GetComponent<BaseMelee>();
                hitInfo.WeaponPrefab = GetComponent<BaseMelee>().gameManager.FindPrefab(GetComponent<BaseMelee>().PrefabName).GetComponent<AttackEntity>();
                hitInfo.IsPredicting = true;
                hitInfo.damageProperties = GetComponent<BaseMelee>().damageProperties;
                GetComponent<BaseMelee>().DoAttackShared(hitInfo);
            }
        }

        public static float lastHeal = 0;
        public static float lastheal = 0;
        public static float lastHealFriend = 0;
        public static float lasthealFriend = 0;
        public static BuildingGrade.Enum bu;
         
        class ForceInfo
        {
            public ForceInfo(string text)
            {
                Text = text;
                CurrentForce = GenerateForce();
                ToForce = GenerateForce();
                ColorForce = GenerateColorForce(CurrentForce);
                ToColorForce = GenerateColorForce(CurrentForce);
                SizeForce = (int)UnityEngine.Random.Range(24, 32);
            }
             
            public static Color GenerateColorForce(Vector2 CurrentPos) => new Color32((byte)UnityEngine.Random.Range(100, 255), (byte)UnityEngine.Random.Range(100, 255), (byte)UnityEngine.Random.Range(100, 255), 255);
            public static Vector2 GenerateForce() => new Vector2(UnityEngine.Random.Range(50, Screen.width - 50), UnityEngine.Random.Range(50, Screen.height - 50));
            public string Text = "";
            public int SizeForce = 16;
            public Color ColorForce = Color.white;
            public Color ToColorForce = Color.red;
            public Vector2 CurrentForce = new Vector2(0, 0);
            public Vector2 ToForce = new Vector2(0, 0);
        }
        List<ForceInfo> Forces = new List<ForceInfo>();
        List<ForceInfo> Forces1 = new List<ForceInfo>();
        Vector3 rotate = Vector3.zero;

        private IEnumerator upd()
        {
            while (true)
            {
                try
                {
                    if (_rotateBuild && Visuals.FullDrawing.IsOpen())
                    {
                        var planner = LocalPlayer.Entity.GetHeldEntity() as Planner;
                        if (UnityEngine.Input.GetKey(KeyCode.LeftArrow))
                            if (planner != null && planner.IsValid())
                            {
                                rotate.y = rotate.y + 1f;
                                planner.SetFieldValue("rotationOffset", rotate);
                            }
                        if (UnityEngine.Input.GetKey(KeyCode.RightArrow))
                            if (planner != null && planner.IsValid())
                            {
                                rotate.y = rotate.y - 1f;
                                planner.SetFieldValue("rotationOffset", rotate);
                            }
                        if (UnityEngine.Input.GetKey(KeyCode.UpArrow))
                            if (planner != null && planner.IsValid())
                            {
                                rotate.z = rotate.z + 1f;
                                planner.SetFieldValue("rotationOffset", rotate);
                            }
                        if (UnityEngine.Input.GetKey(KeyCode.DownArrow))
                            if (planner != null && planner.IsValid())
                            {
                                rotate.z = rotate.z - 1f;
                                planner.SetFieldValue("rotationOffset", rotate);
                            }
                    }
                    if (_Grade && (UnityEngine.Input.GetKeyDown(_keyGradeBuild) || _autoGrade))
                    {
                        var bufferList = Networkable();
                        for (int i = 0; i < bufferList.Count; i++)
                        {
                            if (_numGrade == 0)
                                bu = BuildingGrade.Enum.Wood;
                            if (_numGrade == 1)
                                bu = BuildingGrade.Enum.Stone;
                            if (_numGrade == 2)
                                bu = BuildingGrade.Enum.Metal;
                            if (_numGrade == 3)
                                bu = BuildingGrade.Enum.TopTier;
                            var build = bufferList[i] as BuildingBlock;
                            if (build != null && (int)Vector3.Distance(LocalPlayer.Entity.transform.position, build.transform.position) <= _buildGradeDist)
                            {
                                if (_numGrade == 0)
                                    if (build.grade != BuildingGrade.Enum.Wood && build.grade != BuildingGrade.Enum.Stone && build.grade != BuildingGrade.Enum.Metal && build.grade != BuildingGrade.Enum.TopTier)
                                        build.UpgradeToGrade(bu, LocalPlayer.Entity);
                                if (_numGrade == 1)
                                    if (build.grade != BuildingGrade.Enum.Stone && build.grade != BuildingGrade.Enum.Metal && build.grade != BuildingGrade.Enum.TopTier)
                                        build.UpgradeToGrade(bu, LocalPlayer.Entity);
                                if (_numGrade == 2)
                                    if (build.grade != BuildingGrade.Enum.Metal && build.grade != BuildingGrade.Enum.TopTier)
                                        build.UpgradeToGrade(bu, LocalPlayer.Entity);
                                if (_numGrade == 3)
                                    if (build.grade != BuildingGrade.Enum.TopTier)
                                        build.UpgradeToGrade(bu, LocalPlayer.Entity);
                            }
                        }
                    }
                    if (_tyktyk && Input.GetKey(_doortyk) && Visuals.FullDrawing.IsOpen())
                    {
                        var bufferList = Networkable();
                        for (int i = 0; i < bufferList.Count; i++)
                        {
                            var dor = bufferList[i] as Door;
                            if (dor != null)
                                if (dor != null && (int)Vector3.Distance(LocalPlayer.Entity.transform.position, dor.transform.position) <= 4f)
                                    dor.Menu_KnockDoor(LocalPlayer.Entity);
                        }
                    }
                    if (_autoOpen && Visuals.FullDrawing.IsOpen())
                    {
                        var bufferList = Networkable();
                        for (int i = 0; i < bufferList.Count; i++)
                        {
                            var dor = bufferList[i] as Door;
                            if (dor != null)
                                if (dor != null && (int)Vector3.Distance(LocalPlayer.Entity.eyes.position, dor.transform.position) <= 1.5f)
                                    dor.Menu_OpenDoor(LocalPlayer.Entity);
                        }
                    }
                    if (_autoUnLockCodeLock && UnityEngine.Input.GetKeyDown(_unlockcode) && Visuals.FullDrawing.IsOpen())
                    {
                        var bufferList = Networkable();
                        for (int i = 0; i < bufferList.Count; i++)
                        {
                            var dor = bufferList[i] as CodeLock;
                            if (dor != null && dor.IsLocked() && (int)Vector3.Distance(LocalPlayer.Entity.eyes.transform.position, dor.transform.position) <= 4f)
                            {
                                dor.ServerRPC("UnlockWithCode", _codeKey.ToString());
                                dor.ServerRPC("TryLock");
                            }
                        }
                    }
                }
                catch { }
                yield return new WaitForSeconds(0f);
            }
        }
        float time;
        void OnGUI()
        {
            if (!DrawMenu.AssetsLoad.Loaded) return;
            try
            {
                if (!Menu.MainMenu._IsMenu && Menu.CFG.MiscConfig._backGroundMenu)
                {
                    for (int i = 0; i < Forces.Count; i++)
                    {
                        Forces[i].CurrentForce = Vector2.Lerp(Forces[i].CurrentForce, Forces[i].ToForce, Time.deltaTime * 0.8f);
                        if (Forces[i].ColorForce == Forces[i].ToColorForce)
                            Forces[i].ToColorForce = ForceInfo.GenerateColorForce(Forces[i].CurrentForce);
                        Forces[i].ColorForce = Color.Lerp(Forces[i].ColorForce, Forces[i].ToColorForce, Time.deltaTime * 6);
                        FullDrawing.TextureColor(new Rect(Forces[i].CurrentForce.x - 5f, Forces[i].CurrentForce.y - 1, Forces[i].SizeForce, Forces[i].SizeForce), DrawMenu.AssetsLoad._dildo, Forces[i].ColorForce);
                        FullDrawing.Line(Forces[i].CurrentForce, Forces[i].ToForce, Forces[i].ColorForce, 0.5f);
                        //FullDrawing.String(new Vector2(Forces[i].CurrentForce.x, Forces[i].CurrentForce.y), Forces[i].Text, Forces[i].ColorForce,true, Forces[i].SizeForce);
                        if (Vector2.Distance(Forces[i].CurrentForce, Forces[i].ToForce) < 15) { Forces[i].ToForce = ForceInfo.GenerateForce(); }
                    }
                    for (int i = 0; i < Forces1.Count; i++)
                    {
                        Forces1[i].CurrentForce = Vector2.Lerp(Forces1[i].CurrentForce, Forces1[i].ToForce, Time.deltaTime * 0.8f);
                        if (Forces1[i].ColorForce == Forces1[i].ToColorForce)
                            Forces1[i].ToColorForce = ForceInfo.GenerateColorForce(Forces1[i].CurrentForce);
                        Forces1[i].ColorForce = Color.Lerp(Forces1[i].ColorForce, Forces1[i].ToColorForce, Time.deltaTime * 3);
                        Main.Visuals.FullDrawing.Swastika(Forces1[i].CurrentForce.x, Forces1[i].CurrentForce.y, Menu.MainMenu.rotationdegree, Menu.MainMenu.color, (int)UnityEngine.Random.Range(5, 10));
                        if (Vector2.Distance(Forces1[i].CurrentForce, Forces1[i].ToForce) < 15) { Forces1[i].ToForce = ForceInfo.GenerateForce(); }
                    }
                    //GUI.DragWindow(new Rect(0, 0, Screen.width, Screen.height));
                } 
                if (LocalPlayer.Entity != null && _autoReload)
                {
                    if (LocalPlayer.Entity.GetHeldEntity().GetComponent<BaseProjectile>() != null && !Automatic.reload && Automatic.lastShot <= (Automatic.reloadTime - (Automatic.reloadTime / 10)) && Automatic.lastShot > 0)
                    {
                        float time_full = (Automatic.reloadTime - (Automatic.reloadTime / 10));
                        float time_left = Automatic.lastShot;
                        FullDrawing.IndicatorReload(new Vector2(Screen.width / 2, Screen.height * 0.88f), time_left, time_full, 350, 7, 1f);
                    }
                }
            }
            catch { }
             
            DrawOnlyPlayers.String(new Vector2(-999, -999), "", Menu.CFG.VisuаlCоnfig._colorPlayers, true, 11, FontStyle.Bold, 2);
        }
        public static GUIStyle check = null;
        public static bool _pressOff = false;
        [Replacement(typeof(PlayerWalkMovement), "DoFixedUpdate")]
        public void DoFixedUpdate(ModelState modelState)
        {
            var playerWalkMovement = GetComponent<PlayerWalkMovement>();

            playerWalkMovement.groundAngle = playerWalkMovement.groundAngleNew;
            playerWalkMovement.groundNormal = playerWalkMovement.groundNormalNew;
            playerWalkMovement.swimming = (modelState.waterLevel > 0.65f);
            playerWalkMovement.grounded = (playerWalkMovement.groundAngle <= playerWalkMovement.maxAngleWalking && !playerWalkMovement.jumping && !playerWalkMovement.swimming);
            playerWalkMovement.climbing = (playerWalkMovement.groundAngle <= playerWalkMovement.maxAngleClimbing && !playerWalkMovement.jumping && !playerWalkMovement.swimming && !playerWalkMovement.grounded);
            playerWalkMovement.sliding = (playerWalkMovement.groundAngle <= playerWalkMovement.maxAngleSliding && !playerWalkMovement.jumping && !playerWalkMovement.swimming && !playerWalkMovement.grounded && !playerWalkMovement.climbing);
            playerWalkMovement.jumping = (playerWalkMovement.body.velocity.y > 0f && !playerWalkMovement.swimming && !playerWalkMovement.grounded && !playerWalkMovement.climbing && !playerWalkMovement.sliding);
            playerWalkMovement.falling = (!playerWalkMovement.swimming && !playerWalkMovement.grounded && !playerWalkMovement.climbing && !playerWalkMovement.sliding && !playerWalkMovement.jumping);
            bool isMounted = playerWalkMovement.Owner.isMounted;
            playerWalkMovement.body.isKinematic = isMounted;
            if (playerWalkMovement.body.isKinematic)
            {
                playerWalkMovement.body.velocity = Vector3.zero;
            }
            if (!playerWalkMovement.flying && (playerWalkMovement.wasJumping || playerWalkMovement.wasFalling) && !playerWalkMovement.jumping && !playerWalkMovement.falling && !playerWalkMovement.swimming && UnityEngine.Time.time - playerWalkMovement.groundTime > 0.3f)
            {
                playerWalkMovement.Owner.OnLand(playerWalkMovement.velocity.y);
                playerWalkMovement.landTime = UnityEngine.Time.time;
            }
            if (!playerWalkMovement.wasSwimming && playerWalkMovement.swimming)
            {
                playerWalkMovement.body.velocity *= 0.1f;
            }
            if (playerWalkMovement.grounded || playerWalkMovement.climbing || playerWalkMovement.sliding)
            {
                playerWalkMovement.groundTime = UnityEngine.Time.time;
            }
            playerWalkMovement.UpdateVelocity();
            playerWalkMovement.UpdateGravity(modelState);
            playerWalkMovement.wasGrounded = playerWalkMovement.grounded;
            playerWalkMovement.wasClimbing = playerWalkMovement.climbing;
            playerWalkMovement.wasSliding = playerWalkMovement.sliding;
            playerWalkMovement.wasSwimming = playerWalkMovement.swimming;
            playerWalkMovement.wasJumping = playerWalkMovement.jumping;
            playerWalkMovement.wasFalling = playerWalkMovement.falling;
            playerWalkMovement.velocity = playerWalkMovement.body.velocity;
            playerWalkMovement.groundAngleNew = float.MaxValue;
            playerWalkMovement.groundNormalNew = Vector3.up;
             
            //var speed = (playerWalkMovement.swimming /*swim*/ || playerWalkMovement.GetForcedDuck() /* crouch */ > 0.5f) ? 1.7f : (playerWalkMovement.jumping /*jump*/ ? 6f : 5.5f);
            if (_farmBot[0] && _Resources.Count > 0)
            { 
                var activeItem = LocalPlayer.Entity.Belt.GetActiveItem();
                if (LocalPlayer.Entity != null && BaseEntityEx.IsValid(LocalPlayer.Entity))
                {
                    Vector3 vels = playerWalkMovement.body.velocity;
                    var vector = LocalPlayer.Entity.transform.position;
                    var heldEntity = LocalPlayer.Entity.GetHeldEntity();
                    if (heldEntity != null && (activeItem.info.shortname.Contains("cakefiveyear") || activeItem.info.shortname.Contains("hammer.salvaged")
                                        || activeItem.info.shortname.Contains("icepick.salvaged") || activeItem.info.shortname.Contains("jackhammer")
                                        || activeItem.info.shortname.Contains("pickaxe") || activeItem.info.itemid == 3506021
                                        || activeItem.info.shortname.Contains("stone.pickaxe") || activeItem.info.shortname.Contains("bone.club") || activeItem.info.shortname.Contains("candycaneclub")))
                    {
                        BaseEntity Entity1 = null;
                        var num1 = 9999f;
                        var bufferList = Networkable();
                        for (int i = 0; i < bufferList.Count; i++)
                        {
                            var Entity2 = bufferList[i] as BaseEntity;
                            if (!(Entity2 == null) && BaseEntityEx.IsValid(Entity2))
                            {
                                if (Entity2 is OreResourceEntity)
                                {
                                    if (((Entity2.name.Contains("stone") && _farmBot[1]) || (Entity2.name.Contains("sulfur") && _farmBot[3]) || (Entity2.name.Contains("metal") && _farmBot[2])))
                                    {
                                        var position = Entity2.transform.position;
                                        position.y = vector.y;
                                        var num2 = Vector3.Distance(vector, position);
                                        if (num2 <= 9999f && num2 < num1)
                                        {
                                            Entity1 = Entity2;
                                            num1 = num2;
                                        }
                                    }
                                }
                            }
                        }
                        if (Entity1 != null)
                        { 
                            Vector3 direction = (Entity1.transform.position - LocalPlayer.Entity.transform.position).normalized * (_farmBotSpeed + 4f);
                            playerWalkMovement.body.velocity = new Vector3(direction.x, vels.y, direction.z);
                        } 
                    } 
                    if (heldEntity != null && (activeItem.info.shortname.Contains("axe.salvaged") || activeItem.info.shortname.Contains("bone.club")
                                        || activeItem.info.shortname.Contains("cakefiveyear") || activeItem.info.shortname.Contains("chainsaw") || activeItem.info.shortname.Contains("candycaneclub")
                                        || activeItem.info.shortname.Contains("hammer.salvaged") || activeItem.info.shortname.Contains("hatchet")
                                        || activeItem.info.itemid == 3506021 || activeItem.info.shortname.Contains("sickle") || activeItem.info.shortname.Contains("stonehatchet")))
                    {
                        BaseEntity Entity1 = null;
                        var num1 = 9999f;
                        var bufferList = Networkable();
                        for (int i = 0; i < bufferList.Count; i++)
                        {
                            var Entity2 = bufferList[i] as BaseEntity;
                            if (!(Entity2 == null) && BaseEntityEx.IsValid(Entity2))
                            {
                                if (Entity2 is TreeEntity)
                                {
                                    if (_farmBot[4])
                                    {
                                        var position = Entity2.transform.position;
                                        position.y = vector.y;
                                        var num2 = Vector3.Distance(vector, position);
                                        if (num2 <= 9999f && num2 < num1)
                                        { 
                                            Entity1 = Entity2;
                                            num1 = num2;
                                        }
                                    }
                                }
                            }
                        }
                        if (Entity1 != null)
                        { 
                            Vector3 direction = (Entity1.transform.position - LocalPlayer.Entity.transform.position).normalized * (_farmBotSpeed + 4f);
                            playerWalkMovement.body.velocity = new Vector3(direction.x, vels.y, direction.z);
                        } 
                    } 
                }
            }
        }
        public static AttackEntity af = null;
        public static float lastshot = 0;
        public static float lastShot = 0;
        public static bool reload = false;
        public static bool shot = false;
        public static float f = 0;
        public static float ff = 0;
        public static float fff = 0;
        public static float ffff = 0;


         
        [Replacement(typeof(BaseProjectile), "ShotFired")]
        public virtual void ShotFired()
        {
            reload = false;
            shot = true;
            lastshot = Time.time;
            GetComponent<BaseProjectile>().numShotsFired++;
            GetComponent<BaseProjectile>().aimconePenalty += GetComponent<BaseProjectile>().aimconePenaltyPerShot;
            if (GetComponent<BaseProjectile>().aimconePenalty > GetComponent<BaseProjectile>().aimConePenaltyMax)
            {
                GetComponent<BaseProjectile>().aimconePenalty = GetComponent<BaseProjectile>().aimConePenaltyMax;
            }
            GetComponent<BaseProjectile>().lastShotTime = UnityEngine.Time.time;
        } 
        public static float reloadTime;
        public static bool rocket = true;
        public static float lastjump = 0;
        public static float lastJump = 0;
        public struct RecoilZ
        {
            public float aimSway;
            public float aimSwaySpeed; 
        }
        public static Dictionary<int, InstEoka> RemEoka = new Dictionary<int, InstEoka>();
        public static Dictionary<int, RecoilZ> RemRecoil = new Dictionary<int, RecoilZ> { };
        public struct InstEoka
        {
            public float successFraction;
            public float repeatDelay;
            public bool automatic;
            public float deployDelay;
        } 
        public class fov : EntityComponent<BasePlayer>
        {
            [Replacement(typeof(PlayerEyes), "UpdateCamera")]
            public void UpdateCamera(Camera cam)
            {
                if (cam == null)
                    return;

                if (Menu.CFG.AutomaticConfig._fieldOfView)
                    cam.fieldOfView = 95 + _fieldOfViewRadius;
                else if (!Menu.CFG.MiscConfig._x6Zoom)
                    cam.fieldOfView = ConVar.Graphics.fov;
                if (UnityEngine.Input.GetKey(Menu.CFG.MiscConfig._x6ZoomKey) && Menu.CFG.MiscConfig._x6Zoom && Visuals.FullDrawing.IsOpen())
                    cam.fieldOfView = LocalPlayer.Entity.input.state.IsDown(BUTTON.FIRE_SECONDARY)? 40: 15f;
                else if(!Menu.CFG.AutomaticConfig._fieldOfView)
                    cam.fieldOfView = ConVar.Graphics.fov;
                BasePlayer.CameraMode currentViewMode = base.baseEntity.currentViewMode;
                if (currentViewMode != BasePlayer.CameraMode.FirstPerson)
                {
                    if (currentViewMode == BasePlayer.CameraMode.Eyes)
                    {
                        GetComponent<PlayerEyes>().DoInEyeCamera(cam);
                        return;
                    }
                    if (currentViewMode == BasePlayer.CameraMode.ThirdPerson)
                    {
                        GetComponent<PlayerEyes>().DoThirdPersonCamera(cam);
                        return;
                    }
                }
                else
                    GetComponent<PlayerEyes>().DoFirstPersonCamera(cam);
            }
        } 
        public static bool _eye;
        [Replacement(typeof(BasePlayer), "ClientInput")]
        internal virtual void ClientInput(InputState state)
        {
            var activeItem = LocalPlayer.Entity.Belt.GetActiveItem();
            var held_entity = LocalPlayer.Entity.GetHeldEntity();
            try
            {
                var held = LocalPlayer.Entity.GetHeldEntity().GetComponent<BaseProjectile>();

                ItemDefinition bp1 = held.primaryMagazine.ammoType;
                if (bp1 != null)
                {
                    ItemModProjectile test = bp1.GetComponent<ItemModProjectile>();
                    if (test != null)
                    {
                        AimBot.AimBot.tess=((test.GetRandomVelocity() * held.projectileVelocityScale));
                    }
                }
                reloadTime = held.reloadTime;
                if (_autoReload)
                {
                    if (held != null)
                    {
                        var heldshit = LocalPlayer.Entity.GetHeldEntity().GetComponent<BaseProjectile>();
                        if (!reload)
                            lastShot = (Time.time - lastshot);
                        if (shot && (lastShot > 0.2f)) {
                            heldshit.ServerRPC("StartReload");
                            shot = false;
                        }
                        if (lastShot > (held.reloadTime - (held.reloadTime / 10)) && !reload && heldshit.primaryMagazine.CanReload(LocalPlayer.Entity)) {
                            heldshit.ServerRPC("Reload");
                            heldshit.primaryMagazine.contents = (heldshit.primaryMagazine.capacity);
                            heldshit.ResetReloadCooldown();
                            heldshit.UpdateAmmoDisplay();
                            reload = false;
                            shot = true;
                            rocket = true;
                            lastshot = Time.time;
                        }
                    }
                }
            }
            catch { }
            if (_spamGlassHammer)
            {
                try
                {
                    if (activeItem != null && activeItem.info.shortname.Contains("hammer"))
                    {
                        var melee = held_entity as BaseMelee;
                        if (melee != null)
                        {
                            foreach (var player in BasePlayer.VisiblePlayerList)
                            {
                                RaycastHit _raycastHit;
                                if (!Menu.CFG.AimBotConfig._friendList.Contains(player.userID) && !Physics.Linecast(LocalPlayer.Entity.eyes.transform.position, player.model.eyeBone.transform.position, out _raycastHit, Rust.Layers.Construction | Rust.Layers.World) && !player.IsLocalPlayer() && !player.IsWounded() && player != null && BaseEntityEx.IsValid(player) && BaseEntityEx.IsValid(melee) && !player.IsDead() && !player.IsSleeping())
                                {
                                    var dist = (int)Vector3.Distance(LocalPlayer.Entity.eyes.transform.position, player.model.headBone.transform.position);
                                    if (dist < 4f)
                                    {
                                        if (!melee.HasAttackCooldown())
                                        {
                                            var playerPos = player.model.headBone.position;
                                            var hit = new HitTest();
                                            hit.MaxDistance = 1000;
                                            hit.HitTransform = player.model.headBone.transform;
                                            hit.AttackRay = new Ray(LocalPlayer.Entity.eyes.position, (playerPos - LocalPlayer.Entity.eyes.position).normalized);
                                            hit.DidHit = true;
                                            hit.HitEntity = player;
                                            hit.HitMaterial = "glass";
                                            hit.HitPoint = player.model.eyeBone.transform.InverseTransformPoint(player.model.eyeBone.position + new Vector3(UnityEngine.Random.Range(-0.2f, 0.2f), UnityEngine.Random.Range(-0.2f, 0.2f), UnityEngine.Random.Range(-0.2f, 0.2f)));
                                            hit.HitNormal = player.model.eyeBone.transform.InverseTransformPoint(player.model.eyeBone.position + new Vector3(UnityEngine.Random.Range(-0.2f, 0.2f), UnityEngine.Random.Range(-0.2f, 0.2f), UnityEngine.Random.Range(-0.2f, 0.2f)));
                                            hit.damageProperties = melee.damageProperties;
                                            melee.StartAttackCooldown(0f);
                                            melee.ProcessAttack(hit);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch { }
            } 

                    if (LocalPlayer.Entity != null && LocalPlayer.Entity.GetHeldEntity())
            {
                var bp2 = LocalPlayer.Entity.GetHeldEntity().GetComponent<BowWeapon>();
                var field = typeof(BowWeapon).GetField("attackReady", BindingFlags.Instance | BindingFlags.NonPublic);
                if (Menu.CFG.MiscConfig._bow)
                {
                    if (bp2 != null)
                    {
                        bp2.automatic = true;
                        field.SetValue(bp2, true);
                    }
                }
            }
            try
            {
                if (LocalPlayer.Entity != null && Menu.CFG.MiscConfig._shotTheArmor)
                {
                    foreach (var item in LocalPlayer.Entity.inventory.containerWear.itemList)
                    {
                        if (item != null)
                        {
                            bool fg = false;
                            var component = item.info.GetComponent<ItemModWearable>(); 
                            {
                                component.blocksAiming = false;
                                if (component.occlusionType == UIBlackoutOverlay.blackoutType.HELMETSLIT)
                                    fg = false;
                                UIBlackoutOverlay uiblackoutOverlay = UIBlackoutOverlay.Get(UIBlackoutOverlay.blackoutType.HELMETSLIT);
                                if (uiblackoutOverlay) 
                                    uiblackoutOverlay.SetAlpha((!fg) ? 0 : 1); 
                            }
                        }
                    }
                }
                if (LocalPlayer.Entity != null && LocalPlayer.Entity.GetHeldEntity())
                {
                    var bp = LocalPlayer.Entity.GetHeldEntity().GetComponent<BaseProjectile>();
                    var bp1 = LocalPlayer.Entity.GetHeldEntity().GetComponent<FlintStrikeWeapon>();
                    var itemID = LocalPlayer.Entity.GetHeldEntity().GetOwnerItemDefinition().itemid;
                    if (bp1 != null)
                    {
                        if (Menu.CFG.MiscConfig._eoka)
                        {
                            if (bp1.successFraction != 0f)
                            {
                                if (!RemEoka.ContainsKey(itemID))
                                {
                                    var newinfo = new InstEoka();
                                    newinfo.successFraction = bp1.successFraction;
                                    newinfo.repeatDelay = bp1.repeatDelay;
                                    newinfo.automatic = bp1.automatic;
                                    newinfo.deployDelay = bp1.deployDelay;
                                    RemEoka.Add(itemID, newinfo);
                                }
                                else
                                {
                                    if (bp1.successFraction != 0f)
                                    {
                                        InstEoka updateinfo;
                                        updateinfo = RemEoka[itemID];
                                        updateinfo.successFraction = bp1.successFraction;
                                        updateinfo.repeatDelay = bp1.repeatDelay;
                                        updateinfo.deployDelay = bp1.deployDelay;
                                        updateinfo.automatic = bp1.automatic;
                                        RemEoka[itemID] = updateinfo;
                                    }

                                }
                                bp1.successFraction = 100f;
                                bp1.automatic = true;
                                bp1.repeatDelay = 0f;
                                bp1.deployDelay = 0f;
                            }
                        }
                        else
                            if (bp1.successFraction == 0f)
                        {
                            bp1.deployDelay = RemEoka[itemID].deployDelay;
                            bp1.automatic = RemEoka[itemID].automatic;
                            bp1.repeatDelay = RemEoka[itemID].repeatDelay;
                            bp1.successFraction = RemEoka[itemID].successFraction;
                        }
                    }
                    if (bp != null && !bp1)
                    {
                        if (Menu.CFG.MiscConfig._automatic)
                        {
                            bp.automatic = true;
                        }
                        if (Menu.CFG.MiscConfig._sway)
                        {
                            if (bp.aimSway != 0f)
                            {
                                if (!RemRecoil.ContainsKey(itemID))
                                {
                                    var newinfo = new RecoilZ
                                    {
                                        aimSway = bp.aimSway,
                                        aimSwaySpeed = bp.aimSwaySpeed
                                    };
                                    RemRecoil.Add(itemID, newinfo);
                                }
                                else
                                {
                                    if (bp.aimSway != 0f)
                                    {
                                        RecoilZ updateinfo;
                                        updateinfo = RemRecoil[itemID];
                                        updateinfo.aimSway = bp.aimSway;
                                        updateinfo.aimSwaySpeed = bp.aimSwaySpeed;
                                        RemRecoil[itemID] = updateinfo;
                                    }
                                    bp.aimSway = 0f;
                                    bp.aimSwaySpeed = 0f;
                                }
                            }
                        }
                        else
                        {
                            if (bp.aimSway == 0f)
                            {
                                bp.aimSway = RemRecoil[itemID].aimSway;
                                bp.aimSwaySpeed = RemRecoil[itemID].aimSwaySpeed;
                            }
                        }
                    }
                }
            }
            catch
            {
            }
             
            LocalPlayer.Entity.input.FrameUpdate();
            LocalPlayer.Entity.modelState.ducked = false;
            LocalPlayer.Entity.modelState.sprinting = false;
            LocalPlayer.Entity.modelState.aiming = false;
            LocalPlayer.Entity.modelState.sleeping = LocalPlayer.Entity.IsSleeping();
            LocalPlayer.Entity.modelState.waterLevel = LocalPlayer.Entity.WaterFactor();
            LocalPlayer.Entity.voiceRecorder.ClientInput(state);
                typeof(PlayerEyes).GetField("EyeOffset",
                                     BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetField).SetValue(LocalPlayer.Entity.transform.position,
                                       new Vector3(0f, (UnityEngine.Input.GetKey(Menu.CFG.MiscConfig._upEyeKey) && Menu.CFG.MiscConfig._upEye) ? 2.8f : 1.5f, 0f));

            try
            {
               
                if (Visuals.FullDrawing.IsOpen() && Menu.CFG.MiscConfig._viewMode)
                    if (UnityEngine.Input.GetKeyDown(Menu.CFG.MiscConfig._viewModeeKey))
                        _eye = !_eye;
                if (_eye && LocalPlayer.Entity != null)
                {
                    ConVar.Client.camdist = 3f;
                    LocalPlayer.Entity.selectedViewMode++;
                    if (LocalPlayer.Entity.selectedViewMode > BasePlayer.CameraMode.FirstPerson) 
                        LocalPlayer.Entity.selectedViewMode = BasePlayer.CameraMode.ThirdPerson; 
                }
                if (Menu.CFG.MiscConfig._time)
                {
                    if (UnityEngine.Input.GetKey(Menu.CFG.MiscConfig._timeScaleKey))
                        UnityEngine.Time.timeScale = Menu.CFG.MiscConfig._timeScale;
                    else
                        UnityEngine.Time.timeScale = 1f;
                }
                var heldshits = LocalPlayer.Entity.GetHeldEntity().GetComponent<BaseProjectile>();
                if (Menu.CFG.MiscConfig._silentShoot && UnityEngine.Input.GetKey(Menu.CFG.MiscConfig._silentShootKey) && heldshits != null && Visuals.FullDrawing.IsOpen())
                {
                    if (heldshits.primaryMagazine.contents != 0)
                    { 
                        if (!heldshits.HasReloadCooldown())
                        {
                            if (!heldshits.HasAttackCooldown())
                            {
                                for (int i = 0; i < 3; i++) 
                                    heldshits.LaunchProjectile();
                                heldshits.primaryMagazine.contents--;
                                heldshits.UpdateAmmoDisplay();
                                heldshits.ShotFired();
                                heldshits.BeginCycle();
                            }
                        }
                    }
                } 
            }
            catch { } 
            if (LocalPlayer.Entity != null && LocalPlayer.Entity.GetHeldEntity())
            {
                var ActiveModel = BaseViewModel.ActiveModel;
                /*if (Menu.CFG.MiscConfig._noAnimation)
                { 
                    if (ActiveModel != null)
                    {
                        if (LocalPlayer.Entity.input.state.IsDown(BUTTON.FIRE_PRIMARY))
                            ActiveModel.animator.speed = -1;
                    }
                } */ // No Animation
                if (ActiveModel != null)
                {
                    ActiveModel.bob.bobAmountRun = Menu.CFG.MiscConfig._noBobbing ? 0f : 0.02f;
                    ActiveModel.bob.bobAmountWalk = Menu.CFG.MiscConfig._noBobbing ? 0f : 0.005f;
                    ActiveModel.bob.bobSpeedRun = Menu.CFG.MiscConfig._noBobbing ? 0f : 13f;
                    ActiveModel.bob.bobSpeedWalk = Menu.CFG.MiscConfig._noBobbing ? 0f : 9f;
                }
            } 
            UnityEngine.Physics.IgnoreLayerCollision((int)Rust.Layer.Player_Movement, (int)Rust.Layer.Water, !Menu.CFG.MiscConfig._collisionWater);//Ходить по воде
            UnityEngine.Physics.IgnoreLayerCollision((int)Rust.Layer.Player_Movement, (int)Rust.Layer.Tree, Menu.CFG.MiscConfig._noCollisionTree);//Ходить сквозь дерево
            UnityEngine.Physics.IgnoreLayerCollision((int)Rust.Layer.Player_Movement, (int)Rust.Layer.AI, Menu.CFG.MiscConfig._noCollisionAI);//Ходить сквозь людей 
            if (held_entity  != null && Menu.CFG.MiscConfig._fakeShoot && (UnityEngine.Input.GetKey(Menu.CFG.MiscConfig._fakeShootKey) || Menu.CFG.MiscConfig._alwayFakeShoot) && Visuals.FullDrawing.IsOpen())
                if (held_entity.GetComponent<BaseProjectile>() != null)
                    held_entity.SendSignalBroadcast(BaseEntity.Signal.Attack, ""); // fake shoot 
            if (LocalPlayer.Entity.HasLocalControls() && !NeedsKeyboard.AnyActive())
            {
                LocalPlayer.Entity.Belt.ClientInput(state);
            }
            else
            {
                UIInventory.Close();
                MapInterface.SetOpen(false);
            }
            if (!LocalPlayer.Entity.Frozen)
            {
                LocalPlayer.Entity.HeldEntityInput();
                if (LocalPlayer.Entity.movement)
                {
                    using (TimeWarning.New("movement.ClientInput", 0.1f))
                    {
                        LocalPlayer.Entity.movement.ClientInput(state, LocalPlayer.Entity.modelState);
                    }
                }
                using (TimeWarning.New("UseAction", 0.1f))
                {
                    LocalPlayer.Entity.UseAction(state);
                }
                if (Buttons.Chat.JustPressed && LocalPlayer.Entity.input.hadInputBuffer && ConVar.Graphics.chat && ConVar.Chat.enabled)
                {
                    ConVar.Chat.open();
                }
                using (TimeWarning.New("MapInterface Update", 0.1f))
                {
                    MapInterface.DoPlayerUpdate();
                    MapInterface.SetOpen(Buttons.Map.IsDown);
                }
            } 
            var heldEntity = LocalPlayer.Entity.GetHeldEntity(); 
            if (_farmBot[0] && heldEntity != null && (activeItem.info.shortname.Contains("axe.salvaged") || activeItem.info.shortname.Contains("bone.club")
                                        || activeItem.info.shortname.Contains("cakefiveyear") || activeItem.info.shortname.Contains("chainsaw") || activeItem.info.shortname.Contains("candycaneclub")
                                        || activeItem.info.shortname.Contains("hammer.salvaged") || activeItem.info.shortname.Contains("hatchet")
                                        || activeItem.info.itemid == 3506021 || activeItem.info.shortname.Contains("sickle") || activeItem.info.shortname.Contains("stonehatchet")
                                        || activeItem.info.shortname.Contains("cakefiveyear") || activeItem.info.shortname.Contains("hammer.salvaged")
                                        || activeItem.info.shortname.Contains("icepick.salvaged") || activeItem.info.shortname.Contains("jackhammer")
                                        || activeItem.info.shortname.Contains("pickaxe")
                                        || activeItem.info.shortname.Contains("stone.pickaxe") || activeItem.info.shortname.Contains("bone.club") || activeItem.info.shortname.Contains("candycaneclub")))
            {
                if (_farmBot[1] || _farmBot[2] || _farmBot[3] || _farmBot[4])
                    LocalPlayer.Entity.modelState.SetFlag(ModelState.Flag.Sprinting, true);
                if (_farmBot[5])
                    LocalPlayer.Entity.modelState.SetFlag(ModelState.Flag.OnLadder, true);

            }
        }  
    }
}