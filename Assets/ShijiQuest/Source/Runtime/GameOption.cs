using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriInspector;

namespace ShijiQuest
{ 
    [CreateAssetMenu(fileName = "GameOption", menuName = "ShijiQuest/GameOption")]
    public class GameOption : ScriptableObject
    {
        [ReadOnly, System.NonSerialized]
        public float optionWeight = 0;
        public class SortAscending : IComparer<GameOption>
        {
            public int Compare(GameOption lhs, GameOption rhs)
            => lhs.optionWeight.CompareTo(rhs.optionWeight);
        }
        public class SortDescending : IComparer<GameOption>
        {
            public int Compare(GameOption lhs, GameOption rhs)
            => -lhs.optionWeight.CompareTo(rhs.optionWeight);
        }
    }
}
