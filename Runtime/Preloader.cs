using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

namespace SmallTail.Preload
{
    public static class Preloader
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void OnLoad()
        {
            Stopwatch sw = Stopwatch.StartNew();
            int count = 0;
            
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

                        string name = (preload.GetCustomAttribute(typeof(PreloadedAttribute)) as PreloadedAttribute)?.Name ?? preload.Name;
                        instance.name = name;
                        
                        instance.AddComponent(preload);
                        Object.DontDestroyOnLoad(instance);

                        count++;
                    }
                }
            }
            
            sw.Stop();
            Debug.Log($"[ST Preloader] Preloaded {count} {(count == 1 ? "script" : "scripts")} in {sw.Elapsed.TotalSeconds}s.");
        }
    }
}