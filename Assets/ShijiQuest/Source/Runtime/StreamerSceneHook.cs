using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriInspector;

namespace ShijiQuest
{
    public class StreamerSceneHook : MonoBehaviour
    {
        [Required]
        public StreamerStats stats;
        private Coroutine main;
        private void Awake() 
        {
            stats.ResetState();
        }
        // Start is called before the first frame update
        private void Start() 
        {
            main = StartCoroutine(Main());
        }
        private IEnumerator Main()
        {
            stats.mentality = 1;
            while(!Mathf.Approximately(stats.mentality, 0))
            {
                if(stats.mentality < 0.5f)
                {
                    stats.mood = StreamerStats.Mood.Irritated;
                }
                else
                {
                    stats.mood = StreamerStats.Mood.Normal;
                }
                yield return null;
            }            
            // Breakdown!
            stats.mood = StreamerStats.Mood.Angry;
            
        }

        // Update is called once per frame
        void Update()
        {
        }
    }
}
