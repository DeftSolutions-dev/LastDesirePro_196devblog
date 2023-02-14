using UnityEngine;
using LastDesirePro.DrawMenu;
using static LastDesirePro.DrawMenu.Prefab;

namespace LastDesirePro.ColorPicker {
  [Attributes.Component]
  public class GUIColorPicker: MonoBehaviour {
    public static AssetBundle myLoadedAssetBundle;
    public static Color color = Color.white;

    public static ColorPicker colorPicker;
    public static bool window;
    public static int id;
    public static string title;
    private void OnGUI() {
      if (colorPicker == null) {
        colorPicker = new ColorPicker(color);
      }
      if (window && !Menu.MainMenu._IsMenu) {
        colorPicker.DrawWindow();
        color = colorPicker.color;
      }
    }
    public class ColorPicker {
      public Color color {
        get {
          return _color;
        }
      }
      Color _color;

      float h = 0 f, s = 1 f, v = 1 f;

      Rect windowRect = new Rect(190, 20, 165, 140);

      GUIStyle previewStyle;
      GUIStyle labelStyle;
      GUIStyle svStyle, hueStyle;

      Texture2D hueTexture, svTexture;
      public static Texture2D circle, rightArrow, leftArrow, button, buttonHighlighted;

      const int kHSVPickerSize = 120,
        kHuePickerWidth = 16;
      public ColorPicker(Color c) {
        _color = c;
        Setup();
      }

