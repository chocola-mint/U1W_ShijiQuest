using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriInspector;

namespace Freeheart.UI
{
    public sealed class QuadScreenRect : MonoBehaviour
    {
        [SerializeField, Min(0)] 
        float distanceFromCamera = 0;
        void Update()
        {
            Snap();
        }

        [Button("Snap to Camera")]
        public void Snap()
        {
            var min = Camera.main.ViewportToWorldPoint(Vector2.zero);
            var max = Camera.main.ViewportToWorldPoint(Vector2.one);
            Vector2 size = max - min;
            transform.localScale = size;
            transform.position = new Vector3(){
                x = Camera.main.transform.position.x,
                y = Camera.main.transform.position.y,
                z = Camera.main.transform.position.z + Camera.main.nearClipPlane + distanceFromCamera
            };
        }
    }
}
