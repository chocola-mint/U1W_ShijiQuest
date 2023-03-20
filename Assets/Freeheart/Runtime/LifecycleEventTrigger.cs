using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TriInspector;

namespace Freeheart
{
    [DeclareFoldoutGroup("Events", Title = "Events")]
    public sealed class LifecycleEventTrigger : MonoBehaviour
    {
        [SerializeField, Group("Events")]
        private UnityEvent onAwake, onStart, onEnable, onDisable, onDestroy;
        private void Awake() => onAwake.Invoke();
        private void Start() => onStart.Invoke();
        private void OnEnable() => onEnable.Invoke();
        private void OnDisable() => onDisable.Invoke();
        private void OnDestroy() => onDestroy.Invoke();
    }
}
