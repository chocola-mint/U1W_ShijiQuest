using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Freeheart.UI
{
    [RequireComponent(typeof(Animation))]
    public class AnimationWidget : WidgetNode
    {
        private Animation anim;
        private static class AnimKeys
        {
            public const string Show = "Show", Hide = "Hide";
        }
        void Awake() 
        {
            anim = GetComponent<Animation>();
            // Write defaults using Show.
            anim.GetClip(AnimKeys.Show).SampleAnimation(gameObject, 0);
        }
        void Update() 
        {
            isIdle = !anim.isPlaying;
        }
        public override void Show()
        {
            if(isVisible) return;
            anim.CrossFade(AnimKeys.Show);
            isIdle = false;
            isVisible = true;
        }
        public override void Hide()
        {
            if(!isVisible) return;
            anim.CrossFade(AnimKeys.Hide);
            isIdle = false;
            isVisible = false;
        }
    }
}
