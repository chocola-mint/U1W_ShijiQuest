using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriInspector;

namespace ShijiQuest
{
    [CreateAssetMenu(fileName = "NestedGameOption", menuName = "ShijiQuest/NestedGameOption")]
    public class NestedGameOption : GameOption
    {
        [Required]
        public GameOption outerGameOption;
        public override void AddWeight(float value)
        {
            outerGameOption.AddWeight(value);
            base.AddWeight(value);
        }
    }
}
