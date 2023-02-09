using System.Collections.Generic;
using UnityEngine;
namespace LastDesirePro.Main.Objects
{
    public partial class ObjectsCheck
    {
        public struct OreMarker
        {
            public string name;
            public Vector3 position;
            public BaseEntity entity;
        }
        public static Dictionary<int, OreMarker> _OreHotSpot = new Dictionary<int, OreMarker> { };
    }
}
