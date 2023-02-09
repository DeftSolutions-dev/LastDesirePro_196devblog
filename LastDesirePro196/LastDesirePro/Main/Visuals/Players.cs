using LastDesirePro.Attributes;
using LastDesirePro.DrawMenu;
using LastDesirePro.Menu;
using ProtoBuf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using UnityEngine;
using static LastDesirePro.Menu.CFG.VisuаlCоnfig;
namespace LastDesirePro.Main.Visuals
{
    [Component()]
    class Players : MonoBehaviour
    {
        private void OnGUI()
        {
            if (!DrawMenu.AssetsLoad.Loaded) return;
            if (_players[9]) DrawWindow();
            Player();
        }
        [Save] Rect windowRect = new Rect(20, 20, 250, 140);
        public void DrawWindow(int id = 9999, string title = "") {

            GUI.color = Color.white;
            save = GUI.Window(232, windowRect, new GUI.WindowFunction(ItemMenu), "");
            windowRect.x = save.x;
            windowRect.y = save.y;
            GUI.color = Color.white;
        }
        Rect save;
        void ItemMenu(int windowID)
        {
            try
            {
                Rect outline = new Rect(0, 0, windowRect.width, windowRect.height);
                Rect Loutline = MenuFix.Line(outline);
                Rect Doutline = MenuFix.Line(Loutline);
                Rect LGoutline = MenuFix.Line(Doutline, 3);
                Rect fill = MenuFix.Line(LGoutline);
                Rect line1 = new Rect(fill.x - 5, fill.y - 5, fill.width + 10, 15);
                Rect line2 = new Rect(fill.x - 5, fill.y + 5, fill.width + 10, 13);
                var curEvent = Event.current;
                if (outline.Contains(curEvent.mousePosition))
                    Drawing.DrawRect(outline, new Color32(200, 200, 200, 255), 3);
                else
                    Drawing.DrawRect(outline, new Color32(122, 122, 122, 255), 3);
                Drawing.DrawRect(Loutline, new Color32(28, 28, 45, 255), 3);
                Drawing.DrawRect(Doutline, new Color32(28, 28, 45, 255), 3);
                Drawing.DrawRect(fill, new Color32(28, 28, 45, 255), 3);//Внутрений фон
                Drawing.DrawRect(line1, new Color32(32, 34, 50, 255), 3);//Верхняя панель
                Drawing.DrawRect(line2, new Color32(32, 34, 50, 255), 0); //Фикс низа верхний панели
                DrawMenu.Prefab.String(new Vector2(5, 4), "Inv<color=red>Belt</color>", Color.white, false, 15, FontStyle.Normal, 0);
                using (new GUILayout.VerticalScope())
                {
                    var inventoryList = new Dictionary<BasePlayer, int> { };
                    var centerScreen = new Vector2(Screen.width / 2f, Screen.height / 2f);
                    foreach (var player in BasePlayer.VisiblePlayerList)
                    {
                        if (player != null && !player.IsSleeping() && !player.IsLocalPlayer() && !player.IsDead())
                        {
                            int distanceFromCenter = (int)(Vector2.Distance(MainCamera.mainCamera.WorldToScreenPoint(player.model.headBone.transform.position), centerScreen));
                            var onScreen = player.transform.position - MainCamera.mainCamera.transform.position;
                            if (distanceFromCenter <= 300 && player.Health() > 0f && Vector3.Dot(MainCamera.mainCamera.transform.TransformDirection(Vector3.forward), onScreen) > 0)
                                inventoryList.Add(player, distanceFromCenter);
                        }
                    }
                    if (inventoryList.Count > 0)
                    {
                        inventoryList = inventoryList.OrderBy(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
                        var closestPlayer = inventoryList.Keys.First();
                        if (closestPlayer != null && !closestPlayer.IsDead() && !closestPlayer.IsSleeping())
                        {
                            DrawMenu.Prefab.String(new Vector2(-55, 1), closestPlayer.displayName.ToString(), Color.white, false, 15, FontStyle.Normal, 2);
                            GUILayout.Space(10f);
                            var cameradistance = (int)Vector3.Distance(LocalPlayer.Entity.eyes.position, closestPlayer.transform.position);
                            var list = closestPlayer.inventory.containerBelt.itemList;
                            if (list.Count != 0)
                            {
                                for (int x = 0; x < list.Count; x++)
                                {
                                    GUILayout.Label((list[x].uid == closestPlayer.clActiveItem ? "<color=green> > " : "<color=white>")
                                        + list[x].info.displayName.english.ToString()
                                        + ((((int)list[x].condition != 0) ? ("  " + (100 * (int)list[x].condition / (int)list[x].maxCondition).ToString() + "%") : (((int)list[x].maxCondition != 0) ? ("  <color=red>0%</color>") : "  " + (int)list[x].amount + "x")))
                                        + "</color>",
                                        DrawMenu.Prefab._TextStyle2);
                                }
                                GUILayout.Space(13f);
                                GUILayout.Label(" " + String.Format("<color=while>{0} meters        {1}</color>", (int)cameradistance, closestPlayer.userID),
                                       DrawMenu.Prefab._TextStyle2);
                            }
                            else
                            {
                                GUILayout.Label("<color=red>No Items!!!</color>",
                                        DrawMenu.Prefab._TextStyle2);
                                GUILayout.Space(13f);
                                GUILayout.Label(" " + String.Format("<color=while>{0} meters        {1}</color>", (int)cameradistance, closestPlayer.userID),
                                       DrawMenu.Prefab._TextStyle2);
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

        [Replacement(typeof(BasePlayer), "VisUpdateUsingCulling")]
        private void VisUpdateUsingCulling(float dist, bool visibility)
        {
            float entityMinCullDist = ConVar.Culling.entityMinCullDist;
            float entityMinAnimatorCullDist = ConVar.Culling.entityMinAnimatorCullDist;
            float entityMinShadowCullDist = ConVar.Culling.entityMinShadowCullDist;
            float entityMaxDist = ConVar.Culling.entityMaxDist;
            GetComponent<BasePlayer>().UpdateCullingBounds();
            if (Inj._unLoad && _players[7])
            {
                bool isVisible = 444 <= entityMaxDist && (444 <= entityMinCullDist || true);
                GetComponent<BasePlayer>().isVisible = isVisible;
                GetComponent<BasePlayer>().isAnimatorVisible = (GetComponent<BasePlayer>().isVisible || 444 <= entityMinAnimatorCullDist);
                GetComponent<BasePlayer>().isShadowVisible = (GetComponent<BasePlayer>().isVisible || 444 <= entityMinShadowCullDist);
            }
            else {
                bool isVisible = dist <= entityMaxDist && (dist <= entityMinCullDist || visibility);
                GetComponent<BasePlayer>().isVisible = isVisible;
                GetComponent<BasePlayer>().isAnimatorVisible = (GetComponent<BasePlayer>().isVisible || dist <= entityMinAnimatorCullDist);
                GetComponent<BasePlayer>().isShadowVisible = (GetComponent<BasePlayer>().isVisible || dist <= entityMinShadowCullDist);
            }


        }
        private void Update()
        {
            try
            {
                if (LocalPlayer.Entity != null)
                {
                    if (Inj._unLoad && _players[7])
                    {
                        var smm1 = BaseViewModel.ActiveModel.GetComponentsInChildren<SkinnedMeshRenderer>();
                        foreach (var r in smm1)
                        {
                            if (r != null)
                            {
                                if ((r.material.name.Contains("Hand") || r.material.name.Contains("Arm") || r.material.name.Contains("Glove")) && !_players[14])
                                {
                                    if (!dictShaders.ContainsKey(r.material.name))
                                        dictShaders.Add(r.material.name, r.material.shader);
                                    switch ((int)_chams)
                                    {
                                        case 0:
                                            {
                                                if (r.material.shader != AssetsLoad._Chams)
                                                {
                                                    r.material.shader = AssetsLoad._Chams;
                                                    r.material.SetColor("_ColorVisible", new Color(1f, 4f, 1f, 0f));
                                                    r.material.SetColor("_ColorBehind", new Color(4f, 1f, 1f, 0f));
                                                }
                                            }
                                            break;
                                        case 1:
                                            {
                                                if (r.material.shader != AssetsLoad._ColorChams)
                                                {
                                                    r.material.shader = AssetsLoad._ColorChams;
                                                    r.material.SetColor("_ColorVisible", new Color(0, 1, 0, 1));
                                                    r.material.SetColor("_ColorBehind", new Color(1, 0, 0, 1));
                                                }
                                            }
                                            break;
                                        case 2:
                                            {
                                                if (r.material.shader != AssetsLoad._Pulsing)
                                                {
                                                    r.material.shader = AssetsLoad._Pulsing;
                                                    r.material.SetColor("_Emissioncolour", new Color(1.5f, 0f, 3f, 0));
                                                }
                                            }
                                            break;
                                        case 3:
                                            {
                                                if (r.material.shader != AssetsLoad._Rainbow)
                                                    r.material.shader = AssetsLoad._Rainbow;
                                            }
                                            break;
                                        case 4:
                                            {
                                                if (r.material.shader != AssetsLoad._Wireframe)
                                                {
                                                    r.material.shader = AssetsLoad._Wireframe;
                                                    r.material.SetColor("_WireColor", new Color(2.1f, 0.7f, 4.7f, 0.3f));
                                                }

                                            }
                                            break;
                                        case 5:
                                            {
                                                if (LocalPlayer.Entity.Health() > 0f)
                                                {
                                                    if (r.material.shader != AssetsLoad._Pulsing)
                                                        r.material.shader = AssetsLoad._Pulsing;
                                                    if (LocalPlayer.Entity.Health() < 999f)
                                                        r.material.SetColor("_Emissioncolour", new Color(0, 2.59f, 0, 0));
                                                    if (LocalPlayer.Entity.Health() < 80)
                                                        r.material.SetColor("_Emissioncolour", new Color(1.863f, 2.59f, 0, 0));
                                                    if (LocalPlayer.Entity.Health() < 60)
                                                        r.material.SetColor("_Emissioncolour", new Color(2.226f, 2.59f, 0, 0));
                                                    if (LocalPlayer.Entity.Health() < 40)
                                                        r.material.SetColor("_Emissioncolour", new Color(2.953f, 2.226f, 0, 0));
                                                    if (LocalPlayer.Entity.Health() < 20)
                                                        r.material.SetColor("_Emissioncolour", new Color(2.953f, 1.863f, 0, 0));
                                                    if (LocalPlayer.Entity.Health() < 10)
                                                        r.material.SetColor("_Emissioncolour", new Color(2.953f, 0, 0, 0));
                                                    if (LocalPlayer.Entity.Health() <= 4.7f)
                                                        r.material.SetColor("_Emissioncolour", new Color(3.316f, 0, 0, 0));
                                                }

                                            }
                                            break;
                                    }
                                }
                                else
                                {
                                    if (dictShaders.ContainsKey(r.material.name))
                                    {
                                        switch ((int)_chams)
                                        {
                                            case 0:
                                                {
                                                    if (r.material.shader == AssetsLoad._Chams)
                                                        r.material.shader = dictShaders[r.material.name];
                                                }
                                                break;
                                            case 1:
                                                {
                                                    if (r.material.shader == AssetsLoad._ColorChams)
                                                        r.material.shader = dictShaders[r.material.name];
                                                }
                                                break;
                                            case 2:
                                                {
                                                    if (r.material.shader == AssetsLoad._Pulsing)
                                                        r.material.shader = dictShaders[r.material.name];
                                                }
                                                break;
                                            case 3:
                                                {
                                                    if (r.material.shader == AssetsLoad._Rainbow)
                                                        r.material.shader = dictShaders[r.material.name];
                                                }
                                                break;
                                            case 4:
                                                {
                                                    if (r.material.shader == AssetsLoad._Wireframe)
                                                        r.material.shader = dictShaders[r.material.name];
                                                }
                                                break;
                                            case 5:
                                                {
                                                    if (r.material.shader == AssetsLoad._Pulsing)
                                                        r.material.shader = dictShaders[r.material.name];
                                                }
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (AssetsLoad._Chams != null && AssetsLoad._ColorChams != null && AssetsLoad._Pulsing != null && AssetsLoad._Rainbow != null && AssetsLoad._Wireframe != null)
                    {
                        foreach (var player_chams in BasePlayer.VisiblePlayerList)
                        {
                            if (player_chams != null && player_chams.gameObject != null)
                            {
                                var smm = (SkinnedMultiMesh)typeof(PlayerModel).GetField("_multiMesh", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetField).GetValue(player_chams.playerModel);
                                foreach (var r in smm.Renderers)
                                {
                                    if (r.name.Contains("hair") && player_chams != null && r != null && !player_chams.IsSleeping() && smm != null && player_chams.IsAlive())
                                    {
                                        if (Inj._unLoad && _players[7])
                                        {
                                            if (r.enabled == true)
                                                r.enabled = false;
                                        }
                                        else
                                        {
                                            if (r.enabled == false)
                                                r.enabled = true;
                                        }
                                    }
                                    else
                                    {
                                        if (Inj._unLoad && _players[7])
                                        {
                                            if (!dictShaders.ContainsKey(r.material.name))
                                                dictShaders.Add(r.material.name, r.material.shader);
                                            switch ((int)_chams)
                                            {
                                                case 0:
                                                    {
                                                        if (r.material.shader != AssetsLoad._Chams)
                                                        {
                                                            r.material.shader = AssetsLoad._Chams;
                                                            r.material.SetColor("_ColorVisible", new Color(1f, 4f, 1f, 0f));
                                                            r.material.SetColor("_ColorBehind", new Color(4f, 1f, 1f, 0f));
                                                        }
                                                    }
                                                    break;
                                                case 1:
                                                    {
                                                        if (r.material.shader != AssetsLoad._ColorChams)
                                                        {
                                                            r.material.shader = AssetsLoad._ColorChams;
                                                            r.material.SetColor("_ColorVisible", new Color(0, 1, 0, 1));
                                                            r.material.SetColor("_ColorBehind", new Color(1, 0, 0, 1));
                                                        }
                                                    }
                                                    break;
                                                case 2:
                                                    {
                                                        if (r.material.shader != AssetsLoad._Pulsing)
                                                        {
                                                            r.material.shader = AssetsLoad._Pulsing;
                                                            r.material.SetColor("_Emissioncolour", new Color(1.5f, 0f, 3f, 0));
                                                        }
                                                    }
                                                    break;
                                                case 3:
                                                    {
                                                        if (r.material.shader != AssetsLoad._Rainbow)
                                                            r.material.shader = AssetsLoad._Rainbow;
                                                    }
                                                    break;
                                                case 4:
                                                    {
                                                        if (r.material.shader != AssetsLoad._Wireframe)
                                                        {
                                                            r.material.shader = AssetsLoad._Wireframe;
                                                            r.material.SetColor("_WireColor", new Color(2.1f, 0.7f, 4.7f, 0.3f));
                                                        }

                                                    }
                                                    break;
                                                case 5:
                                                    {
                                                        if (player_chams.Health() > 0f)
                                                        {
                                                            if (r.material.shader != AssetsLoad._Pulsing)
                                                                r.material.shader = AssetsLoad._Pulsing;
                                                            if (player_chams.Health() < 999f)
                                                                r.material.SetColor("_Emissioncolour", new Color(0, 2.59f, 0, 0));
                                                            if (player_chams.Health() < 80)
                                                                r.material.SetColor("_Emissioncolour", new Color(1.863f, 2.59f, 0, 0));
                                                            if (player_chams.Health() < 60)
                                                                r.material.SetColor("_Emissioncolour", new Color(2.226f, 2.59f, 0, 0));
                                                            if (player_chams.Health() < 40)
                                                                r.material.SetColor("_Emissioncolour", new Color(2.953f, 2.226f, 0, 0));
                                                            if (player_chams.Health() < 20)
                                                                r.material.SetColor("_Emissioncolour", new Color(2.953f, 1.863f, 0, 0));
                                                            if (player_chams.Health() < 10)
                                                                r.material.SetColor("_Emissioncolour", new Color(2.953f, 0, 0, 0));
                                                            if (player_chams.Health() <= 4.7f)
                                                                r.material.SetColor("_Emissioncolour", new Color(3.316f, 0, 0, 0));
                                                        }

                                                    }
                                                    break;
                                            }
                                        }
                                        else
                                        {
                                            if (dictShaders.ContainsKey(r.material.name))
                                            {
                                                switch ((int)_chams)
                                                {
                                                    case 0:
                                                        {
                                                            if (r.material.shader == AssetsLoad._Chams)
                                                                r.material.shader = dictShaders[r.material.name];
                                                        }
                                                        break;
                                                    case 1:
                                                        {
                                                            if (r.material.shader == AssetsLoad._ColorChams)
                                                                r.material.shader = dictShaders[r.material.name];
                                                        }
                                                        break;
                                                    case 2:
                                                        {
                                                            if (r.material.shader == AssetsLoad._Pulsing)
                                                                r.material.shader = dictShaders[r.material.name];
                                                        }
                                                        break;
                                                    case 3:
                                                        {
                                                            if (r.material.shader == AssetsLoad._Rainbow)
                                                                r.material.shader = dictShaders[r.material.name];
                                                        }
                                                        break;
                                                    case 4:
                                                        {
                                                            if (r.material.shader == AssetsLoad._Wireframe)
                                                                r.material.shader = dictShaders[r.material.name];
                                                        }
                                                        break;
                                                    case 5:
                                                        {
                                                            if (r.material.shader == AssetsLoad._Pulsing)
                                                                r.material.shader = dictShaders[r.material.name];
                                                        }
                                                        break;
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
        public static BasePlayer baseplayer = null;
        void Start()
        {
            try
            { 
                base.StartCoroutine(corpse());
            }
            catch { }
        }
        private static IEnumerator corpse()
        {
            while (true)
            {
                try
                {
                    if (LocalPlayer.Entity != null && LocalPlayer.Entity.IsValid() && BaseNetworkable.clientEntities != null)
                    {
                        loot.Clear();
                        _loot.Clear();
                        foreach (var ba in BaseNetworkable.clientEntities)
                        {
                            if (ba is LootableCorpse && ba != null && loot != null && BaseEntityEx.IsValid(ba as LootableCorpse))
                                loot.Add(ba as LootableCorpse);
                            if (ba is DroppedItemContainer && ba != null && _loot != null && BaseEntityEx.IsValid(ba as DroppedItemContainer))
                                _loot.Add(ba as DroppedItemContainer);
                        }
                    }
                }
                catch { } 
                yield return new WaitForSeconds(2f);
            }
        }
        public static HashSet<LootableCorpse> loot = new HashSet<LootableCorpse>();
        public static HashSet<DroppedItemContainer> _loot = new HashSet<DroppedItemContainer>();
        void Player()
        {
            try
            {
                if (_players[13])
                {
                    foreach (var Corpse in loot)
                    {
                        if (Corpse != null)
                        {
                            var vectora = MainCamera.mainCamera.WorldToScreenPoint(Corpse.transform.position);
                            if (vectora.z > 0f && Main.Visuals.FullDrawing.IsInScreen(Corpse.transform.position))
                            {
                                var _distancea = (int)Vector3.Distance(MainCamera.mainCamera.transform.position, Corpse.transform.position);
                                vectora.x += 3f; vectora.y = Screen.height - (vectora.y + 1f);
                                FullDrawing.String(new Vector2(vectora.x, vectora.y), string.Format("{0} m.", _distancea), _colorCorpses, true, 10, FontStyle.Bold, 2, _players[8], _colorFon, _radiusFon);
                                FullDrawing.String(new Vector2(vectora.x, vectora.y - 10f), String.Format("Corpse [{0}]", Corpse._playerName), _colorCorpses, true, 10, FontStyle.Bold, 2, _players[8], _colorFon, _radiusFon);
                            }
                        }
                    }
                    foreach (var DroppedItemContainer in _loot)
                    {
                        if (DroppedItemContainer != null )
                        {
                            var vectora = MainCamera.mainCamera.WorldToScreenPoint(DroppedItemContainer.transform.position);
                            if (vectora.z > 0f && Main.Visuals.FullDrawing.IsInScreen(DroppedItemContainer.transform.position))
                            {
                                var _distancea = (int)Vector3.Distance(MainCamera.mainCamera.transform.position, DroppedItemContainer.transform.position);
                                vectora.x += 3f; vectora.y = Screen.height - (vectora.y + 1f);
                                FullDrawing.String(new Vector2(vectora.x, vectora.y), string.Format("{0} m.", _distancea), _colorCorpses, true, 10, FontStyle.Bold, 2, _players[8], _colorFon, _radiusFon);
                                FullDrawing.String(new Vector2(vectora.x, vectora.y - 10f), String.Format("Corpse [{0}]", DroppedItemContainer._playerName), _colorCorpses, true, 10, FontStyle.Bold, 2, _players[8], _colorFon, _radiusFon);
                            }
                        }
                    }
                }
                if (Others._debugCamera)
                {
                    var pos = MainCamera.mainCamera.WorldToScreenPoint(Others._lockPos);
                    if (pos.z > 0)
                        FullDrawing.String(new Vector2(pos.x, (float)Screen.height - pos.y), "You Lock.", Color.white, true, 15, FontStyle.Bold, 2, true, Color.black, 0f);
                }
                foreach (var player in BasePlayer.VisiblePlayerList)
                {
                    if ((player != null) && (player.Health() > 0f) && !player.IsLocalPlayer() && DrawOnlyPlayers.IsInScreen(player.model.rootBone.transform.position))
                    {
                        baseplayer = player;
                        var position = player.transform.position;
                        var vector = MainCamera.mainCamera.WorldToScreenPoint(position);
                        var screenPos = MainCamera.mainCamera.WorldToScreenPoint(player.model.headBone.transform.position + new Vector3(0f, 0.3f, 0f));
                        var vector1 = MainCamera.mainCamera.WorldToScreenPoint(position + new Vector3(0f, 1.7f, 0f));
                        var vector2 = MainCamera.mainCamera.WorldToScreenPoint(position + new Vector3(0f, 1.2f, 0f));
                        var num1 = Mathf.Abs(vector.y - vector1.y);
                        var num2 = Mathf.Abs(vector.y - vector2.y);
                        var cameradistance = (int)Vector3.Distance(LocalPlayer.Entity.eyes.position, player.transform.position);
                        if (!player.IsSleeping() && player.Health() > 0f)
                        {
                            if (cameradistance <= _dis)
                            {
                                if (screenPos.z > 1f)
                                {
                                    if (_players[4])
                                    {
                                        GUI.color = UnityEngine.Color.white;
                                        Vector3[] bonePositions = player.GetBonePositions();
                                        Vector2[] array = new Vector2[16];
                                        for (int i = 0; i < bonePositions.Length; i++)
                                        {
                                            Vector2 vector4 = MainCamera.mainCamera.WorldToScreenPoint(bonePositions[i]);
                                            array[i] = new Vector2(vector4.x, (float)Screen.height - vector4.y);
                                        }
                                        FullDrawing.Line(array[0], array[1], _players[12] && FullDrawing.IsVisible(player, Bones.boneList[1]) ? _colorVisible : _colorBone, 2f);
                                        FullDrawing.Line(array[1], array[2], _players[12] && FullDrawing.IsVisible(player, Bones.boneList[2]) ? _colorVisible : _colorBone, 2f);
                                        FullDrawing.Line(array[2], array[3], _players[12] && FullDrawing.IsVisible(player, Bones.boneList[3]) ? _colorVisible : _colorBone, 2f);
                                        FullDrawing.Line(array[1], array[4], _players[12] && FullDrawing.IsVisible(player, Bones.boneList[4]) ? _colorVisible : _colorBone, 2f);
                                        FullDrawing.Line(array[1], array[7], _players[12] && FullDrawing.IsVisible(player, Bones.boneList[7]) ? _colorVisible : _colorBone, 2f);
                                        FullDrawing.Line(array[4], array[5], _players[12] && FullDrawing.IsVisible(player, Bones.boneList[5]) ? _colorVisible : _colorBone, 2f);
                                        FullDrawing.Line(array[7], array[8], _players[12] && FullDrawing.IsVisible(player, Bones.boneList[8]) ? _colorVisible : _colorBone, 2f);
                                        FullDrawing.Line(array[5], array[6], _players[12] && FullDrawing.IsVisible(player, Bones.boneList[6]) ? _colorVisible : _colorBone, 2f);
                                        FullDrawing.Line(array[8], array[9], _players[12] && FullDrawing.IsVisible(player, Bones.boneList[9]) ? _colorVisible : _colorBone, 2f);
                                        FullDrawing.Line(array[3], array[10], _players[12] && FullDrawing.IsVisible(player, Bones.boneList[10]) ? _colorVisible : _colorBone, 2f);
                                        FullDrawing.Line(array[3], array[13], _players[12] && FullDrawing.IsVisible(player, Bones.boneList[13]) ? _colorVisible : _colorBone, 2f);
                                        FullDrawing.Line(array[10], array[11], _players[12] && FullDrawing.IsVisible(player, Bones.boneList[11]) ? _colorVisible : _colorBone, 2f);
                                        FullDrawing.Line(array[13], array[14], _players[12] && FullDrawing.IsVisible(player, Bones.boneList[14]) ? _colorVisible : _colorBone, 2f);
                                        FullDrawing.Line(array[11], array[12], _players[12] && FullDrawing.IsVisible(player, Bones.boneList[12]) ? _colorVisible : _colorBone, 2f);
                                        FullDrawing.Line(array[14], array[15], _players[12] && FullDrawing.IsVisible(player, Bones.boneList[15]) ? _colorVisible : _colorBone, 2f);
                                        GUI.color = UnityEngine.Color.white;
                                    }
                                }
                                if (screenPos.z > 1.5f)
                                {
                                    if ((_players[0] || _players[6] || _players[5]))
                                    {
                                        if (!player.IsDucked() && !player.IsWounded() && !Menu.CFG.AimBotConfig._friendList.Contains(player.userID))
                                            DrawOnlyPlayers.DrawStats(new Vector2(vector1.x, (float)Screen.height - vector1.y), num1 / 2.5f, num1,
                                                player.Health(), player.displayName,
                                                (player.GetHeldEntity() != null) ? player.GetHeldEntity().GetItem().info.displayName.english : "Nothing", $"{cameradistance} m.", _players[8], _colorFon, _radiusFon);
                                        if (player.IsDucked() && !player.IsWounded() && !Menu.CFG.AimBotConfig._friendList.Contains(player.userID))
                                            DrawOnlyPlayers.DrawStats(new Vector2(vector2.x, (float)Screen.height - vector2.y), num2 / 2.5f, num2,
                                                player.Health(), player.displayName,
                                                (player.GetHeldEntity() != null) ? player.GetHeldEntity().GetItem().info.displayName.english : "Nothing", $"{cameradistance} m.", _players[8], _colorFon, _radiusFon);
                                        if (Menu.CFG.AimBotConfig._friendList.Contains(player.userID) && !player.IsWounded())
                                        {
                                            DrawOnlyPlayers.String(new Vector2(vector2.x, (float)Screen.height - vector2.y), string.Format("{0}", player.displayName), Color.green, true, 11);
                                            DrawOnlyPlayers.String(new Vector2(vector2.x, (float)Screen.height - vector2.y - -10f), string.Format("{0} m.", cameradistance), Color.green, true, 11);
                                        }
                                    }
                                }
                                if (_players[2])
                                {
                                    if (player.IsWounded() && _players[2] && _players[0])
                                        DrawOnlyPlayers.String(new Vector2(screenPos.x, Screen.height - screenPos.y - -10f), string.Format("{0}", "WOUNDED"), new Color32(255, 0, 0, 255), true, 12, FontStyle.Bold, 2, _players[8], _colorFon, _radiusFon);
                                }
                            }
                        }
                        else
                        {
                            if (cameradistance <= _dis * 8)
                                if (_players[3] && player.IsSleeping())
                                {
                                    DrawOnlyPlayers.String(new Vector2(screenPos.x, Screen.height - screenPos.y), string.Format("{0}", player.displayName), _colorSleepers, true, 11);
                                    DrawOnlyPlayers.String(new Vector2(screenPos.x, Screen.height - screenPos.y - -10f), string.Format("{0} m.", cameradistance), _colorSleepers, true, 11);
                                }
                        }
                    }

                }
            }
            catch { }
        }
    }
} 