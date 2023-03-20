using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriInspector;

namespace Freeheart.Audio
{
    [CreateAssetMenu(fileName = "SoundAsset", menuName = "Freeheart/Audio/SoundAsset", order = 1)]
    public class SoundAsset : ScriptableObject
    {
        [SerializeField, Required]
        private AudioClip audioClip;
        [SerializeField, Range(0, 1)] 
        private float volumeScale = 1.0f;
        public AudioClip GetAudioClip() => audioClip;
        public float GetVolumeScale() => volumeScale;
        public (AudioClip, float) GetSound() => (audioClip, volumeScale);
    }
}
