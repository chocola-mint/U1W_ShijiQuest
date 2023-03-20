using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriInspector;

namespace Freeheart.FX2D
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class MirrorSpriteRenderer : MonoBehaviour
    {
        [SerializeField, Required, ReadOnly]
        private SpriteRenderer spriteRenderer;
        [SerializeField, Required]
        private SpriteRenderer mirrorTarget;
        private void Reset() 
        {
            TryGetComponent<SpriteRenderer>(out spriteRenderer);
        }
        private void OnValidate() 
        {
            TryGetComponent<SpriteRenderer>(out spriteRenderer);
        }
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            spriteRenderer.color = mirrorTarget.color;
            spriteRenderer.flipX = mirrorTarget.flipX;
            spriteRenderer.flipY = mirrorTarget.flipY;
        }
    }
}
