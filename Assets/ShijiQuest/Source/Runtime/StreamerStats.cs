using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriInspector;

namespace ShijiQuest
{
    [CreateAssetMenu(fileName = "Streamer", menuName = "ShijiQuest/Streamer")]
    public class StreamerStats : ScriptableObject
    {
        public enum Mood
        {
            Normal,
            Irritated,
            Angry,
            Happy,
            Sad,
        }
        [ReadOnly, System.NonSerialized]
        public float mentality = 1;
        [ReadOnly, System.NonSerialized]
        public Mood mood = Mood.Normal;
        [ReadOnly, System.NonSerialized]
        public int viewCount = 100, subCount = 10;
        private void Awake() 
        {
            ResetState();
        }
        public void ResetState() 
        {
            mentality = 1;
            mood = Mood.Normal;
            viewCount = 100;
            subCount = 10;
        }
        public void AddMentality(float value)
        {
            mentality = Mathf.Clamp01(mentality + value / 3.0f);
        }
    }
}
