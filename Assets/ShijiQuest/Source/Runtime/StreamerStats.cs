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
        public float mentality = 0;
        [ReadOnly, System.NonSerialized]
        public Mood mood = Mood.Normal;
        [ReadOnly, System.NonSerialized]
        public int viewCount = 100, subCount = 10;
    }
}
