using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Freeheart.UI
{
    public class DestroyIfPlatform : MonoBehaviour
    {
        [SerializeField] private RuntimePlatform runtimePlatform;
        private void Awake() 
        {
            if(Application.platform == runtimePlatform) DestroyImmediate(gameObject);
        }
    }
}
