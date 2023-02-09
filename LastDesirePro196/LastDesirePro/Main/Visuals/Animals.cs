using LastDesirePro.Attributes;
using UnityEngine;
using static LastDesirePro.Menu.CFG.VisuаlCоnfig;
using static LastDesirePro.Main.Objects.ObjectsCheck;
using System.Collections.Generic;
using System.Reflection;

namespace LastDesirePro.Main.Visuals
{
    [Component]
    class Animals : MonoBehaviour
    {
        void OnGUI() { Animal();
        } 
        void Animal()
        {

            try
            {
                if (_animals[0] || _animals[1] || _animals[2] || _animals[3] || _animals[4] || _animals[5])
                    foreach (var a in BaseNpc.VisibleNpcList)
                    {
                        if (a != null)
                        {
                            var vector = MainCamera.mainCamera.WorldToScreenPoint(a.transform.position);
                            if (vector.z > 0f && Main.Visuals.FullDrawing.IsInScreen(a.transform.position))
                            {
                                var animal_distance = (int)Vector3.Distance(MainCamera.mainCamera.transform.position, a.transform.position);
                                if (animal_distance <= _animalDist)
                                {
                                    vector.x += 3f;
                                    vector.y = UnityEngine.Screen.height - (vector.y + 1f);
                                    var screen = MainCamera.mainCamera.WorldToScreenPoint(a.transform.position + new Vector3(0f, 0.6f, 0f));
                                    if (a.name.Contains("stag") && _animals[0])
                                    {
                                        Main.Visuals.FullDrawing.Health(new Vector2(vector.x, vector.y), a.health, 150, 30, 7, 1f);
                                        FullDrawing.String(new Vector2(vector.x, vector.y + 12), string.Format("{0} m.", animal_distance), _colorAnimals[0], true, 10, FontStyle.Bold, 2, _animals[6], _colorFonAnimal, _radiusFonAnimal);
                                        FullDrawing.String(new Vector2(vector.x, vector.y), string.Format("{0}", "Stag"), _colorAnimals[0], true, 10, FontStyle.Bold, 2, _animals[6], _colorFonAnimal, _radiusFonAnimal);
                                    }
                                    if (a.name.Contains("wolf") && _animals[1])
                                    {
                                        Main.Visuals.FullDrawing.Health(new Vector2(vector.x, vector.y + 2), a.health, 150, 30, 7, 1f);
                                        FullDrawing.String(new Vector2(vector.x, vector.y + 12), string.Format("{0} m.", animal_distance), _colorAnimals[1], true, 10, FontStyle.Bold, 2, _animals[6], _colorFonAnimal, _radiusFonAnimal);
                                        FullDrawing.String(new Vector2(vector.x, vector.y), string.Format("{0}", "Wolf"), _colorAnimals[1], true, 10, FontStyle.Bold, 2, _animals[6], _colorFonAnimal, _radiusFonAnimal);
                                    }
                                    if (a.name.Contains("horse") && _animals[2])
                                    {
                                        Main.Visuals.FullDrawing.Health(new Vector2(vector.x, vector.y + 2), a.health, 150, 30, 7, 1f);
                                        FullDrawing.String(new Vector2(vector.x, vector.y + 12), string.Format("{0} m.", animal_distance), _colorAnimals[2], true, 10, FontStyle.Bold, 2, _animals[6], _colorFonAnimal, _radiusFonAnimal);
                                        FullDrawing.String(new Vector2(vector.x, vector.y), string.Format("{0}", "Horse"), _colorAnimals[2], true, 10, FontStyle.Bold, 2, _animals[6], _colorFonAnimal, _radiusFonAnimal);
                                    }
                                    if (a.name.Contains("chicken") && _animals[3])
                                    {
                                        Main.Visuals.FullDrawing.Health(new Vector2(vector.x, vector.y + 2), a.health, 25, 30, 7, 1f);
                                        FullDrawing.String(new Vector2(vector.x, vector.y + 12), string.Format("{0} m.", animal_distance), _colorAnimals[3], true, 10, FontStyle.Bold, 2, _animals[6], _colorFonAnimal, _radiusFonAnimal);
                                        FullDrawing.String(new Vector2(vector.x, vector.y), string.Format("{0}", "Chicken"), _colorAnimals[3], true, 10, FontStyle.Bold, 2, _animals[6], _colorFonAnimal, _radiusFonAnimal);
                                    }
                                    if (a.name.Contains("bear") && _animals[4])
                                    {
                                        Main.Visuals.FullDrawing.Health(new Vector2(vector.x, vector.y + 2), a.health, 400, 30, 7, 1f);
                                        FullDrawing.String(new Vector2(vector.x, vector.y + 12), string.Format("{0} m.", animal_distance), _colorAnimals[4], true, 10, FontStyle.Bold, 2, _animals[6], _colorFonAnimal, _radiusFonAnimal);
                                        FullDrawing.String(new Vector2(vector.x, vector.y), string.Format("{0}", "Bear"), _colorAnimals[4], true, 10, FontStyle.Bold, 2, _animals[6], _colorFonAnimal, _radiusFonAnimal);
                                    }
                                    if (a.name.Contains("boar") && _animals[5])
                                    {
                                        Main.Visuals.FullDrawing.Health(new Vector2(vector.x, vector.y + 2), a.health, 150, 30, 7, 1f);
                                        FullDrawing.String(new Vector2(vector.x, vector.y + 12), string.Format("{0} m.", animal_distance), _colorAnimals[5], true, 10, FontStyle.Bold, 2, _animals[6], _colorFonAnimal, _radiusFonAnimal);
                                        FullDrawing.String(new Vector2(vector.x, vector.y), string.Format("{0}", "Boar"), _colorAnimals[5], true, 10, FontStyle.Bold, 2, _animals[6], _colorFonAnimal, _radiusFonAnimal);
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
