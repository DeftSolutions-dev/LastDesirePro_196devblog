using UnityEngine;

namespace LastDesirePro.DrawMenu
{
    public class Vectors
    {
        public float x, y;
        public Vectors(float nx, float ny)
        {
            x = nx;
            y = ny;
        }
        public Vector2 ToVector2() =>
            new Vector2(x, y);
        public static implicit operator Vector2(Vectors vector) => vector.ToVector2();
        public static implicit operator Vectors(Vector2 vector) => new Vectors(vector.x, vector.y);
    }
}
