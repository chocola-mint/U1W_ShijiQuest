using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Freeheart.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundSource : MonoBehaviour
    {
        [SerializeField] 
        protected VolumeSettings volumeSettings;
        private AudioSource audioSource;
        private AudioSource secondaryAudioSource;
        private float playOneShotBufferedUntil = 0;
        void Awake() 
        {
            audioSource = GetComponent<AudioSource>();
            if(volumeSettings)
            {
                SetVolume(volumeSettings.GetVolumeScale());
                volumeSettings.onVolumeChanged += SetVolume;
            }
        }
        // Start is called before the first frame update
        void Start()
        {

        }
        void OnDestroy() 
        {
            if(volumeSettings)
            {
                volumeSettings.onVolumeChanged -= SetVolume;
            }
        }
        private void SetVolume(float value) 
        {
            audioSource.volume = value;
            if(secondaryAudioSource) secondaryAudioSource.volume = value;
        }
        public void SetLoop(bool value) => audioSource.loop = value;
        public void Play(SoundAsset soundAsset, ulong delay)
        {
            audioSource.clip = soundAsset.GetAudioClip();
            audioSource.volume = volumeSettings.GetVolumeScale();
            if(delay > 0) audioSource.Play(delay);
            else audioSource.Play();
        }
        public void Play(SoundAsset soundAsset) => Play(soundAsset, 0);
        public void PlayScheduledLoop(SoundAsset soundAsset)
        {
            secondaryAudioSource = gameObject.AddComponent<AudioSource>();
            secondaryAudioSource.volume = volumeSettings.GetVolumeScale();
            secondaryAudioSource.clip = soundAsset.GetAudioClip();
            secondaryAudioSource.PlayScheduled(UnityEngine.AudioSettings.dspTime + audioSource.clip.length);
            secondaryAudioSource.loop = true;
        }
        public void Stop() => audioSource.Stop();
        public void LinearVanish(float duration)
        {
            StartCoroutine(CoroLinearVanish(duration));
        }
        private IEnumerator CoroLinearVanish(float duration)
        {
            float startTime = Time.unscaledTime;
            float start = audioSource.volume;
            float t = 0;
            while(t < 1)
            {
                t = (Time.unscaledTime - startTime) / duration;
                audioSource.volume = Mathf.Lerp(start, 0, t);
                yield return null;
            }
        }
        public void PlayOneShot(SoundAsset soundAsset)
        {
            audioSource.PlayOneShot(soundAsset.GetAudioClip(), volumeSettings.GetVolumeScale() * soundAsset.GetVolumeScale());
        }
        public void PlayOneShotScaled(SoundAsset soundAsset, float extraScale)
        {
            audioSource.PlayOneShot(soundAsset.GetAudioClip(), volumeSettings.GetVolumeScale() * soundAsset.GetVolumeScale() * extraScale);
        }
        public void PlayOneShotBuffered(SoundAsset soundAsset)
        {
            if(Time.unscaledTime > playOneShotBufferedUntil)
            {
                playOneShotBufferedUntil = Time.unscaledTime + soundAsset.GetAudioClip().length / 2;
                PlayOneShot(soundAsset);
            }
        }
    }
}
