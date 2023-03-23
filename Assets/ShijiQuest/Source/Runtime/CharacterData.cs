using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using System.Linq;
using TriInspector;

namespace ShijiQuest
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "ShijiQuest/CharacterData")]
    public class CharacterData : ScriptableObject
    {
        public LocalizedString localizedName;
        public GameObject prefab;
        [Required, InlineEditor]
        public AmountValue HP, MP;
        [Required, InlineEditor]
        public FloatValue ATK, DEF, MAG, MDEF;
        public enum Status
        {
            Normal,
            Asleep,
        }
        private Status previousStatus = Status.Normal;
        [System.NonSerialized]
        public Status status = Status.Normal;
        public void AssignStatus(Status value)
        {
            previousStatus = status;
            status = value;
        }
        public bool MatchStatus(Status value) => status == value;
        public bool justWokeUp => previousStatus == Status.Asleep && status == Status.Normal;
        public bool justFellAsleep => previousStatus == Status.Normal && status == Status.Asleep;
        public float receiveDamageBuffer = 0;
        public float receiveHPHealBuffer = 0;
        public float receiveMPHealBuffer = 0;
        [Min(0)]
        public float ATKMod, DEFMod, MAGMod, MDEFMod;
        public float trueATK => ATK.value + ATKMod;
        public float trueDEF => DEF.value + DEFMod;
        public float trueMAG => MAG.value + MAGMod;
        public float trueMDEF => MDEF.value + MDEFMod;
        public bool isDefending = false;
        public List<ElementType> strongAgainst, weakAgainst;
        public List<ElementType> strongAgainstMod, weakAgainstMod;
        public const float elementWeaknessFactor = 0.5f;
        public float EvaluateElementMod(ElementType elementType)
        {
            float mod = 1;
            if(strongAgainst.Contains(elementType)) mod -= elementWeaknessFactor;
            if(strongAgainstMod.Contains(elementType)) mod -= elementWeaknessFactor;
            if(weakAgainst.Contains(elementType)) mod += elementWeaknessFactor;
            if(weakAgainstMod.Contains(elementType)) mod += elementWeaknessFactor;
            return mod;
        }
        public void ClearMods()
        {
            ATKMod = DEFMod = MAGMod = MDEFMod = 0;
        }
        public void ResetState()
        {
            previousStatus = status = Status.Normal;
            HP.MaxOut();
            MP.MaxOut();
            ClearMods();
            inventory.Clear();
            strongAgainstMod.Clear();
            weakAgainstMod.Clear();
        }
        [InlineEditor]
        public Inventory inventory;
        // todo: a group for spells available
        public List<SpellData> spells = new();
        public IEnumerable<SpellData> GetUsableSpells() => spells.Where(x => x.canUse);
    }
}