      void Setup() {
        ColorHSV.RGBToHSV(_color, out h, out s, out v);
        previewStyle = new GUIStyle();
        previewStyle.normal.background = Texture2D.whiteTexture;

        labelStyle = new GUIStyle();
        labelStyle.fontSize = 12;

        hueTexture = CreateHueTexture(20, kHSVPickerSize);
        hueStyle = new GUIStyle();
        hueStyle.normal.background = hueTexture;

        svTexture = CreateSVTexture(_color, kHSVPickerSize);
        svStyle = new GUIStyle();
        svStyle.normal.background = svTexture;
      }
      public void DrawWindow(int id = 0, string title = "") {
        windowRect = GUI.Window(id, windowRect, DrawColorPickerWindow, title);
      }
      void DrawColorPickerWindow(int windowID) {
        DoMain();
        DrawColorPicker();

        if (Event.current.type == EventType.Repaint) {
          var rect = GUILayoutUtility.GetLastRect();
          windowRect.height = rect.y + rect.height + 10 f;
        }

        GUI.DragWindow();
      }
      void DoMain() {

        Rect outline = new Rect(0, 0, windowRect.width, windowRect.height);
        Rect Loutline = MenuFix.Line(outline);
        Rect Doutline = MenuFix.Line(Loutline);
        Rect LGoutline = MenuFix.Line(Doutline, 3);
        Rect fill = MenuFix.Line(LGoutline);
        Rect line1 = new Rect(fill.x - 5, fill.y - 5, fill.width + 10, 15);
        Rect line2 = new Rect(fill.x - 5, fill.y + 5, fill.width + 10, 13);
        var curEvent = Event.current;
        if (outline.Contains(curEvent.mousePosition))
          Drawing.DrawRect(outline, new Color32(200, 200, 200, 255), 6);
        else
          Drawing.DrawRect(outline, new Color32(122, 122, 122, 255), 6);
        Drawing.DrawRect(Loutline, new Color32(28, 28, 45, 255), 6);
        Drawing.DrawRect(Doutline, new Color32(28, 28, 45, 255), 6);
        Drawing.DrawRect(fill, new Color32(28, 28, 45, 255), 6); //Внутрений фон
        Drawing.DrawRect(line1, new Color32(32, 34, 50, 255), 6); //Верхняя панель
        Drawing.DrawRect(line2, new Color32(32, 34, 50, 255), 0); //Фикс низа верхний панели
        String(new Vector2(83, 4), title, Color.white, true, 15, FontStyle.Normal);
      }
      public void DrawColorPicker() {
        using(new GUILayout.VerticalScope()) {
          GUILayout.Space(5 f);
          DrawPreview(_color);

          GUILayout.Space(5 f);
          DrawHSVPicker(ref _color);
          GUILayout.Space(5 f);
          if (Button("Сохранить", 120, 20, new GUILayoutOption[0])) {
            switch (id) {
            case 0:
              break;
            case 1:
              Menu.CFG.VisuаlCоnfig._colorPlayers = color;
              break;
            case 2:
              Menu.CFG.VisuаlCоnfig._colorSleepers = color;
              break;
            case 3:
              Menu.CFG.VisuаlCоnfig._colorBone = color;
              break;
            case 4:
              Menu.CFG.VisuаlCоnfig._colorDistance = color;
              break;
            case 5:
              Menu.CFG.VisuаlCоnfig._colorItems = color;
              break;
            case 6:
              Menu.CFG.VisuаlCоnfig._colorBox = color;
              break;
            case 7:
              Menu.CFG.VisuаlCоnfig._colorFon = color;
              break;
            case 8:
              Menu.CFG.VisuаlCоnfig._colorAnimals[0] = color;
              break;
            case 9:
              Menu.CFG.VisuаlCоnfig._colorAnimals[1] = color;
              break;
            case 10:
              Menu.CFG.VisuаlCоnfig._colorAnimals[2] = color;
              break;
            case 11:
              Menu.CFG.VisuаlCоnfig._colorAnimals[3] = color;
              break;
            case 12:
              Menu.CFG.VisuаlCоnfig._colorAnimals[4] = color;
              break;
            case 13:
              Menu.CFG.VisuаlCоnfig._colorAnimals[5] = color;
              break;
            case 14:
              Menu.CFG.VisuаlCоnfig._colorFonAnimal = color;
              break;

            case 15:
              Menu.CFG.VisuаlCоnfig._colorResource[0] = color;
              break;
            case 16:
              Menu.CFG.VisuаlCоnfig._colorResource[1] = color;
              break;
            case 17:
              Menu.CFG.VisuаlCоnfig._colorResource[2] = color;
              break;
            case 18:
              Menu.CFG.VisuаlCоnfig._colorResource[3] = color;
              break;
            case 19:
              Menu.CFG.VisuаlCоnfig._colorFonResource = color;
              break;

            case 20:
              Menu.CFG.VisuаlCоnfig._colorCollectible[0] = color;
              break;
            case 21:
              Menu.CFG.VisuаlCоnfig._colorCollectible[1] = color;
              break;
            case 22:
              Menu.CFG.VisuаlCоnfig._colorCollectible[2] = color;
              break;
            case 23:
              Menu.CFG.VisuаlCоnfig._colorCollectible[3] = color;
              break;
            case 24:
              Menu.CFG.VisuаlCоnfig._colorCollectible[4] = color;
              break;
            case 25:
              Menu.CFG.VisuаlCоnfig._colorCollectible[5] = color;
              break;
            case 26:
              Menu.CFG.VisuаlCоnfig._colorCollectible[6] = color;
              break;
            case 27:
              Menu.CFG.VisuаlCоnfig._colorCollectible[7] = color;
              break;
            case 28:
              Menu.CFG.VisuаlCоnfig._colorFonCollectible = color;
              break;

            case 29:
              Menu.CFG.VisuаlCоnfig._colorContainer[0] = color;
              break;
            case 30:
              Menu.CFG.VisuаlCоnfig._colorContainer[1] = color;
              break;
            case 31:
              Menu.CFG.VisuаlCоnfig._colorContainer[3] = color;
              break;
            case 32:
              Menu.CFG.VisuаlCоnfig._colorContainer[2] = color;
              break;
            case 33:
              Menu.CFG.VisuаlCоnfig._colorContainer[4] = color;
              break;
            case 34:
              Menu.CFG.VisuаlCоnfig._colorContainer[5] = color;
              break;
            case 35:
              Menu.CFG.VisuаlCоnfig._colorContainer[6] = color;
              break;
            case 36:
              Menu.CFG.VisuаlCоnfig._colorContainer[7] = color;
              break;
            case 37:
              Menu.CFG.VisuаlCоnfig._colorContainer[8] = color;
              break;
            case 38:
              Menu.CFG.VisuаlCоnfig._colorContainer[14] = color;
              break;
            case 39:
              Menu.CFG.VisuаlCоnfig._colorContainer[9] = color;
              break;
            case 40:
              Menu.CFG.VisuаlCоnfig._colorContainer[10] = color;
              break;
            case 41:
              Menu.CFG.VisuаlCоnfig._colorContainer[11] = color;
              break;
            case 42:
              Menu.CFG.VisuаlCоnfig._colorContainer[12] = color;
              break;
            case 43:
              Menu.CFG.VisuаlCоnfig._colorContainer[13] = color;
              break;
            case 44:
              Menu.CFG.VisuаlCоnfig._colorFonContainer = color;
              break;
            case 45:
              Menu.CFG.VisuаlCоnfig._colorCupboard = color;
              break;
            case 46:
              Menu.CFG.VisuаlCоnfig._colorFonCupboard = color;
              break;
            case 47:
              Menu.CFG.VisuаlCоnfig._colorVisible = color;
              break;

            case 48:
              Menu.CFG.VisuаlCоnfig._colorFonAutoTurret = color;
              break;
            case 49:
              Menu.CFG.VisuаlCоnfig._colorAutoTurret = color;
              break;
            case 50:
              Menu.CFG.VisuаlCоnfig._colorStash = color;
              break;
            case 51:
              Menu.CFG.VisuаlCоnfig._colorFonStash = color;
              break;

            case 52:
              Menu.CFG.VisuаlCоnfig._colorTrap[0] = color;
              break;
            case 53:
              Menu.CFG.VisuаlCоnfig._colorTrap[1] = color;
              break;
            case 54:
              Menu.CFG.VisuаlCоnfig._colorTrap[2] = color;
              break;
            case 55:
              Menu.CFG.VisuаlCоnfig._colorTrap[3] = color;
              break;
            case 56:
              Menu.CFG.VisuаlCоnfig._colorFonTrap = color;
              break;
            case 57:
              Menu.CFG.VisuаlCоnfig._colorDroppedItem = color;
              break;
            case 58:
              Menu.CFG.VisuаlCоnfig._colorFonDroppedItem = color;
              break;
            case 59:
              Menu.CFG.VisuаlCоnfig._colorHelicopter = color;
              break;
            case 60:
              Menu.CFG.VisuаlCоnfig._colorFonHelicopter = color;
              break;
            case 61:
              Menu.CFG.VisuаlCоnfig._colorBradleyAPC = color;
              break;
            case 62:
              Menu.CFG.VisuаlCоnfig._colorFonBradleyAPC = color;
              break;
            case 63:
              Menu.CFG.VisuаlCоnfig._colorheli_crate = color;
              break;
            case 64:
              Menu.CFG.VisuаlCоnfig._colorbradley_crate = color;
              break;

            case 65:
              Menu.CFG.VisuаlCоnfig._colorStorage[12] = color;
              break;
            case 66:
              Menu.CFG.VisuаlCоnfig._colorStorage[0] = color;
              break;
            case 67:
              Menu.CFG.VisuаlCоnfig._colorStorage[1] = color;
              break;
            case 68:
              Menu.CFG.VisuаlCоnfig._colorStorage[2] = color;
              break;
            case 69:
              Menu.CFG.VisuаlCоnfig._colorStorage[3] = color;
              break;
            case 70:
              Menu.CFG.VisuаlCоnfig._colorStorage[4] = color;
              break;
            case 71:
              Menu.CFG.VisuаlCоnfig._colorStorage[5] = color;
              break;
            case 72:
              Menu.CFG.VisuаlCоnfig._colorStorage[6] = color;
              break;
            case 73:
              Menu.CFG.VisuаlCоnfig._colorStorage[7] = color;
              break;
            case 74:
              Menu.CFG.VisuаlCоnfig._colorStorage[8] = color;
              break;
            case 75:
              Menu.CFG.VisuаlCоnfig._colorStorage[9] = color;
              break;
            case 76:
              Menu.CFG.VisuаlCоnfig._colorStorage[10] = color;
              break;
            case 77:
              Menu.CFG.VisuаlCоnfig._colorStorage[11] = color;
              break;
            case 78:
              Menu.CFG.VisuаlCоnfig._colorFonStorage = color;
              break;
            case 79:
              Menu.CFG.RadarConfig._colorPlayers = color;
              break;
            case 80:
              Menu.CFG.RadarConfig._colorNPC = color;
              break;

            case 81:
              Menu.CFG.RadarConfig._colorAnimals[0] = color;
              break;
            case 82:
              Menu.CFG.RadarConfig._colorAnimals[1] = color;
              break;
            case 83:
              Menu.CFG.RadarConfig._colorAnimals[2] = color;
              break;
            case 84:
              Menu.CFG.RadarConfig._colorAnimals[3] = color;
              break;
            case 85:
              Menu.CFG.RadarConfig._colorAnimals[4] = color;
              break;
            case 86:
              Menu.CFG.RadarConfig._colorAnimals[5] = color;
              break;
            case 87:
              Menu.CFG.RadarConfig._colorOOFIndicator = color;
              break;
            case 88:
              Menu.CFG.RadarConfig._colorOOFIndicatorNPC = color;
              break;
            case 89:
              Menu.CFG.AimBotConfig._colorFov = color;
              break;
            case 90:
              Menu.CFG.AimBotConfig._colorpAimFov = color;
              break;
            case 91:
              Menu.CFG.VisuаlCоnfig._colorCorpses = color;
              break;
            case 92:
              Menu.CFG.VisuаlCоnfig._colorRaid = color;
              break;
            case 93:
              Menu.CFG.VisuаlCоnfig._colorFonRaid = color;
              break;
            case 94:
              Menu.CFG.MiscConfig._ColorHitLog = color;
              break;
            case 95:
              Menu.CFG.AimBotConfig._pAimTargetColor = color;
              break;
            case 96:
              Menu.CFG.AimBotConfig._silentPlayerPredictFovColor = color;
              break;
            case 97:
              Menu.CFG.MiscConfig._hitMarkerColor = color;
              break;
            case 98:
              Menu.CFG.VisuаlCоnfig._colorMonumentInfo = color;
              break;
            case 99:
              //Пусто
              break;
            case 100:
              Menu.CFG.MiscConfig._colorMarkerSystem = color;
              break;
            case 101:
              Menu.CFG.MiscConfig._colorFonMarkerSystem = color;
              break;
            }
            id = 0;
            window = false;
          }
        }
      }

