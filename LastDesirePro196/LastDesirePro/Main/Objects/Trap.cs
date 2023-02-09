using System.Collections.Generic;
using UnityEngine;
namespace LastDesirePro.Main.Objects
{
    public partial class ObjectsCheck
    {
        public struct Trap
        {
            public string name;
            public float health;
            public Vector3 position;
            public BaseEntity entity;
        }
        public static Dictionary<int, Trap> _GunTrap = new Dictionary<int, Trap> { };
    }
}
