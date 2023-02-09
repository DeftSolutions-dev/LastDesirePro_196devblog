using UnityEngine;
using System;
using System.Collections.Generic;

namespace LastDesirePro.DrawMenu
{
    public class Convertor
    {
        private static Dictionary<KeyCode, string> keyNames; 
        public static Dictionary<KeyCode, string> KeyNames
        {
            get
            {
                if (keyNames == null)
                    Init();
                return keyNames;
            }
        }
        private static void Init()
        {
            keyNames = new Dictionary<KeyCode, string>();
            foreach (KeyCode k in Enum.GetValues(typeof(KeyCode))) 
                if (!keyNames.ContainsKey(k))
                    keyNames.Add(k, Enum.GetName(typeof(KeyCode), k)); 
            for (int i = 0; i < 10; i++)
            {
                keyNames[(KeyCode)((int)KeyCode.Alpha0 + i)] = i.ToString();
                keyNames[(KeyCode)((int)KeyCode.Keypad0 + i)] = "Num " + i.ToString();
            }
            keyNames[KeyCode.CapsLock] = "Caps";
            keyNames[KeyCode.ScrollLock] = "Scroll";
            keyNames[KeyCode.RightShift] = "R-Shift";
            keyNames[KeyCode.RightControl] = "R-Control";
            keyNames[KeyCode.LeftShift] = "L-Shift";
            keyNames[KeyCode.LeftControl] = "L-Control";
            keyNames[KeyCode.Escape] = "Esc";
            keyNames[KeyCode.UpArrow] = "Up";
            keyNames[KeyCode.DownArrow] = "Down";
            keyNames[KeyCode.LeftArrow] = "Left";
            keyNames[KeyCode.RightArrow] = "Right";
            keyNames[KeyCode.RightBracket] = "R-Bracket";
            keyNames[KeyCode.LeftBracket] = "L-Bracket";
            keyNames[KeyCode.KeypadDivide] = "Divide";
            keyNames[KeyCode.KeypadMultiply] = "Multiply";
            keyNames[KeyCode.KeypadMinus] = "Minus";
            keyNames[KeyCode.KeypadPlus] = "Plus";
            keyNames[KeyCode.KeypadEnter] = "Enter";
            keyNames[KeyCode.KeypadPeriod] = "Period";
        }
        public static Texture2D Base64ToTexture(string encodedData)
        {
            byte[] data = Convert.FromBase64String(encodedData);
            Texture2D texture2D = new Texture2D(1, 1, TextureFormat.ARGB32, true, true);
            texture2D.hideFlags = HideFlags.HideAndDontSave;
            texture2D.filterMode = FilterMode.Bilinear;
            texture2D.LoadImage(data, true);
            return texture2D;
        }
    }
}
