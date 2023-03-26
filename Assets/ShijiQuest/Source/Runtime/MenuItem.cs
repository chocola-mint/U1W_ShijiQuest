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
    public class MenuItem : MonoBehaviour, ISelectHandler, IDeselectHandler, ISubmitHandler
    {
        private TMP_Text display;
        [Required]
        public string itemName = "NAME HERE";
        [Required]
        public string offPrefix = "・";
        [Required]
        public string onPrefix = "◎";
        public event System.Action onSelect, onSubmit;
        public AudioClip playOnSelect, playOnSubmit;
        private void Awake() 
        {
            TryGetComponent<TMP_Text>(out display);
        }
        // Start is called before the first frame update
        void Start()
        {
            display.text = $"{offPrefix}{itemName}";
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        public void OnSubmit(BaseEventData eventData)
        {
            onSubmit?.Invoke();
            AudioSource.PlayClipAtPoint(playOnSubmit, Vector3.zero);
        }
        public void OnSelect(BaseEventData eventData)
        {
            display.text = $"{onPrefix}{itemName}";
            onSelect?.Invoke();
            AudioSource.PlayClipAtPoint(playOnSelect, Vector3.zero);
        }

        public void OnDeselect(BaseEventData eventData)
        {
            display.text = $"{offPrefix}{itemName}";
        }
        private void OnDisable() 
        {
           OnDeselect(null);
        }
    }
}
