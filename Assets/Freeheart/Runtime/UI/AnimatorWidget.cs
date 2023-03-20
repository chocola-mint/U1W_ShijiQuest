using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriInspector;

namespace Freeheart.UI
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorWidget : WidgetNode
    {
        [ShowInInspector, ReadOnly, SerializeField]
        [InfoBox("The Animator Controller should have a \"SHOW\" state and a \"HIDE\" state.")]
        private Animator animator;
        private static class AnimStates
        {
            public static readonly int SHOW = Animator.StringToHash(nameof(SHOW));
            public static readonly int HIDE = Animator.StringToHash(nameof(HIDE));
        }
        void Reset() 
        {
            animator = GetComponent<Animator>();
        }
        void Awake()
        {
            if(!animator) animator = GetComponent<Animator>();
            animator.Play(AnimStates.HIDE, layer: 0, normalizedTime: 1);
        }
        void Start()
        {
        
        }
        void LateUpdate()
        {
            isIdle = animator.GetAnimatorTransitionInfo(0).normalizedTime <= 0
            && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1;
        }
        public override void Show()
        {
            if(isVisible || !isIdle) return;
            animator.CrossFade(AnimStates.SHOW, 1, 0, 0);
            isIdle = false;
            isVisible = true;
        }
        public override void Hide()
        {
            if(!isVisible || !isIdle) return;
            animator.CrossFade(AnimStates.HIDE, 1, 0, 0);
            isIdle = false;
            isVisible = false;
        }
    }
}
