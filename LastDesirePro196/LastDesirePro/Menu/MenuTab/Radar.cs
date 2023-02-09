using static LastDesirePro.Menu.CFG.RadarConfig;
using LastDesirePro.Menu.CFG;
using UnityEngine;
using LastDesirePro.Main.Visuals;
using System.IO;
using static LastDesirePro.DrawMenu.Prefab;

namespace LastDesirePro.Menu.MenuTab
{
    public class Radar
    { 
        public static Vector2 _Radar;  
        public static void DoRadar()
        {
            ScrollViewMenu(new Rect(15, 60, 750, 440), ref _Radar, () =>
            {
                GUILayout.BeginVertical(GUILayout.Width(180)); GUILayout.Space(5f);
                Toggle("Radar Enabled", ref _Enabled, 16);
                Toggle("Radar Players", ref _Players, 16, _colorPlayers, true, 79);
                Toggle("Radar NPC", ref _Npc, 16, _colorNPC, true, 80);
                Toggle("Ass Indicator Players", ref _OOFIndicator, 16, _colorOOFIndicator, true, 87);
                Toggle("Ass Indicator NPC", ref _OOFIndicatorNPC, 16, _colorOOFIndicatorNPC, true, 88);
                GUILayout.Space(5f);
                if (_Enabled)
                {
                    using (new GUILayout.HorizontalScope())
                    {
                        GUIContent[] chams = { new GUIContent("Static Local"), new GUIContent("Dynamic Local") };
                        if (Button("◄", 25))
                        {
                            _Type--;
                            if (_Type == -1)
                                _Type = 0;
                        }
                        GUILayout.Label(chams[(int)_Type], _TextStyle1);
                        if (Button("►", 25))
                        {
                            _Type++;
                            if (_Type == 2)
                                _Type = 1;
                        }
                    }
                    Slider(0, 200, ref _Size, 200, $"Radar Size: {(int)_Size}");
                    Slider(0, 180, ref _Radius, 200, $"Radar Radius: {(int)_Radius}");
                } 
                GUILayout.Space(5f);
                Toggle("Radar Stag", ref _animals[0], 16, _colorAnimals[0], true, 81);
                Toggle("Radar Wolf", ref _animals[1], 16, _colorAnimals[1], true, 82);
                Toggle("Radar Horse", ref _animals[2], 16, _colorAnimals[2], true, 83);
                Toggle("Radar Chicken", ref _animals[3], 16, _colorAnimals[3], true, 84);
                Toggle("Radar Bear", ref _animals[4], 16, _colorAnimals[4], true, 85);
                Toggle("Radar Boar", ref _animals[5], 16, _colorAnimals[5], true, 86);
                GUILayout.EndVertical();
            }); 
        }
    }
}
