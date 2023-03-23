using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ChocoUtil.Algorithms.Random;
using System.Linq;
namespace ShijiQuest
{
    using WeightedOption = ChocoUtil.Algorithms.WeightedValue<GameOption>;

    [CreateAssetMenu(fileName = "GameOptionRanking", menuName = "ShijiQuest/GameOptionRanking")]
    public class GameOptionRanking : ScriptableObject
    {
        public List<GameOption> gameOptions = new();
        public void Clear() => gameOptions.Clear();
        public void AddOption(GameOption gameOption, bool resetWeight = true) 
        { 
            if(resetWeight)
                gameOption.optionWeight = 1;
            gameOptions.Add(gameOption);
        }
        public void AddOptions(IEnumerable<GameOption> options, bool resetWeight = true)
        {
            foreach(var option in options) AddOption(option, resetWeight);
        }
        public bool Contains(GameOption gameOption) => gameOptions.Contains(gameOption);
        public int GetRank(GameOption gameOption)
        {
            gameOptions.Sort(new GameOption.SortDescending());
            return gameOptions.IndexOf(gameOption);
        }
        public float GetRankNormalized(GameOption gameOption)
        {
            if(gameOptions.Count <= 1) return Mathf.Approximately(gameOption.optionWeight, 0) ? 1 : 0;
            else return (float) GetRank(gameOption) / (float) (gameOptions.Count - 1);
        }
        public GameOption GetFirst() 
        {
            gameOptions.Sort(new GameOption.SortDescending());
            return gameOptions[0];
        }
        public float GetFirstWeightNormalized()
        {
            if(gameOptions.Count == 1) return 1;
            float sum = gameOptions.Sum(x => x.optionWeight);
            if(Mathf.Approximately(sum, 0)) return 0;
            return gameOptions.Max(x => x.optionWeight) / sum;
        }
        public GameOption GetWeightedRandom()
        {
            var wOptions = new WeightedOption[gameOptions.Count];
            int i = 0;
            foreach(var option in gameOptions)
                wOptions[i++] = new(option.optionWeight, option);
            return Select(wOptions);
        }
        public GameOption GetUnweightedRandom()
        {
            return gameOptions[Random.Range(0, gameOptions.Count)];
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
