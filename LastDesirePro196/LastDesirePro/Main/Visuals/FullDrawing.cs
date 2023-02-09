using LastDesirePro.DrawMenu;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using UnityEngine;

namespace LastDesirePro.Main.Visuals
{
    public class FullDrawing
    {
        public static void Line(Vector2 pointA, Vector2 pointB, UnityEngine.Color color, float width, bool antiAlias = true)
        {
            GUI.color = Color.white;
            if (!lineTex)
                Initialize();
            var dx = pointB.x - pointA.x;
            var dy = pointB.y - pointA.y;
            var len = Mathf.Sqrt(dx * dx + dy * dy);
            if (len < 0.001f)
                return;
            Texture2D tex;
            if (antiAlias)
            {
                width = width * 3.0f;
                tex = aaLineTex;
            }
            else
                tex = lineTex;
            var wdx = width * dy / len;
            var wdy = width * dx / len;
            var matrix = Matrix4x4.identity;
            matrix.m00 = dx;
            matrix.m01 = -wdx;
            matrix.m03 = pointA.x + 0.5f * wdx;
            matrix.m10 = dy;
            matrix.m11 = wdy;
            matrix.m13 = pointA.y - 0.5f * wdy;
            GL.PushMatrix();
            GL.MultMatrix(matrix);
            GUI.color = color;
            GUI.DrawTexture(lineRect, tex);
            GL.PopMatrix();
            GUI.color = Color.white;
        } 
        public static void FillRGB(Vector2 point, float width, float height, Color color, float alpha)
        {
            Texture2D texture2D = new Texture2D(1, 1);
            Color color2 = color;
            color2.a = alpha;
            Color[] pixels = texture2D.GetPixels();
            for (int i = 0; i < pixels.Length; i++)
            {
                pixels[i] = color2;
            }
            texture2D.SetPixels(pixels);
            texture2D.Apply();
            GUI.DrawTexture(new Rect(point.x, point.y, width, height), texture2D);
        } 
        public static float RAD2DEG(float x) => (float)(x) * (float)(180f / Mathf.PI);
        public static float DEG2RAD(float x) => (float)(x) * (float)(Mathf.PI / 180f);
        public static void Swastika(float w, float h ,float rotation_degree,Color color,int size = 6)
        { 
            var length = (int)(size);
            var gamma = Mathf.Atan(length / length);
            var i = 0;
            while (i < 4)
            {
                List<float> parts = new List<float>();
                parts.Add(length * Mathf.Sin(DEG2RAD(rotation_degree + (i * 90))));
                parts.Add(length * Mathf.Cos(DEG2RAD(rotation_degree + (i * 90))));
                parts.Add((length / Mathf.Cos(gamma)) * Mathf.Sin(DEG2RAD(rotation_degree + (i * 90) + RAD2DEG(gamma))));
                parts.Add((length / Mathf.Cos(gamma)) * Mathf.Cos(DEG2RAD(rotation_degree + (i * 90) + RAD2DEG(gamma))));
                FullDrawing.Line(new Vector2((float)w, (float)h), new Vector2((float)(w) + parts[0], (float)(h) - parts[1]), color, 0.7f, true);
                FullDrawing.Line(new Vector2((float)(w) + parts[0], (float)(h) - parts[1]), new Vector2((float)(w) + parts[2], (float)(h) - parts[3]), color, 0.5f, true);
                i++;
            }
        } 
        public static void Circle(Color color,Vector2 gl, float radius, int rab)
        {
            var offset = 360f / rab;//Градус 
            for (float angle = 0; angle < 360; angle += offset)
            {
                var pos1 = gl + new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle)) * (radius);
                var pos3 = gl + new Vector2(Mathf.Cos(Mathf.Deg2Rad * (angle + offset)), Mathf.Sin(Mathf.Deg2Rad * (angle + offset))) * (radius);
                FullDrawing.Line(pos1, pos3, Color.black, 2.3f, true); 
                var pos = gl + new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle)) * radius;
                var pos2 = gl + new Vector2(Mathf.Cos(Mathf.Deg2Rad * (angle + offset)), Mathf.Sin(Mathf.Deg2Rad * (angle + offset))) * radius; 
                FullDrawing.Line(pos, pos2, color, 1f, true); 
            }
        }
        public static float _circle = 50f;
        public static bool IsInScreen(Vector3 pos)
        {
            var vector = MainCamera.mainCamera.transform.InverseTransformPoint(pos);
            var vector2 = MainCamera.mainCamera.WorldToScreenPoint(pos);
            if (vector2.x > 0f && vector2.x < (float)Screen.width && vector2.y > 0f && vector2.y < (float)Screen.height && vector.z > 0f)
                return true;
            return false;
        } 
        public static bool IsOpen() => !UIChat.isOpen && !UIInventory.isOpen && !UICrafting.isOpen && !MainMenuSystem.isOpen && !DeveloperTools.isOpen;
        public static void Initialize()
        {
            if (lineTex == null)
            {
                lineTex = new Texture2D(1, 1, TextureFormat.ARGB32, true);
                lineTex.SetPixel(0, 1, Color.white);
                lineTex.Apply();
            }
            if (aaLineTex == null)
            {
                aaLineTex = new Texture2D(1, 3, TextureFormat.ARGB32, true);
                aaLineTex.SetPixel(0, 0, new Color(1, 1, 1, 0));
                aaLineTex.SetPixel(0, 1, Color.white);
                aaLineTex.SetPixel(0, 2, new Color(1, 1, 1, 0));
                aaLineTex.Apply();
            }
            if (txt3D == null)
            {
                txt3D = new Texture2D(1, 1, TextureFormat.ARGB32, true);
                txt3D.SetPixel(0, 1, Color.white);
                txt3D.Apply();
            } 
        }
        static FullDrawing()
        {
            style = new GUIStyle(GUI.skin.label) { fontSize = 12, font = LastDesirePro.Menu.MainMenu._TextFont };
            outlineStyle = new GUIStyle(GUI.skin.label) { fontSize = 12, font = LastDesirePro.Menu.MainMenu._TextFont };
        }
        private static Texture2D aaLineTex = null;
        private static Texture2D lineTex = null;
        private static Rect lineRect = new Rect(0f, 0f, 1f, 1f);
        private static Texture2D txt3D = null;
        private static GUIStyle style = new GUIStyle(GUI.skin.label) { fontSize = 12, font = LastDesirePro.Menu.MainMenu._TextFont };
        private static GUIStyle outlineStyle = new GUIStyle(GUI.skin.label) { fontSize = 12, font = LastDesirePro.Menu.MainMenu._TextFont };
        public static bool IsVisible(BasePlayer player, string bone)
        {
            RaycastHit _raycastHit;
            if (Physics.Linecast(MainCamera.mainCamera.transform.position, player.playerModel.FindBone(bone).position, out _raycastHit, LayerMask))
                return true;
            return false;
        }
        private static readonly LayerMask LayerMask = Rust.Layers.Terrain | Rust.Layers.World | Rust.Layers.Ragdolls | Rust.Layers.Deploy | Rust.Layers.Construction | Rust.Layers.PlayerBuildings;
        public static void TextureColor(Rect rec, Texture2D txt, Color col)
        {
            GUI.color = col;
            GUI.DrawTexture(rec, txt);
            GUI.color = Color.white;
        }
        public static void String(Vector2 pos, string text, Color color, bool center = true, int size = 12, FontStyle fontStyle = FontStyle.Bold, int depth = 1, bool fon = false, Color colors = new Color(), float radius = 1f)
        {
            GUI.color = Color.white;
            style.fontSize = size;
            style.richText = true;
            style.normal.textColor = color;
            style.fontStyle = fontStyle;
            outlineStyle.fontSize = size;
            outlineStyle.richText = true;
            outlineStyle.normal.textColor = new Color(0f, 0f, 0f, 1f);
            outlineStyle.fontStyle = fontStyle;
            var content = new GUIContent(text);
            var content2 = new GUIContent(text);
            if (center)
                pos.x -= style.CalcSize(content).x / 2f;
            switch (depth)
            {
                case 1:
                    GUI.Label(new Rect(pos.x, pos.y, 999, 999), content, style);
                    return;
                case 2:
                    if (fon) 
                        DrawMenu.Drawing.DrawRect(new Rect(pos.x - 2f, pos.y + 1f, style.CalcSize(content).x + 5.5f, style.CalcSize(content).y - 4f), colors, radius);
                    GUI.Label(new Rect(pos.x + 1f, pos.y + 1f, 999, 999), content2, outlineStyle);
                    GUI.Label(new Rect(pos.x, pos.y, 999, 999), content, style);
                    return;
            }
            GUI.color = Color.white;
        }

        public static void LogString(Vector2 pos, string text, Color color, bool center = true,
            int size = 12, FontStyle fontStyle = FontStyle.Bold,
            bool fon = false, Color colors = new Color(),
            float radius = 1f,float time =1f, float maxTime = 5f)
        {
            GUI.color = Color.white;
            style.fontSize = size;
            style.richText = true;
            style.normal.textColor = color;
            style.fontStyle = fontStyle;
            outlineStyle.fontSize = size;
            outlineStyle.richText = true;
            outlineStyle.normal.textColor = new Color(0f, 0f, 0f, 1f);
            outlineStyle.fontStyle = fontStyle;
            var content = new GUIContent(text); 
            if (center)
                pos.x -= style.CalcSize(content).x / 2f;
            var num = (style.CalcSize(content).x) * time / maxTime;
            if (fon)
            { 
                DrawMenu.Drawing.DrawRect(new Rect(pos.x - 2f, pos.y + 1f, style.CalcSize(content).x + 5.5f, style.CalcSize(content).y), Menu.CFG.MiscConfig._ColorHitLog, radius);
                DrawMenu.Drawing.DrawRect(new Rect(pos.x - 2f, pos.y + 1f, (style.CalcSize(content).x - num < 0) ? 0: style.CalcSize(content).x - num, style.CalcSize(content).y), new Color32(150, 98, 239, 255), radius);
                var border1 = new Rect(pos.x - 2f, pos.y + 1f, style.CalcSize(content).x + 5.5f, style.CalcSize(content).y - 3);
                var box1 = MenuFix.Line(border1,1);
                DrawMenu.Drawing.DrawRect(border1, Color.white, radius);
                DrawMenu.Drawing.DrawRect(box1, Menu.CFG.MiscConfig._ColorHitLog, radius);
            }
            GUI.Label(new Rect(pos.x, pos.y, 999, 999), content, style);
            GUI.color = Color.white;
        }
        public static void Health(Vector2 Head,  float health, float maxHealth , float width = 28, float height = 5f, float thickness = 1f)
        {
            GUI.color = Color.white;
            float num = (width - thickness) * health / maxHealth;
            if (health < maxHealth * 0.2f)
                RectFilled(Head.x - width / 2f + thickness, Head.y - height + thickness, num, height - thickness * 2f, Color.red);
            else if (health < maxHealth * 0.4f)
                RectFilled(Head.x - width / 2f + thickness, Head.y - height + thickness, num, height - thickness * 2f, new Color(1f, 0.7f, 0.16f, 1f));
            else if (health < maxHealth * 0.7f)
                RectFilled(Head.x - width / 2f + thickness, Head.y - height + thickness, num, height - thickness * 2f, Color.yellow);
            else if (health < maxHealth * 1.000001f)
                RectFilled(Head.x - width / 2f + thickness, Head.y - height + thickness, num, height - thickness * 2f, Color.green);
            RectFilled(Head.x - width / 2f + thickness -1f, Head.y - height, width, 1, Color.black);
            RectFilled(Head.x - width / 2f + thickness - 1f, Head.y - height + thickness * 6f, width, 1, Color.black);
            RectFilled(Head.x - width / 2f + thickness - 1f, Head.y - height, 1f, height , Color.black);
            RectFilled(Head.x + width / 2f + thickness - 1f, Head.y - height, 1f, height , Color.black);
            GUI.color = Color.white;
        }
        public static void Indicator(Vector2 Head, float health, float maxHealth, float width = 28, float height = 5f, float thickness = 1f,string text = "")
        {
            GUI.color = Color.white;
            float num = (width - thickness) * health / maxHealth; 

            if (health < maxHealth * 0.2f)
                RectFilled(Head.x - width / 2f + thickness, Head.y - height + thickness, num, height - thickness * 2f, Color.green); 
            else if (health < maxHealth * 0.4f)
                RectFilled(Head.x - width / 2f + thickness, Head.y - height + thickness, num, height - thickness * 2f, Color.yellow); 
            else if (health < maxHealth * 0.7f)
                RectFilled(Head.x - width / 2f + thickness, Head.y - height + thickness, num, height - thickness * 2f, new Color(1f, 0.7f, 0.16f, 1f));
            else if (health < maxHealth * 1.000001f)
                RectFilled(Head.x - width / 2f + thickness, Head.y - height + thickness, num, height - thickness * 2f, Color.red);


            RectFilled(Head.x - width / 2f + thickness - 1f, Head.y - height, width, 1, Color.black);
            RectFilled(Head.x - width / 2f + thickness - 1f, Head.y - height + thickness * (height - 1f), width, 1, Color.black);
            RectFilled(Head.x - width / 2f + thickness - 1f, Head.y - height, 1f, height, Color.black);
            RectFilled(Head.x + width / 2f + thickness - 1f, Head.y - height, 1f, height, Color.black);
            String(new Vector2(Head.x, Head.y - height), text, Color.white,true,11,FontStyle.Bold,2);
            GUI.color = Color.white;
        }
        public static void IndicatorReload(Vector2 Head, float health, float maxHealth, float width = 28, float height = 5f, float thickness = 1f)
        { 
            GUI.color = Color.white;
            float num = (width - thickness) * health / maxHealth; 
                RectFilled(Head.x - width / 2f + thickness, Head.y - height + thickness, num, height - thickness * 2f, new Color(0.67f, 0.45f, 0.9f, 1f)); 
            RectFilled(Head.x - width / 2f + thickness - 1f, Head.y - height, width, 1, Color.black);
            RectFilled(Head.x - width / 2f + thickness - 1f, Head.y - height + thickness * (height -1f), width, 1, Color.black);
            RectFilled(Head.x - width / 2f + thickness - 1f, Head.y - height, 1f, height, Color.black);
            RectFilled(Head.x + width / 2f + thickness - 1f, Head.y - height, 1f, height, Color.black);
            String(new Vector2(Head.x, Head.y - height-13f),  ((int)(100 * health / maxHealth)).ToString() + "%", Color.white, true,10,FontStyle.Bold,2);
            GUI.color = Color.white;
        }
        public static void RectFilled(float x, float y, float width, float height, Color color)
        {
            GUI.color = color; 
            if (!txt3D)
                Initialize();
            GUI.DrawTexture(new Rect(x, y, width, height), txt3D);
        }
    }
} 