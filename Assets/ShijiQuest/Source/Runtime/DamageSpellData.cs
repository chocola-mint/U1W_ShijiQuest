using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShijiQuest
{
    [CreateAssetMenu(fileName = "DamageSpellData", menuName = "ShijiQuest/DamageSpellData")]
    public class DamageSpellData : SpellData
    {
        public float power = 20;
        public ElementType elementType;
        protected override void OnUse(CharacterDataRef target)
        {
            user.MagicAttack(target, power, elementType);
        }
    }
}
