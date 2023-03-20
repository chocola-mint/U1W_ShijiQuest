using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Freeheart.Audio
{
    [CreateAssetMenu(fileName = "VolumeSettings", menuName = "Freeheart/Audio/VolumeSettings", order = 1)]
    public class VolumeSettings : ScriptableObject
    {
        [SerializeField, Range(0, 1)] 
        private float volumeScale = 1.0f;
        public event System.Action<float> onVolumeChanged;
        public float GetVolumeScale() => volumeScale;
        public void SetVolumeScale(float value)
        {
            volumeScale = value;
            onVolumeChanged?.Invoke(value);
        }
    }
}
