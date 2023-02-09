using System.Collections.Generic;
using UnityEngine;
namespace LastDesirePro.Main.Objects
{
    public partial class ObjectsCheck
    {
        public struct Container1
        {
            public string name;
            public BaseEntity entity;
            public Vector3 position;
        }
        public static Dictionary<int, Container1> _Container1 = new Dictionary<int, Container1>() { };
    }
}
