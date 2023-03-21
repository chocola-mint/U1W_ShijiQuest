using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

namespace ShijiQuest
{
    [CreateAssetMenu(fileName = "VSLocalizedStringRef", menuName = "ShijiQuest/VSLocalizedStringRef")]
    public class VSLocalizedStringRef : ScriptableObject
    {
        public LocalizedString localizedString;
        public string Get() => localizedString.GetLocalizedString();
    }
}
