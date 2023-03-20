using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TriInspector;

namespace ShijiQuest
{
    [RequireComponent(typeof(TMP_Text))]
    public class MenuItem : MonoBehaviour
    {
        private TMP_Text display;
        [Required]
        public string itemName = "NAME HERE";
        [Required]
        public string offPrefix = "・";
        [Required]
        public string onPrefix = "◎";
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
        
        }
    }
}
