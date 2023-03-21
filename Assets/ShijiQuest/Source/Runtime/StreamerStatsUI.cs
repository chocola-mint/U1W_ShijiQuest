using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using TriInspector;
using TMPro;

namespace ShijiQuest
{
    public class StreamerStatsUI : MonoBehaviour
    {
        [Required]
        public StreamerStats stats;
        [Required]
        public TMP_Text moodValue, mentalityValue, viewCountValue, subCountValue;
        public LocalizedString moodValueString;
        
        // Start is called before the first frame update
        void Start()
        {
            
        }
        void OnEnable()
        {
            moodValueString.StringChanged += UpdateMoodValue;
        }
    
        void OnDisable()
        {
            moodValueString.StringChanged -= UpdateMoodValue;
        }
    
        void UpdateMoodValue(string translatedValue)
        {
            moodValue.text = translatedValue;
        }

        // Update is called once per frame
        void Update()
        {
            moodValueString.TableEntryReference = stats.mood.ToString();
            mentalityValue.text = $"{Mathf.CeilToInt(stats.mentality * 100)}";
            viewCountValue.text = $"{stats.viewCount}";
            subCountValue.text = $"{stats.subCount}";
        }
    }
}
