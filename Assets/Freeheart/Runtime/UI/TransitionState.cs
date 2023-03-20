using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Freeheart.UI
{
    public class TransitionState : MonoBehaviour
    {
        public bool isOver = false;
        public void MarkAsOver() => isOver = true;
    }
}
