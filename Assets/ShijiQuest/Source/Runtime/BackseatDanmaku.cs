using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriInspector;

namespace ShijiQuest
{
    [RequireComponent(typeof(Danmaku))]
    public class BackseatDanmaku : MonoBehaviour
    {
        [Required]
        public GameOption desiredOption;
        [Min(0.01f)]
        public float addWeight;
        private void Start() 
        {
            GetComponent<Danmaku>().onClick += 
            () => desiredOption.AddWeight(addWeight);

            // Partial influence by just appearing.
            desiredOption.AddWeight(addWeight * 0.25f);
        }
    }
}
