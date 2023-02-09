using System.Collections.Generic;
using UnityEngine;
namespace LastDesirePro.Main.Objects
{
    public partial class ObjectsCheck
    {
        public struct AutoTurrets
        {
            public string name;
            public float health;
            public Vector3 position;
            public BaseEntity entity;
        }
        public static Dictionary<int, AutoTurrets> _AutoTurret = new Dictionary<int, AutoTurrets> { };
    }
}
