using System.Collections.Generic;
using UnityEngine;
namespace LastDesirePro.Main.Objects
{
    public partial class ObjectsCheck
    {
        public struct Resource
        {
            public string name;
            public Vector3 position;
            public BaseEntity entity;
        }
        public static Dictionary<int, Resource> _Resources = new Dictionary<int, Resource> { };
    }
}
