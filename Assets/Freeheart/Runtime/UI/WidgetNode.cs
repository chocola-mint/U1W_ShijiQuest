using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Freeheart.UI
{
    /// <summary>
    /// Abstract class that represents an UI widget that can be shown and hidden.
    /// </summary>
    public abstract class WidgetNode : MonoBehaviour
    {
        public bool isVisible { get; protected set; } = false;
        public bool isIdle { get; protected set; } = true;
        public abstract void Show();
        public abstract void Hide();
    }
}
