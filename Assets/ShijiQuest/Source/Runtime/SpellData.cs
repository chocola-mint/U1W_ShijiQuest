using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using TriInspector;

namespace ShijiQuest
{
    public class SpellData : NestedGameOption
    {
        [Min(0)]
        public float cost = 0;
        [Required]
        public CharacterDataRef user;
        public LocalizedString localizedName;
        public LocalizedString localizedDescription;
        public LocalizedString responseMessage;
        private AmountValue consumeValue => user.value.MP;
        public bool canUse => consumeValue.value >= cost;
        public void Use(CharacterDataRef target)
        {
            if(canUse)
            {
                consumeValue.Set(consumeValue.value - cost);
                OnUse(target);
            }
        }
        protected virtual void OnUse(CharacterDataRef target) {}
    }
}
