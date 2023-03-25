using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriInspector;

namespace Freeheart.UI
{
    public class QuadCanvasRect : MonoBehaviour
    {
        [Required]
        public RectTransform rectTransform;
        public float zOffset = 0;
        private Vector3[] fourCornersArray = new Vector3[4];
        public bool targetInScreenSpace = true;
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            Snap();
        }
        
        [Button("Snap to Canvas Rect")]
        public void Snap()
        {
            rectTransform.GetWorldCorners(fourCornersArray);
            if(targetInScreenSpace)
            {
                for(int i = 0; i < fourCornersArray.Length; ++i)
                    fourCornersArray[i] = Camera.main.ScreenToWorldPoint(fourCornersArray[i]);
            }
            Vector3 center = (fourCornersArray[0] + fourCornersArray[1] + fourCornersArray[2] + fourCornersArray[3]) / 4;
            center.z += zOffset;
            transform.position = center;

            transform.localScale = new Vector3(
                fourCornersArray[2].x - fourCornersArray[1].x, 
                fourCornersArray[1].y - fourCornersArray[0].y, 
                1);
        }
    }
}
