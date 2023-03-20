using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using TriInspector;

namespace Freeheart.UI.Yarn
{
    public class YarnStartNodeInjector : MonoBehaviour
    {
        [SerializeField, Required]
        private YarnNodeRef injectSource;
        [SerializeField]
        private DialogueRunner dialogueRunner;
        private void Reset() 
        {
            dialogueRunner = FindObjectOfType<DialogueRunner>();
        }
        private void Awake() 
        {
            Debug.Log("YarnStartNodeInjector Awake");
            dialogueRunner.startNode = injectSource.nodeName;
            Debug.Log($"Injected {injectSource.nodeName}");
        }
        /// <summary>
        /// Set the name of the start node.
        /// </summary>
        [YarnCommand("set_start_node")]
        public static void SetStartNode(string nodeName)
        {
            Debug.Log($"{nameof(YarnStartNodeInjector)}: {nameof(SetStartNode)}({nodeName})");
            FindObjectOfType<YarnStartNodeInjector>().injectSource.SetNodeName(nodeName);
        }
    }
}
