using LastDesirePro.Attributes;
using UnityEngine;
namespace LastDesirePro.Menu.CFG
{
    internal class VisuаlCоnfig
    {
        [Save] public static float per1 = 1f;
        [Save] public static float per2 = 1f;
        [Save] public static float per3 = 1f; 




        /*---------------------- Visual Players ------------------------ */
        [Save] public static bool[] _players = new bool[] {
        false,//Players         0
        false,//Дистанция       1
        false,//Предметы       2
        false,//Спящие         3
        false,//Кости          4
        false,//Бар хп          5
        false,//Боксы           6
        false,//Чамсы          7 
        false,//Задний фон     8
        false,//Ремень         9
        false,//Состояние      10
        false,//кОЛИЧЕСТВО     11 
        false,//VisibleCheck    12
        false,//Corpses    13
        false,//ignoreLocalPlayer    14
        };
        [Save] public static float _chams = 0;
        [Save] public static float _dis = 500;
        [Save] public static float _radiusFon = 1f;

        /*---------------------- Visual Players Colors ------------------------ */

        [Save] public static Color32 _colorVisible = new Color32(255, 0, 0, 255);
        [Save] public static Color32 _colorPlayers = new Color32(255,255,255,255);
        [Save] public static Color32 _colorSleepers = new Color32(255, 255, 255, 255);
        [Save] public static Color32 _colorDistance = new Color32(255, 255, 255, 255);
        [Save] public static Color32 _colorBone = new Color32(255, 255, 255, 255);
        [Save] public static Color32 _colorBox = new Color32(255, 255, 255, 255);
        [Save] public static Color32 _colorItems = new Color32(255, 255, 255, 255);
        [Save] public static Color32 _colorCorpses = new Color32(255, 255, 255, 255);
        [Save] public static Color32 _colorFon = new Color32(0, 0, 0, 255);

        /*---------------------- Visual Animals ------------------------ */

        [Save] public static bool[] _animals = new bool[]
        {
            false, //Stag
            false, //Wolf
            false, //Horse
            false, //Chicken
            false, //Bear
            false, //Boar
            false //Задний фон 6
        }; 
        [Save] public static float _animalDist = 300f;
        /*---------------------- Visual Animals Colors ------------------------ */

        [Save] public static float _radiusFonAnimal = 1f;
        [Save] public static Color32 _colorFonAnimal = new Color32(0, 0, 0, 255);
        [Save] public static Color32[] _colorAnimals = { new Color32(255, 255, 255, 255), 
            new Color32(255, 255, 255, 255), 
            new Color32(255, 255, 255, 255), 
            new Color32(255, 255, 255, 255), 
            new Color32(255, 255, 255, 255), 
            new Color32(255, 255, 255, 255),
        };



        /*---------------------- Visual Resource ------------------------ */

        [Save]
        public static bool[] _resource = new bool[]
        {
            false, //Stone
            false, //Metal
            false, //Sulfur 
            false, //Marker ore 
            false //Fon  
        };
        [Save] public static float _resourceDist = 300f;
        /*---------------------- Visual Resource Colors ------------------------ */

        [Save] public static float _radiusFonResource = 1f;
        [Save] public static Color32 _colorFonResource = new Color32(0, 0, 0, 255);
        [Save] public static Color32[] _colorResource = { new Color32(255, 255, 255, 255),
            new Color32(192, 192, 192, 255),
            new Color32(255, 255, 0, 255),
            new Color32(255, 0, 0, 255)
        };


        /*---------------------- Visual Resource Collectible ------------------------ */

        [Save]
        public static bool[] _collectible = new bool[]
        {
            false, //stone
            false, //metal
            false, //sulfur 
            false, //hemp
            false, //mushroom  
            false, //wood  
            false, //pumpkin  
            false, //corn  
            false //fon  
        };
        [Save] public static float _collectibleDist = 300f;
        /*---------------------- Visual Resource Collectible Colors ------------------------ */

        [Save] public static float _radiusFonCollectible = 1f;
        [Save] public static Color32 _colorFonCollectible = new Color32(0, 0, 0, 255);
        [Save]
        public static Color32[] _colorCollectible = { new Color32(255, 255, 255, 255),
            new Color32(192, 192, 192, 255),
            new Color32(255, 255, 0, 255),
            new Color32(0, 153, 0, 255),
            new Color32(100, 50, 0, 255),
            new Color32(130, 65, 0, 255),
            new Color32(255, 128, 0, 255),
            new Color32(204, 204, 0, 255),
        };




