using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using TriInspector;

namespace Freeheart.UI.Yarn
{
    public class YarnTransitionMod : MonoBehaviour
    {
        [SerializeField, Required]
        private TransitionManager transitionManager;
        [SerializeField, Required]
        private DialogueRunner dialogueRunner;
        [SerializeField, Required]
        private DynamicSceneRef nextSceneRef;
        private bool entranceTransitionOverFlag = false;
        private void Reset() 
        {
            transitionManager = FindObjectOfType<TransitionManager>();
            dialogueRunner = FindObjectOfType<DialogueRunner>();
        }
        private void Awake() 
        {
            Debug.Log("YarnTransitionMod Awake");
            transitionManager.onEntranceTransitionOver.AddListener(CallbackOnEntranceTransitionOver);
            dialogueRunner.AddCommandHandler<string>("next_scene", NextScene);
            dialogueRunner.AddCommandHandler("await_entrance", CoroutineWaitUntilEntranceTransitionIsOver);
        }
        public void CallbackOnEntranceTransitionOver()
        {
            entranceTransitionOverFlag = true;
        }
        /// <summary>
        /// Start a coroutine that waits until the entrance transition is over.
        /// </summary>
        public Coroutine CoroutineWaitUntilEntranceTransitionIsOver() 
        {
            return StartCoroutine(WaitUntilEntranceTransitionIsOver());
        }
        public IEnumerator WaitUntilEntranceTransitionIsOver()
        {
            yield return new WaitUntil(() => entranceTransitionOverFlag);
        }
        /// <summary>
        /// Start the transition to the next scene. If a name is not supplied, this will use the DynamicSceneRef object assigned in Unity.
        /// </summary>
        public void NextScene(string sceneName)
        {
            if(sceneName.Length == 0)
                transitionManager.MoveToScene(nextSceneRef.GetSceneRef());
            else transitionManager.MoveToScene(sceneName);
        }
    }
}
