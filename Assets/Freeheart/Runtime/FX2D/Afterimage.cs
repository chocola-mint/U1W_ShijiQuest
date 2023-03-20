using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriInspector;
namespace Freeheart.FX2D
{
    public class Afterimage : MonoBehaviour
    {
        private class Body : MonoBehaviour
        {
            public float lifetime = 1.0f;
            private float startTime = 0;
            public SpriteRenderer spriteRenderer;
            private Color startColor;
            private void Start() 
            {
                startTime = Time.time;
                startColor = spriteRenderer.color;
            }
            private void Update()
            {
                float t = (Time.time - startTime) / lifetime;
                if(t >= 1) Destroy(gameObject);
                else spriteRenderer.color = Color.Lerp(startColor, Color.clear, t);
            }
        }
        private SpriteRenderer[] spriteRenderers;
        [SerializeField, Min(0.01f)]
        private float interval = 0.1f;
        [SerializeField, Min(0.01f)]
        private float imageLifetime = 0.25f;
        private float nextImageTime = 0;
        [SerializeField]
        private Color hue = new Color(0.5f, 0.5f, 0.8f);
        protected virtual void Reset() 
        {
            
        }
        private void CreateBody(SpriteRenderer spriteRenderer)
        {
            GameObject bodyGO = new();
            bodyGO.hideFlags = HideFlags.HideInHierarchy;
            bodyGO.transform.SetPositionAndRotation(spriteRenderer.transform.position, spriteRenderer.transform.rotation);
            bodyGO.transform.localScale = spriteRenderer.transform.lossyScale;
            var body = bodyGO.AddComponent<Body>();
            body.spriteRenderer = bodyGO.AddComponent<SpriteRenderer>();
            body.spriteRenderer.sprite = spriteRenderer.sprite;
            body.spriteRenderer.flipX = spriteRenderer.flipX;
            body.spriteRenderer.flipY = spriteRenderer.flipY;
            body.spriteRenderer.color = spriteRenderer.color * hue;
            body.spriteRenderer.sharedMaterial = spriteRenderer.sharedMaterial;
            body.spriteRenderer.sortingOrder = spriteRenderer.sortingOrder;
            body.lifetime = imageLifetime;
        }
        [ContextMenu("Clear remaining after images")]
        public void ClearRemainingAfterimages()
        {
            foreach(var body in FindObjectsOfType<Body>()) DestroyImmediate(body.gameObject);
        }
        private void Awake() 
        {
            spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        }

        // Start is called before the first frame update
        void Start()
        {
            nextImageTime = Time.time + interval;
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            if(Time.time >= nextImageTime)
            {
                foreach(var spriteRenderer in spriteRenderers)
                    CreateBody(spriteRenderer);
                nextImageTime = Time.time + interval;
            }
        }
    }
}
