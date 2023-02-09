using System.Collections.Generic;
using UnityEngine;
namespace LastDesirePro.Main.Objects
{
    public partial class ObjectsCheck
    {
        public struct Supply
        {
            public string name;
            public Vector3 position;
            public BaseEntity entity;
        }
        public static Dictionary<int, Supply> _SupplyDrop = new Dictionary<int, Supply> { };
    }
}
