using System.Collections.Generic;
using UnityEngine;
namespace LastDesirePro.Main.Objects
{
    public partial class ObjectsCheck
    {
        public struct Helicopter
        {
            public string name;
            public float health;
            public Vector3 position;
            public float MainRotor;
            public float TailRotor;
            public BaseEntity entity;
        }
        public static Dictionary<int, Helicopter> _BaseHelicopter = new Dictionary<int, Helicopter> { };
    }
}
