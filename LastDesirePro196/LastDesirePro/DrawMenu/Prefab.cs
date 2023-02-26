
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace LastDesirePro.DrawMenu
{
    public static class Prefab
    {   

        public static GUIStyle _MenuTabStyle;
        public static GUIStyle _LabelStyle;
        public static GUIStyle _LabelStyle1;
        public static GUIStyle _HeaderStyle;
        public static GUIStyle _TextStyle;
        public static GUIStyle _TextStyle1;
        public static GUIStyle _TextStyle2;
        public static GUIStyle _TextToggle; 
        public static GUIStyle _sliderStyle;
        public static GUIStyle _sliderThumbStyle;
        public static GUIStyle _sliderVThumbStyle;
        public static GUIStyle _listStyle;
        public static GUIStyle _ButtonStyle;
        public static GUIStyle _TextMainMenu;
        public static Color32 _OutlineBorderBlack = new Color32(88, 88, 88, 255);
        public static Color32 _OutlineBorderLightGray = new Color32(28, 28, 28, 255);
        public static Color32 _OutlineBorderDarkGray = new Color32(28, 28, 28, 255);
        public static Color32 _FillLightBlack = new Color32(8, 8, 8, 255);
        public static Color32 _Accent1 = new Color32(28, 28, 28, 255);
        public static Color32 _Accent2 = new Color32(28, 28, 28, 255);
        public static Color32 _ToggleBoxBGStyle; 
        public static Regex digitsOnly = new Regex(@"[^\d]");
        public static Texture2D check;
        static Prefab()
        {
            check = Convertor.Base64ToTexture("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RD0iZ2ltcDpkb2NpZDpnaW1wOjdlNjkyYzEyLWJkOTItNDBmYy1iNjgxLWY2NDc2ZjBjZWYwYiIKICAgeG1wTU06SW5zdGFuY2VJRD0ieG1wLmlpZDo0NzczMjYyMi0yY2JmLTQxYWMtYTM2Yi1mNTAyNjdlNzFjYjUiCiAgIHhtcE1NOk9yaWdpbmFsRG9jdW1lbnRJRD0ieG1wLmRpZDozZWZkYWZmMC00OTk3LTRiYTAtYTdhNS03NTk0ZGRmNzRjOGEiCiAgIGRjOkZvcm1hdD0iaW1hZ2UvcG5nIgogICBHSU1QOkFQST0iMi4wIgogICBHSU1QOlBsYXRmb3JtPSJXaW5kb3dzIgogICBHSU1QOlRpbWVTdGFtcD0iMTY0ODk3MjYzOTk1NDIyMyIKICAgR0lNUDpWZXJzaW9uPSIyLjEwLjMwIgogICB0aWZmOk9yaWVudGF0aW9uPSIxIgogICB4bXA6Q3JlYXRvclRvb2w9IkdJTVAgMi4xMCI+CiAgIDx4bXBNTTpIaXN0b3J5PgogICAgPHJkZjpTZXE+CiAgICAgPHJkZjpsaQogICAgICBzdEV2dDphY3Rpb249InNhdmVkIgogICAgICBzdEV2dDpjaGFuZ2VkPSIvIgogICAgICBzdEV2dDppbnN0YW5jZUlEPSJ4bXAuaWlkOmVjZWFiOGQ3LTNkNzAtNGQyYi1iZjQ4LWRmY2U3ZjA4YzAyZCIKICAgICAgc3RFdnQ6c29mdHdhcmVBZ2VudD0iR2ltcCAyLjEwIChXaW5kb3dzKSIKICAgICAgc3RFdnQ6d2hlbj0iMjAyMS0xMS0yNlQyMTo1MTozNSIvPgogICAgIDxyZGY6bGkKICAgICAgc3RFdnQ6YWN0aW9uPSJzYXZlZCIKICAgICAgc3RFdnQ6Y2hhbmdlZD0iLyIKICAgICAgc3RFdnQ6aW5zdGFuY2VJRD0ieG1wLmlpZDoxY2QyMzliNi0xYzFjLTRjODQtODhjMi1jMzU0MzEwNTZjNTYiCiAgICAgIHN0RXZ0OnNvZnR3YXJlQWdlbnQ9IkdpbXAgMi4xMCAoV2luZG93cykiCiAgICAgIHN0RXZ0OndoZW49IjIwMjItMDQtMDNUMDM6NTc6MTkiLz4KICAgIDwvcmRmOlNlcT4KICAgPC94bXBNTTpIaXN0b3J5PgogIDwvcmRmOkRlc2NyaXB0aW9uPgogPC9yZGY6UkRGPgo8L3g6eG1wbWV0YT4KICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgIAo8P3hwYWNrZXQgZW5kPSJ3Ij8+9SxxAQAAAAZiS0dEAP8A/wD/oL2nkwAAAAlwSFlzAAALEwAACxMBAJqcGAAAAAd0SU1FB+YEAwc5E2SJgfAAAAwRSURBVHja7dwBkho5FETB7gnf/8r4AnYYAgNNvcwTrIWkKv2e2OMAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA+D9OSwDAp9xut9vTQXaeskwBAGA98JUBBQAAoa8MKAAACH5FQAEAIB38ioACAEA8/BUBBQCAaPArAQoAAOHwVwQUAADC4V8vAQoAANnwL5cABQCAbPCXS8CPrQwAu8XGBAAAIWkSoAAAIPzLJUABAED4B0uAvwEAQPgHmQAAIPyDUwATAAAIFiAFAACv/yCfAAAQ/v8Ky8FPASYAAGACAIDXv9d/YQpgAgCA8A9SAAAgWI4UAAC8/oP8DQAAwv+R4Bz5WwATAAAIUgAAvP69/oN8AgAQ/jwangOfAUwAACBIAQDw+kcBAED4owAAAJMFSgEAEF6YAAAg/FEAABD+KAAAgAIAgNc/CgAAwh8FAABQAADw+kcBAED4owAAAAoAAF7/KAAACH8UAABAAQDA6x8FAED4gwIAACgAAF7/oAAACH8UAADgDud5ngoAAF7/KAAACH8UAACEPwoAAHy3he//CgCA1z/VImMJAIQ/rde/CQAARCkAAF7/BPkEACD8uScwh8b/JgAAYAIAgNc/hde/AgAg/AmG/3H4BAAAzWJjCQC8/mm9/k0AAIQ/UQoAAMRe/8fhEwCA1z+58DcBABD+RCkAABB7/R+HTwAAXv/kwl8BABD+BMP/OHwCABD+JCkAAHj9x17/x+ETAIDXv/BPZqEJAIDwRwEAAK//xL/dzw/g9S/8TQAAEP4oAADg9T+5BrYBgNe/8DcBAED4owAAgNf/5FpYAgCvf+FvAgCA8EcBAACv/8k1sQQAXv/C3wQAAOGPAgAAXv+Ta2MJALz+hb8CAIDwF/4BPgEACH8UAADw+k+skyUA8PoX/iYAAAh/FAAA8PqfXC9LAOD1L/xNAAAQ/igAAOD1P7lulgDA61/4mwAAIPxRAADA639y/SwBgNe/8DcBAED4owAAgNf/5DpaAgCvf+FvAgCA8EcBAACv/8n1tAQAXv/CXwEAQPgL/wCfAACEPwoAAHj9J9bWEgB4/Qt/EwAA4S/8UQAAwOt/co0tAYDXv/A3AQAQ/qAAAIDX/+RaWwIAr3/hbwIAIPxBAQAAr//JNbcEgNc/wt8EAED4gwIAAF7/k2tvCQCvf4S/CQCA8AcFAAC8/id/A0sAeP0j/E0AAIQ/KAAA4PU/+VtYAsDrH+GvAAAIf4R/gE8AgPAHBQAAvP4Tv4slALz+Ef4mAADCHxQAAPD6n/x9LAHg9Y/wNwEAEP6gAACA1//k72QJAK9/hL8JAIDwBwUAALz+J38vSwB4/SP8TQAAhD8oAADg9T/5u1kCwOsf4W8CACD8QQEAAK//yd/PEgBe/wh/BQBA+CP8A3wCAIQ/KAAA4PWf+C0tAeD1j/A3AQAQ/qAAAIDX/+RvagkAr3+EvwkAgPCHgF+W4PUXlvYMeP1zud/WEnz2ZeJwgde/8EcBiF9GDhsIfwUABSB+ETl4OHcIfxSA8OXjEOL84d5BAQhfPg4kzh/uGxSA6MXjUOIc4p5BAQhfOg4oziHuFhSA6IXjoOIs4l7hGdn/E+C3XzguTED4YwIQD08HF+cRdwgKQPSycYBxHnF38IjUJwCXDQDEJgCF8NfkcSZxZ6AARC8aBxpnEncF95j/BFC7aFys2KOAAuCCBfD658/7QBA65OBMuhcwAXDRWAOwH0EBwKULeP0zuR+EnoMPzqQ7ABMAF40LGOw9UABwEQNe/0zuCwHnMgBn0nnHBACXMthnoADgcga8/lEABBrgPAp/FABc0mBfgQLgwrF2gNc/CgBKAPYSwh8FABc39hDCHwUAFzj2DqAAuHisJ+D1jwKAEoD9gvBHAcCljn0CKAC43AGvfxQAlADsDeEPCgAueuwJQAHAhQ9e/6AAAMqg8IcvLwAuIuuNPQCYACAAwOsf7t1LAgmXEM6ac4cJAAgD/N6gAIBQAK9/FABQAvzGCH8UABAQfltAAUBQAF7/KAAoAfg9Ef4oAAgN/I4IfxQAhAd+P0ABQIgAXv8oAA4KKG7CHxQAhAl+L0ABQKiA1z88ud8EDS42nB9nBBMAEDB+G0ABAEEDXv9M7jsBg8tOKcN5wAQAhI7fAVAAQPiA1z+T+0+w4AJUwLD3MQEAQWTNAQVAq0YggXuKyX0oVHApKlvY55gAgHCyvoACoGUDuJeY3I9ePbgkvf6xrzEBAGFlPQEFQOtGaIF7iMl9KVBwcdr32MMoAC5DXKD2O/YuAT8OI4LMmgEKgBKAQAN3DYU9KlBwqdrb2KeYAIBwsz6AAqChI+TA3cLkXhUmwsRFay9jT2IC4MAi8KwFoAAoAQDuESb3rCXwinLx2rfYg5gAOMQIQP92QAFQAhCE4M5gcu9aAoHiMrZHsd8wAcDBFor+nYACAMIRPBKY3MOWQJi4oO1J7C1MAHDQBaV/E6AAoAQITHAfMLmXLYEwcWnbfwh/FABcwi5v+w7hT4BPAC4BhD+gAKAECFJw5knsa0sgUFzm9hjCHxMAXAyKm/9OQAFACVACcL5BAQAlQEER/qAA4KJQAoQ/oAAoASgBOM/wLfvcEggTrnPp2z/2AZgAuDyIha/wBxQAJQAhjLMLr93vlkCY8PkgsFeEP5gAuFCIlTfhDygASgDREoBzCgqAywUlA+cTFACXDKsBLfwBBUAJwCsdZxIUAFgvAUqF8IePnwFL4EXJe0PD7y/8wQTA5UOsvAl/QAFQApSAaAnA2YNLnAVLIEx4T5D4rYU/mADgQoqVN+EPKAAoAdESgHMGlzoTlkCY8Npw8bsKfzABwCUVK2/CH1AAUAKiJQBnChQAXFgKAc4SKACwGvrCH7h8QbYEXpDg9Q8KAEoACH8I8AnARQaACQAmAaA0gwKAEgDCHyb5BOByA8AEAJMAUJBBAUAJAOEPCgBKAAh/2OBvAFx8AJgAYBIASjCYAAAIf1AAcBECMJIdlmCHTwEovYACoASA8Af+yicAlyMAJgCYBICCCwoASgAIf5jkE4ALEwATAEwCQJkFBQAlAIQ/TPIJwCUKgAkAJgGguIIJAIDwBwUAlyoAI3lgCZp8CkBRBQUAJQCEPygAKAEg/GGdvwFw4bpwAUwAMAkAZRQUAJQAEP4wyScAXMIAJgBgEoDiCQoASgAIf5jkEwAuZgATADAJQMkEEwAA4Q8KAC5qAEbud0vAPXwKQKkEBQAlAIQ/fDmfAHB5A5gAgEkACiQoAKAEIPxhkk8AuNABTADAJABlERQAUAIQ/jDJJwBc8gAmAGASgGIICgAoAQh/UABACUD4wwZ/A4AAAFAAAJQ/SJxXS8Cr+BQg/AETAAQCACYAmASg7AEKAEoAwh94K58AEBIAJgBgEoBiBwoAKAEIf5jkEwCCA8AEAEwCUOJAAQAlAOEPk3wCQJgAmACASQAKGygAoAQg/GGSTwAIGAATADAJQDkDEwBA+At/MAEAUwDhD5gAgOABMAEAkwAlDFAAQAkQ/sBl+QSAMAIwAQCTAIVL4QIFAJQA4Q9M8gkAAQVgAgAmAcoVoACAEiD8gUk+ASC0AEwAwCRAkQIUAFAChD8wyScABBmACQCYBChNgAIASoDwByb5BIBwAzABAJMABQlQAEAJEP6AAgBKgPAHNvgbAAQfgAkAmAQoQYACAEqA8Acm+QSAMAQwAQCTAIUHUABACRD+gAIASoDwBxQAUAKEP6AAgBIg/AEFABQBwQ8oAKAECH9AAQAlQPgDCgAoAoIfUABAERD8gAIA9SIg+AEFACJFQOgDCgBECoHQBwAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAC+zW9RGiIQcbUaAgAAAABJRU5ErkJggg==");

            _MenuTabStyle = new GUIStyle();
            _MenuTabStyle.font = LastDesirePro.Menu.MainMenu._TextFont;
            _MenuTabStyle.fontSize = 29;

            _HeaderStyle = new GUIStyle();
            _HeaderStyle.font = LastDesirePro.Menu.MainMenu._TextFont;
            _HeaderStyle.fontSize = 15;
            _HeaderStyle.alignment = TextAnchor.MiddleCenter;

            _LabelStyle = new GUIStyle();
            _LabelStyle.font = LastDesirePro.Menu.MainMenu._TextFont;
            _LabelStyle.fontSize = 15;
            _LabelStyle.alignment = TextAnchor.LowerCenter;
            _LabelStyle1 = new GUIStyle();
            _LabelStyle1.font = LastDesirePro.Menu.MainMenu._TextFont;
            _LabelStyle1.fontSize = 47;
            _LabelStyle1.alignment = TextAnchor.MiddleLeft;

            _TextStyle = new GUIStyle();
            _TextStyle.font = LastDesirePro.Menu.MainMenu._TextFont;
            _TextStyle.fontSize = 17;
            _TextStyle.alignment = TextAnchor.LowerLeft;


            _TextStyle2 = new GUIStyle();
            _TextStyle2.font = LastDesirePro.Menu.MainMenu._TextFont;
            _TextStyle2.fontSize = 14;
            _TextStyle2.alignment = TextAnchor.LowerLeft;



            _TextStyle1 = new GUIStyle();
            _TextStyle1.font = LastDesirePro.Menu.MainMenu._TextFont;
            _TextStyle1.fontSize = 17;
            _TextStyle1.normal.textColor = Color.white;
            _TextStyle1.onNormal.textColor = Color.white;
            _TextStyle1.alignment = TextAnchor.MiddleCenter;
            _TextStyle1.padding.top = 5;



            _sliderStyle = new GUIStyle();
            _sliderThumbStyle = new GUIStyle(GUI.skin.horizontalSliderThumb);
            _sliderThumbStyle.fixedWidth = 7;

            _sliderVThumbStyle = new GUIStyle(GUI.skin.verticalSliderThumb);
            _sliderVThumbStyle.fixedHeight = 7;

            _listStyle = new GUIStyle();
            _listStyle.padding.left = _listStyle.padding.right = _listStyle.padding.top = _listStyle.padding.bottom = 4;
            _listStyle.alignment = TextAnchor.MiddleLeft;
            _listStyle.font = LastDesirePro.Menu.MainMenu._TextFont;
            _listStyle.fontSize = 15;

            _ButtonStyle = new GUIStyle();
            _ButtonStyle.alignment = TextAnchor.MiddleCenter;
            _ButtonStyle.font = LastDesirePro.Menu.MainMenu._TextFont;
            _ButtonStyle.fontSize = 15;
            _ButtonStyle.padding.left = _ButtonStyle.padding.right = _ButtonStyle.padding.top = _ButtonStyle.padding.bottom = 4;
             


            _TextToggle = new GUIStyle();
            _TextToggle.font = LastDesirePro.Menu.MainMenu._TextFont;
            _TextToggle.fontSize = 17;
            _TextToggle.alignment = TextAnchor.LowerLeft;

            MenuFix.FixGUIStyleColor(_sliderStyle);
            MenuFix.FixGUIStyleColor(_MenuTabStyle);
            MenuFix.FixGUIStyleColor(_TextStyle);
            MenuFix.FixGUIStyleColor(_TextToggle);
            MenuFix.FixGUIStyleColor(_TextStyle1); 



            _MenuTabStyle.normal.textColor = new Color32(160, 160, 160, 255);
            _MenuTabStyle.onNormal.textColor = new Color32(255, 255, 255, 255);
            _MenuTabStyle.hover.textColor = new Color32(210, 210, 210, 255);
            _MenuTabStyle.onHover.textColor = new Color32(255, 255, 255, 255);
            _MenuTabStyle.active.textColor = new Color32(255, 255, 255, 255);
            _MenuTabStyle.onActive.textColor = new Color32(255, 255, 255, 255);
              
            _TextStyle.normal.textColor = new Color32(160, 160, 160, 255);
            _TextStyle.onNormal.textColor = new Color32(150, 98, 239, 255);
            _TextStyle.hover.textColor = new Color32(210, 210, 210, 255);
            _TextStyle.onHover.textColor = new Color32(255, 255, 255, 255);
            _TextStyle.active.textColor = new Color32(150, 98, 239, 255);
            _TextStyle.onActive.textColor = new Color32(150, 98, 239, 255); 


            _TextToggle.normal.textColor = new Color32(150, 98, 239, 255);
            _TextToggle.onNormal.textColor = new Color32(150, 98, 239, 255);
            _TextToggle.hover.textColor = new Color32(180, 143, 245, 255);
            _TextToggle.onHover.textColor = new Color32(180, 143, 245, 255);

            _HeaderStyle.normal.textColor = new Color32(210, 210, 210, 255);
            _listStyle.normal.textColor = new Color32(210, 210, 210, 255);
            _listStyle.onNormal.textColor = Color.grey;
            _listStyle.hover.textColor = Color.grey;
            _ButtonStyle.normal.textColor = new Color32(210, 210, 210, 255);
            _ButtonStyle.onNormal.textColor = new Color32(210, 210, 210, 255);
            _ButtonStyle.hover.textColor = new Color32(150, 98, 239, 255);
            _ButtonStyle.onHover.textColor = new Color32(150, 98, 239, 255);
            _ButtonStyle.active.textColor = new Color32(100, 100, 100, 255);
            _ButtonStyle.onActive.textColor = new Color32(100, 100, 100, 255);
 
            var btex = new Texture2D(1, 1);
              btex.SetPixel(0, 0, new Color32(130, 130, 160, 255));
              btex.Apply();
              _ButtonStyle.hover.background = btex;
              var btex2 = new Texture2D(1, 1);
              btex2.SetPixel(0, 0, new Color32(125, 125, 160, 255));
              btex2.Apply();
            _ButtonStyle.normal.background = btex2;
            var btex3 = new Texture2D(1, 1);
            btex3.SetPixel(0, 0, new Color32(100, 100, 120, 255)); 
            btex3.Apply();
            _ButtonStyle.active.background = btex3;
            var tex = new Texture2D(1, 1);
            tex.SetPixel(0, 0, new Color32(210, 210, 210, 255));
            tex.Apply(); 
            _listStyle.hover.background = tex;
            _listStyle.onHover.background = tex;
            var tex2 = new Texture2D(1, 1);
            tex2.SetPixel(0, 0, new Color32(130, 130, 130, 255));
            tex2.Apply();
            _listStyle.normal.background = tex2;
            _listStyle.onNormal.background = tex2;

            _ToggleBoxBGStyle = new Color32(71, 70, 71, 255); 
             
            style = new GUIStyle(GUI.skin.label) { fontSize = 12, font = LastDesirePro.Menu.MainMenu._TextFont };
            outlineStyle = new GUIStyle(GUI.skin.label) { fontSize = 12, font = LastDesirePro.Menu.MainMenu._TextFont };
            _TextMainMenu = new GUIStyle(GUI.skin.label) { font = Menu.MainMenu._TabFont, fontSize = 30 };

            styleTexture = new Texture2D(1, 1);
            styleTexture.SetPixel(0, 0, Color.green); 
            leftstyle = new GUIStyle();
            leftstyle.alignment = TextAnchor.MiddleRight;
            leftstyle.wordWrap = true;
            leftstyle.font = Menu.MainMenu._TextFont;
            leftstyle.fontStyle = FontStyle.Bold;
            leftstyle.fontSize = 12;
        }
        private static GUIStyle leftstyle = new GUIStyle();
        private static GUIStyle style = new GUIStyle(GUI.skin.label) { fontSize = 12 };
        private static GUIStyle outlineStyle = new GUIStyle(GUI.skin.label) { fontSize = 12 };
        public static void String(Vector2 pos, string text, Color color, bool center = true, int size = 12, FontStyle fontStyle = FontStyle.Bold, int depth = 1)
        {
            leftstyle.fontSize = size;
            leftstyle.richText = true;
            leftstyle.normal.textColor = color;
            leftstyle.fontStyle = fontStyle;
            leftstyle.font = LastDesirePro.Menu.MainMenu._TextFont;

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
            GUIContent content = new GUIContent(text);
            GUIContent content2 = new GUIContent(text);
            if (center)
                pos.x -= style.CalcSize(content).x / 2f;
            switch(depth)
            {
                case 0:

                    GUI.Label(new Rect(pos.x, pos.y, 300f, 25f), content, style);
                    break;
                    case 1:
                    GUI.Label(new Rect(pos.x + 1f, pos.y + 1f, 300f, 25f), content2, outlineStyle);
                    GUI.Label(new Rect(pos.x, pos.y, 300f, 25f), content, style);
                    break;
                case 2: 
                    GUI.Label(new Rect(pos.x, pos.y, 300f, 25f), content, leftstyle);
                    break;
            }  
        } 
        public static Texture2D styleTexture;
        public static bool Toggle(string text, ref bool state, int fontsize = 17, Color colors = new Color(),bool color = false,int num = 0)
        { 
            bool pressed = false;
            int bordersize = 1;
            int boxsize = 20;
            int lastfontsize = _TextStyle.fontSize;
            _TextStyle.fontSize = fontsize;
            GUILayout.Space(3);
            Rect area = GUILayoutUtility.GetRect(150, 20);
            Rect border = new Rect(area.x + bordersize, area.y + bordersize, boxsize - bordersize * 2, boxsize - bordersize * 2);
            Rect togglebox = MenuFix.Line(border);
 
                var curEvent = Event.current;
            if (area.Contains(curEvent.mousePosition))
            {
                if (state)
                    Drawing.DrawRect(border, new Color32(1, 143, 245, 255), 4);
                else
                    Drawing.DrawRect(border, new Color32(255, 255, 255, 255), 4);
                Drawing.DrawRect(togglebox, _ToggleBoxBGStyle, 4);
                if(!state)
                Drawing.DrawRect(togglebox, new Color32(255, 255, 255, 122), 4);
            }
            else
            {
                if (state)
                    Drawing.DrawRect(border, new Color32(162, 105, 246, 255), 4);
                else
                Drawing.DrawRect(border, new Color32(111, 111, 111, 255), 4);
                Drawing.DrawRect(togglebox, _ToggleBoxBGStyle, 4);
            }
            if (color )
            {
                using (new GUILayout.HorizontalScope())
                {

                    if (GUI.Button(area, GUIContent.none, _TextStyle))
                    {
                        state = !state;
                        pressed = true;
                    }
                    if (GUI.Button(new Rect(area.x + 215, area.y, 20, 20), GUIContent.none))
                    {
                        
                        ColorPicker.GUIColorPicker.id = num;
                        ColorPicker.GUIColorPicker.window = true;
                        ColorPicker.GUIColorPicker.title = text;
                    }
                        styleTexture.SetPixel(0, 0, colors);
                        styleTexture.Apply(); 
                    GUI.DrawTexture(new Rect(area.x + 215, area.y, 20, 20), styleTexture, ScaleMode.StretchToFill, true, 1, colors, 1111, 4); 
                }
            }
            else
            {
                if (GUI.Button(area, GUIContent.none, _TextStyle))
                {
                    state = !state;
                    pressed = true;
                }
            }
            if (Event.current.type == EventType.Repaint)
            {
                bool hover = area.Contains(Event.current.mousePosition);
                bool active = hover && Input.GetMouseButton(0);
                if(state)
                    _TextToggle.Draw(new Rect(area.x + 25, area.y, 130, boxsize), text, hover, active, false, false);
                else
                    _TextStyle.Draw(new Rect(area.x + 25, area.y, 130, boxsize), text, hover, active, false, false);
            }
            _TextStyle.fontSize = lastfontsize;
            if (state)
            {  
                GUI.color = Color.white;
                Drawing.DrawRect(togglebox, new Color32(61, 51, 99, 255), 4); 
                GUI.color = new Color32(158, 102, 250,255);
                GUI.DrawTexture(new Rect(area.x + bordersize, area.y + bordersize, boxsize - bordersize * 2, boxsize - bordersize * 2), check);
                GUI.color = Color.white;
            }
            return pressed;
        }
        public static void Slider(float left, float right, ref float value, int size, string text)
        {
            GUILayout.Space(5f);
            value = GUILayout.HorizontalSlider(value, left, right, GUI.skin.horizontalSlider,"label", GUILayout.Width(size));
            Rect position = GUILayoutUtility.GetLastRect();
            Drawing.DrawRect(position, new Color32(32, 34, 55, 255), 15);
            Drawing.DrawRect(new Rect(position.x, position.y + position.height / 2 - 1, position.width, 2), _ToggleBoxBGStyle, 15);
            Drawing.DrawRect(new Rect(position.x, position.y + position.height - 9, (position.width / right) * value, 6), new Color32(153, 105, 246, 255), 15);
            String(new Vector2(position.x + size /2+4, position.y-9), text,Color.white,true,12,FontStyle.Normal,1); 
        }
        public static void ScrollViewMenu(Rect area, ref Vector2 scrollpos, Action code, int padding = 20, params GUILayoutOption[] options)
        {
            Drawing.DrawRect(area, _OutlineBorderBlack, 6);
            Drawing.DrawRect(MenuFix.Line(area), _OutlineBorderLightGray, 6);
            Rect inlined = MenuFix.Line(area, 1);
            Drawing.DrawRect(inlined, new Color32(32, 34, 55, 255), 6);
            Color lastColor = _MenuTabStyle.normal.textColor;
            int lastFontSize = _MenuTabStyle.fontSize;
            _MenuTabStyle.normal.textColor = _MenuTabStyle.onNormal.textColor;
            _MenuTabStyle.fontSize = 15; 
            GUILayout.BeginArea(inlined);
            {
                GUILayout.BeginHorizontal();
                {
                    GUILayout.FlexibleSpace(); 
                    _MenuTabStyle.normal.textColor = lastColor;
                    _MenuTabStyle.fontSize = lastFontSize;
                    GUILayout.FlexibleSpace();
                }
                GUILayout.EndHorizontal();
                GUILayout.Space(2);
                Rect rects;
                Rect inner;
                GUILayout.BeginHorizontal();
                {
                    scrollpos = GUILayout.BeginScrollView(scrollpos, false, true);
                    {
                        GUILayout.BeginHorizontal();
                        {
                            GUILayout.Space(padding);
                            GUILayout.BeginVertical(GUILayout.MinHeight(inlined.height));
                            {

                                try { code(); }
                                catch (Exception e) { Debug.LogException(e); }
                            }
                            GUILayout.EndVertical();
                            inner = GUILayoutUtility.GetLastRect();
                        }
                        GUILayout.EndHorizontal();
                    }
                    GUILayout.EndScrollView();
                    rects = GUILayoutUtility.GetLastRect();
                    GUILayout.Space(1);
                }
                GUILayout.EndHorizontal();
                GUILayout.Space(1);
                Drawing.DrawRect(new Rect(rects.x + rects.width - 16, rects.y, 16, rects.height), new Color32(32, 34, 55, 255), 6); 
            }
            GUILayout.EndArea();

        } 
        public static bool Button(string text, float w, float h = 25, params GUILayoutOption[] options)
        {
            List<GUILayoutOption> par = options.ToList();
            par.Add(GUILayout.Height(h));
            par.Add(GUILayout.Width(w));
            Rect area = GUILayoutUtility.GetRect(w, h, par.ToArray());
            Drawing.DrawRect(area, _OutlineBorderBlack, 2);
            return GUI.Button(MenuFix.Line(area), text, _ButtonStyle);
        } 
        public static KeyCode Bind(KeyCode key,string text,ref bool bind)
        {
            GUILayout.Space(5f);
            using (new GUILayout.HorizontalScope())
            {
                GUILayout.BeginVertical(GUILayout.Width(50)); 
                GUILayout.Label(text, _TextStyle1);
                GUILayout.EndVertical();
                KeyCode lastkey; 
                if (Button(!bind ? ((key == KeyCode.None) ? "<b>...</b>" : Convertor.KeyNames[key]) : "Set key", 70f, 25f))
                    bind = true;
                if (bind)
                {
                    lastkey = GetPressedKey();
                    if (lastkey != KeyCode.None)
                    {
                        bind = false;
                        key = lastkey;
                        lastkey = KeyCode.None;
                        return key;
                    }
                    if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
                    {
                        bind = false;
                        key = KeyCode.None;
                        return key;
                    }
                }
            }
            return key;
        }
        public static KeyCode GetPressedKey()
        {
            KeyCode[] array = (KeyCode[])Enum.GetValues(typeof(KeyCode));
            for (int i = 0; i < array.Length; i++)
                if (UnityEngine.Input.GetKeyDown(array[i]) && !UnityEngine.Input.GetKeyDown(KeyCode.Escape)) 
                    return array[i]; 
            return KeyCode.None;
        }
        public static int Tab(float x, float y, float w, float h, string[] text, int list,float WH = 137)
        {
            for (int i = 0; i < text.Length; i++)
            {

                Rect area = new Rect(x + (i * WH), y, w, 20);
                Drawing.DrawRect(area, _OutlineBorderBlack, 2);
                
                if (GUI.Button(MenuFix.Line(area), text[i], _ButtonStyle))
                {
                    list = i;
                    ColorPicker.GUIColorPicker.window = false;
                }
                if (list == i)
                Drawing.DrawRect(new Rect(x + (i * WH), y + 20, w, 2), new Color(0.67f, 0.45f, 0.9f, 1f),0); 
            }
            return list;
        }
        public static int MainTab(float x, float y, float w, float h, string[] text, int list, float WH = 137)
        {
            for (int i = 0; i < text.Length; i++)
            {

                Rect area = new Rect(x + (i * WH), y, w, h); 
                GUIStyle f = new GUIStyle();
                f.alignment = TextAnchor.MiddleCenter;
                f.font = LastDesirePro.Menu.MainMenu._TextFont;
                f.fontSize = 25;
                f.padding.left = f.padding.right = f.padding.top = f.padding.bottom = 4;

                f.normal.textColor = new Color32(210, 210, 210, 255);
                f.onNormal.textColor = new Color32(210, 210, 210, 255);
                f.hover.textColor = new Color32(150, 98, 239, 255);
                f.onHover.textColor = new Color32(150, 98, 239, 255);
                f.active.textColor = new Color32(100, 100, 100, 255);
                f.onActive.textColor = new Color32(100, 100, 100, 255);

                var btex = new Texture2D(1, 1);
                btex.SetPixel(0, 0, new Color32(0, 0, 0, 0));
                btex.Apply();
                f.hover.background = btex;
                var btex2 = new Texture2D(1, 1);
                btex2.SetPixel(0, 0, new Color32(0, 0, 0, 0));
                btex2.Apply();
                f.normal.background = btex2;
                var btex3 = new Texture2D(1, 1);
                btex3.SetPixel(0, 0, new Color32(0, 0, 0, 0));
                btex3.Apply();
                f.active.background = btex3;  


                GUIStyle f1 = new GUIStyle();
                f1.alignment = TextAnchor.MiddleCenter;
                f1.font = LastDesirePro.Menu.MainMenu._TextFont;
                f1.fontSize = 25;
                f1.padding.left = f1.padding.right = f1.padding.top = f1.padding.bottom = 4;

                f1.normal.textColor = new Color32(150, 98, 239, 255);
                f1.onNormal.textColor = new Color32(150, 98, 239, 255);
                f1.hover.textColor = new Color32(162, 105, 246, 255);
                f1.onHover.textColor = new Color32(162, 105, 246, 255);
                f1.active.textColor = new Color32(162, 105, 246, 255);
                f1.onActive.textColor = new Color32(162, 105, 246, 255);

                var btex1 = new Texture2D(1, 1);
                btex1.SetPixel(0, 0, new Color32(0, 0, 0, 0));
                btex1.Apply();
                f1.hover.background = btex1;
                var btex21 = new Texture2D(1, 1);
                btex21.SetPixel(0, 0, new Color32(0, 0, 0, 0));
                btex21.Apply();
                f1.normal.background = btex21;
                var btex31 = new Texture2D(1, 1);
                btex31.SetPixel(0, 0, new Color32(0, 0, 0, 0));
                btex31.Apply();
                f1.active.background = btex31; 
                if (GUI.Button(area, text[i], list == i ?f1 : f))
                {
                    list = i;
                    ColorPicker.GUIColorPicker.window = false;
                } 
                Drawing.DrawRect(new Rect( x + (i * WH), y + 30, w, 2), list == i ? new Color(0.67f, 0.45f, 0.9f,1f) : new Color(1, 1, 1, 0.5f),5f);

            }
            return list;
        } 
        public static string TextField(string text, string label, int width)
        {
            GUILayout.BeginHorizontal();
            {
                GUILayout.Label(label, _TextStyle1);
                int lastFontSize = _TextStyle1.fontSize;
                _TextStyle1.fontSize = 13;
                float height = _TextStyle1.CalcSize(new GUIContent("asdf")).y;
                Rect rect = GUILayoutUtility.GetRect(width, height);
                Rect Loutline = MenuFix.Line(rect);
                Drawing.DrawRect(new Rect(Loutline.x, Loutline.y + 4, Loutline.width, Loutline.height + 1), new Color(0.67f, 0.45f, 0.9f, 1f), 2); 
                text = GUI.TextField(new Rect(Loutline.x + 4, Loutline.y + 2, Loutline.width, Loutline.height), text, _TextStyle1);
                GUILayout.FlexibleSpace();
                _TextStyle1.fontSize = lastFontSize;
            }
            GUILayout.EndHorizontal();
            return text;
        }
        public static int TextField(int text, string label, int width, int min = 0000, int max = 9999)
        {
            GUILayout.BeginHorizontal();
            {
                GUILayout.Label(label, _TextStyle1);
                int lastFontSize = _TextStyle1.fontSize;
                _TextStyle1.fontSize = 13;
                float height = _TextStyle.CalcSize(new GUIContent("asdf")).y;
                Rect rect = GUILayoutUtility.GetRect(width, height);
                Rect Loutline = MenuFix.Line(rect);
                Drawing.DrawRect(new Rect(Loutline.x, Loutline.y + 4, Loutline.width, Loutline.height + 1), new Color(0.67f, 0.45f, 0.9f, 1f),2);
                try
                {
                    int value = int.Parse(digitsOnly.Replace(GUI.TextField(new Rect(rect.x, rect.y + 2, rect.width, rect.height), text.ToString(), _TextStyle1), ""));
                    if (value >= min && value <= max)
                        text = value;
                }
                catch { }
                GUILayout.FlexibleSpace();
                _TextStyle1.fontSize = lastFontSize;
            }
            GUILayout.EndHorizontal();
            return text;
        }
    }
}
