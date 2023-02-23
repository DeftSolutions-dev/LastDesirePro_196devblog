using static LastDesirePro.Menu.CFG.MiscConfig;
using LastDesirePro.Menu.CFG;
using UnityEngine;
using LastDesirePro.Main.Visuals;
using System.IO;
using static LastDesirePro.DrawMenu.Prefab;
using SilentOrbit.ProtocolBuffers;
using System;

namespace LastDesirePro.Menu.MenuTab
{
    public class Other
    { 
        public static Vector2 _Other;
        public static bool key;
        public static string Generator()
        {
            var chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            var rand = new System.Random();
            var String = new char[40];
            for (var i = 0; i < String.Length; i++)
                String[i] = chars[rand.Next(chars.Length)];
            var RandomString = new string(String);
            return RandomString;
        }
        public static void DoOther()
        {
            ScrollViewMenu(new Rect(15, 60, 750, 440), ref _Other, () =>
            {
                GUILayout.BeginVertical(GUILayout.Width(180)); GUILayout.Space(5f);

                _menuKey = Bind(_menuKey, "Menu Key: ", ref key);
                Toggle("BackGround Menu", ref _backGroundMenu, 16);
                Toggle("Connect the YRS", ref _yrs, 16);
                GUILayout.Space(10f);
                if (Button("HWID SPOOF the YRS", 200))
                {
                    Main.Misc.Misc.cfg.Setting._hwid = Generator();
                    Main.Misc.Misc.cfg.SaveSettings();
                }
                GUILayout.Space(10f);
                if (Button("Load Cfg", 100))
                    ConfigManager.Init();
                GUILayout.Space(10f);
                if (Button("Save Cfg", 100))
                   ConfigManager.SaveConfig(ConfigManager.Config());
                GUILayout.Space(10f);
                if (Button("UnLoad", 100))
                    Inj.bruh.unLoad = true;
                GUILayout.Label( "Current Server Info",DrawMenu.Prefab._TextStyle1);
                GUILayout.Space(2);
                if (Network.Net.cl.IsConnected()) 
                    GUILayout.TextField($"connect {Network.Net.cl.connectedAddress}:{Network.Net.cl.connectedPort}", DrawMenu.Prefab._TextStyle1);
                GUILayout.Space(4); 
                GUILayout.Label("Servеr Nаme", DrawMenu.Prefab._TextStyle1);
                GUILayout.Space(2);
                if (Network.Net.cl.IsConnected())
                    GUILayout.TextField($"{Network.Net.cl.ServerName}", DrawMenu.Prefab._TextStyle1);
                GUILayout.EndVertical();
            }); 
        }
    }
}
