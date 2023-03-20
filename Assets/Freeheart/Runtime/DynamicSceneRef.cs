using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Freeheart
{
    [CreateAssetMenu(fileName = "DynamicSceneRef", menuName = "Freeheart/DynamicSceneRef", order = 1)]
    public sealed class DynamicSceneRef : ScriptableObject
    {
        [SerializeField]
        private SceneRef sceneRef;
        public void SetSceneRef(SceneRef sceneRef) => this.sceneRef = sceneRef;
        public SceneRef GetSceneRef() => sceneRef;
    }
}