        /*---------------------- Visual Home Storage Container ------------------------ */

        [Save]
        public static bool[] _container = new bool[]
        {
            false, //woodbox         0
            false, //box wooden      1
            false, //refinery small  2
            false, //bbq             3
            false, //furnace         4
            false, //repair bench    5
            false, //furnace large   6
            false, //dropbox         7
            false, //stocking small  8
            false, //research table  9
            false, //fireplace       10
            false, //workbench 1lvl  11
            false, //workbench 2lvl  12
            false, //workbench 3lvl  13
            false, //stocking large  14
            false, //bar hp          15
            false, //fon             16
        };
        [Save] public static float _containerDist = 150f;
        /*---------------------- Visual Home Collectible Colors ------------------------ */

        [Save] public static float _radiusFonContainer = 1f;
        [Save] public static Color32 _colorFonContainer = new Color32(0, 0, 0, 255);
        [Save]
        public static Color32[] _colorContainer = {
            new Color32(255, 255, 255, 255), //0
            new Color32(255, 255, 255, 255), //1
            new Color32(255, 255, 255, 255), //2
            new Color32(255, 255, 255, 255), //3
            new Color32(255, 255, 255, 255), //4
            new Color32(255, 255, 255, 255), //5
            new Color32(255, 255, 255, 255), //6
            new Color32(255, 255, 255, 255), //7
            new Color32(255, 255, 255, 255), //8
            new Color32(255, 255, 255, 255), //9
            new Color32(255, 255, 255, 255), //10
            new Color32(255, 255, 255, 255), //11
            new Color32(255, 255, 255, 255), //12
            new Color32(255, 255, 255, 255), //13
            new Color32(255, 255, 255, 255), //14
            new Color32(255, 255, 255, 255), //15
        };



        /*---------------------- Visual Home Cupboard ------------------------ */

        [Save]
        public static bool[] _cupboard = new bool[]
        { 
            false, //cupboard      0
            false, //authorized    1
            false, //bar hp        2
            false //fon            3
        };
        [Save] public static float _cupboardDist = 150f;
        /*---------------------- Visual Home Cupboard Colors ------------------------ */

        [Save] public static float _radiusFonCupboard = 1f;
        [Save] public static Color32 _colorFonCupboard = new Color32(0, 0, 0, 255);
        [Save] public static Color32 _colorCupboard = new Color32(255, 255, 255, 255);




        /*---------------------- Visual Home AutoTurret ------------------------ */

        [Save]
        public static bool[] _autoturret = new bool[]
        {
            false, //autoturret      0
            false, //authorized      1
            false, //bar hp          2
            false //fon              3
        };
        [Save] public static float _autoturretDist = 150f;
        /*---------------------- Visual Home AutoTurret ------------------------ */

        [Save] public static float _radiusFonAutoTurret = 1f;
        [Save] public static Color32 _colorFonAutoTurret = new Color32(0, 0, 0, 255);
        [Save] public static Color32 _colorAutoTurret = new Color32(255, 255, 255, 255);





        /*---------------------- Visual Home Stash ------------------------ */

        [Save]
        public static bool[] _stash = new bool[]
        {
            false, //autoturret      0
            false, //bar hp          1 
            false //fon              2
        };
        [Save] public static float _stashDist = 150f;
        /*---------------------- Visual Home Stash ------------------------ */

        [Save] public static float _radiusFonStash = 1f;
        [Save] public static Color32 _colorFonStash = new Color32(0, 0, 0, 255);
        [Save] public static Color32 _colorStash = new Color32(255, 255, 255, 255);




        /*---------------------- Visual Home Trap ------------------------ */

        [Save]
        public static bool[] _trap = new bool[]
        {
            false, //Mine                 0
            false, //BearTrap             1
            false, //FlameTurret          2
            false, //GunTrap              3
            false, //bar hp               4
            false, //fon                  5
        };
        [Save] public static float _trapDist = 150f;
        /*---------------------- Visual Home Trap ------------------------ */

        [Save] public static float _radiusFonTrap = 1f;
        [Save] public static Color32 _colorFonTrap = new Color32(0, 0, 0, 255);
        [Save] public static Color32[] _colorTrap = {
            new Color32(255, 255, 255, 255), //0
            new Color32(255, 255, 255, 255), //1
            new Color32(255, 255, 255, 255), //2
            new Color32(255, 255, 255, 255), //3 
        };




