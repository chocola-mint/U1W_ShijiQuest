using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriInspector;

namespace ShijiQuest
{
    [CreateAssetMenu(fileName = "AmountValue", menuName = "ShijiQuest/AmountValue")]
    public class AmountValue : FloatValue
    {
        [Button, ShowIf(nameof(HasMax))]
        public void MaxOut()
        {
            if(max) Set(max.value);
        }
        [InlineEditor]
        public FloatValue max;
        public bool HasMax() => max;
        public override void Set(float value)
        {
            if(max) base.Set(Mathf.Clamp(value, 0, max.value));
            else base.Set(Mathf.Max(value, 0));
        }
        public float GetPercentage() => value / max.value;
        private void OnValidate() 
        {
            if(max) Set(value);
        }
    }
}
