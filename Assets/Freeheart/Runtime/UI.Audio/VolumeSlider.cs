using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Freeheart.Audio;
using TriInspector;
using UnityEngine.UI;

namespace Freeheart.UI.Audio
{
    [RequireComponent(typeof(Slider))]
    public class VolumeSlider : MonoBehaviour
    {
        [SerializeField, ReadOnly]
        private Slider slider;
        [SerializeField, Required]
        private VolumeSettings volumeSettings;
        void Reset() 
        {
            if(!TryGetComponent<Slider>(out slider)) 
                slider = gameObject.AddComponent<Slider>();
        }
        // Start is called before the first frame update
        void Start()
        {
            slider.value = volumeSettings.GetVolumeScale();
        }
        // Update is called once per frame
        void Update()
        {
            volumeSettings.SetVolumeScale(slider.value);
        }
    }
}
