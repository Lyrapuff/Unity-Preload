using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using SmallTail.Preload.Attributes;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

namespace SmallTail.Preload
{
    public static class Preloader
    {
        private static List<Component> _components = new List<Component>();
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void OnLoad()
        {
            Stopwatch sw = Stopwatch.StartNew();

            _components = Preload();
            
            sw.Stop();
            Debug.Log($"[ST Preloader] Preloaded {_components.Count} {(_components.Count == 1 ? "script" : "scripts")} in {sw.Elapsed.TotalSeconds}s.");
        }

        private static List<Component> Preload()
        {
            List<Component> components = new List<Component>();

            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (Assembly assembly in assemblies)
            {
                IEnumerable<Type> preloads = assembly.GetTypes()
                    .Where(x => x.GetCustomAttribute(typeof(PreloadedAttribute)) != null);

                foreach (Type preload in preloads)
                {
                    if (preload.BaseType == typeof(MonoBehaviour))
                    {
                        GameObject instance = new GameObject();
                        Object.DontDestroyOnLoad(instance);

                        PreloadedAttribute attribute = preload.GetCustomAttribute(typeof(PreloadedAttribute)) as PreloadedAttribute;
                        string name = attribute?.Name ?? preload.Name;
                        instance.name = name;
                        
                        Component component = instance.AddComponent(preload);
                        components.Add(component);
                    }
                }
            }
            
            return components;
        }

        public static T Get<T>() where T : Component
        {
            return _components.FirstOrDefault(comp => comp.GetType() == typeof(T)) as T;
        }
    }
}