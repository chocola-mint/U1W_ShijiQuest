using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriInspector;

namespace Freeheart
{
    [CreateAssetMenu(fileName = "SceneRef", menuName = "Freeheart/SceneRef", order = 1)]
    public class SceneRef : ScriptableObject
    {
        [Scene, SerializeField]
        private string sceneName;
        public string GetSceneName() => sceneName;
        
    }
}
