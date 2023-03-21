using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriInspector;

namespace ShijiQuest
{
    [CreateAssetMenu(fileName = "HealItemData", menuName = "ShijiQuest/HealItemData")]
    public class HealItemData : ItemData
    {
        [HideIf(nameof(useHPPercentage)), Min(0)]
        public float healHPAmount = 4;
        [ShowIf(nameof(useHPPercentage)), Range(0, 1)]
        public float healHPPercentage = 0.2f;
        public bool useHPPercentage = false;
        [HideIf(nameof(useMPPercentage)), Min(0)]
        public float healMPAmount = 4;
        [ShowIf(nameof(useMPPercentage)), Range(0, 1)]
        public float healMPPercentage = 0.2f;
        public bool useMPPercentage = false;
        protected override void OnConsume()
        {
            if(useHPPercentage)
            {
                owner.HealHP(Mathf.Ceil(owner.value.HP.max.value * healHPPercentage));
            }
            else
            {
                owner.HealHP(healHPAmount);
            }
            if(useMPPercentage)
            {
                owner.HealMP(Mathf.Ceil(owner.value.MP.max.value * healMPPercentage));
            }
            else
            {
                owner.HealMP(healMPAmount);
            }
        }
    }
}
