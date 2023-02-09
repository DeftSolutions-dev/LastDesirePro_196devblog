using UnityEngine;

namespace LastDesirePro.DrawMenu
{
	public static class Drawing
	{
		public static void DrawRect(Rect position, UnityEngine.Color color, float radius, GUIContent content = null)
		{
			var backgroundColor = GUI.backgroundColor;
			GUI.backgroundColor = color;
			RectFilled(position, color, radius);
			GUI.backgroundColor = backgroundColor;
		}
		static Drawing()
		{
			if (txt3D == null)
			{
				txt3D = new Texture2D(1, 1, TextureFormat.ARGB32, true);
				txt3D.SetPixel(0, 1, UnityEngine.Color.white);
				txt3D.Apply();
			}
		}
		 
		private static Texture2D txt3D = null;
		public static void RectFilled(Rect position, UnityEngine.Color color, float radius)
		{
			GUI.color = UnityEngine.Color.white;
			GUI.DrawTexture(position, txt3D, ScaleMode.StretchToFill, true, 1, color, 1111, radius);
        }
        public static void DrawRects(Rect position, UnityEngine.Color color, float radius, GUIContent content = null)
        {
            var backgroundColor = GUI.backgroundColor;
            GUI.backgroundColor = color;
            RectFill(position, color, radius);
            GUI.backgroundColor = backgroundColor;
        }
        public static void RectFill(Rect position, UnityEngine.Color color, float radius)
        {
            GUI.color = UnityEngine.Color.white;
            GUI.DrawTexture(position, txt3D, ScaleMode.ScaleToFit, true, 1, color, 1, radius);
        }
	}
}
