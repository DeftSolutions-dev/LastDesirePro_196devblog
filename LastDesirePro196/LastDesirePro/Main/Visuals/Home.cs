using LastDesirePro.Attributes;
using LastDesirePro.DrawMenu;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static LastDesirePro.Menu.CFG.VisuаlCоnfig;
using static LastDesirePro.Main.Objects.ObjectsCheck;
using static LastDesirePro.DrawMenu.Prefab;

namespace LastDesirePro.Main.Visuals
{
    [Component]
    class Home : MonoBehaviour
    {
		void OnGUI() {  
			if (!DrawMenu.AssetsLoad.Loaded) return;
			if (_cupboard[1]) DrawWindow();
			if (_autoturret[1]) DrawWindows();
			GetHome();
		}
		void GetHome()
        {
			try
			{
				if (_StorageContainer.Count > 0)
				{
					foreach (var t in _StorageContainer.Values)
					{
						var vector = MainCamera.mainCamera.WorldToScreenPoint(t.position);
						if (vector.z > 0f && Main.Visuals.FullDrawing.IsInScreen(t.position))
						{
							var resource_distance = (int)Vector3.Distance(MainCamera.mainCamera.transform.position, t.position);
							if (resource_distance <= _containerDist)
							{

								vector.x += 3f; vector.y = Screen.height - (vector.y + 1f);
								if (t.name.Contains("woodbox") && _container[0])
								{
									if (_container[15])
										Main.Visuals.FullDrawing.Health(new Vector2(vector.x, vector.y - 11f), t.health, 150, 30, 7, 1f);
									FullDrawing.String(new Vector2(vector.x, vector.y - 12), string.Format("{0}", "Small box"), _colorContainer[0], true, 10, FontStyle.Bold, 2, _container[16], _colorFonContainer, _radiusFonContainer);
									FullDrawing.String(new Vector2(vector.x, vector.y), string.Format("{0} m.", resource_distance), _colorContainer[0], true, 10, FontStyle.Bold, 2, _container[16], _colorFonContainer, _radiusFonContainer);
								}
								if (t.name.Contains("box.wooden.") && _container[1])
								{
									if (_container[15])
										Main.Visuals.FullDrawing.Health(new Vector2(vector.x, vector.y - 11f), t.health, 300, 30, 7, 1f);
									FullDrawing.String(new Vector2(vector.x, vector.y - 12), string.Format("{0}", "Big box"), _colorContainer[1], true, 10, FontStyle.Bold, 2, _container[16], _colorFonContainer, _radiusFonContainer);
									FullDrawing.String(new Vector2(vector.x, vector.y), string.Format("{0} m.", resource_distance), _colorContainer[1], true, 10, FontStyle.Bold, 2, _container[16], _colorFonContainer, _radiusFonContainer);
								}
								if (t.entity.ShortPrefabName.Contains("refinery_small") && _container[2])
								{
									if (_container[15])
										Main.Visuals.FullDrawing.Health(new Vector2(vector.x, vector.y - 11f), t.health, 1500, 30, 7, 1f);
									FullDrawing.String(new Vector2(vector.x, vector.y - 12), string.Format("{0}", "Small Oil Refinery"), _colorContainer[2], true, 10, FontStyle.Bold, 2, _container[16], _colorFonContainer, _radiusFonContainer);
									FullDrawing.String(new Vector2(vector.x, vector.y), string.Format("{0} m.", resource_distance), _colorContainer[2], true, 10, FontStyle.Bold, 2, _container[16], _colorFonContainer, _radiusFonContainer);
								}
								if (t.name.Contains("bbq.") && _container[3])
								{
									if (_container[15])
										Main.Visuals.FullDrawing.Health(new Vector2(vector.x, vector.y - 11f), t.health, 200, 30, 7, 1f);
									FullDrawing.String(new Vector2(vector.x, vector.y - 12), string.Format("{0}", "BBQ"), _colorContainer[3], true, 10, FontStyle.Bold, 2, _container[16], _colorFonContainer, _radiusFonContainer);
									FullDrawing.String(new Vector2(vector.x, vector.y), string.Format("{0} m.", resource_distance), _colorContainer[3], true, 10, FontStyle.Bold, 2, _container[16], _colorFonContainer, _radiusFonContainer);
								}
								if ((t.name.Contains("furnace.prefab") || t.name.Contains("furnace._deployed")) && _container[4])
								{
									if (_container[15])
										Main.Visuals.FullDrawing.Health(new Vector2(vector.x, vector.y - 11f), t.health, 500, 30, 7, 1f);
									FullDrawing.String(new Vector2(vector.x, vector.y - 12), string.Format("{0}", "Furnace"), _colorContainer[4], true, 10, FontStyle.Bold, 2, _container[16], _colorFonContainer, _radiusFonContainer);
									FullDrawing.String(new Vector2(vector.x, vector.y), string.Format("{0} m.", resource_distance), _colorContainer[4], true, 10, FontStyle.Bold, 2, _container[16], _colorFonContainer, _radiusFonContainer);
								}
								if (t.entity.ShortPrefabName.Contains("repairbench") && _container[5])
								{
									if (_container[15])
										Main.Visuals.FullDrawing.Health(new Vector2(vector.x, vector.y - 11f), t.health, 200, 30, 7, 1f);
									FullDrawing.String(new Vector2(vector.x, vector.y - 12), string.Format("{0}", "Repair bench"), _colorContainer[5], true, 10, FontStyle.Bold, 2, _container[16], _colorFonContainer, _radiusFonContainer);
									FullDrawing.String(new Vector2(vector.x, vector.y), string.Format("{0} m.", resource_distance), _colorContainer[5], true, 10, FontStyle.Bold, 2, _container[16], _colorFonContainer, _radiusFonContainer);
								}
								if (t.name.Contains("furnace.large.") && _container[6])
								{
									if (_container[15])
										Main.Visuals.FullDrawing.Health(new Vector2(vector.x, vector.y - 11f), t.health, 1500, 30, 7, 1f);
									FullDrawing.String(new Vector2(vector.x, vector.y - 12), string.Format("{0}", "Furnace large"), _colorContainer[6], true, 10, FontStyle.Bold, 2, _container[16], _colorFonContainer, _radiusFonContainer);
									FullDrawing.String(new Vector2(vector.x, vector.y), string.Format("{0} m.", resource_distance), _colorContainer[6], true, 10, FontStyle.Bold, 2, _container[16], _colorFonContainer, _radiusFonContainer);
								}
								if (t.name.Contains("dropbox.deployed") && _container[7])
								{
									if (_container[15])
										Main.Visuals.FullDrawing.Health(new Vector2(vector.x, vector.y - 11f), t.health, 100, 30, 7, 1f);
									FullDrawing.String(new Vector2(vector.x, vector.y - 12), string.Format("{0}", "Dropbox"), _colorContainer[7], true, 10, FontStyle.Bold, 2, _container[16], _colorFonContainer, _radiusFonContainer);
									FullDrawing.String(new Vector2(vector.x, vector.y), string.Format("{0} m.", resource_distance), _colorContainer[7], true, 10, FontStyle.Bold, 2, _container[16], _colorFonContainer, _radiusFonContainer);
								}
								if (t.name.Contains("stocking_small_deployed") && _container[8])
								{
									if (_container[15])
										Main.Visuals.FullDrawing.Health(new Vector2(vector.x, vector.y - 11f), t.health, 100, 30, 7, 1f);
									FullDrawing.String(new Vector2(vector.x, vector.y - 12), string.Format("{0}", "Stocking small"), _colorContainer[8], true, 10, FontStyle.Bold, 2, _container[16], _colorFonContainer, _radiusFonContainer);
									FullDrawing.String(new Vector2(vector.x, vector.y), string.Format("{0} m.", resource_distance), _colorContainer[8], true, 10, FontStyle.Bold, 2, _container[16], _colorFonContainer, _radiusFonContainer);
								}
								if (t.name.Contains("researchtable_deployed") && _container[9])
								{
									if (_container[15])
										Main.Visuals.FullDrawing.Health(new Vector2(vector.x, vector.y - 11f), t.health, 200, 30, 7, 1f);
									FullDrawing.String(new Vector2(vector.x, vector.y - 12), string.Format("{0}", "Research table"), _colorContainer[9], true, 10, FontStyle.Bold, 2, _container[16], _colorFonContainer, _radiusFonContainer);
									FullDrawing.String(new Vector2(vector.x, vector.y), string.Format("{0} m.", resource_distance), _colorContainer[9], true, 10, FontStyle.Bold, 2, _container[16], _colorFonContainer, _radiusFonContainer);
								}
								if (t.name.Contains("fireplace.deployed") && _container[10])
								{
									if (_container[15])
										Main.Visuals.FullDrawing.Health(new Vector2(vector.x, vector.y - 11f), t.health, 500, 30, 7, 1f);
									FullDrawing.String(new Vector2(vector.x, vector.y - 12), string.Format("{0}", "Fireplace"), _colorContainer[10], true, 10, FontStyle.Bold, 2, _container[16], _colorFonContainer, _radiusFonContainer);
									FullDrawing.String(new Vector2(vector.x, vector.y), string.Format("{0} m.", resource_distance), _colorContainer[10], true, 10, FontStyle.Bold, 2, _container[16], _colorFonContainer, _radiusFonContainer);
								}
								if (t.name.Contains("workbench1.deployed") && _container[11])
								{
									if (_container[15])
										Main.Visuals.FullDrawing.Health(new Vector2(vector.x, vector.y - 11f), t.health, 500, 30, 7, 1f);
									FullDrawing.String(new Vector2(vector.x, vector.y - 12), string.Format("{0}", "Workbench [1lvl]"), _colorContainer[11], true, 10, FontStyle.Bold, 2, _container[16], _colorFonContainer, _radiusFonContainer);
									FullDrawing.String(new Vector2(vector.x, vector.y), string.Format("{0} m.", resource_distance), _colorContainer[11], true, 10, FontStyle.Bold, 2, _container[16], _colorFonContainer, _radiusFonContainer);
								}
								if (t.name.Contains("workbench2.deployed") && _container[12])
								{
									if (_container[15])
										Main.Visuals.FullDrawing.Health(new Vector2(vector.x, vector.y - 11f), t.health, 750, 30, 7, 1f);
									FullDrawing.String(new Vector2(vector.x, vector.y - 12), string.Format("{0}", "Workbench [2lvl]"), _colorContainer[12], true, 10, FontStyle.Bold, 2, _container[16], _colorFonContainer, _radiusFonContainer);
									FullDrawing.String(new Vector2(vector.x, vector.y), string.Format("{0} m.", resource_distance), _colorContainer[12], true, 10, FontStyle.Bold, 2, _container[16], _colorFonContainer, _radiusFonContainer);
								}
								if (t.name.Contains("workbench3.deployed") && _container[13])
								{
									if (_container[15])
										Main.Visuals.FullDrawing.Health(new Vector2(vector.x, vector.y - 11f), t.health, 1000, 30, 7, 1f);
									FullDrawing.String(new Vector2(vector.x, vector.y - 12), string.Format("{0}", "Workbench [3lvl]"), _colorContainer[13], true, 10, FontStyle.Bold, 2, _container[16], _colorFonContainer, _radiusFonContainer);
									FullDrawing.String(new Vector2(vector.x, vector.y), string.Format("{0} m.", resource_distance), _colorContainer[13], true, 10, FontStyle.Bold, 2, _container[16], _colorFonContainer, _radiusFonContainer);
								}
								if (t.name.Contains("stocking_large_deployed") && _container[14])
								{
									if (_container[15])
										Main.Visuals.FullDrawing.Health(new Vector2(vector.x, vector.y - 11f), t.health, 100, 30, 7, 1f);
									FullDrawing.String(new Vector2(vector.x, vector.y - 12), string.Format("{0}", "Stocking large"), _colorContainer[14], true, 10, FontStyle.Bold, 2, _container[16], _colorFonContainer, _radiusFonContainer);
									FullDrawing.String(new Vector2(vector.x, vector.y), string.Format("{0} m.", resource_distance), _colorContainer[14], true, 10, FontStyle.Bold, 2, _container[16], _colorFonContainer, _radiusFonContainer);
								}
							}
						}
					}
				}
				if (_Cupboard.Count > 0)
				{
					foreach (var t in _Cupboard.Values)
					{
						var vector = MainCamera.mainCamera.WorldToScreenPoint(t.position);
						if (vector.z > 0f && Main.Visuals.FullDrawing.IsInScreen(t.position))
						{
							var resource_distance = (int)Vector3.Distance(MainCamera.mainCamera.transform.position, t.position);
							if (resource_distance <= _cupboardDist)
							{
								vector.x += 3f; vector.y = Screen.height - (vector.y + 1f);
								if (_cupboard[0])
								{
									if (_cupboard[2])
										FullDrawing.Health(new Vector2(vector.x, vector.y - 11f), t.health, 100, 30, 7, 1f);
									FullDrawing.String(new Vector2(vector.x, vector.y - 12), string.Format("{0}", "Cupboard"), _colorCupboard, true, 10, FontStyle.Bold, 2, _cupboard[3], _colorFonCupboard, _radiusFonCupboard);
									FullDrawing.String(new Vector2(vector.x, vector.y), string.Format("{0} m.", resource_distance), _colorCupboard, true, 10, FontStyle.Bold, 2, _cupboard[3], _colorFonCupboard, _radiusFonCupboard);
								}

							}
						}
					}
				}
				if (_AutoTurret.Count > 0)
				{
					foreach (var t in _AutoTurret.Values)
					{
						var vector = MainCamera.mainCamera.WorldToScreenPoint(t.position);
						if (vector.z > 0f && Main.Visuals.FullDrawing.IsInScreen(t.position))
						{
							var autoturret_distance = (int)Vector3.Distance(MainCamera.mainCamera.transform.position, t.position);
							if (autoturret_distance <= _autoturretDist)
							{
								vector.x += 3f; vector.y = Screen.height - (vector.y + 1f);
								if (_autoturret[0])
								{
									if (_autoturret[2])
										FullDrawing.Health(new Vector2(vector.x, vector.y - 11f), t.health, 1000, 30, 7, 1f);
									FullDrawing.String(new Vector2(vector.x, vector.y - 12), string.Format("{0}", "Auto Turret"), _colorAutoTurret, true, 10, FontStyle.Bold, 2, _autoturret[3], _colorFonAutoTurret, _radiusFonAutoTurret);
									FullDrawing.String(new Vector2(vector.x, vector.y), string.Format("{0} m.", autoturret_distance), _colorAutoTurret, true, 10, FontStyle.Bold, 2, _autoturret[3], _colorFonAutoTurret, _radiusFonAutoTurret);
								}

							}
						}
					}
				}
				if (_StashContainer.Count > 0)
				{
					foreach (var t in _StashContainer.Values)
					{
						var vector = MainCamera.mainCamera.WorldToScreenPoint(t.position);
						if (vector.z > 0f && Main.Visuals.FullDrawing.IsInScreen(t.position))
						{
							var _distance = (int)Vector3.Distance(MainCamera.mainCamera.transform.position, t.position);
							if (_distance <= _stashDist)
							{
								vector.x += 3f;
								vector.y = UnityEngine.Screen.height - (vector.y + 1f);

								if (t.IsHidden)
								{
									if (_stash[0])
									{
										if (_stash[1])
											Main.Visuals.FullDrawing.Health(new Vector2(vector.x, vector.y - 22), t.health, 150, 30, 7, 1f);
										FullDrawing.String(new Vector2(vector.x, vector.y - 24), string.Format("{0}", "Stash"), _colorStash, true, 10, FontStyle.Bold, 2, _stash[2], _colorFonStash, _radiusFonStash);
										FullDrawing.String(new Vector2(vector.x, vector.y - 12), string.Format("{0}", "<b>Closed</b>"), Color.red, true, 10, FontStyle.Bold, 2, _stash[2], _colorFonStash, _radiusFonStash);
										FullDrawing.String(new Vector2(vector.x, vector.y), string.Format("{0} m.", _distance), _colorStash, true, 10, FontStyle.Bold, 2, _stash[2], _colorFonStash, _radiusFonStash);
									}
								}
								if (!t.IsHidden)
								{
									if (_stash[0])
									{
										if (_stash[1])
											Main.Visuals.FullDrawing.Health(new Vector2(vector.x, vector.y - 22), t.health, 150, 30, 7, 1f);
										FullDrawing.String(new Vector2(vector.x, vector.y - 24), string.Format("{0}", "Stash"), _colorStash, true, 10, FontStyle.Bold, 2, _stash[2], _colorFonStash, _radiusFonStash);
										FullDrawing.String(new Vector2(vector.x, vector.y - 12), string.Format("{0}", "<b>Open</b>"), Color.green, true, 10, FontStyle.Bold, 2, _stash[2], _colorFonStash, _radiusFonStash);
										FullDrawing.String(new Vector2(vector.x, vector.y), string.Format("{0} m.", _distance), _colorStash, true, 10, FontStyle.Bold, 2, _stash[2], _colorFonStash, _radiusFonStash);
									}
								}
							}
						}
					}
				}
				if (_Landmine.Count > 0)
				{
					foreach (var t in _Landmine.Values)
					{
						var vector = MainCamera.mainCamera.WorldToScreenPoint(t.position);
						if (vector.z > 0f && Main.Visuals.FullDrawing.IsInScreen(t.position))
						{
							var _distance = (int)Vector3.Distance(MainCamera.mainCamera.transform.position, t.position);
							if (_distance <= _trapDist)
							{
								vector.x += 3f;
								vector.y = UnityEngine.Screen.height - (vector.y + 1f);

								if (_trap[0])
								{
									if (_trap[4])
										FullDrawing.Health(new Vector2(vector.x, vector.y - 10), t.health, 100, 30, 7, 1f);
									FullDrawing.String(new Vector2(vector.x, vector.y - 12), string.Format("{0}", "Mine"), _colorTrap[0], true, 10, FontStyle.Bold, 2, _trap[5], _colorFonTrap, _radiusFonTrap);
									FullDrawing.String(new Vector2(vector.x, vector.y), string.Format("{0} m.", _distance), _colorTrap[0], true, 10, FontStyle.Bold, 2, _trap[5], _colorFonTrap, _radiusFonTrap);
								}
							}
						}
					}
				}
				if (_BearTrap.Count > 0)
				{
					foreach (var t in _BearTrap.Values)
					{
						var vector = MainCamera.mainCamera.WorldToScreenPoint(t.position);
						if (vector.z > 0f && Main.Visuals.FullDrawing.IsInScreen(t.position))
						{
							var _distance = (int)Vector3.Distance(MainCamera.mainCamera.transform.position, t.position);
							if (_distance <= _trapDist)
							{
								vector.x += 3f;
								vector.y = UnityEngine.Screen.height - (vector.y + 1f);

								if (_trap[1])
								{
									if (_trap[4])
										FullDrawing.Health(new Vector2(vector.x, vector.y - 10), t.health, 200, 30, 7, 1f);
									FullDrawing.String(new Vector2(vector.x, vector.y - 12), string.Format("{0}", "Trap"), _colorTrap[1], true, 10, FontStyle.Bold, 2, _trap[5], _colorFonTrap, _radiusFonTrap);
									FullDrawing.String(new Vector2(vector.x, vector.y), string.Format("{0} m.", _distance), _colorTrap[1], true, 10, FontStyle.Bold, 2, _trap[5], _colorFonTrap, _radiusFonTrap);
								}
							}
						}
					}
				}
				if (_FlameTurret.Count > 0)
				{
					foreach (var t in _FlameTurret.Values)
					{
						var vector = MainCamera.mainCamera.WorldToScreenPoint(t.position);
						if (vector.z > 0f && Main.Visuals.FullDrawing.IsInScreen(t.position))
						{
							var _distance = (int)Vector3.Distance(MainCamera.mainCamera.transform.position, t.position);
							if (_distance <= _trapDist)
							{
								vector.x += 3f;
								vector.y = UnityEngine.Screen.height - (vector.y + 1f);

								if (_trap[2])
								{
									if (_trap[4])
										FullDrawing.Health(new Vector2(vector.x, vector.y - 10), t.health, 300, 30, 7, 1f);
									FullDrawing.String(new Vector2(vector.x, vector.y - 12), string.Format("{0}", "Flame Turret"), _colorTrap[2], true, 10, FontStyle.Bold, 2, _trap[5], _colorFonTrap, _radiusFonTrap);
									FullDrawing.String(new Vector2(vector.x, vector.y), string.Format("{0} m.", _distance), _colorTrap[2], true, 10, FontStyle.Bold, 2, _trap[5], _colorFonTrap, _radiusFonTrap);
								}
							}
						}
					}
				}
				if (_GunTrap.Count > 0)
				{
					foreach (var t in _GunTrap.Values)
					{
						var vector = MainCamera.mainCamera.WorldToScreenPoint(t.position);
						if (vector.z > 0f && Main.Visuals.FullDrawing.IsInScreen(t.position))
						{
							var _distance = (int)Vector3.Distance(MainCamera.mainCamera.transform.position, t.position);
							if (_distance <= _trapDist)
							{
								vector.x += 3f;
								vector.y = UnityEngine.Screen.height - (vector.y + 1f);

								if (_trap[3])
								{
									if (_trap[4])
										FullDrawing.Health(new Vector2(vector.x, vector.y - 10), t.health, 300, 30, 7, 1f);
									FullDrawing.String(new Vector2(vector.x, vector.y - 12), string.Format("{0}", "Gun Trap"), _colorTrap[3], true, 10, FontStyle.Bold, 2, _trap[5], _colorFonTrap, _radiusFonTrap);
									FullDrawing.String(new Vector2(vector.x, vector.y), string.Format("{0} m.", _distance), _colorTrap[3], true, 10, FontStyle.Bold, 2, _trap[5], _colorFonTrap, _radiusFonTrap);
								}
							}
						}
					}
				}
				foreach (var aa in BaseEntity.clientEntities)
				{
					var t = aa as DroppedItem;
					if (t != null && t.IsValid())
					{
						var vector = MainCamera.mainCamera.WorldToScreenPoint(t.transform.position);
						if (vector.z > 0f && Main.Visuals.FullDrawing.IsInScreen(t.transform.position))
						{
							var _distance = (int)Vector3.Distance(MainCamera.mainCamera.transform.position, t.transform.position);
							if (_distance <= _droppedItemDist)
							{
								vector.x += 3f;
								vector.y = UnityEngine.Screen.height - (vector.y + 1f);
								if (_droppedItem[0])
								{
									FullDrawing.String(new Vector2(vector.x, vector.y - 10f), string.Format("{0}", t.item.info.displayName.english), _colorDroppedItem, true, 9, FontStyle.Bold, 2, _droppedItem[3], _colorFonDroppedItem, _radiusFonDroppedItem);
									FullDrawing.String(new Vector2(vector.x, vector.y), string.Format("{0} m.", _distance), _colorDroppedItem, true, 9, FontStyle.Bold, 2, _droppedItem[3], _colorFonDroppedItem, _radiusFonDroppedItem);
									if (_droppedItem[1])
										FullDrawing.String(new Vector2(vector.x, vector.y + (_droppedItem[2] && (t.item.maxCondition != 0) ? 20f : 10f)), string.Format("{0}", "-Ammout: " + (int)t.item.amount + "x"), _colorDroppedItem, true, 9, FontStyle.Bold, 2, _droppedItem[3], _colorFonDroppedItem, _radiusFonDroppedItem);
									if (_droppedItem[2])
									{
										if ((int)t.item.condition != 0 && (int)t.item.maxCondition != 0)
											FullDrawing.String(new Vector2(vector.x, vector.y + 10f), ("-Condition: " + (int)t.item.condition + "/" + (int)t.item.maxCondition), _colorDroppedItem, true, 9, FontStyle.Bold, 2, _droppedItem[3], _colorFonDroppedItem, _radiusFonDroppedItem);
										if ((int)t.item.condition == 0 && (int)t.item.maxCondition != 0)
											FullDrawing.String(new Vector2(vector.x, vector.y + 10f), "-Condition: broken!", Color.red, true, 9, FontStyle.Bold, 2, _droppedItem[3], _colorFonDroppedItem, _radiusFonDroppedItem);
									}
								}

							}
						}
					}
				}
				/*foreach (var aa in BaseEntity.clientEntities)
				{
					var t = aa as SleepingBag;
					if (t != null)
					{
						var vector = MainCamera.mainCamera.WorldToScreenPoint(t.transform.position);
						if (vector.z > 0f && Main.Visuals.FullDrawing.IsInScreen(t.transform.position))
						{
							var _distance = (int)Vector3.Distance(MainCamera.mainCamera.transform.position, t.transform.position);
							if (_distance <= _droppedItemDist && _players[0])
							{
								vector.x += 3f;
								vector.y = UnityEngine.Screen.height - (vector.y + 1f);
								FullDrawing.String(new Vector2(vector.x, vector.y - 10f), string.Format("{0}", t.deployerUserID), _colorDroppedItem, true, 9, FontStyle.Bold);
								FullDrawing.String(new Vector2(vector.x, vector.y), string.Format("{0}", t.niceName), _colorDroppedItem, true, 9, FontStyle.Bold);
								FullDrawing.String(new Vector2(vector.x, vector.y + 10f), string.Format("{0} m", _distance), _colorDroppedItem, true, 9, FontStyle.Bold); 
							}
						}
					}
				}*/ //Спальники
			}
			catch { }
		}
		private void Update()
		{
			try
			{
				if (LocalPlayer.Entity != null)
				{
					if (AssetsLoad._Rainbow != null)
					{
						foreach (var aa in BaseEntity.clientEntities)
						{
							var t = aa as DroppedItem;
							if (t != null && t.gameObject != null && t.IsValid())
							{
								var smm = t.GetComponentsInChildren<SkinnedMeshRenderer>();
								for (int j = 0; j < smm.Length; j++)
								{
									foreach (var r in smm[j].materials)
									{
										if ((t.name.Contains("ammo.") || t.name.Contains("rifle.") || t.name.Contains("bow.") || t.name.Contains("crossbow") || t.name.Contains("fishingrod.") ||
											   t.name.Contains("flamethrower") || t.name.Contains("knife.") || t.name.Contains("lmg.") || t.name.Contains("pistol.") ||
											   t.name.Contains("rocket.") || t.name.Contains("salvaged.") || t.name.Contains("shotgun.") || t.name.Contains("smg.") ||
											   t.name.Contains("spear.") || t.name.Contains("weapon.")) && t != null && r != null && smm != null)
										{
											if (_droppedItem[4])
											{
												if (!dictShaders.ContainsKey(r.name))
													dictShaders.Add(r.name, r.shader);
												if (r.shader != AssetsLoad._Rainbow)
													r.shader = AssetsLoad._Rainbow;
											}
											else
											{
												if (dictShaders.ContainsKey(r.name))
												{
													if (r.shader == AssetsLoad._Rainbow)
														r.shader = dictShaders[r.name];
												}
											}
										}
									}
								}
								var sm = t.GetComponentsInChildren<MeshRenderer>();
								for (int h = 0; h < sm.Length; h++)
								{
									foreach (var r in sm[h].materials)
									{
										if ((t.name.Contains("ammo.") || t.name.Contains("rifle.") || t.name.Contains("bow.") || t.name.Contains("crossbow") || t.name.Contains("fishingrod.") ||
												  t.name.Contains("flamethrower") || t.name.Contains("knife.") || t.name.Contains("lmg.") || t.name.Contains("pistol.") ||
												  t.name.Contains("rocket.") || t.name.Contains("salvaged.") || t.name.Contains("shotgun.") || t.name.Contains("smg.") ||
												  t.name.Contains("spear.") || t.name.Contains("weapon.")) && t != null && r != null && sm != null)
										{
											if (_droppedItem[4])
											{
												if (!dictShaders.ContainsKey(r.name))
													dictShaders.Add(r.name, r.shader);
												if (r.shader != AssetsLoad._Rainbow)
													r.shader = AssetsLoad._Rainbow;
											}
											else
											{
												if (dictShaders.ContainsKey(r.name))
												{
													if (r.shader == AssetsLoad._Rainbow)
														r.shader = dictShaders[r.name];
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			catch { }
		}
		public static Dictionary<string, Shader> dictShaders = new Dictionary<string, Shader> { };


		Rect windowRect = new Rect(20, 240, 180, 140);
		public void DrawWindow(int id = 99998, string title = "") => windowRect = GUI.Window(id, windowRect, CupboardMenu, title);
		void CupboardMenu(int windowID)
        {
			try
			{
				var outline = new Rect(0, 0, windowRect.width, windowRect.height);
				var Loutline = MenuFix.Line(outline);
				var Doutline = MenuFix.Line(Loutline);
				var LGoutline = MenuFix.Line(Doutline, 3);
				var fill = MenuFix.Line(LGoutline);
				var line1 = new Rect(fill.x - 5, fill.y - 5, fill.width + 10, 15);
				var line2 = new Rect(fill.x - 5, fill.y + 5, fill.width + 10, 13);
				var curEvent = Event.current;
				if (outline.Contains(curEvent.mousePosition))
					Drawing.DrawRect(outline, new Color32(200, 200, 200, 255), 6);
				else
					Drawing.DrawRect(outline, new Color32(122, 122, 122, 255), 6);
				Drawing.DrawRect(Loutline, new Color32(28, 28, 45, 255), 6);
				Drawing.DrawRect(Doutline, new Color32(28, 28, 45, 255), 6);
				Drawing.DrawRect(fill, new Color32(28, 28, 45, 255), 6);//Внутрений фон
				Drawing.DrawRect(line1, new Color32(32, 34, 50, 255), 6);//Верхняя панель
				Drawing.DrawRect(line2, new Color32(32, 34, 50, 255), 0); //Фикс низа верхний панели
				String(new Vector2(89, 4), "Authorized Cupboard", Color.white, true, 15, FontStyle.Normal);
				using (new GUILayout.VerticalScope())
				{
					if (_Cupboard.Count > 0)
					{
						var authorizedList = new Dictionary<BuildingPrivlidge, int> { };
						var centerScreen = new Vector2(Screen.width / 2f, Screen.height / 2f);
						foreach (var aa in BaseEntity.clientEntities)
						{
							var t = aa as BuildingPrivlidge;
							if (t != null && !t.IsDead())
							{
								int distanceFromCenter = (int)(Vector2.Distance(MainCamera.mainCamera.WorldToScreenPoint(t.transform.position), centerScreen));
								var onScreen = t.transform.position - MainCamera.mainCamera.transform.position;
								if (distanceFromCenter <= 200 && t.health > 0f && Vector3.Dot(MainCamera.mainCamera.transform.TransformDirection(Vector3.forward), onScreen) > 0)
									authorizedList.Add(t, distanceFromCenter);
							}
						}
						if (authorizedList.Count > 0)
						{
							authorizedList = authorizedList.OrderBy(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
							var closestBuilding = authorizedList.Keys.First();
							if (closestBuilding != null)
							{
								var autoturret_distance = (int)Vector3.Distance(MainCamera.mainCamera.transform.position, closestBuilding.transform.position);
								if (autoturret_distance <= _cupboardDist)
								{
									GUILayout.Space(5f);
									var list = new List<ProtoBuf.PlayerNameID>();
									list = closestBuilding.authorizedPlayers;
									for (int x = 0; x < list.Count; x++)
									{
										GUILayout.Label("<color=white>" + "Name: " + list[x].username.ToString() + ((true && list[x].userid != 0) ? "\n  -ID: " + list[x].userid : "") + "</color>", _TextStyle2);
										GUILayout.Space(5f);
									}
								}
							}
						}
					}
				}
				if (Event.current.type == EventType.Repaint)
				{
					var rect = GUILayoutUtility.GetLastRect();
					windowRect.height = rect.y + rect.height + 10f;
				}
			}
			catch { }
				GUI.DragWindow();
		}

		Rect windowsRect = new Rect(20, 360, 180, 140);
		public void DrawWindows(int id = 99991, string title = "") => windowsRect = GUI.Window(id, windowsRect, TurretMenu, title);
		void TurretMenu(int windowID)
        {
			try
			{
				var outline = new Rect(0, 0, windowsRect.width, windowsRect.height);
				var Loutline = MenuFix.Line(outline);
				var Doutline = MenuFix.Line(Loutline);
				var LGoutline = MenuFix.Line(Doutline, 3);
				var fill = MenuFix.Line(LGoutline);
				var line1 = new Rect(fill.x - 5, fill.y - 5, fill.width + 10, 15);
				var line2 = new Rect(fill.x - 5, fill.y + 5, fill.width + 10, 13);
				var curEvent = Event.current;
				if (outline.Contains(curEvent.mousePosition))
					Drawing.DrawRect(outline, new Color32(200, 200, 200, 255), 6);
				else
					Drawing.DrawRect(outline, new Color32(122, 122, 122, 255), 6);
				Drawing.DrawRect(Loutline, new Color32(28, 28, 45, 255), 6);
				Drawing.DrawRect(Doutline, new Color32(28, 28, 45, 255), 6);
				Drawing.DrawRect(fill, new Color32(28, 28, 45, 255), 6);//Внутрений фон
				Drawing.DrawRect(line1, new Color32(32, 34, 50, 255), 6);//Верхняя панель
				Drawing.DrawRect(line2, new Color32(32, 34, 50, 255), 0); //Фикс низа верхний панели
				String(new Vector2(89, 4), "Authorized Turret", Color.white, true, 15, FontStyle.Normal);
				using (new GUILayout.VerticalScope())
				{
					var authorizedList = new Dictionary<AutoTurret, int> { };
					var centerScreen = new Vector2(Screen.width / 2f, Screen.height / 2f);
					foreach (var aa in BaseEntity.clientEntities)
					{
						var t = aa as AutoTurret;
						if (t != null && !t.IsDead())
						{
							int distanceFromCenter = (int)(Vector2.Distance(MainCamera.mainCamera.WorldToScreenPoint(t.transform.position), centerScreen));
							var onScreen = t.transform.position - MainCamera.mainCamera.transform.position;
							if (distanceFromCenter <= 200 && t.health > 0f && Vector3.Dot(MainCamera.mainCamera.transform.TransformDirection(Vector3.forward), onScreen) > 0)
								authorizedList.Add(t, distanceFromCenter);
						}
					}
					if (authorizedList.Count > 0)
					{
						authorizedList = authorizedList.OrderBy(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
						var closestBuilding = authorizedList.Keys.First();
						if (closestBuilding != null)
						{
							var autoturret_distance = (int)Vector3.Distance(MainCamera.mainCamera.transform.position, closestBuilding.transform.position);
							if (autoturret_distance <= _autoturretDist)
							{
								GUILayout.Space(5f);
								var list = new List<ProtoBuf.PlayerNameID>();
								list = closestBuilding.authorizedPlayers;
								for (int x = 0; x < list.Count; x++)
								{
									GUILayout.Label("<color=white>" + "Name: " + list[x].username.ToString() + ((true && list[x].userid != 0) ? "\n  -ID: " + list[x].userid : "") + "</color>", _TextStyle2);
									GUILayout.Space(5f);
								}
							}
						}
					}
				}
				if (Event.current.type == EventType.Repaint)
				{
					var rect = GUILayoutUtility.GetLastRect();
					windowsRect.height = rect.y + rect.height + 10f;
				}
			}
			catch { }
			GUI.DragWindow();
		}
	}
}
