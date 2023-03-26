using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

namespace ShijiQuest
{

    [CreateAssetMenu(fileName = "DanmakuBag", menuName = "ShijiQuest/DanmakuBag")]
    public class DanmakuBag : ScriptableObject
    {
        [System.Serializable]
        public struct Entry
        {
            public float weight;
            public GameObject danmakuPrefab;
            public GameObject GetPrefab() => danmakuPrefab;
        }
        private float weightSum = 0;
        public List<Entry> entries = new();
        private void OnEnable() 
        {
            weightSum = 0;
            foreach(var entry in entries) weightSum += entry.weight;
        }
        public GameObject Sample()
        {
            if(Mathf.Approximately(weightSum, 0))
            {
                return entries[Random.Range(0, entries.Count)].GetPrefab();
            }
            else
            {
                float weightKey = Random.Range(0, weightSum);
                float currentWeight = 0;
                foreach(var entry in entries)
                {
                    float nextWeight = currentWeight + entry.weight;
                    if(nextWeight >= weightKey) return entry.GetPrefab();
                    else currentWeight = nextWeight;
                }
            }
            return entries[^1].GetPrefab();
        }
    }
}
