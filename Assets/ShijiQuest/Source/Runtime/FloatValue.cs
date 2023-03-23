using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShijiQuest
{
    [CreateAssetMenu(fileName = "FloatValue", menuName = "ShijiQuest/FloatValue")]
    public class FloatValue : ScriptableObject
    {
        public float value = 0;
        public virtual void Set(float value) => this.value = value;
        public int AsInt() => Mathf.RoundToInt(value);
    }
}
