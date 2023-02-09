using Facepunch;
using LastDesirePro.Attributes;
using Network;
using ProtoBuf;
using Rust;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using UnityEngine;
using static TrackIR;

namespace LastDesirePro.Main.Objects
{
    [Component]
    public partial class ObjectsCheck : MonoBehaviour
    {
        private void Start()
        {
            base.StartCoroutine(ObjectIsValid());
        } 
        private static IEnumerator ObjectIsValid()
        {
            while (true)
            {
                try
                {
                    var t = new List<int> { };
                    var k = new List<int>(_PlantEntity.Keys);
                    foreach (var key in k)
                        if (!_PlantEntity[key].entity.IsValid())
                            t.Add(key);
                    if (t.Count > 0)
                        foreach (var key in t)
                            _PlantEntity.Remove(key);



                    var temp212 = new List<int> { };
                    var keys212 = new List<int>(_marker.Keys);
                    foreach (var key in keys212)
                        if (!_marker[key].entity.IsValid())
                            temp212.Add(key);
                    if (temp212.Count > 0)
                        foreach (var key in temp212)
                            _marker.Remove(key);
                    var temp114 = new List<int> { };
                    var keys114 = new List<int>(_SupplyDrop.Keys);
                    foreach (var key in keys114)
                        if (!_SupplyDrop[key].entity.IsValid())
                            temp114.Add(key);
                    if (temp114.Count > 0)
                        foreach (var key in temp114)
                            _SupplyDrop.Remove(key);


                    var temp41 = new List<int> { };
                    var keys41 = new List<int>(_ContainerStorage.Keys);
                    foreach (var key in keys41)
                        if (!_ContainerStorage[key].entity.IsValid())
                            temp41.Add(key);
                    if (temp41.Count > 0)
                        foreach (var key in temp41)
                            _ContainerStorage.Remove(key);

                    var temp14 = new List<int> { };
                    var keys14 = new List<int>(_Container1.Keys);
                    foreach (var key in keys14)
                        if (!_Container1[key].entity.IsValid())
                            temp14.Add(key);
                    if (temp14.Count > 0)
                        foreach (var key in temp14)
                            _Container1.Remove(key);

                    var temp13 = new List<int> { };
                    var keys13 = new List<int>(_Container1.Keys);
                    foreach (var key in keys13)
                        if (!_Container1[key].entity.IsValid())
                            temp13.Add(key);
                    if (temp13.Count > 0)
                        foreach (var key in temp13)
                            _Container1.Remove(key);


                    var temp12 = new List<int> { };
                    var keys12 = new List<int>(_BradleyAPC.Keys);
                    foreach (var key in keys12)
                        if (!_BradleyAPC[key].entity.IsValid())
                            temp12.Add(key);
                    if (temp12.Count > 0)
                        foreach (var key in temp12)
                            _BradleyAPC.Remove(key);

                    var temp11 = new List<int> { };
                    var keys11 = new List<int>(_BaseHelicopter.Keys);
                    foreach (var key in keys11)
                        if (!_BaseHelicopter[key].entity.IsValid())
                            temp11.Add(key);
                    if (temp11.Count > 0)
                        foreach (var key in temp11)
                            _BaseHelicopter.Remove(key);

                    var temp10 = new List<int> { };
                    var keys10 = new List<int>(_CollectibleEntity.Keys);
                    foreach (var key in keys10)
                        if (!_CollectibleEntity[key].entity.IsValid())
                            temp10.Add(key);
                    if (temp10.Count > 0)
                        foreach (var key in temp10)
                            _CollectibleEntity.Remove(key);

                    var temp9 = new List<int> { };
                    var keys9 = new List<int>(_OreHotSpot.Keys);
                    foreach (var key in keys9)
                        if (!_OreHotSpot[key].entity.IsValid())
                            temp9.Add(key);
                    if (temp9.Count > 0)
                        foreach (var key in temp9)
                            _OreHotSpot.Remove(key);


                    var temp8 = new List<int> { };
                    var keys8 = new List<int>(_GunTrap.Keys);
                    foreach (var key in keys8)
                        if (!_GunTrap[key].entity.IsValid())
                            temp8.Add(key);
                    if (temp8.Count > 0)
                        foreach (var key in temp8)
                            _GunTrap.Remove(key);

                    var temp7 = new List<int> { };
                    var keys7 = new List<int>(_FlameTurret.Keys);
                    foreach (var key in keys7)
                        if (!_FlameTurret[key].entity.IsValid())
                            temp7.Add(key);
                    if (temp7.Count > 0)
                        foreach (var key in temp7)
                            _FlameTurret.Remove(key);

                    var temp6 = new List<int> { };
                    var keys6 = new List<int>(_BearTrap.Keys);
                    foreach (var key in keys6)
                        if (!_BearTrap[key].entity.IsValid())
                            temp6.Add(key);
                    if (temp6.Count > 0)
                        foreach (var key in temp6)
                            _BearTrap.Remove(key);

                    var temp5 = new List<int> { };
                    var keys5 = new List<int>(_Landmine.Keys);
                    foreach (var key in keys5)
                        if (!_Landmine[key].entity.IsValid())
                            temp5.Add(key);
                    if (temp5.Count > 0)
                        foreach (var key in temp5)
                            _Landmine.Remove(key);

                    var temp = new List<int> { };
                    var keys = new List<int>(_Resources.Keys);
                    foreach (var key in keys)
                        if (!_Resources[key].entity.IsValid())
                            temp.Add(key);
                    if (temp.Count > 0)
                        foreach (var key in temp)
                            _Resources.Remove(key);

                    var temp1 = new List<int> { };
                    var keys1 = new List<int>(_StorageContainer.Keys);
                    foreach (var key in keys1)
                        if (!_StorageContainer[key].entity.IsValid())
                            temp1.Add(key);
                    if (temp1.Count > 0)
                        foreach (var key in temp1)
                            _StorageContainer.Remove(key);

                    var temp2 = new List<int> { };
                    var keys2 = new List<int>(_Cupboard.Keys);
                    foreach (var key in keys2)
                        if (!_Cupboard[key].entity.IsValid())
                            temp2.Add(key);
                    if (temp2.Count > 0)
                        foreach (var key in temp2)
                            _Cupboard.Remove(key);

                    var temp3 = new List<int> { };
                    var keys3 = new List<int>(_AutoTurret.Keys);
                    foreach (var key in keys3)
                        if (!_AutoTurret[key].entity.IsValid())
                            temp3.Add(key);
                    if (temp3.Count > 0)
                        foreach (var key in temp3)
                            _AutoTurret.Remove(key);

                    var temp4 = new List<int> { };
                    var keys4 = new List<int>(_StashContainer.Keys);
                    foreach (var key in keys4)
                        if (!_StashContainer[key].entity.IsValid())
                            temp4.Add(key);
                    if (temp4.Count > 0)
                        foreach (var key in temp4)
                            _StashContainer.Remove(key); 
                }
                catch { }
                yield return new WaitForSeconds(2f);
            }
        }



