using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShijiQuest
{
    [CreateAssetMenu(fileName = "GameOptionRanking", menuName = "ShijiQuest/GameOptionRanking")]
    public class GameOptionRanking : ScriptableObject
    {
        private List<GameOption> gameOptions = new();
        public void Clear() => gameOptions.Clear();
        private bool dirty = false;
        public void AddOption(GameOption gameOption) 
        { 
            dirty = true;
            gameOption.optionWeight = 0;
            gameOptions.Add(gameOption);
        }
        public void AddOptions(IEnumerable<GameOption> options)
        {
            foreach(var option in options) AddOption(option);
        }
        public bool Contains(GameOption gameOption) => gameOptions.Contains(gameOption);
        public int GetRank(GameOption gameOption)
        {
            if(dirty) gameOptions.Sort(new GameOption.SortDescending());
            return gameOptions.IndexOf(gameOption);
        }
        public float GetRankNormalized(GameOption gameOption)
        {
            if(gameOptions.Count <= 1) return Mathf.Approximately(gameOption.optionWeight, 0) ? 1 : 0;
            else return (float) GetRank(gameOption) / (float) (gameOptions.Count - 1);
        }
        public GameOption GetFirst() 
        {
            if(dirty) gameOptions.Sort(new GameOption.SortDescending());
            return gameOptions[0];
        }
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void LateUpdate()
        {
            
        }
    }
}
