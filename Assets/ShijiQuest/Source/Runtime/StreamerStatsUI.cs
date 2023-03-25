using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
        public Image angryImage;
        [Required]
        public TMP_Text moodValue, mentalityValue, viewCountValue, subCountValue;
        public LocalizedString moodValueString;
        [Required]
        public Sprite angrySpriteSmall, angrySpriteBig;
        
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
        void LateUpdate()
        {
            moodValueString.TableEntryReference = stats.mood.ToString();
            mentalityValue.text = $"{Mathf.CeilToInt(stats.mentality * 100)}%";
            viewCountValue.text = $"{stats.viewCount}";
            subCountValue.text = $"{stats.subCount}";
            if(stats.mood == StreamerStats.Mood.Irritated)
            {
                angryImage.enabled = true;
                angryImage.sprite = angrySpriteSmall;
            }
            else if(stats.mood == StreamerStats.Mood.Angry)
            {
                angryImage.enabled = true;
                angryImage.sprite = angrySpriteBig;
                enabled = false; // Stop updating because this is the lose condition.
            }
            else angryImage.enabled = false;

        }
    }
}
