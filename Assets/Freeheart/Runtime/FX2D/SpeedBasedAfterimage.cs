using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriInspector;

namespace Freeheart.FX2D
{
    public sealed class SpeedBasedAfterimage : Afterimage
    {
        [SerializeField, ReadOnly, Required]
        private Rigidbody2D rb;
        [SerializeField, Min(0.01f)]
        private float minimumSpeed = 1.0f;
        [SerializeField]
        private bool disabledWhenRigidbodyIsNotSimulated = false;
        protected override void Reset()
        {
            base.Reset();
            if(!TryGetComponent<Rigidbody2D>(out rb)) rb = GetComponentInParent<Rigidbody2D>();
        }
        protected override void Update()
        {
            if(disabledWhenRigidbodyIsNotSimulated && !rb.simulated) return;
            if(rb.velocity.sqrMagnitude > minimumSpeed * minimumSpeed)
                base.Update();
        }
    }
}
