
using UnityEngine;

namespace LastDesirePro.DrawMenu
{
    public static class MenuFix
    {
        public static Texture2D TexClear;

        static MenuFix()
        {
            TexClear = new Texture2D(1, 1);
            TexClear.SetPixel(0, 0, new Color(0, 0, 0, 0));
            TexClear.Apply();
        }

        public static void FixGUIStyleColor(GUIStyle style)
        {
            style.normal.background = TexClear;
            style.onNormal.background = TexClear;
            style.hover.background = TexClear;
            style.onHover.background = TexClear;
            style.active.background = TexClear;
            style.onActive.background = TexClear;
            style.focused.background = TexClear;
            style.onFocused.background = TexClear;
        }

        public static Rect Line(Rect rect, int border = 1)
        {
            Rect _rect = new Rect(rect.x + border, rect.y + border, rect.width - border * 2, rect.height - border * 2);
            return _rect;
        }
        public static Rect AbsRect(float x1, float y1, float x2, float y2)
        {
            float width = y2 - y1;
            float height = x2 - x1;
            return new Rect(x1, y1, width, height);
        }
    }
}
