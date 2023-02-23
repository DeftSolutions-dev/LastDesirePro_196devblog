using LastDesirePro.Attributes;
using UnityEngine;
using LastDesirePro.DrawMenu;
using LastDesirePro.Menu.MenuTab;
using System.Collections.Generic;
using System.Reflection;
using System.Collections;
using LastDesirePro.Main.Visuals;
using static LastDesirePro.Menu.CFG.MiscConfig;
using LastDesirePro.Main.Misc;

namespace LastDesirePro.Menu
{
    [Component]
    public class MainMenu : MonoBehaviour
    { 
        public static Font _TabFont;
        public static Font _TextFont;
        public static Font _EspFont;
        void Start()
        {
            base.StartCoroutine(Radius1());
        }
        void OnGUI()
        {
            if (!DrawMenu.AssetsLoad.Loaded) return;
            if (!_IsMenu)
           // UnityEngine.Debug.LogError(Network.Net.cl.connectedAddress.ToString() + ":" + Network.Net.cl.connectedPort.ToString());
            MenuRect = GUI.Window(1, MenuRect, DoMenu, "", "label"); 
            color = rainbow.GetColor();
            if (_cross)
            {
                switch (_crossList)
                {
                    case 0:
                        FullDrawing.Line(new Vector2(Screen.width / 2f - 3f, Screen.height / 2f), new Vector2(Screen.width / 2f - 13f, Screen.height / 2f), Color.white, 1.5f, true);
                        Drawing.RectFilled(new Rect(Screen.width / 2f, Screen.height / 2f, 2, 2), Color.red, 111f);
                        FullDrawing.Line(new Vector2(Screen.width / 2f + 5f, Screen.height / 2f), new Vector2(Screen.width / 2f + 15f, Screen.height / 2f), Color.white, 1.5f, true);
                        break;
                        case 1:
                        Main.Visuals.FullDrawing.Swastika(Screen.width / 2f, Screen.height / 2f, rotation_degree, color);
                        break;
                }  
            }
        }
        private IEnumerator Radius1()
        {
            while (true)
            {
                if (DrawMenu.AssetsLoad.Loaded)
                {
                    if (rotation_degree > 89f)
                        rotation_degree = 0f;
                    rotation_degree +=  LocalPlayer.Entity != null ? 2f : 0.5f;
                }
                if (DrawMenu.AssetsLoad.Loaded)
                {
                    if (rotationdegree > 89f)
                        rotationdegree = 0f;
                    rotationdegree += LocalPlayer.Entity != null ? 3f : 1f;
                }
                yield return new WaitForSeconds(0f);
            }
        }
        public void Log()
        {
            if (DrawMenu.AssetsLoad.Loaded && !log)
            {
                /* Main.Visuals.Others.HitLogs.Add("<color=red>" + 
                     $"Bind the MENU - <color=green>{Menu.CFG.MiscConfig._menuKey.ToString()}</color>.\n" +
                     "Желательно настроить в главном меню, и сохранить конфиг.\n" +
                     "Любые вопросы, пишите администрации." + 
                     "</color>", 20f); */
                foreach (KeyValuePair<string, string> keyValuePair in AssetsLoad.values)
                {
                    Others.HitLogs.Add(keyValuePair.Key, 20f);
                }
                log = true;
            }
        }
        public static bool log = false;
        public static float rotation_degree = 0f;
        public static float rotationdegree = 0f;
        void Update()
        {
            Log();
            if (Input.GetKeyDown(Menu.CFG.MiscConfig._menuKey)) 
                _IsMenu = !_IsMenu; 
            if (!DrawMenu.AssetsLoad.Loaded) return; 
        }
        public static string desire;
        public static bool _IsMenu = false;
        void DoMenu(int id)
        { 
            DoMain();
            MainTab.DoTab();
            GUI.DragWindow();
        }
        RainbowColor rainbow = new RainbowColor(0.10f);
        public static UnityEngine.Color color;
        public class RainbowColor
        {
            private UnityEngine.Color color;
            public float Speed;
            public RainbowColor(float speed = 0.25f)
            {
                Speed = speed;
                color = UnityEngine.Color.HSVToRGB(.34f, .84f, .67f);
            }
            public UnityEngine.Color GetColor()
            {
                UnityEngine.Color.RGBToHSV(color, out float h, out float s, out float v);
                return color = UnityEngine.Color.HSVToRGB(h + Time.deltaTime * Speed, s, v);
            }
        }
        void DoMain()
        {
            Rect outline = new Rect(0, 0, MenuRect.width, MenuRect.height);
            Rect Loutline = MenuFix.Line(outline);
            Rect Doutline = MenuFix.Line(Loutline);
            Rect LGoutline = MenuFix.Line(Doutline, 3);
            Rect fill = MenuFix.Line(LGoutline);
            Rect line1 = new Rect(fill.x-5, fill.y-5, fill.width+10, 50);
            Rect line2 = new Rect(fill.x-5, fill.y+33, fill.width+10, 13);
            var curEvent = Event.current;
            if (outline.Contains(curEvent.mousePosition))
                Drawing.DrawRect(outline, new Color32(200, 200, 200, 255), 6);
            else
                Drawing.DrawRect(outline, new Color32(122, 122, 122, 255), 6);
            Drawing.DrawRect(Loutline, new Color32(28, 28, 45, 255), 6);
            Drawing.DrawRect(Doutline, new Color32(28, 28, 45, 255), 6);
            Drawing.DrawRect(fill, new Color32(28, 28, 45, 255), 6);//Внутрений фон
            Drawing.DrawRect(line1, new Color32(32, 34, 50, 255), 6);//Верхняя панель
            Drawing.DrawRect(line2, new Color32(32, 34, 50, 255), 0); //Фикс низа верхний панели
        }
        public static Rect MenuRect = new Rect(29, 29, 780, 510);
    } 
}
