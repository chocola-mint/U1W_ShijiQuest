using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

namespace ShijiQuest
{
    [CreateAssetMenu(fileName = "VSEventObject", menuName = "ShijiQuest/VSEventObject")]
    public class VSEventObject : ScriptableObject
    {
        #if UNITY_EDITOR
        public ScriptGraphAsset definition;
        #endif
    }
}
