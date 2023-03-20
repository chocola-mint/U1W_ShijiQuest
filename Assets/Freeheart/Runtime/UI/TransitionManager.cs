using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using TriInspector;

namespace Freeheart.UI
{
    /// <summary>
    /// Component that handles transition effects for the scene it is in.
    /// <br></br>
    /// Will trigger "Entrance" transition on the first frame of activation.
    /// The "Exit" transition will happen when <see cref="RestartScene"></see> or
    /// <see cref="MoveToScene"></see> is invoked.
    /// </summary>
    public sealed class TransitionManager : MonoBehaviour
    {
        
        public enum TransitionCleanupMode
        {
            Destroy,
            Disable,
            DoNothing,
        }
        [SerializeField]
        [Tooltip("What to do with entrance transition objects after they're done.")] 
        private TransitionCleanupMode cleanupMode = TransitionCleanupMode.DoNothing;
        // ! These objects should set themselves to inactive once they are done.
        [Title("Prefabs that contain transition effects.")]
        [
            SerializeField, 
            ValidateInput(nameof(ValidateEntranceTransitions))
        ] 
        private List<GameObject> entrance = new();
        [
            SerializeField, 
            ValidateInput(nameof(ValidateExitTransitions))
        ] 
        private List<GameObject> exit = new();
        private TriValidationResult ValidateEntranceTransitions() => ValidateTransitions(entrance);
        private TriValidationResult ValidateExitTransitions() => ValidateTransitions(exit);
        private TriValidationResult ValidateTransitions(List<GameObject> transitions)
        {
            try{
                foreach(var transition in transitions)
                    if(!transition) return TriValidationResult.Warning("All transitions must be assigned.");
                    else if(!transition.TryGetComponent<TransitionState>(out _)) 
                        return TriValidationResult.Warning($"All transitions should have the {nameof(TransitionState)} component.");
            }
            catch(System.Exception e)
            {
                Debug.LogError(e);
                return TriValidationResult.Valid;
            }
            return TriValidationResult.Valid;
        }
        public UnityEvent onEntranceTransitionOver, onExitTransitionStart;
        public bool isBusy { get; private set; }
        private Dictionary<string, AsyncOperation> loadTasks = new();
        private void Start() 
        {
            StartCoroutine(CoroEnterScene());    
        }
        private IEnumerator ProcessTransitions(List<GameObject> transitionList)
        {
            // Process transitions in sequential order.
            foreach(var transitionPrefab in transitionList)
            {
                // Instantiate transition object.
                var transition = Instantiate(transitionPrefab);
                var transitionState = transition.GetComponent<TransitionState>();
                Debug.Assert(transitionState, $"Missing {nameof(TransitionState)}");
                // Then wait for the transition to end.
                yield return new WaitUntil(() => transitionState.isOver);
                // Finally, cleanup.
                switch(cleanupMode)
                {
                    case TransitionCleanupMode.Destroy:
                        Destroy(transition);
                        break;
                    case TransitionCleanupMode.Disable:
                        transition.SetActive(false);
                        break;
                    case TransitionCleanupMode.DoNothing:
                        default:
                        break;
                }
            }
        }
        private IEnumerator CoroEnterScene()
        {
            if(entrance.Count > 0)
                yield return ProcessTransitions(entrance);
            onEntranceTransitionOver.Invoke();
        }
        /// <summary>
        /// Reload the current scene, triggering transitions.
        /// </summary>
        public void RestartScene() => MoveToScene(SceneManager.GetActiveScene().name);
        /// <summary>
        /// Load the given scene, triggering transitions.
        /// </summary>
        public void MoveToScene(string sceneName)
        {
            if(isBusy) return;
            onExitTransitionStart.Invoke();
            StopAllCoroutines();
            StartCoroutine(CoroMoveToScene(sceneName));
        }
        /// <summary>
        /// Shortcut that takes in the scene asset instead. Will use the name of the asset as scene name.
        /// </summary>
        public void MoveToScene(Object sceneAsset)
        {
            MoveToScene(sceneAsset.name);
        }
        /// <summary>
        /// Shortcut that takes in a <see cref="SceneRef"></see> ScriptableObject.
        /// </summary>
        /// <param name="sceneKey"></param>
        public void MoveToScene(SceneRef sceneKey)
        {
            MoveToScene(sceneKey.GetSceneName());
        }
        public void MoveToScene(DynamicSceneRef dynamicSceneRef)
        {
            MoveToScene(dynamicSceneRef.GetSceneRef());
        }
        public void PreloadScene(string sceneName)
        {
            var task = SceneManager.LoadSceneAsync(sceneName);
            task.allowSceneActivation = false;
            loadTasks[sceneName] = task;
        }
        private IEnumerator CoroMoveToScene(string sceneName)
        {
            isBusy = true;
            // Start the async load scene task.
            if(!loadTasks.TryGetValue(sceneName, out var task))
            {
                task = SceneManager.LoadSceneAsync(sceneName);
                task.allowSceneActivation = false;
            }
            // Process exit transitions.
            cleanupMode = TransitionCleanupMode.DoNothing;
            if(exit != null)
            {
                if(exit.Count > 0)
                    yield return ProcessTransitions(exit);
            }
            // Then wait for the load scene operation to be done.
            task.allowSceneActivation = true;
            yield return task;
        }
        public void Quit()
        {
            onExitTransitionStart.Invoke();
            StopAllCoroutines();
            StartCoroutine(CoroQuitApplication());
        }
        private IEnumerator CoroQuitApplication()
        {
            // Process exit transitions.
            cleanupMode = TransitionCleanupMode.DoNothing;
            if(exit != null)
            {
                if(exit.Count > 0)
                    yield return ProcessTransitions(exit);
            }
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.ExitPlaymode();
            #else
            Application.Quit();
            #endif
        }
    }
}
