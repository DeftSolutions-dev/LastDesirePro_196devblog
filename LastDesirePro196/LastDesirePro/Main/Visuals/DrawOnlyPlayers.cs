using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using UnityEngine; 

namespace LastDesirePro.Main.Visuals
{
    public class DrawOnlyPlayers
    {
        public static bool IsInScreen(Vector3 pos)
        {
            var vector = MainCamera.mainCamera.transform.InverseTransformPoint(pos);
            var vector2 = MainCamera.mainCamera.WorldToScreenPoint(pos);
            if (vector2.x > 0f && vector2.x < (float)Screen.width && vector2.y > 0f && vector2.y < (float)Screen.height && vector.z > 0f)
                return true;
            return false;
        } 
        public static void Initialize()
        {
            if (txt3D == null)
            {
                txt3D = new Texture2D(1, 1, TextureFormat.ARGB32, true);
                txt3D.SetPixel(0, 1, Color.white);
                txt3D.Apply();
            } 
        }
        static DrawOnlyPlayers()
        {
            style = new GUIStyle(GUI.skin.label) { fontSize = 12, font = LastDesirePro.Menu.MainMenu._TextFont };
            outlineStyle = new GUIStyle(GUI.skin.label) { fontSize = 12, font = LastDesirePro.Menu.MainMenu._TextFont };
            Automatic.Automatic.check = style;
        }
        public static Texture2D txt3D = null;
        public static GUIStyle style = new GUIStyle(GUI.skin.label) { fontSize = 12 };
        public static GUIStyle outlineStyle = new GUIStyle(GUI.skin.label) { fontSize = 12 };
        public static void String(Vector2 pos, string text, Color color, bool center = true, int size = 12, FontStyle fontStyle = FontStyle.Bold, int depth = 2,bool fon = false,Color colors = new Color(), float radius = 1f)
        {
            style.fontSize = size;
            style.richText = true;
            style.normal.textColor = color;
            style.fontStyle = fontStyle;
            style.font = LastDesirePro.Menu.MainMenu._TextFont;
            outlineStyle.fontSize = size;
            outlineStyle.richText = true;
            outlineStyle.font = LastDesirePro.Menu.MainMenu._TextFont;
            outlineStyle.normal.textColor = new Color(0f, 0f, 0f, 1f);
            outlineStyle.fontStyle = fontStyle;
            var content = new GUIContent(text);
            var content2 = new GUIContent(text);
            if (center)
                pos.x -= style.CalcSize(content).x / 2f;
            switch (depth)
            {
                case 1:
                    GUI.Label(new Rect(pos.x, pos.y, 300f, 25f), content, style);
                    return;
                case 2:
                    if (fon)
                    {
                        Vector2 vector = style.CalcSize(content);
                        DrawMenu.Drawing.DrawRect(new Rect(pos.x - 2f, pos.y + 1f, vector.x + 6f, vector.y - 4f), colors, radius);
                    } 
                    GUI.Label(new Rect(pos.x + 1f, pos.y + 1f, 300f, 25f), content2, outlineStyle);
                    GUI.Label(new Rect(pos.x, pos.y, 300f, 25f), content, style);
                    return;
            }
        }
        public static void DrawStats(Vector2 Head, float Width, float Height, float health,string str = "", string str1 = "", string str2 = "", bool fon = false, Color colors = new Color(), float radius = 1f)
        { 

            var num = health / 100f;
            if (health > 0f)
            {
                GUI.color = Color.white;

                if(LastDesirePro.Menu.CFG.VisuаlCоnfig._players[0])
                    String(new Vector2(Head.x + Width / 20f - 3, Head.y - 15f), str, Menu.CFG.VisuаlCоnfig._colorPlayers, true, 11, FontStyle.Bold,2, fon,colors,radius);//Name
                if (LastDesirePro.Menu.CFG.VisuаlCоnfig._players[2] && LastDesirePro.Menu.CFG.VisuаlCоnfig._players[0])
                    String(new Vector2(Head.x + Width / 20f - 3, Head.y + (LastDesirePro.Menu.CFG.VisuаlCоnfig._players[1] ? (Height + 10f): (Height - 3f))), str1, Menu.CFG.VisuаlCоnfig._colorItems, true, 11, FontStyle.Bold, 2, fon, colors, radius);//Item 
                if (LastDesirePro.Menu.CFG.VisuаlCоnfig._players[1] && LastDesirePro.Menu.CFG.VisuаlCоnfig._players[0])
                    String(new Vector2(Head.x + Width / 20f-3, Head.y + Height - 3f), str2, Menu.CFG.VisuаlCоnfig._colorDistance, true, 11, FontStyle.Bold, 2, fon, colors, radius);//Distans

                GUI.color = Color.white;
                if (LastDesirePro.Menu.CFG.VisuаlCоnfig._players[5])
                {
                    GUI.color = Color.white;
                    if (health < 25f)
                        RectFilled(Head.x - Width / 2f - 1f, Head.y + Height - 3, -4f, (Height - 2f) * -num, Color.red);
                    else if (health < 50f)
                        RectFilled(Head.x - Width / 2f - 1f, Head.y + Height - 3, -4f, (Height - 2f) * -num, new Color(1f, 0.7f, 0.16f, 1f));
                    else if (health < 70f)
                        RectFilled(Head.x - Width / 2f - 1f, Head.y + Height - 3, -4f, (Height - 2f) * -num, Color.yellow);
                    else if (health < 9999f)
                        RectFilled(Head.x - Width / 2f - 1f, Head.y + Height - 3f, -4f, (Height - 2f) * -num, Color.green);
                    GUI.color = Color.white;
                    RectFilled(Head.x - Width / 2f - 6, Head.y - 1f, 6f, 1f, Color.black);
                    RectFilled(Head.x - Width / 2f - 6, Head.y + Height - 3f, 6f, 1f, Color.black);
                    RectFilled(Head.x - Width / 2f - 6, Head.y, 1f, Height - 2f, Color.black);
                    RectFilled(Head.x - Width / 2f - 1f, Head.y, 1f, Height - 2f, Color.black);
                    GUI.color = Color.white;
                   //String(new Vector2(Head.x - Width / 2f - 3f, Head.y + Height - 3f ), (int)health + "", color, true, 8);
                    GUI.color = Color.white;
                }
                if (LastDesirePro.Menu.CFG.VisuаlCоnfig._players[6])
                {
                    GUI.color = Color.white;
                    RectFilled(Head.x - Width / 2f + 2f, Head.y + 2f, Width - 3, Height - 6, new Color32(0, 0, 0, 60));
                    RectFilled(Head.x - Width / 2f, Head.y, 1f, Height - 2f, LastDesirePro.Menu.CFG.VisuаlCоnfig._players[12] && (FullDrawing.IsVisible(Players.baseplayer, Bones.boneList[6]) || FullDrawing.IsVisible(Players.baseplayer, Bones.boneList[0]) || FullDrawing.IsVisible(Players.baseplayer, Bones.boneList[9])) ? Menu.CFG.VisuаlCоnfig._colorVisible : Menu.CFG.VisuаlCоnfig._colorBox);
                    RectFilled(Head.x + Width / 2f, Head.y, 1f, Height - 2f, LastDesirePro.Menu.CFG.VisuаlCоnfig._players[12] && (FullDrawing.IsVisible(Players.baseplayer, Bones.boneList[6]) || FullDrawing.IsVisible(Players.baseplayer, Bones.boneList[0]) || FullDrawing.IsVisible(Players.baseplayer, Bones.boneList[9])) ? Menu.CFG.VisuаlCоnfig._colorVisible : Menu.CFG.VisuаlCоnfig._colorBox);
                    RectFilled(Head.x - Width / 2f, Head.y, Width, 1f, LastDesirePro.Menu.CFG.VisuаlCоnfig._players[12] && (FullDrawing.IsVisible(Players.baseplayer, Bones.boneList[6]) || FullDrawing.IsVisible(Players.baseplayer, Bones.boneList[0]) || FullDrawing.IsVisible(Players.baseplayer, Bones.boneList[9])) ? Menu.CFG.VisuаlCоnfig._colorVisible : Menu.CFG.VisuаlCоnfig._colorBox);
                    RectFilled(Head.x - Width / 2f, Head.y + Height - 3f, Width, 1f, LastDesirePro.Menu.CFG.VisuаlCоnfig._players[12] && (FullDrawing.IsVisible(Players.baseplayer, Bones.boneList[6]) || FullDrawing.IsVisible(Players.baseplayer, Bones.boneList[0]) || FullDrawing.IsVisible(Players.baseplayer, Bones.boneList[9])) ? Menu.CFG.VisuаlCоnfig._colorVisible : Menu.CFG.VisuаlCоnfig._colorBox);

                    GUI.color = Color.white;
                }
            }
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
public static class Bones
{
    static Bones()
    {
        boneList[0] = "head";
        boneList[1] = "neck";
        boneList[2] = "spine1";
        boneList[3] = "pelvis";
        boneList[4] = "l_upperarm";
        boneList[5] = "l_forearm";
        boneList[6] = "l_hand";
        boneList[7] = "r_upperarm";
        boneList[8] = "r_forearm";
        boneList[9] = "r_hand";
        boneList[10] = "l_hip";
        boneList[11] = "l_knee";
        boneList[12] = "l_foot";
        boneList[13] = "r_hip";
        boneList[14] = "r_knee";
        boneList[15] = "r_foot";
    }
    public static Vector3[] GetBonePositions(this BasePlayer player)
    {
        Vector3[] array = new Vector3[Bones.boneList.Count];
        for (int i = 0; i < Bones.boneList.Count; i++) 
            array[i] = player.playerModel.FindBone(Bones.boneList[i]).position; 
        return array;
    }
    public static Dictionary<int, string> boneList = new Dictionary<int, string>();

}