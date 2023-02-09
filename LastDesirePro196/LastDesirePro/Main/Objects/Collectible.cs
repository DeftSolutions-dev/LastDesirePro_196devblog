using System.Collections.Generic;
using UnityEngine;
namespace LastDesirePro.Main.Objects
{
    public partial class ObjectsCheck
    {
        public struct Collectible
        {
            public string name;
            public Vector3 position;
            public BaseEntity entity;
        }
        public static Dictionary<int, Collectible> _CollectibleEntity = new Dictionary<int, Collectible> { };
    }
}
