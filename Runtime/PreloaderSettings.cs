using System.Collections.Generic;
using UnityEngine;

namespace SmallTail.Preload
{
    [CreateAssetMenu(menuName = "Preloader/Settings", fileName = "PreloaderSettings")]
    public class PreloaderSettings : ScriptableObject
    {
        public List<GameObject> Preloaded => _preloaded;
        
        [SerializeField] private List<GameObject> _preloaded;
    }
}