      void DrawPreview(Color c) {
        using(new GUILayout.VerticalScope()) {
          var tmp = GUI.backgroundColor;
          GUI.backgroundColor = new Color(c.r, c.g, c.b);
          GUILayout.Label("", previewStyle, GUILayout.Width(kHSVPickerSize + kHuePickerWidth + 10), GUILayout.Height(12 f));

          GUILayout.Space(1 f);

          var alpha = c.a;
          GUI.backgroundColor = new Color(alpha, alpha, alpha);
          GUILayout.Label("", previewStyle, GUILayout.Width(kHSVPickerSize + kHuePickerWidth + 10), GUILayout.Height(2 f));

          GUI.backgroundColor = tmp;
        }
      }

      void DrawHSVPicker(ref Color c) {
        using(new GUILayout.HorizontalScope()) {
          GUILayout.Label("", svStyle, GUILayout.Width(kHSVPickerSize), GUILayout.Height(kHSVPickerSize));
          DrawSVHandler(GUILayoutUtility.GetLastRect(), ref c);

          GUILayout.Space(10 f);

          GUILayout.Label("", hueStyle, GUILayout.Width(kHuePickerWidth), GUILayout.Height(kHSVPickerSize));
          DrawHueHandler(GUILayoutUtility.GetLastRect(), ref c);
        }
      }

