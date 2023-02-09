using LastDesirePro.Attributes;
using LastDesirePro.DrawMenu;
using LastDesirePro.Main.Visuals;
using System;
using System.Reflection;
using UnityEngine;
using static LastDesirePro.Menu.CFG.RadarConfig;

namespace LastDesirePro.Main.Radar_2D
{
    [Component]
    class Radar : MonoBehaviour
    {
        void OnGUI()
        {
            if (!DrawMenu.AssetsLoad.Loaded) return;
            if (_Enabled)
            {
                GUI.color = Color.white;
                _Vew.width = ( _Vew.height = _Size +160f);
				veww = GUI.Window(345, _Vew, RadarMenu, "","label" );
              _Vew.x = veww.x;
                _Vew.y = veww.y;
                GUI.color = Color.white;
            }
			if(_OOFIndicator || _OOFIndicatorNPC)
			ASS(); 
		}
		Vector2 screen_center = new Vector2((float)Screen.width / 2f, (float)Screen.height / 2f);
		void ASS()
        {
			try
			{
				if (LocalPlayer.Entity != null)
				{
					foreach (var player in BasePlayer.VisiblePlayerList)
					{
						if (player.health > 0f && !player.IsSleeping() && !player.IsLocalPlayer())
						{
							var cameradistance = (int)Vector3.Distance(MainCamera.mainCamera.transform.position, player.transform.position);
							var centerPos = MainCamera.mainCamera.transform.position;
							var bonePos = player.FindBone("head").transform.position;
							var fov = 150f;
							var math0 = centerPos.x - bonePos.x;
							var math1 = centerPos.z - bonePos.z;
							var math2 = Mathf.Atan2(math0, math1) * Mathf.Rad2Deg - 270 - MainCamera.mainCamera.transform.eulerAngles.y;
							var mart3 = fov * Mathf.Cos(math2 * Mathf.Deg2Rad);
							var math4 = fov * Mathf.Sin(math2 * Mathf.Deg2Rad);
							if (fov <= 500)
							{
								if (_OOFIndicator && player.userID > 111111111111u && player != null && !player.IsLocalPlayer() && !player.IsSleeping() && !player.IsDead() && !DrawOnlyPlayers.IsInScreen(player.model.rootBone.transform.position))
								{
									Visuals.FullDrawing.String(new Vector2(Screen.width / 2 + mart3 - 4f, Screen.height / 2 + math4 - 4f), "⁉", Menu.CFG.AimBotConfig._friendList.Contains(player.userID) ? new Color32(0, 255, 0, 255) : _colorOOFIndicator, true, 20);
									Visuals.FullDrawing.String(new Vector2(Screen.width / 2 + mart3 - 4f, Screen.height / 2 + math4 + 16f), $"{cameradistance} m.", Color.white, true, 12);
								}
								if (_OOFIndicatorNPC && player.userID < 111111111111u && !DrawOnlyPlayers.IsInScreen(player.model.rootBone.transform.position))
								{
									Visuals.FullDrawing.String(new Vector2(Screen.width / 2 + mart3 - 4f, Screen.height / 2 + math4 - 4f), "⁉", _colorOOFIndicatorNPC, true, 20);
									Visuals.FullDrawing.String(new Vector2(Screen.width / 2 + mart3 - 4f, Screen.height / 2 + math4 + 16f), $"{cameradistance} m.", Color.white, true, 12);
								}
							}
						}
					}
				}
			}
			catch { }
		} 
		void RadarMenu(int windowID)
        {
			try
			{
				Vector2 realradаrcenter = new Vector2(_Vew.width / 2f, (_Vew.height) / 2f);
				radarcenter = new Vector2(_Vew.width / 2f, (_Vew.height) / 2f);
				if (LocalPlayer.Entity != null)
				{
					Vector2 localpos = GameToRadarPosition(LocalPlayer.Entity.transform.position);
					if (_Type == 0 || _Type == 1)
					{
						radarcenter.x = radarcenter.x - localpos.x;
						radarcenter.y = radarcenter.y + localpos.y;
					}
				}
				DrawMenu.Drawing.DrawRect(new Rect(realradаrcenter.x, 0, 1f, _Vew.height), Color.black, 6f);
				DrawMenu.Drawing.DrawRect(new Rect(0, realradаrcenter.y, _Vew.width, 1f), Color.black, 6f);
				Rect outline = new Rect(0, 0, _Vew.width, _Vew.height);
				Rect Loutline = MenuFix.Line(outline);
				Drawing.DrawRects(outline, new Color32(233, 233, 233, 255), _Radius);
				Drawing.DrawRect(Loutline, new Color32(28, 28, 45, 122), _Radius);
				Vector2 top = new Vector2(realradаrcenter.x, realradаrcenter.y - 7);
				Vector2 left = new Vector2(realradаrcenter.x + 3, realradаrcenter.y + 3);
				Vector2 right = new Vector2(realradаrcenter.x - 3, realradаrcenter.y + 3);
				if (_Type == 0)
				{
					if (LocalPlayer.Entity != null)
					{
						top = RotatePoint(top, realradаrcenter, Math.Round((double)LocalPlayer.Entity.transform.eulerAngles.y, 2));
						left = RotatePoint(left, realradаrcenter, Math.Round((double)LocalPlayer.Entity.transform.eulerAngles.y, 2));
						right = RotatePoint(right, realradаrcenter, Math.Round((double)LocalPlayer.Entity.transform.eulerAngles.y, 2));
					}
					Visuals.FullDrawing.Line(top, left, Color.white, 1.2f, true);
					Visuals.FullDrawing.Line(left, right, Color.white, 1.2f, true);
					Visuals.FullDrawing.Line(right, top, Color.white, 1.2f, true);
				}

				if (_Players)
				{
					foreach (BasePlayer player in BasePlayer.VisiblePlayerList)
					{
						if (player != null && !player.IsLocalPlayer() && !player.IsSleeping() && !player.IsDead())
						{
							Vector2 radаrpos = GameToRadarPosition(player.transform.position);
							Vector2 rpos = new Vector2(radarcenter.x + radаrpos.x, radarcenter.y - radаrpos.y);
							if (player.userID > 111111111111u)
							{
								if (_Type == 0)
								{
									GUI.color = Color.white;
									Vector2 t = new Vector2(rpos.x, rpos.y - 7f);
									Vector2 i = new Vector2(rpos.x + 3f, rpos.y + 3f);
									Vector2 r = new Vector2(rpos.x - 3f, rpos.y + 3f);
									t = RotatePoint(t, rpos, Math.Round((double)player.model.eyeBone.transform.eulerAngles.y, 2));
									i = RotatePoint(i, rpos, Math.Round((double)player.model.eyeBone.transform.eulerAngles.y, 2));
									r = RotatePoint(r, rpos, Math.Round((double)player.model.eyeBone.transform.eulerAngles.y, 2));
									Visuals.FullDrawing.Line(t, i, _colorPlayers, 1f, true);
									Visuals.FullDrawing.Line(i, r, _colorPlayers, 1f, true);
									Visuals.FullDrawing.Line(r, t, _colorPlayers, 1f, true);
									GUI.color = Color.white;
								}
								if (_Type == 1)
								{
									GUI.color = Color.white;
									DrawRadarDot(rpos, Color.grey, 3f);
									DrawRadarDot(rpos, Menu.CFG.AimBotConfig._friendList.Contains(player.userID) ? new Color32(0, 255, 0, 255) : _colorPlayers, 2f);
									GUI.color = Color.white;
								}
							}
							else if (player.userID < 111111111111u && _Npc)
							{
								if (_Type == 0)
								{
									GUI.color = Color.white;
									Vector2 t = new Vector2(rpos.x, rpos.y - 7f);
									Vector2 i = new Vector2(rpos.x + 3f, rpos.y + 3f);
									Vector2 r = new Vector2(rpos.x - 3f, rpos.y + 3f);
									t = RotatePoint(t, rpos, Math.Round((double)player.model.eyeBone.transform.eulerAngles.y, 2));
									i = RotatePoint(i, rpos, Math.Round((double)player.model.eyeBone.transform.eulerAngles.y, 2));
									r = RotatePoint(r, rpos, Math.Round((double)player.model.eyeBone.transform.eulerAngles.y, 2));
									Visuals.FullDrawing.Line(t, i, _colorNPC, 1f, true);
									Visuals.FullDrawing.Line(i, r, _colorNPC, 1f, true);
									Visuals.FullDrawing.Line(r, t, _colorNPC, 1f, true);
									GUI.color = Color.white;
								}
								if (_Type == 1)
								{
									GUI.color = Color.white;
									DrawRadarDot(rpos, Color.grey, 3f);
									DrawRadarDot(rpos, _colorNPC, 2f);
									GUI.color = Color.white;
								}
							}
						}
					}
					foreach (var a in BaseNpc.VisibleNpcList)
					{
						if (a != null && a.IsValid())
						{
							Vector2 radаrpos = GameToRadarPosition(a.transform.position);
							Vector2 rpos = new Vector2(radarcenter.x + radаrpos.x, radarcenter.y - radаrpos.y);
							if (a.name.Contains("stag") && _animals[0])
							{
								GUI.color = Color.white;
								DrawRadarDot(rpos, Color.grey, 3f);
								DrawRadarDot(rpos, _colorAnimals[0], 2f);
								GUI.color = Color.white;
							}
							if (a.name.Contains("wolf") && _animals[1])
							{
								GUI.color = Color.white;
								DrawRadarDot(rpos, Color.grey, 3f);
								DrawRadarDot(rpos, _colorAnimals[1], 2f);
								GUI.color = Color.white;
							}
							if (a.name.Contains("horse") && _animals[2])
							{
								GUI.color = Color.white;
								DrawRadarDot(rpos, Color.grey, 3f);
								DrawRadarDot(rpos, _colorAnimals[2], 2f);
								GUI.color = Color.white;
							}
							if (a.name.Contains("chicken") && _animals[3])
							{
								GUI.color = Color.white;
								DrawRadarDot(rpos, Color.grey, 3f);
								DrawRadarDot(rpos, _colorAnimals[3], 2f);
								GUI.color = Color.white;
							}
							if (a.name.Contains("bear") && _animals[4])
							{
								GUI.color = Color.white;
								DrawRadarDot(rpos, Color.grey, 3f);
								DrawRadarDot(rpos, _colorAnimals[4], 2f);
								GUI.color = Color.white;
							}
							if (a.name.Contains("boar") && _animals[5])
							{
								GUI.color = Color.white;
								DrawRadarDot(rpos, Color.grey, 3f);
								DrawRadarDot(rpos, _colorAnimals[5], 2f);
								GUI.color = Color.white;
							}
						}
					}
				}
			}
			catch { }
			GUI.DragWindow();
		}
        void DrawRadarDot(Vector2 pos, Color color, float size = 2) => Drawing.DrawRect(new Rect(pos.x - size, pos.y - size, size * 2, size * 2), color, 6f);
        public Vector2 GameToRadarPosition(Vector3 pos)
        {
            Vector2 endpos;
            endpos.x = pos.x / (_Zoom * 2f / (1f * _Size+150f));
            endpos.y = pos.z / (_Zoom * 2f / (1f * _Size + 150f));
            if (_Type == 1)
            {
                Vector2 newpoints = RotatePoint(endpos, new Vector2((_Vew.width) / 2, (_Vew.height) / 2), Math.Round(MainCamera.mainCamera.transform.eulerAngles.y, 2));
                return newpoints;
            }
            return endpos;
        }
        public static Vector2 RotatePoint(Vector2 pointToRotate, Vector2 centerPoint, double angleInDegrees)
        {
            double angleInRadians = angleInDegrees * (Math.PI / 180);
            double cosTheta = Math.Cos(angleInRadians);
            double sinTheta = Math.Sin(angleInRadians);
            return new Vector2((int)(cosTheta * (pointToRotate.x - centerPoint.x) - sinTheta * (pointToRotate.y - centerPoint.y) + centerPoint.x), (int)(sinTheta * (pointToRotate.x - centerPoint.x) + cosTheta * (pointToRotate.y - centerPoint.y) + centerPoint.y));
		}
		public static Rect veww;
		public static Vector2 radarcenter; 
	}
}
