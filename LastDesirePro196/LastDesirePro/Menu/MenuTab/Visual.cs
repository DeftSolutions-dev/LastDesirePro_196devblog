using static LastDesirePro.Menu.CFG.VisuаlCоnfig;
using LastDesirePro.Menu.CFG;
using UnityEngine;
using LastDesirePro.Main.Visuals;
using System.IO;
using static LastDesirePro.DrawMenu.Prefab;

namespace LastDesirePro.Menu.MenuTab
{
    public class Visual
    { 
        public static Vector2 _PlayersModels, _Players, _Animals, _Resources, _Others, _Resources1,_Home,_Home1;
        public static float f = 0;
        public static float he = 100;
        public static int tab = 0;
        public static string[] tb = new string[] { "Players", "Animals", "Resources", "Home", "Others" };
        public static void DoVisual()
        { 
            tab = Tab(23, 60, 133, 100, tb, tab, 150);
            switch (tab)
            {
                case 0:
                    { 
                        GUIContent[] boxInt = {  };
                        ScrollViewMenu(new Rect(15, 85, 375, 415), ref _Players, () =>
                        { 
                            GUILayout.BeginVertical(GUILayout.Width(180)); 
                            GUILayout.Space(5f);
                            Toggle("Players", ref _players[0], 16, _colorPlayers,true,1); 
                            if (_players[0])
                            { 
                                    Toggle("Distance", ref _players[1], 16, _colorDistance, true, 4);
                                    Toggle("Held Items", ref _players[2], 16, _colorItems, true, 5); 
                            }
                            Toggle("Sleeping", ref _players[3], 16, _colorSleepers,true,2);
                            Toggle("Corpses", ref _players[13], 16, _colorCorpses, true, 91);
                            Toggle("Bone", ref _players[4], 16, _colorBone, true, 3);
                            Toggle("Is Visible", ref _players[12], 16, _colorVisible, true, 47);
                            Toggle("Bar Health", ref _players[5], 16);
                            Toggle("Box", ref _players[6], 16, _colorBox, true, 6);
                            Toggle("Chams", ref _players[7], 16);  
                            if (_players[7])
                            {
                                Toggle("Ignore Local", ref _players[14], 16); GUILayout.Space(5f);
                                using (new GUILayout.HorizontalScope())
                                {
                                    GUIContent[] chams = { new GUIContent("Chams"), new GUIContent("Color Chams"), new GUIContent("Pulsing"), new GUIContent("Rainbow"), new GUIContent("Wireframe"), new GUIContent("Health") };
                                    if (Button("◄", 25))
                                    { _chams--;
                                        if (_chams == -1)
                                            _chams = 0;
                                    }
                                    GUILayout.Label(chams[(int)_chams], _TextStyle1);
                                    if (Button("►", 25))
                                    { _chams++;
                                        if (_chams == 6)
                                            _chams = 5;
                                    }
                                }
                            } 
                            Toggle("Player Belt", ref _players[9], 16);
                            GUILayout.Space(50f);
                            Toggle("Text background", ref _players[8], 16, _colorFon, true, 7);
                            GUILayout.EndVertical();
                            Slider(0, 5f, ref _radiusFon, 200, $"Background rounding:  {string.Format("{0:0.#}", (float)_radiusFon)}");
                        });
                        if (_players[0])
                        {
                            ScrollViewMenu(new Rect(400, 85, 245, 415), ref _PlayersModels, () =>
                            {
                                GUILayout.BeginVertical(GUILayout.Width(180)); GUILayout.Space(5f);

                                GUI.DrawTexture(new Rect(65, 25, 115, 330), DrawMenu.AssetsLoad._Players);
                            Main.Visuals.FullDrawing.Line(new Vector2(117,                                  //Точка А - Лево, право по оси Х
                                    68                                                                      //Точка А - Верх, низ по оси Y
                                    ), new Vector2(120f,                                                    //Точка B - Лево, право по оси Х
                                    80                                                                      //Точка B - Верх, низ по оси Y
                                    ), Menu.CFG.VisuаlCоnfig._colorBone, 1, true);  
                            Main.Visuals.FullDrawing.Line(new Vector2(120f, 80), new Vector2(140, 88),Menu.CFG.VisuаlCоnfig._colorBone,1, true); 
                            Main.Visuals.FullDrawing.Line(new Vector2(140, 88), new Vector2(149, 112),Menu.CFG.VisuаlCоnfig._colorBone,1, true); 
                            Main.Visuals.FullDrawing.Line(new Vector2(149, 112), new Vector2(139, 130),Menu.CFG.VisuаlCоnfig._colorBone,1, true); 
                            Main.Visuals.FullDrawing.Line(new Vector2(120f, 80), new Vector2(100, 94),Menu.CFG.VisuаlCоnfig._colorBone,1, true); 
                            Main.Visuals.FullDrawing.Line(new Vector2(100, 94), new Vector2(102, 110),Menu.CFG.VisuаlCоnfig._colorBone,1, true); 
                            Main.Visuals.FullDrawing.Line(new Vector2(102, 110), new Vector2(92, 137),Menu.CFG.VisuаlCоnfig._colorBone,1, true); 
                            Main.Visuals.FullDrawing.Line(new Vector2(120f, 80), new Vector2(119, 148),Menu.CFG.VisuаlCоnfig._colorBone,1, true);
                            Main.Visuals.FullDrawing.Line(new Vector2(119, 148), new Vector2(109, 150),Menu.CFG.VisuаlCоnfig._colorBone,1, true);  
                            Main.Visuals.FullDrawing.Line(new Vector2(109, 150), new Vector2(101, 189),Menu.CFG.VisuаlCоnfig._colorBone,1, true);
                            Main.Visuals.FullDrawing.Line(new Vector2(101, 189), new Vector2(106, 228),Menu.CFG.VisuаlCоnfig._colorBone,1, true);  
                            Main.Visuals.FullDrawing.Line(new Vector2(119, 148), new Vector2(131, 151),Menu.CFG.VisuаlCоnfig._colorBone,1, true); 
                            Main.Visuals.FullDrawing.Line(new Vector2(131, 151), new Vector2(132, 190),Menu.CFG.VisuаlCоnfig._colorBone,1, true); 
                            Main.Visuals.FullDrawing.Line(new Vector2(132, 190), new Vector2(140, 242),Menu.CFG.VisuаlCоnfig._colorBone,1, true);
                                try
                                {
                                    DrawOnlyPlayers.DrawStats(new Vector2(120, 23), 130, 330, 100, "DesirePro v2?", "Nothing", string.Format("{0}", (int)_dis), _players[8], _colorFon, _radiusFon);
                                }
                                catch { }
                                GUILayout.Space(380f);
                                Slider(0, 500, ref _dis, 190, $"{(int)_dis} m.");
                                GUILayout.Space(10f);
                                GUILayout.EndVertical();
                            });
                        }
                        break;
                    }
                case 1:
                    {
                        ScrollViewMenu(new Rect(15, 85, 750, 415), ref _Animals, () =>
                        {
                            GUILayout.BeginVertical(GUILayout.Width(180));
                            GUILayout.Space(5f);
                            Toggle("Stag", ref _animals[0], 16, _colorAnimals[0], true, 8);
                            Toggle("Wolf", ref _animals[1], 16, _colorAnimals[1], true, 9);
                            Toggle("Horse", ref _animals[2], 16, _colorAnimals[2], true, 10);
                            Toggle("Chicken", ref _animals[3], 16, _colorAnimals[3], true, 11);
                            Toggle("Bear", ref _animals[4], 16, _colorAnimals[4], true, 12);
                            Toggle("Boar", ref _animals[5], 16, _colorAnimals[5], true, 13);
                            Toggle("Text background", ref _animals[6], 16, _colorFonAnimal, true, 14); 
                            GUILayout.EndVertical();
                            GUILayout.Space(5f);
                            Slider(0, 5f, ref _radiusFonAnimal, 200, $"Background rounding:  {string.Format("{0:0.#}", (float)_radiusFonAnimal)}");
                            GUILayout.Space(5f);
                            Slider(0, 500, ref _animalDist, 200, $"{(int)_animalDist} m.");
                        });
                        break;
                    }
                case 2:
                    {
                        ScrollViewMenu(new Rect(15, 85, 370, 415), ref _Resources, () =>
                        {
                            GUILayout.BeginVertical(GUILayout.Width(180));
                            GUILayout.Space(5f);
                            Toggle("Stone", ref _resource[0], 16, _colorResource[0], true, 15);
                            Toggle("Metal", ref _resource[1], 16, _colorResource[1], true, 16);
                            Toggle("Sulfur", ref _resource[2], 16, _colorResource[2], true, 17);
                            Toggle("Bonus Marker", ref _resource[3], 16, _colorResource[3], true, 18);
                            Toggle("Text background", ref _resource[4], 16, _colorFonResource, true, 19);
                            GUILayout.EndVertical();
                            GUILayout.Space(5);
                            Slider(0, 5f, ref _radiusFonResource, 200, $"Background rounding:  {string.Format("{0:0.#}", (float)_radiusFonResource)}");
                            GUILayout.Space(5);
                            Slider(0, 500, ref _resourceDist, 200, $"{(int)_resourceDist} m."); 
                        });
                        ScrollViewMenu(new Rect(395, 85, 370, 415), ref _Resources1, () =>
                        {
                            GUILayout.BeginVertical(GUILayout.Width(180));
                            GUILayout.Space(5f);
                            Toggle("Spawned Stone", ref _collectible[0], 16, _colorCollectible[0], true, 20);
                            Toggle("Spawned Metal", ref _collectible[1], 16, _colorCollectible[1], true, 21);
                            Toggle("Spawned Sulfur", ref _collectible[2], 16, _colorCollectible[2], true, 22);
                            Toggle("Spawned Hemp", ref _collectible[3], 16, _colorCollectible[3], true, 23);
                            Toggle("Spawned Mushroom", ref _collectible[4], 16, _colorCollectible[4], true, 24);
                            Toggle("Spawned Wood", ref _collectible[5], 16, _colorCollectible[5], true, 25);
                            Toggle("Spawned Pumpkin", ref _collectible[6], 16, _colorCollectible[6], true, 26);
                            Toggle("Spawned Corn", ref _collectible[7], 16, _colorCollectible[7], true, 27); 
                            Toggle("Text background", ref _collectible[8], 16, _colorFonCollectible, true, 28); 
                            GUILayout.EndVertical();
                            GUILayout.Space(5);
                            Slider(0, 5f, ref _radiusFonCollectible, 200, $"Background rounding:  {string.Format("{0:0.#}", (float)_radiusFonCollectible)}");
                            GUILayout.Space(5);
                            Slider(0, 500, ref _collectibleDist, 200, $"{(int)_collectibleDist} m.");
                        });
                        break;
                    }
                case 3: 
                    {
                        ScrollViewMenu(new Rect(15, 85, 370, 415), ref _Home, () =>
                        {
                            GUILayout.BeginVertical(GUILayout.Width(180));
                            GUILayout.Space(5f);
                            Toggle("Small box", ref _container[0], 16, _colorContainer[0], true, 29);
                            Toggle("Big box", ref _container[1], 16, _colorContainer[1], true, 30);
                            Toggle("BBQ", ref _container[3], 16, _colorContainer[3], true, 31);
                            Toggle("Small Oil Refinery", ref _container[2], 16, _colorContainer[2], true, 32);
                            Toggle("Furnace", ref _container[4], 16, _colorContainer[4], true, 33);
                            Toggle("Repair bench", ref _container[5], 16, _colorContainer[5], true, 34);
                            Toggle("Furnace large", ref _container[6], 16, _colorContainer[6], true, 35);
                            Toggle("Dropbox", ref _container[7], 16, _colorContainer[7], true, 36);
                            Toggle("Stocking small", ref _container[8], 16, _colorContainer[8], true, 37);
                            Toggle("Stocking large", ref _container[14], 16, _colorContainer[14], true, 38);
                            Toggle("Research table", ref _container[9], 16, _colorContainer[9], true, 39);
                            Toggle("Fireplace", ref _container[10], 16, _colorContainer[10], true, 40);
                            Toggle("Workbench [1lvl]", ref _container[11], 16, _colorContainer[11], true, 41);
                            Toggle("Workbench [2lvl]", ref _container[12], 16, _colorContainer[12], true, 42);
                            Toggle("Workbench [3lvl]", ref _container[13], 16, _colorContainer[13], true, 43);
                            Toggle("Bar Health", ref _container[15], 16);
                            Toggle("Text background", ref _container[16], 16, _colorFonContainer, true, 44);
                            GUILayout.EndVertical();
                            GUILayout.Space(5);
                            Slider(0, 5f, ref _radiusFonContainer, 200, $"Background rounding:  {string.Format("{0:0.#}", (float)_radiusFonContainer)}");
                            GUILayout.Space(5);
                            Slider(0, 500, ref _containerDist, 200, $"{(int)_containerDist} m.");
                        });
                        ScrollViewMenu(new Rect(395, 85, 370, 415), ref _Home1, () =>
                        {
                            GUILayout.BeginVertical(GUILayout.Width(180));
                            GUILayout.Space(5f);
                            Toggle("Cupboard", ref _cupboard[0], 16, _colorCupboard, true, 45);
                            if (_cupboard[0])
                            {
                                Toggle("Authorized Players", ref _cupboard[1], 16);
                                Toggle("Bar Health", ref _cupboard[2], 16);
                                Toggle("Text background", ref _cupboard[3], 16, _colorFonCupboard, true, 46);
                                GUILayout.Space(5);
                                Slider(0, 5f, ref _radiusFonCupboard, 200, $"Background rounding:  {string.Format("{0:0.#}", (float)_radiusFonCupboard)}");
                                GUILayout.Space(5);
                                Slider(0, 500, ref _cupboardDist, 200, $"{(int)_cupboardDist} m.");
                            } 
                            Toggle("AutoTurret", ref _autoturret[0], 16, _colorAutoTurret, true, 49);
                            if (_autoturret[0])
                            {
                                Toggle("Authorized Players", ref _autoturret[1], 16);
                                Toggle("Bar Health", ref _autoturret[2], 16);
                                Toggle("Text background", ref _autoturret[3], 16, _colorFonAutoTurret, true, 48);
                                GUILayout.Space(5);
                                Slider(0, 5f, ref _radiusFonAutoTurret, 200, $"Background rounding:  {string.Format("{0:0.#}", (float)_radiusFonAutoTurret)}");
                                GUILayout.Space(5);
                                Slider(0, 500, ref _autoturretDist, 200, $"{(int)_autoturretDist} m.");
                            } 
                            Toggle("Stesh", ref _stash[0], 16, _colorStash, true, 50);
                            if (_stash[0])
                            { 
                                Toggle("Bar Health", ref _stash[1], 16);
                                Toggle("Text background", ref _stash[2], 16, _colorFonStash, true, 51);
                                GUILayout.Space(5);
                                Slider(0, 5f, ref _radiusFonStash, 200, $"Background rounding:  {string.Format("{0:0.#}", (float)_radiusFonStash)}");
                                GUILayout.Space(5);
                                Slider(0, 500, ref _stashDist, 200, $"{(int)_stashDist} m.");
                            }
                            GUILayout.Space(10f);
                            Toggle("Mine", ref _trap[0], 16, _colorTrap[0], true, 52);
                            Toggle("Trap", ref _trap[1], 16, _colorTrap[1], true, 53);
                            Toggle("Flame Turret", ref _trap[2], 16, _colorTrap[2], true, 54);
                            Toggle("Gun Trap", ref _trap[3], 16, _colorTrap[3], true, 55);
                            if (_trap[0]|| _trap[1] || _trap[2] || _trap[3])
                            {
                                Toggle("Bar Health", ref _trap[4], 16);
                                Toggle("Text background", ref _trap[5], 16, _colorFonTrap, true, 56);
                                GUILayout.Space(5);
                                Slider(0, 5f, ref _radiusFonTrap, 200, $"Background rounding:  {string.Format("{0:0.#}", (float)_radiusFonTrap)}");
                                GUILayout.Space(5);
                                Slider(0, 500, ref _trapDist, 200, $"{(int)_trapDist} m.");
                            }
                            GUILayout.Space(10f);
                            Toggle("Item", ref _droppedItem[0], 16, _colorDroppedItem, true, 57);
                            Toggle("Rainbow Ammo/Gun", ref _droppedItem[4], 16);
                            if (_droppedItem[0]) {
                                Toggle("Ammout", ref _droppedItem[1], 16);
                                Toggle("Condition", ref _droppedItem[2], 16); 
                                Toggle("Text background", ref _droppedItem[3], 16, _colorFonDroppedItem, true, 58);
                                GUILayout.Space(5);
                                Slider(0, 5f, ref _radiusFonDroppedItem, 200, $"Background rounding:  {string.Format("{0:0.#}", (float)_radiusFonDroppedItem)}");
                                GUILayout.Space(5);
                                Slider(0, 500, ref _droppedItemDist, 200, $"{(int)_droppedItemDist} m.");
                            }
                            GUILayout.EndVertical(); 
                        });
                        break;
                    }
                case 4:
                    {
                        ScrollViewMenu(new Rect(15, 85, 370, 415), ref _Home, () =>
                        {
                            GUILayout.BeginVertical(GUILayout.Width(180));
                            GUILayout.Space(5f);
                            Toggle("Helicopter", ref _Helicopter[0], 16, _colorHelicopter, true, 59);
                            Toggle("Helicopter Crate", ref _Helicopter[2], 16, _colorheli_crate, true, 63);
                            if (_Helicopter[0] || _Helicopter[2])
                            {
                                Toggle("Text background", ref _Helicopter[1], 16, _colorFonHelicopter, true, 60); 
                                Slider(0, 5f, ref _radiusFonHelicopter, 200, $"Background rounding:  {string.Format("{0:0.#}", (float)_radiusFonHelicopter)}");
                            }
                            Toggle("BradleyAPC", ref _BradleyAPC[0], 16, _colorBradleyAPC, true, 61);
                            Toggle("BradleyAPC Crate", ref _BradleyAPC[2], 16, _colorbradley_crate, true, 64);
                            if (_BradleyAPC[0] || _BradleyAPC[2])
                            {
                                Toggle("Text background", ref _BradleyAPC[1], 16, _colorFonBradleyAPC, true, 62); 
                                Slider(0, 5f, ref _radiusFonBradleyAPC, 200, $"Background rounding:  {string.Format("{0:0.#}", (float)_radiusFonBradleyAPC)}");
                            }
                            Toggle("Raid", ref _raid[8], 16, _colorRaid, true, 92);
                            if (_raid[8])
                            {
                                Toggle("C4", ref _raid[0], 16);
                                Toggle("Satchel", ref _raid[1], 16);
                                Toggle("Rocket incendiary", ref _raid[2], 16);
                                Toggle("Rocket", ref _raid[3], 16);
                                Toggle("Beancan", ref _raid[4], 16);
                                Toggle("F1", ref _raid[5], 16);
                                Toggle("Explosion Bullet", ref _raid[6], 16);
                                Slider(0, 600f, ref _secondRaid, 200, $"Second :  {string.Format("{0}", (int)_secondRaid)}");
                                Toggle("Text background", ref _raid[7], 16, _colorFonRaid, true, 93); 
                                Slider(0, 5f, ref _radiusFonRaid, 200, $"Background rounding:  {string.Format("{0:0.#}", (float)_radiusFonRaid)}");
                            }
                            Toggle("Monument Info", ref _MonumentInfo, 16, _colorMonumentInfo, true, 98);
                            GUILayout.EndVertical(); 
                        });
                        ScrollViewMenu(new Rect(395, 85, 370, 415), ref _Home1, () =>
                        {
                            GUILayout.BeginVertical(GUILayout.Width(180));
                            GUILayout.Space(5f);
                            Toggle("Supply Drop", ref _Storage[13], 16, _colorStorage[12], true, 65);
                            Toggle("Crate tools", ref _Storage[0], 16, _colorStorage[0], true, 66);
                            Toggle("Crate small", ref _Storage[1], 16, _colorStorage[1], true, 67);
                            Toggle("Crate normal", ref _Storage[2], 16, _colorStorage[2], true, 68);
                            Toggle("Crate normal food", ref _Storage[3], 16, _colorStorage[3], true, 69);
                            Toggle("Crate normal medical", ref _Storage[4], 16, _colorStorage[4], true, 70);
                            Toggle("Crate mine", ref _Storage[5], 16, _colorStorage[5], true, 71);
                            Toggle("Crate military", ref _Storage[6], 16, _colorStorage[6], true, 72);
                            Toggle("Crate elite", ref _Storage[7], 16, _colorStorage[7], true, 73);
                            Toggle("Barrel", ref _Storage[8], 16, _colorStorage[8], true, 74);
                            Toggle("Barrel fire", ref _Storage[9], 16, _colorStorage[9], true, 75);
                            Toggle("Oil barrel", ref _Storage[10], 16, _colorStorage[10], true, 76);
                            Toggle("Recycler", ref _Storage[11], 16, _colorStorage[11], true, 77); 
                            Toggle("Text background", ref _Storage[12], 16, _colorFonStorage, true, 78);
                            GUILayout.EndVertical(); 
                            Slider(0, 5f, ref _radiusFonStorage, 200, $"Background rounding:  {string.Format("{0:0.#}", (float)_radiusFonStorage)}"); 
                            Slider(0, 500, ref _StorageDist, 200, $"{(int)_StorageDist} m.");
                        });
                        break;
                    }
            }
        }
    }
}
