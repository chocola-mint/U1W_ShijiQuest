using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriInspector;

namespace Freeheart.FX2D
{
    public class FollowTransform : MonoBehaviour
    {
        [SerializeField, Required]
        private Transform followTarget;
        private Vector3 targetToSelf;
        private Quaternion localRotation;
        [SerializeField]
        private bool copyRotation = false;
        [SerializeField]
        private bool detach = true;
        // Start is called before the first frame update
        void Start()
        {
            targetToSelf = transform.position - followTarget.position;
            localRotation = transform.localRotation;
            if(detach) transform.SetParent(null);
        }

        // Update is called once per frame
        void Update()
        {
            transform.position = followTarget.position + targetToSelf;
            if(copyRotation) transform.rotation = followTarget.rotation * localRotation;
        }
    }
}
