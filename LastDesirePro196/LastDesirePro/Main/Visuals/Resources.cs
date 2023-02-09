using LastDesirePro.Attributes;
using LastDesirePro.Main.Objects;
using System.Reflection;
using UnityEngine;
using static LastDesirePro.Main.Objects.ObjectsCheck;
using static LastDesirePro.Menu.CFG.VisuаlCоnfig;

namespace LastDesirePro.Main.Visuals
{
    [Component]
    class Resources : MonoBehaviour
    {
        void OnGUI() => Resource();
        void Resource()
        {
            try
            {
                if (_Resources.Count > 0)
                {
                    foreach (var r in _Resources.Values)
                    {
                        var vector = MainCamera.mainCamera.WorldToScreenPoint(r.position);
                        if (vector.z > 0f && Main.Visuals.FullDrawing.IsInScreen(r.position))
                        {
                            var resource_distance = (int)Vector3.Distance(MainCamera.mainCamera.transform.position, r.position);
                            if (resource_distance <= _resourceDist)
                            {
                                vector.x += 3f; vector.y = Screen.height - (vector.y + 1f);
                                if (r.name.Contains("stone"))
                                {
                                    if (_resource[0])
                                    {
                                        FullDrawing.String(new Vector2(vector.x, vector.y - 12f), string.Format("{0}", "Stone"), _colorResource[0], true, 10, FontStyle.Bold, 2, _resource[4], _colorFonResource, _radiusFonResource);
                                        FullDrawing.String(new Vector2(vector.x, vector.y), string.Format("{0} m.", resource_distance), _colorResource[0], true, 10, FontStyle.Bold, 2, _resource[4], _colorFonResource, _radiusFonResource);
                                    }
                                }
                                if (r.name.Contains("metal"))
                                {
                                    if (_resource[1])
                                    {
                                        FullDrawing.String(new Vector2(vector.x, vector.y - 12f), string.Format("{0}", "Metal"), _colorResource[1], true, 10, FontStyle.Bold, 2, _resource[4], _colorFonResource, _radiusFonResource);
                                        FullDrawing.String(new Vector2(vector.x, vector.y), string.Format("{0} m.", resource_distance), _colorResource[1], true, 10, FontStyle.Bold, 2, _resource[4], _colorFonResource, _radiusFonResource);
                                    }
                                }
                                if (r.name.Contains("sulfur"))
                                {
                                    if (_resource[2])
                                    {
                                        FullDrawing.String(new Vector2(vector.x, vector.y - 12f), string.Format("{0}", "Sulfur"), _colorResource[2], true, 10, FontStyle.Bold, 2, _resource[4], _colorFonResource, _radiusFonResource);
                                        FullDrawing.String(new Vector2(vector.x, vector.y), string.Format("{0} m.", resource_distance), _colorResource[2], true, 10, FontStyle.Bold, 2, _resource[4], _colorFonResource, _radiusFonResource);
                                    }
                                }
                            }
                        }
                    }
                }
                if (_OreHotSpot.Count > 0 && _resource[3])
                {
                    foreach (var o in _OreHotSpot.Values)
                    {
                        var vectors = MainCamera.mainCamera.WorldToScreenPoint(o.position);
                        if (vectors.z > 0f && Main.Visuals.FullDrawing.IsInScreen(o.position))
                        {
                            var resource_distance = (int)Vector3.Distance(MainCamera.mainCamera.transform.position, o.position);
                            if (resource_distance <= 15f)
                            {
                                vectors.x += 3f; vectors.y = Screen.height - (vectors.y + 1f);
                                FullDrawing.String(new Vector2(vectors.x, vectors.y), string.Format("{0}", "☀"), _colorResource[3], true, 15, FontStyle.Normal);
                            }
                        }
                    }
                }
                if (_PlantEntity.Count > 0)
                    foreach (var o in _PlantEntity.Values)
                    {
                        var vector = MainCamera.mainCamera.WorldToScreenPoint(o.position);
                        if (vector.z > 0f && Main.Visuals.FullDrawing.IsInScreen(o.position))
                        {
                            var resource_distance = (int)Vector3.Distance(MainCamera.mainCamera.transform.position, o.position);
                            vector.x += 3f; vector.y = Screen.height - (vector.y + 1f);
                            if (o.name.Contains("hemp") && _collectible[3])
                            {
                                FullDrawing.String(new Vector2(vector.x, vector.y - 11), string.Format("{0}", "Plant Hemp"), _colorCollectible[3], true, 9, FontStyle.Bold, 2, _collectible[8], _colorFonCollectible, _radiusFonCollectible);
                                FullDrawing.String(new Vector2(vector.x, vector.y), string.Format("{0} m.", resource_distance), _colorCollectible[3], true, 9, FontStyle.Bold, 2, _collectible[8], _colorFonCollectible, _radiusFonCollectible);
                            }
                            if (o.name.Contains("pumpkin") && _collectible[6])
                            {
                                FullDrawing.String(new Vector2(vector.x, vector.y - 11), string.Format("{0}", "Plant Pumpkin"), _colorCollectible[6], true, 9, FontStyle.Bold, 2, _collectible[8], _colorFonCollectible, _radiusFonCollectible);
                                FullDrawing.String(new Vector2(vector.x, vector.y), string.Format("{0} m.", resource_distance), _colorCollectible[6], true, 9, FontStyle.Bold, 2, _collectible[8], _colorFonCollectible, _radiusFonCollectible);
                            }
                            if (o.name.Contains("corn") && _collectible[7])
                            {
                                FullDrawing.String(new Vector2(vector.x, vector.y - 11), string.Format("{0}", "Plant Corn"), _colorCollectible[7], true, 9, FontStyle.Bold, 2, _collectible[8], _colorFonCollectible, _radiusFonCollectible);
                                FullDrawing.String(new Vector2(vector.x, vector.y), string.Format("{0} m.", resource_distance), _colorCollectible[7], true, 9, FontStyle.Bold, 2, _collectible[8], _colorFonCollectible, _radiusFonCollectible);
                            }
                        }
                    }
                if (_CollectibleEntity.Count > 0)
                {
                    foreach (var o in _CollectibleEntity.Values)
                    {
                        var vector = MainCamera.mainCamera.WorldToScreenPoint(o.position);
                        if (vector.z > 0f && Main.Visuals.FullDrawing.IsInScreen(o.position))
                        {
                            var resource_distance = (int)Vector3.Distance(MainCamera.mainCamera.transform.position, o.position);
                            if (resource_distance <= _collectibleDist)
                            {
                                vector.x += 3f; vector.y = Screen.height - (vector.y + 1f);
                                if (o.name.Contains("stone-collectable") && _collectible[0])
                                {
                                    FullDrawing.String(new Vector2(vector.x, vector.y - 11), string.Format("{0}", "Spawned Stone"), _colorCollectible[0], true, 9, FontStyle.Bold, 2, _collectible[8], _colorFonCollectible, _radiusFonCollectible);
                                    FullDrawing.String(new Vector2(vector.x, vector.y), string.Format("{0} m.", resource_distance), _colorCollectible[0], true, 9, FontStyle.Bold, 2, _collectible[8], _colorFonCollectible, _radiusFonCollectible);
                                }
                                if (o.name.Contains("metal") && _collectible[1])
                                {
                                    FullDrawing.String(new Vector2(vector.x, vector.y - 11), string.Format("{0}", "Spawned Metal"), _colorCollectible[1], true, 9, FontStyle.Bold, 2, _collectible[8], _colorFonCollectible, _radiusFonCollectible);
                                    FullDrawing.String(new Vector2(vector.x, vector.y), string.Format("{0} m.", resource_distance), _colorCollectible[1], true, 9, FontStyle.Bold, 2, _collectible[8], _colorFonCollectible, _radiusFonCollectible);
                                }
                                if (o.name.Contains("sulfur") && _collectible[2])
                                {
                                    FullDrawing.String(new Vector2(vector.x, vector.y - 11), string.Format("{0}", "Spawned Sulfur"), _colorCollectible[2], true, 9, FontStyle.Bold, 2, _collectible[8], _colorFonCollectible, _radiusFonCollectible);
                                    FullDrawing.String(new Vector2(vector.x, vector.y), string.Format("{0} m.", resource_distance), _colorCollectible[2], true, 9, FontStyle.Bold, 2, _collectible[8], _colorFonCollectible, _radiusFonCollectible);
                                }
                                if (o.name.Contains("hemp") && _collectible[3])
                                {
                                    FullDrawing.String(new Vector2(vector.x, vector.y - 11), string.Format("{0}", "Spawned Hemp"), _colorCollectible[3], true, 9, FontStyle.Bold, 2, _collectible[8], _colorFonCollectible, _radiusFonCollectible);
                                    FullDrawing.String(new Vector2(vector.x, vector.y), string.Format("{0} m.", resource_distance), _colorCollectible[3], true, 9, FontStyle.Bold, 2, _collectible[8], _colorFonCollectible, _radiusFonCollectible);
                                }
                                if (o.name.Contains("mushroom") && _collectible[4])
                                {
                                    FullDrawing.String(new Vector2(vector.x, vector.y - 11), string.Format("{0}", "Spawned Mushroom"), _colorCollectible[4], true, 9, FontStyle.Bold, 2, _collectible[8], _colorFonCollectible, _radiusFonCollectible);
                                    FullDrawing.String(new Vector2(vector.x, vector.y), string.Format("{0} m.", resource_distance), _colorCollectible[4], true, 9, FontStyle.Bold, 2, _collectible[8], _colorFonCollectible, _radiusFonCollectible);
                                }
                                if (o.name.Contains("wood") && _collectible[5])
                                {
                                    FullDrawing.String(new Vector2(vector.x, vector.y - 11), string.Format("{0}", "Spawned Wood"), _colorCollectible[5], true, 9, FontStyle.Bold, 2, _collectible[8], _colorFonCollectible, _radiusFonCollectible);
                                    FullDrawing.String(new Vector2(vector.x, vector.y), string.Format("{0} m.", resource_distance), _colorCollectible[5], true, 9, FontStyle.Bold, 2, _collectible[8], _colorFonCollectible, _radiusFonCollectible);
                                }
                                if (o.name.Contains("pumpkin") && _collectible[6])
                                {
                                    FullDrawing.String(new Vector2(vector.x, vector.y - 11), string.Format("{0}", "Spawned Pumpkin"), _colorCollectible[6], true, 9, FontStyle.Bold, 2, _collectible[8], _colorFonCollectible, _radiusFonCollectible);
                                    FullDrawing.String(new Vector2(vector.x, vector.y), string.Format("{0} m.", resource_distance), _colorCollectible[6], true, 9, FontStyle.Bold, 2, _collectible[8], _colorFonCollectible, _radiusFonCollectible);
                                }
                                if (o.name.Contains("corn") && _collectible[7])
                                {
                                    FullDrawing.String(new Vector2(vector.x, vector.y - 11), string.Format("{0}", "Spawned Corn"), _colorCollectible[7], true, 9, FontStyle.Bold, 2, _collectible[8], _colorFonCollectible, _radiusFonCollectible);
                                    FullDrawing.String(new Vector2(vector.x, vector.y), string.Format("{0} m.", resource_distance), _colorCollectible[7], true, 9, FontStyle.Bold, 2, _collectible[8], _colorFonCollectible, _radiusFonCollectible);
                                }
                            }
                        }
                    }
                }
            }
            catch { }
            }
        }
    }
