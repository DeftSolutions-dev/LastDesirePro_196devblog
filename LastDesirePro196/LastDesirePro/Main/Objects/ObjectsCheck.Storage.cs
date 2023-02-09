using System.Collections.Generic;
using UnityEngine;
namespace LastDesirePro.Main.Objects
{
    public partial class ObjectsCheck
    {
        public struct Storage
        {
            public string name;
            public bool IsOnFire;
            public Vector3 position;
            public BaseEntity entity;
        }
        public static Dictionary<int, Storage> _ContainerStorage = new Dictionary<int, Storage> { };
    }
}
