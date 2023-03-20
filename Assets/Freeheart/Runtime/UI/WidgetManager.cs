using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TriInspector;

namespace Freeheart.UI
{
    [RequireComponent(typeof(GraphicRaycaster))]
    public sealed class WidgetManager : WidgetNode
    {
        [SerializeField, ReadOnly]
        private GraphicRaycaster graphicRaycaster;
        private List<WidgetNode> path = new();
        [SerializeField, Required]
        private WidgetNode defaultNode;
        private Coroutine checkIdleCoroutine = null;
        public bool canReturn => path.Count > 1;
        [SerializeField]
        private bool showOnStart = true;
        public UnityEvent onShow, onHide;
        [SerializeField]
        private bool silenceWarning = false;
        private void Reset() 
        {
            TryGetComponent<GraphicRaycaster>(out graphicRaycaster);
        }
        private void Awake() 
        {
            if(!graphicRaycaster)
                TryGetComponent<GraphicRaycaster>(out graphicRaycaster);
            isVisible = false;
        }
        private void Start() 
        {
            if(showOnStart) Show();
        }
        public void GoTo(WidgetNode node)
        {
            if(path.Contains(node))
            {
                if(!silenceWarning) Debug.LogWarning("Detected cycle.");
                return;
            }
            if(!node.isIdle) return; // Target isn't idle. Don't interrupt.
            if(path.Count == 0) 
            {
                path.Add(node);
                MakeTransition(nodeToShow: node);
            }
            else
            {
                var prevNode = path[^1];
                if(!prevNode.isIdle) return; // prevNode isn't idle. Don't interrupt.
                path.Add(node);
                MakeTransition(nodeToHide: prevNode, nodeToShow: node);
            }
        }
        public void Return()
        {
            if(!canReturn)
            {
                if(!silenceWarning)  Debug.LogWarning("Can't return beyond default node.");
                return;
            }
            var curNode = path[^1];
            var prevNode = path[^2];
            if(!curNode.isIdle || !prevNode.isIdle) return; // Don't interrupt.
            path.RemoveAt(path.Count - 1);
            MakeTransition(nodeToHide: curNode, nodeToShow: prevNode);
        }
        private void MakeTransition(WidgetNode nodeToHide = null, WidgetNode nodeToShow = null)
        {
            if(checkIdleCoroutine != null) StopCoroutine(checkIdleCoroutine);
            checkIdleCoroutine = StartCoroutine(CoroMakeTransition(nodeToHide, nodeToShow));
        }


        IEnumerator CoroMakeTransition(WidgetNode nodeToHide = null, WidgetNode nodeToShow = null)
        {
            isIdle = false;
            if(nodeToHide) nodeToHide.Hide();
            if(nodeToShow) nodeToShow.Show();
            graphicRaycaster.enabled = false;
            yield return null;
            if(nodeToHide) yield return new WaitUntil(() => nodeToHide.isIdle);
            yield return null;
            if(nodeToShow) yield return new WaitUntil(() => nodeToShow.isIdle);
            graphicRaycaster.enabled = true;
            isIdle = true;
        }

        public override void Show()
        {
            if(isVisible) return;
            isVisible = true;
            onShow.Invoke();
            path.Add(defaultNode);
            Debug.Log($"Showing {defaultNode.gameObject.name}");
            MakeTransition(nodeToShow: defaultNode);
        }

        public override void Hide()
        {
            if(!isVisible) return;
            isVisible = false;
            onHide.Invoke();
            Debug.Log("WidgetManager hide");
            MakeTransition(nodeToHide: path[^1]);
            path.Clear();
        }
    }
}
