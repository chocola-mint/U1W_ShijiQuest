using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShijiQuest
{
    [CreateAssetMenu(fileName = "CharacterDataRef", menuName = "ShijiQuest/CharacterDataRef")]
    public class CharacterDataRef : ScriptableObject
    {
        public CharacterData value;
        public void Attack(CharacterDataRef subject)
        {
            CharacterData user = value;
            CharacterData target = subject.value;
            float baseDamage = user.trueATK;
            float defendFactor = target.isDefending ? 0.25f : 1;
            float finalDamage = (baseDamage / target.trueDEF) * defendFactor;
            float targetHPBefore = target.HP.value;
            target.HP.Set(Mathf.Floor(target.HP.value - finalDamage));
            target.receiveDamageBuffer = targetHPBefore - target.HP.value;
        }
        public void MagicAttack(CharacterDataRef subject, float power = 0, ElementType elementType = null)
        {
            CharacterData user = value;
            CharacterData target = subject.value;
            float baseDamage = user.trueMAG + power;
            float elementMod = elementType ? target.EvaluateElementMod(elementType) : 1;
            float defendFactor = target.isDefending ? 0.25f : 1;
            float finalDamage = (baseDamage * elementMod / target.trueMDEF) * defendFactor;
            float targetHPBefore = target.HP.value;
            target.HP.Set(Mathf.Floor(target.HP.value - finalDamage));
            target.receiveDamageBuffer = targetHPBefore - target.HP.value;
        }
        public void HealHP(float amount)
        {
            if(!Mathf.Approximately(amount, 0))
            {
                CharacterData owner = value;
                owner.HP.Set(owner.HP.value + amount);
                owner.receiveHPHealBuffer = amount;
            }
        }
        public void HealMP(float amount)
        {
            if(!Mathf.Approximately(amount, 0))
            {
                CharacterData owner = value;
                owner.MP.Set(owner.MP.value + amount);
                owner.receiveMPHealBuffer = amount;
            }
        }
        public void StartDefending()
        {
            value.isDefending = true;
        }
        public void StopDefending()
        {
            value.isDefending = false;
        }
        public bool IsDefending() => value.isDefending;
    }
}