      void DrawSVHandler(Rect rect, ref Color c) {
        const float size = 10 f;
        const float offset = 5 f;
        GUI.DrawTexture(new Rect(rect.x + s * rect.width - offset, rect.y + (1 f - v) * rect.height - offset, size, size), circle);

        var e = Event.current;
        var p = e.mousePosition;
        if (e.button == 0 && (e.type == EventType.MouseDown || e.type == EventType.MouseDrag) && rect.Contains(p)) {
          s = (p.x - rect.x) / rect.width;
          v = 1 f - (p.y - rect.y) / rect.height;
          c = ColorHSV.HSVToRGB(h, s, v);

          e.Use();
        }
      }

      void DrawHueHandler(Rect rect, ref Color c) {
        const float size = 15 f;
        GUI.DrawTexture(new Rect(rect.x - size * 0.75 f, rect.y + (1 f - h) * rect.height - size * 0.5 f, size, size), rightArrow);
        GUI.DrawTexture(new Rect(rect.x + rect.width - size * 0.25 f, rect.y + (1 f - h) * rect.height - size * 0.5 f, size, size), leftArrow);

        var e = Event.current;
        var p = e.mousePosition;
        if (e.button == 0 && (e.type == EventType.MouseDown || e.type == EventType.MouseDrag) && rect.Contains(p)) {
          h = 1 f - (p.y - rect.y) / rect.height;
          c = ColorHSV.HSVToRGB(h, s, v);
          UpdateSVTexture(c, svTexture);

          e.Use();
        }
      }

