using static LastDesirePro.Menu.CFG.VisuаlCоnfig;
using LastDesirePro.Menu.CFG;
using static LastDesirePro.DrawMenu.Prefab;
using UnityEngine;

namespace LastDesirePro.Menu.MenuTab {
  public class MainTab {
    public static Vector2 _Visual;
    public static float f = 0;
    public static float he = 100;
    public static int tab = 0;
    public static string[] tb = new string[] {
      "ESP",
      "Radar 2D",
      "AimBot",
      "Automatic",
      "Misc",
      "Other"
    };
    public static void DoTab() {
      tab = MainTab(40, 20, 100, 20, tb, tab, 120);
      switch (tab) {
      case 0:
        Visual.DoVisual();
        break;
      case 1:
        Radar.DoRadar();
        break;
      case 2:
        AimBot.DoAimBot();
        break;
      case 3:
        Automatic.DoAutomatic();
        break;
      case 4:
        Misc.DoMisc();
        break;
      case 5:
        Other.DoOther();
        break;
      }
    }
  }
}
