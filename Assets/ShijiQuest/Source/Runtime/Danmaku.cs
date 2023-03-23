using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using TriInspector;

namespace ShijiQuest
{
    [RequireComponent(typeof(Selectable), typeof(TMP_Text))]
    public class Danmaku : MonoBehaviour, IPointerClickHandler
    {
        [Required]
        public GameManager gameManager;
        [TextArea, Required]
        public string content = "";
        protected TMP_Text display;
        public AnimationCurve fadeCurve = AnimationCurve.Linear(0, 0, 1, 1);
        [Min(0.01f)]
        public float fadeDuration = 0.5f;
        [Min(1)]
        public float moveSpeed = 8.0f;
        public static readonly Vector2 moveDirection = Vector2.left;
        public const float maxLifetime = 10.0f;
        public event System.Action onClick;
        [Range(minPositivity, maxPositivity)]
        public float positivity = 2.5f;
        public const float maxPositivity = 5, minPositivity = 0;
        [Range(minAnnoyance, maxAnnoyance)]
        public float annoyance = 1.0f;
        public const float maxAnnoyance = 5, minAnnoyance = 0;
        private void OnGUI() 
        {
            if(TryGetComponent<TMP_Text>(out display)) display.text = content;
        }
        private void Awake() 
        {
            TryGetComponent<TMP_Text>(out display);
            Invoke(nameof(OnBecameInvisible), maxLifetime);
        }
        private void Start() 
        {
            display.text = content;
            var rt = transform as RectTransform;
            rt.Translate(Vector2.right * display.preferredWidth / 2);
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            if(display.raycastTarget)
            {
                onClick?.Invoke();
                display.raycastTarget = false;
                gameManager.ReinforceSelectLock();
                StartCoroutine(CoroFade());
            }
        }
        private IEnumerator CoroFade()
        {
            float startTime = Time.time;
            float t = 0;
            Color startColor = display.color;
            Color endColor = Color.clear;
            do {
                t = (Time.time - startTime) / fadeDuration;
                display.color = Color.Lerp(startColor, endColor, fadeCurve.Evaluate(t));
                yield return null;
            } while(t < 1);
            Destroy(gameObject);
        }
        private void Update() 
        {
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }
        private void OnBecameInvisible() 
        {
            Destroy(gameObject);
        }
    }
}
