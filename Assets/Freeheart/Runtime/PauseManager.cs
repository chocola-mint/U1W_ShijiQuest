using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Freeheart
{
    /// <summary>
    /// An object that can manage pause/unpause logic.
    /// </summary>
    [CreateAssetMenu(fileName = "PauseManager", menuName = "Freeheart/PauseManager", order = 1)]
    public class PauseManager : ScriptableObject
    {
        /// <summary>
        /// Events you can subscribe to receive events on pause/unpause. Remember to unregister
        /// upon Destroy if the receiver is on a GameObject.
        /// </summary>
        public event System.Action onPause, onUnpause;
        public bool isPaused { get; private set; } = false;
        [SerializeField]
        private bool pauseTimeScale = false;
        private float timeScaleBeforePause = 1.0f;
        private void OnEnable() 
        {
            isPaused = false;
        }
        public void Pause()
        {
            if(isPaused) return;
            Debug.Log("Pause");
            isPaused = true;
            if(pauseTimeScale) 
            {
                timeScaleBeforePause = Time.timeScale;
                Time.timeScale = 0;
            }
            onPause?.Invoke();
        }
        public void Unpause()
        {
            if(!isPaused) return;
            Debug.Log("Unpause");
            isPaused = false;
            if(pauseTimeScale)
            {
                Time.timeScale = timeScaleBeforePause;
            }
            onUnpause?.Invoke();
        }
    }
}
