using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using TriInspector;

namespace ShijiQuest
{
    [RequireComponent(typeof(TMP_Text))]
    public class GameOptionText : MonoBehaviour
    {
        private TMP_Text display;
        public Color initialColor = Color.white;
        public Color weightColor = Color.green;
        [Required]
        public GameOption gameOption;
        [Required]
        public GameOptionRanking gameOptionRanking;
        private float targetFillAmount = 0;
        private float currentFillAmount = 0;
        private void Awake() 
        {
            TryGetComponent<TMP_Text>(out display);
        }
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if(gameOptionRanking.Contains(gameOption))
            {
                float progress = 1.0f - gameOptionRanking.GetRankNormalized(gameOption);
                targetFillAmount = progress;
            }
            UpdateFill();
        }
        private void UpdateFill()
        {
            currentFillAmount = Mathf.MoveTowards(currentFillAmount, targetFillAmount, Time.deltaTime);
            Color lFill = Color.Lerp(initialColor, weightColor, Mathf.Sqrt(currentFillAmount));
            Color rFill = Color.Lerp(initialColor, weightColor, currentFillAmount * currentFillAmount);
            display.colorGradient = new(lFill,  rFill, lFill, rFill);
        }
    }
}
