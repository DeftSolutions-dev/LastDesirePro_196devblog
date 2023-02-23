using LastDesirePro.Attributes;
using UnityEngine;

namespace LastDesirePro.Menu.CFG
{
    internal class RadarConfig
    {

        /* ------------------ Radar Enabled (Вкл/Выкл) ------------------------ */
        [Save] public static bool _Enabled = false;





        /* ------------------ Radar Enabled World (Вкл/Выкл определённых объектов и тд) ------------------------ */
        [Save] public static bool _Players = false;
        [Save] public static bool _Npc = false;
        [Save]
        public static bool[] _animals = new bool[]
                {
            false, //Stag
            false, //Wolf
            false, //Horse
            false, //Chicken
            false, //Bear
            false //Boar
                };



        [Save] public static bool _OOFIndicator = false;
        [Save] public static bool _OOFIndicatorNPC = false;
        [Save] public static Color32 _colorOOFIndicator = new Color32(255, 0, 0, 255);
        [Save] public static Color32 _colorOOFIndicatorNPC = new Color32(0, 240, 250, 255);


        /* ------------------ Radar Color (Смена цвета отметок) ------------------------ */
        [Save] public static Color32 _colorPlayers = new Color32(255, 0, 0, 255);
        [Save] public static Color32 _colorNPC = new Color32(0, 240, 250, 255);


        [Save]
        public static Color32[] _colorAnimals = { new Color32(255, 255, 255, 255),
            new Color32(255, 255, 255, 255),
            new Color32(255, 255, 255, 255),
            new Color32(255, 255, 255, 255),
            new Color32(255, 255, 255, 255),
            new Color32(255, 255, 255, 255),
        };



        /* ------------------ Radar Enabled (Сохранение позиции радара, размер и тд.) ------------------------ */
        [Save] public static int _Type = 0; 
        [Save] public static float _Zoom = 390f;
        [Save] public static float _Radius = 6f;
        [Save] public static float _Size = 150;
        [Save] public static Rect _Vew = new Rect((float)Screen.height - _Size - 20f, 10f, _Size + 160, _Size + 160);
    }
}