        [Replacement(typeof(BaseEntity), "UpdatePositionFromNetwork")]
        public virtual void UpdatePositionFromNetwork(Vector3 vPos)
        {
            try
            {
                var BaseEnt = GetComponent<BaseEntity>();
                if (Equals(GetType(), typeof(OreResourceEntity)))
                {
                    if (_Resources.ContainsKey(GetInstanceID()))
                    {
                        var update = new Resource();
                        update.name = _Resources[GetInstanceID()].name;
                        update.entity = _Resources[GetInstanceID()].entity;
                        update.position = vPos;
                        _Resources[GetInstanceID()] = update;
                    }
                    else
                    {
                        var add = new Resource();
                        add.name = name;
                        add.position = vPos;
                        add.entity = BaseEnt;
                        _Resources.Add(GetInstanceID(), add);
                    }
                }
                if (BaseEnt.name.Contains("woodbox") || BaseEnt.name.Contains("box.wooden.")
                    || BaseEnt.ShortPrefabName.Contains("refinery_small") || BaseEnt.name.Contains("bbq.")
                    || BaseEnt.name.Contains("furnace.prefab") || BaseEnt.name.Contains("furnace._deployed")
                    || BaseEnt.ShortPrefabName.Contains("repairbench") || BaseEnt.name.Contains("furnace.large.")
                    || BaseEnt.name.Contains("dropbox.deployed") || BaseEnt.name.Contains("stocking_small_deployed")
                    || BaseEnt.name.Contains("researchtable_deployed") || BaseEnt.name.Contains("fireplace.deployed")
                    || BaseEnt.name.Contains("workbench1.deployed") || BaseEnt.name.Contains("workbench2.deployed")
                    || BaseEnt.name.Contains("workbench3.deployed") || BaseEnt.name.Contains("stocking_large_deployed"))
                {
                    if (_StorageContainer.ContainsKey(GetInstanceID()))
                    {
                        var update = new Container();
                        update.name = _StorageContainer[GetInstanceID()].name;
                        update.health = BaseEnt.GetComponent<StorageContainer>().health;
                        update.entity = _StorageContainer[GetInstanceID()].entity;
                        update.position = vPos;
                        _StorageContainer[GetInstanceID()] = update;
                    }
                    else
                    {
                        var add = new Container();
                        add.name = name;
                        add.health = BaseEnt.GetComponent<StorageContainer>().health;
                        add.position = vPos;
                        add.entity = BaseEnt;
                        _StorageContainer.Add(GetInstanceID(), add);
                    }
                }
                if (BaseEnt.name.Contains("crate_tools.") || BaseEnt.name.Contains("crate_basic.")
                    || BaseEnt.name.Contains("crate_normal_2_food.") || BaseEnt.name.Contains("crate_normal_2.")
                    || BaseEnt.PrefabName.Contains("food") || BaseEnt.PrefabName.Contains("trash-pile-1")
                    || BaseEnt.name.Contains("crate_normal_2_medical.") || BaseEnt.name.Contains("crate_mine.")
                    || BaseEnt.name.Contains("crate_normal.") || BaseEnt.name.Contains("crate_elite.")
                    || BaseEnt.name.Contains("loot-barrel-1.") || BaseEnt.name.Contains("loot-barrel-2.")
                    || BaseEnt.name.Contains("loot_barrel_1.") || BaseEnt.name.Contains("hobobarrel_static.")
                    || BaseEnt.name.Contains("loot_barrel_2.") || BaseEnt.name.Contains("oil_barrel.")
                    || BaseEnt.PrefabName.Contains("recycler"))
                {
                    if (_Container1.ContainsKey(GetInstanceID()))
                    {
                        var update = new Container1();
                        update.name = _Container1[GetInstanceID()].name;
                        update.entity = _Container1[GetInstanceID()].entity;
                        update.position = vPos;
                        _Container1[GetInstanceID()] = update;
                    }
                    else
                    {
                        var add = new Container1();
                        add.name = name;
                        add.position = vPos;
                        add.entity = BaseEnt;
                        _Container1.Add(GetInstanceID(), add);
                    }
                }
                if (BaseEnt.PrefabName.Contains("cupboard"))
                {
                    if (_Cupboard.ContainsKey(GetInstanceID()))
                    {
                        var update = new Building();
                        update.name = _Cupboard[GetInstanceID()].name;
                        update.health = BaseEnt.GetComponent<BuildingPrivlidge>().health;
                        update.entity = _Cupboard[GetInstanceID()].entity;
                        update.position = vPos;
                        _Cupboard[GetInstanceID()] = update;
                    }
                    else
                    {
                        var add = new Building();
                        add.name = "Cupboard";
                        add.health = BaseEnt.GetComponent<BuildingPrivlidge>().health;
                        add.position = vPos;
                        add.entity = BaseEnt;
                        _Cupboard.Add(GetInstanceID(), add);
                    }
                }
                if (Equals(GetType(), typeof(AutoTurret)))
                {
                    if (_AutoTurret.ContainsKey(GetInstanceID()))
                    {
                        var update = new AutoTurrets();
                        update.name = _AutoTurret[GetInstanceID()].name;
                        update.health = BaseEnt.GetComponent<AutoTurret>().health;
                        update.entity = _AutoTurret[GetInstanceID()].entity;
                        update.position = vPos;
                        _AutoTurret[GetInstanceID()] = update;
                    }
                    else
                    {
                        var add = new AutoTurrets();
                        add.name = name;
                        add.health = BaseEnt.GetComponent<AutoTurret>().health;
                        add.position = vPos;
                        add.entity = BaseEnt;
                        _AutoTurret.Add(GetInstanceID(), add);
                    }
                }
                if (Equals(GetType(), typeof(StashContainer)))
                {
                    if (_StashContainer.ContainsKey(GetInstanceID()))
                    {
                        var update = new Stash();
                        update.name = _StashContainer[GetInstanceID()].name;
                        update.health = BaseEnt.GetComponent<StashContainer>().health;
                        update.IsHidden = BaseEnt.GetComponent<StashContainer>().IsHidden();
                        update.entity = _StashContainer[GetInstanceID()].entity;
                        update.position = vPos;
                        _StashContainer[GetInstanceID()] = update;
                    }
                    else
                    {
                        var add = new Stash();
                        add.name = name;
                        add.health = BaseEnt.GetComponent<StashContainer>().health;
                        add.IsHidden = BaseEnt.GetComponent<StashContainer>().IsHidden();
                        add.position = vPos;
                        add.entity = BaseEnt;
                        _StashContainer.Add(GetInstanceID(), add);
                    }
                }
                if (Equals(GetType(), typeof(Landmine)))
                {
                    if (_Landmine.ContainsKey(GetInstanceID()))
                    {
                        var update = new Mine();
                        update.name = _Landmine[GetInstanceID()].name;
                        update.health = BaseEnt.GetComponent<Landmine>().health;
                        update.entity = _Landmine[GetInstanceID()].entity;
                        update.position = vPos;
                        _Landmine[GetInstanceID()] = update;
                    }
                    else
                    {
                        var add = new Mine();
                        add.name = name;
                        add.health = BaseEnt.GetComponent<Landmine>().health;
                        add.position = vPos;
                        add.entity = BaseEnt;
                        _Landmine.Add(GetInstanceID(), add);
                    }
                }
                if (Equals(GetType(), typeof(BearTrap)))
                {
                    if (_BearTrap.ContainsKey(GetInstanceID()))
                    {
                        var update = new Bear();
                        update.name = _BearTrap[GetInstanceID()].name;
                        update.health = BaseEnt.GetComponent<BearTrap>().health;
                        update.entity = _BearTrap[GetInstanceID()].entity;
                        update.position = vPos;
                        _BearTrap[GetInstanceID()] = update;
                    }
                    else
                    {
                        var add = new Bear();
                        add.name = name;
                        add.health = BaseEnt.GetComponent<BearTrap>().health;
                        add.position = vPos;
                        add.entity = BaseEnt;
                        _BearTrap.Add(GetInstanceID(), add);
                    }
                }
                if (Equals(GetType(), typeof(FlameTurret)))
                {
                    if (_FlameTurret.ContainsKey(GetInstanceID()))
                    {
                        var update = new Flame();
                        update.name = _FlameTurret[GetInstanceID()].name;
                        update.health = BaseEnt.GetComponent<FlameTurret>().health;
                        update.entity = _FlameTurret[GetInstanceID()].entity;
                        update.position = vPos;
                        _FlameTurret[GetInstanceID()] = update;
                    }
                    else
                    {
                        var add = new Flame();
                        add.name = name;
                        add.health = BaseEnt.GetComponent<FlameTurret>().health;
                        add.position = vPos;
                        add.entity = BaseEnt;
                        _FlameTurret.Add(GetInstanceID(), add);
                    }
                }
                if (Equals(GetType(), typeof(GunTrap)))
                {
                    if (_GunTrap.ContainsKey(GetInstanceID()))
                    {
                        var update = new Trap();
                        update.name = _GunTrap[GetInstanceID()].name;
                        update.health = BaseEnt.GetComponent<GunTrap>().health;
                        update.entity = _GunTrap[GetInstanceID()].entity;
                        update.position = vPos;
                        _GunTrap[GetInstanceID()] = update;
                    }
                    else
                    {
                        var add = new Trap();
                        add.name = name;
                        add.health = BaseEnt.GetComponent<GunTrap>().health;
                        add.position = vPos;
                        add.entity = BaseEnt;
                        _GunTrap.Add(GetInstanceID(), add);
                    }
                }
                if (Equals(GetType(), typeof(OreHotSpot)))
                {
                    if (_OreHotSpot.ContainsKey(GetInstanceID()))
                    {
                        var update = new OreMarker();
                        update.name = _OreHotSpot[GetInstanceID()].name;
                        update.entity = _OreHotSpot[GetInstanceID()].entity;
                        update.position = vPos;
                        _OreHotSpot[GetInstanceID()] = update;
                    }
                    else
                    {
                        var add = new OreMarker();
                        add.name = name;
                        add.position = vPos;
                        add.entity = BaseEnt;
                        _OreHotSpot.Add(GetInstanceID(), add);
                    }
                }
                if (Equals(GetType(), typeof(CollectibleEntity)))
                {
                    if (_CollectibleEntity.ContainsKey(GetInstanceID()))
                    {
                        var update = new Collectible();
                        update.name = _CollectibleEntity[GetInstanceID()].name;
                        update.entity = _CollectibleEntity[GetInstanceID()].entity;
                        update.position = vPos;
                        _CollectibleEntity[GetInstanceID()] = update;
                    }
                    else
                    {
                        var add = new Collectible();
                        add.name = name;
                        add.position = vPos;
                        add.entity = BaseEnt;
                        _CollectibleEntity.Add(GetInstanceID(), add);
                    }
                }
                if (Equals(GetType(), typeof(BaseHelicopter)))
                {
                    if (_BaseHelicopter.ContainsKey(GetInstanceID()))
                    {
                        var update = new Helicopter();
                        update.name = _BaseHelicopter[GetInstanceID()].name;
                        update.health = GetComponent<BaseHelicopter>().health;
                        update.MainRotor = GetComponent<BaseHelicopter>().weakspots[0].health;
                        update.TailRotor = GetComponent<BaseHelicopter>().weakspots[1].health;
                        update.entity = _BaseHelicopter[GetInstanceID()].entity;
                        update.position = vPos;
                        _BaseHelicopter[GetInstanceID()] = update;
                    }
                    else
                    {
                        var add = new Helicopter();
                        add.name = name;
                        add.health = GetComponent<BaseHelicopter>().health;
                        add.MainRotor = GetComponent<BaseHelicopter>().weakspots[0].health;
                        add.TailRotor = GetComponent<BaseHelicopter>().weakspots[1].health;
                        add.position = vPos;
                        add.entity = BaseEnt;
                        _BaseHelicopter.Add(GetInstanceID(), add);
                    }
                }
                if (Equals(GetType(), typeof(BradleyAPC)))
                {
                    if (_BradleyAPC.ContainsKey(GetInstanceID()))
                    {
                        var update = new Bradley();
                        update.name = _BradleyAPC[GetInstanceID()].name;
                        update.health = GetComponent<BradleyAPC>().health;
                        update.entity = _BradleyAPC[GetInstanceID()].entity;
                        update.position = vPos;
                        _BradleyAPC[GetInstanceID()] = update;
                    }
                    else
                    {
                        var add = new Bradley();
                        add.name = name;
                        add.health = GetComponent<BradleyAPC>().health;
                        add.position = vPos;
                        add.entity = BaseEnt;
                        _BradleyAPC.Add(GetInstanceID(), add);
                    }
                }
                if (Equals(GetType(), typeof(SupplyDrop)))
                {
                    if (_SupplyDrop.ContainsKey(GetInstanceID()))
                    {
                        var update = new Supply();
                        update.name = _SupplyDrop[GetInstanceID()].name;
                        update.entity = _SupplyDrop[GetInstanceID()].entity;
                        update.position = vPos;
                        _SupplyDrop[GetInstanceID()] = update;
                    }
                    else
                    {
                        var add = new Supply();
                        add.name = name;
                        add.position = vPos;
                        add.entity = BaseEnt;
                        _SupplyDrop.Add(GetInstanceID(), add);
                    }
                }
                if (BaseEnt.name.Contains("bradley_crate.") || BaseEnt.name.Contains("heli_crate."))
                {
                    if (_ContainerStorage.ContainsKey(GetInstanceID()))
                    {
                        var update = new Storage();
                        update.name = _ContainerStorage[GetInstanceID()].name;
                        update.IsOnFire = BaseEnt.GetComponent<StorageContainer>().IsOnFire();
                        update.entity = _ContainerStorage[GetInstanceID()].entity;
                        update.position = vPos;
                        _ContainerStorage[GetInstanceID()] = update;
                    }
                    else
                    {
                        var add = new Storage();
                        add.name = name;
                        add.IsOnFire = BaseEnt.GetComponent<StorageContainer>().IsOnFire();
                        add.position = vPos;
                        add.entity = BaseEnt;
                        _ContainerStorage.Add(GetInstanceID(), add);
                    }
                }
                if (BaseEnt.PrefabName.Contains("tree_marking"))
                {
                    if (_marker.ContainsKey(base.GetInstanceID()))
                    {
                        var update = new treemark();
                        update.name = _marker[base.GetInstanceID()].name;
                        update.entity = _marker[base.GetInstanceID()].entity;
                        update.position = vPos;
                        _marker[base.GetInstanceID()] = update;
                    }
                    else
                    {
                        var add = new treemark();
                        add.name = "tree_marking";
                        add.position = vPos;
                        add.entity = BaseEnt;
                        _marker.Add(base.GetInstanceID(), add);
                    }
                }
                if (Equals(GetType(), typeof(PlantEntity)))
                {
                    if (_CollectibleEntity.ContainsKey(GetInstanceID()))
                    {
                        var update = new Plant();
                        update.pick = BaseEnt.GetComponent<PlantEntity>().CanPick();
                        update.ss = BaseEnt.GetComponent<PlantEntity>().currentStage;
                        update.name = _PlantEntity[GetInstanceID()].name;
                        update.entity = _PlantEntity[GetInstanceID()].entity;
                        update.position = vPos;
                        _PlantEntity[GetInstanceID()] = update;
                    }
                    else
                    {
                        var add = new Plant();
                        add.pick = BaseEnt.GetComponent<PlantEntity>().CanPick();
                        add.ss = BaseEnt.GetComponent<PlantEntity>().currentStage;
                        add.name = name;
                        add.position = vPos;
                        add.entity = BaseEnt;
                        _PlantEntity.Add(GetInstanceID(), add);
                    }
                }

            }
            catch { }
            if (transform.localPosition == vPos)
                return;
            transform.localPosition = vPos;
        }
        public struct Plant
        {
            public string name;
            public bool pick;
            public PlantProperties.Stage ss;
            public Vector3 position;
            public BaseEntity entity;
        }
        public static Dictionary<int, Plant> _PlantEntity = new Dictionary<int, Plant> { };
        public struct treemark
        {
            public string name;
            public Vector3 position;
            public BaseEntity entity;
        }
        public static Dictionary<int, treemark> _marker = new Dictionary<int, treemark> { }; 
        public static BufferList<BaseNetworkable> Networkable() => ((ListDictionary<uint, BaseNetworkable>)typeof(BaseNetworkable.EntityRealm).GetField("entityList", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(BaseNetworkable.clientEntities)).Values;
    }
}
