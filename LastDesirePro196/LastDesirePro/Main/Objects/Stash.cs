using System.Collections.Generic;
using UnityEngine;
namespace LastDesirePro.Main.Objects
{
    public partial class ObjectsCheck
    {
        public struct Stash
        {
            public string name;
            public float health;
            public bool IsHidden;
            public Vector3 position;
            public BaseEntity entity;
        }
        public static Dictionary<int, Stash> _StashContainer = new Dictionary<int, Stash> { };
    }
}