      void UpdateSVTexture(Color c, Texture2D tex) {
        float h, _s, _v;
        ColorHSV.RGBToHSV(c, out h, out _s, out _v);

        var size = tex.width;
        for (int y = 0; y < size; y++) {
          var v = 1 f * y / size;
          for (int x = 0; x < size; x++) {
            var s = 1 f * x / size;
            var color = ColorHSV.HSVToRGB(h, s, v);
            tex.SetPixel(x, y, color);
          }
        }

        tex.Apply();
      }

      Texture2D CreateHueTexture(int width, int height) {
        var tex = new Texture2D(width, height);
        for (int y = 0; y < height; y++) {
          var h = 1 f * y / height;
          var color = ColorHSV.HSVToRGB(h, 1 f, 1 f);
          for (int x = 0; x < width; x++) {
            tex.SetPixel(x, y, color);
          }
        }
        tex.Apply();
        return tex;
      }

      Texture2D CreateSVTexture(Color c, int size) {
        var tex = new Texture2D(size, size);
        UpdateSVTexture(c, tex);
        return tex;
      }

    }
    public class ColorHSV {

      public static Color HSVToRGB(float H, float S, float V, bool hdr = false) {
        Color white = Color.white;
        if (S == 0 f) {
          white.r = V;
          white.g = V;
          white.b = V;
        } else if (V == 0 f) {
          white.r = 0 f;
          white.g = 0 f;
          white.b = 0 f;
        } else {
          white.r = 0 f;
          white.g = 0 f;
          white.b = 0 f;
          float num = H * 6 f;
          int num2 = (int) Mathf.Floor(num);
          float num3 = num - (float) num2;
          float num4 = V * (1 f - S);
          float num5 = V * (1 f - S * num3);
          float num6 = V * (1 f - S * (1 f - num3));
          switch (num2 + 1) {
          case 0:
            white.r = V;
            white.g = num4;
            white.b = num5;
            break;
          case 1:
            white.r = V;
            white.g = num6;
            white.b = num4;
            break;
          case 2:
            white.r = num5;
            white.g = V;
            white.b = num4;
            break;
          case 3:
            white.r = num4;
            white.g = V;
            white.b = num6;
            break;
          case 4:
            white.r = num4;
            white.g = num5;
            white.b = V;
            break;
          case 5:
            white.r = num6;
            white.g = num4;
            white.b = V;
            break;
          case 6:
            white.r = V;
            white.g = num4;
            white.b = num5;
            break;
          case 7:
            white.r = V;
            white.g = num6;
            white.b = num4;
            break;
          }
          if (!hdr) {
            white.r = Mathf.Clamp(white.r, 0 f, 1 f);
            white.g = Mathf.Clamp(white.g, 0 f, 1 f);
            white.b = Mathf.Clamp(white.b, 0 f, 1 f);
          }
        }
        return white;
      }

      public static void RGBToHSV(Color color, out float H, out float S, out float V) {
        if (color.b > color.g && color.b > color.r)
          RGBToHSVHelper(4 f, color.b, color.r, color.g, out H, out S, out V);
        else if (color.g > color.r)
          RGBToHSVHelper(2 f, color.g, color.b, color.r, out H, out S, out V);
        else
          RGBToHSVHelper(0 f, color.r, color.g, color.b, out H, out S, out V);
      }

      private static void RGBToHSVHelper(float offset, float dominantcolor, float colorone, float colortwo, out float H, out float S, out float V) {
        V = dominantcolor;
        if (V != 0 f) {
          float num;
          if (colorone > colortwo)
            num = colortwo;
          else
            num = colorone;
          float num2 = V - num;
          if (num2 != 0 f) {
            S = num2 / V;
            H = offset + (colorone - colortwo) / num2;
          } else {
            S = 0 f;
            H = offset + (colorone - colortwo);
          }
          H /= 6 f;
          if (H < 0 f)
            H += 1 f;
        } else {
          S = 0 f;
          H = 0 f;
        }
      }

    }
  }

}