        /*---------------------- Visual Home DroppedItem ------------------------ */

        [Save]
        public static bool[] _droppedItem = new bool[]
        {
            false, //Item             0
            false, //Ammout           1
            false, //Condition        2
            false, //fon              3
            false, //Chams Rainbow    4
        };
        [Save] public static float _droppedItemDist = 150f;
        /*---------------------- Visual Home DroppedItem ------------------------ */

        [Save] public static float _radiusFonDroppedItem = 1f;
        [Save] public static Color32 _colorFonDroppedItem = new Color32(0, 0, 0, 255);
        [Save]
        public static Color32 _colorDroppedItem = new Color32(255, 255, 255, 255);




        /*---------------------- Visual Other BaseHelicopter ------------------------ */

        [Save]
        public static bool[] _Helicopter = new bool[]
        {
            false, //BaseHelicopter     0
            false, //fon                1
            false, //heli_crate         2 
        }; 
        /*---------------------- Visual Other BaseHelicopter ------------------------ */

        [Save] public static float _radiusFonHelicopter = 1f;
        [Save] public static Color32 _colorFonHelicopter = new Color32(0, 0, 0, 255);
        [Save]
        public static Color32 _colorHelicopter = new Color32(255, 255, 255, 255);
        [Save]
        public static Color32 _colorheli_crate = new Color32(255, 255, 255, 255);




        /*---------------------- Visual Other BradleyAPC ------------------------ */

        [Save]
        public static bool[] _BradleyAPC = new bool[]
        {
            false, //BradleyAPC         0 
            false, //fon                1 
            false, //BradleyAPC         2  
        };
        /*---------------------- Visual Other BaseHelicopter ------------------------ */

        [Save] public static float _radiusFonBradleyAPC = 1f;
        [Save] public static Color32 _colorFonBradleyAPC = new Color32(0, 0, 0, 255);
        [Save]
        public static Color32 _colorBradleyAPC = new Color32(255, 255, 255, 255);
        [Save]
        public static Color32 _colorbradley_crate = new Color32(255, 255, 255, 255);



        /*---------------------- Visual Home StorageContainer ------------------------ */

        [Save]
        public static bool[] _Storage = new bool[]
        {
            false, //Crate tools                0
            false, //Crate small            1
            false, //Crate normal         2
            false, //Crate normal food             3
            false, //Crate normal medical                4
            false, //Crate mine            5
            false, //Crate military         6
            false, //Crate elite             7
            false, //Barrel                8
            false, //Barrel fire            9
            false, //Oil barrel         10
            false, //Recycler             11
            false, //fon                 12
            false, //SupplyDrop                 12
        };
        [Save] public static float _StorageDist = 150f;
        /*---------------------- Visual Home Trap ------------------------ */

        [Save] public static float _radiusFonStorage = 1f;
        [Save] public static Color32 _colorFonStorage = new Color32(0, 0, 0, 255);
        [Save]
        public static Color32[] _colorStorage = {
            new Color32(255, 255, 255, 255), //0
            new Color32(255, 255, 255, 255), //1
            new Color32(255, 255, 255, 255), //2
            new Color32(255, 255, 255, 255), //3 
            new Color32(255, 255, 255, 255), //4
            new Color32(255, 255, 255, 255), //5
            new Color32(255, 255, 255, 255), //6
            new Color32(255, 255, 255, 255), //7 
            new Color32(255, 255, 255, 255), //8
            new Color32(255, 255, 255, 255), //9
            new Color32(255, 255, 255, 255), //10
            new Color32(255, 255, 255, 255), //11 
            new Color32(255, 255, 255, 255), //11 
        };


        [Save]
        public static bool[] _raid = new bool[]
   {
            false, //         0
            false, //     1
            false, //  2
            false, //             3
            false, //         4
            false, //    5
            false, //    6
            false, //   7
            false //        8
   };
        [Save] public static float _radiusFonRaid = 1f;
        [Save] public static Color32 _colorFonRaid = new Color32(0, 0, 0, 255);
        [Save] public static Color32 _colorRaid = new Color32(255, 255, 255, 255);
        [Save] public static float _secondRaid = 60f;


        [Save] public static Color32 _colorMonumentInfo = new Color32(255, 255, 255, 255);
        [Save] public static bool _MonumentInfo;

    }
}