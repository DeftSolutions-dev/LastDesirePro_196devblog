using LastDesirePro.Attributes;
using LastDesirePro.Menu;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEngine;

namespace LastDesirePro
{
	public class Inj
	{ 
		public static void Enter()
		{ 
			_Object = new GameObject("Check");
			CheckUnknownInstance();
			Load.transform.parent = null;
			var par = Load.transform.parent;
			if (par != null && par.gameObject != Load)
				par.parent = Load.transform;
			_Object.hideFlags = HideFlags.HideAndDontSave;
			GameObject.DontDestroyOnLoad(_Object);
			Init(); 
			_unLoad = true;
            bruh.unLoad = false;
            try
            {
                Main.Misc.Misc.cfg.LoadSettings();
                Inits(); ConfigManager.Init();
			}
			catch { }
		}
		public static bool _unLoad = true; 
		[Attributes.Component]
        public class bruh : MonoBehaviour
        {
            public static float _time = 5f; //5 sec
            public static bool unLoad;
            private void Update()
            {
                _time -= Time.deltaTime; 
                if (unLoad)
                    Unload();
                if (_time < 0) 
                    _time = 5f; 
            }
            public void Unload()
            {
                _unLoad = false; 
                if (_time < 0)
                {
                    remove();
                    UnityEngine.Object.Destroy(_Object, 0f);
                    _Object = null;
                }
            }
        }
        public static GameObject _Object;
		public static void GoodEnter()
		{
			System.Media.SoundPlayer _play = new System.Media.SoundPlayer { Stream = LastDesirePro.Properties.Resources._10 };
			_play.Play();
		}
		public static void Init()
		{
			foreach (var T in Assembly.GetExecutingAssembly().GetTypes())
			{
				if (T.IsDefined(typeof(ComponentAttribute), false))
					_Object.AddComponent(T);
				var method = T.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				for (int j = 0; j < method.Length; j++)
				{
					var m = method[j];
					if (m.IsDefined(typeof(ReplacementAttribute), false))
						add(m);
				}
			}
		}
		public static Dictionary<ReplacementAttribute, Replacement.ReplacementWrapper> rr => t;
		public static void add(MethodInfo method)
		{
			ReplacementAttribute r = (ReplacementAttribute)Attribute.GetCustomAttribute(method, typeof(ReplacementAttribute));
			if (!(rr.Count((KeyValuePair<ReplacementAttribute, Replacement.ReplacementWrapper> k) => k.Key.Method == r.Method) > 0))
			{
				Replacement.ReplacementWrapper fe = new Replacement.ReplacementWrapper(r.Method, method, r, null);
				fe.Replacement();
				rr.Add(r, fe);
			}
		}
		public static void remove()
		{
			foreach (Replacement.ReplacementWrapper rem in rr.Values)
				rem.Revert();
		}
		public static Dictionary<ReplacementAttribute, Replacement.ReplacementWrapper> t = new Dictionary<ReplacementAttribute, Replacement.ReplacementWrapper>();
		public static void Inits()
		{
			_Object.GetComponent<CoroutineComponent>().StartCoroutine(DrawMenu.AssetsLoad.LoadAssets());
		}
		public static GameObject Load
		{
			get
			{
				return _Object;
			}
			set
			{
				_Object = value;
			}
		}
		public static void EnterModule()
		{
			new Thread((ThreadStart)delegate
			{
				Thread.Sleep(1);
				Enter();
			}).Start();
		}
		public static void CheckUnknownInstance()
		{
			foreach (var item in from obj in Resources.FindObjectsOfTypeAll<GameObject>()
										where obj.name == ("Check")
								 select obj)
			{
				if (item != _Object)
					UnityEngine.Object.Destroy(item, 0f);
			}
		}
		[Component]
		public class CoroutineComponent : MonoBehaviour
		{ }
		/*[Component]
		public class Dump : MonoBehaviour
		{
			class ObjectContainer
			{
				public Component[] cList;
				public GameObject obj;
				public ObjectContainer(GameObject o)
				{
					obj = o;
					Component[] temp = obj.GetComponents(typeof(Component));
					cList = temp;
				}
			}
			public static void superdump()
			{
				try
				{
					List<string> content = new List<string>();
					GameObject[] all_gobjects = FindObjectsOfType<GameObject>();
					content.Add(string.Format("Всего {0} Objects", all_gobjects.Length));
					ObjectContainer[] advanced_all_gobjects = new ObjectContainer[all_gobjects.Length];
					for (int i = 0; i < advanced_all_gobjects.Length; i++)
						advanced_all_gobjects[i] = new ObjectContainer(all_gobjects[i]);
					for (int i = 0; i < advanced_all_gobjects.Length; i++)
					{
						content.Add(string.Format("Object -> Name: \"{0}\"; Layer: {1}", advanced_all_gobjects[i].obj.name, advanced_all_gobjects[i].obj.layer.ToString()));
						content.Add("   Pos: " + advanced_all_gobjects[i].obj.transform.position.ToString());
						if (advanced_all_gobjects[i].obj.tag != "Untagged") { content.Add("   Tag: " + advanced_all_gobjects[i].obj.tag); }
						foreach (Component c in advanced_all_gobjects[i].cList)
						{
							if (c.tag == "Untagged") { content.Add("      Component: " + c.GetType().ToString()); }
							else { content.Add("      Component: " + c.GetType().ToString() + "; Component Tag: " + c.tag); }
						}
					}
					TextWriter tw = new StreamWriter("C:\\Users\\" + Environment.UserName + "\\Desktop\\DumpObjects.txt");
					foreach (string s in content)
					tw.WriteLine(s);
					tw.Close();
				}
				catch (Exception e)
				{
					Debug.LogError(e.ToString());
				}
			}
		}*/ //Debug the Game
	}
}
