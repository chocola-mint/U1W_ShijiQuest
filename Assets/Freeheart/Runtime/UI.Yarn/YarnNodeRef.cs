using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriInspector;

namespace Freeheart.UI.Yarn
{
    [CreateAssetMenu(fileName = "YarnNodeRef", menuName = "Freeheart/UI/Yarn/YarnNodeRef", order = 1)]
    public class YarnNodeRef : ScriptableObject
    {
        [ReadOnly]
        public string nodeName { get; set; }
        [Button]
        public void SetNodeName(string nodeName) => this.nodeName = nodeName;
    }
}
